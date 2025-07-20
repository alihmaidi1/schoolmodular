using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Shared.Domain.Entities;
using Shared.Domain.OperationResult;

namespace Shared.Infrastructure.Services.Archive;

public class ArchiveService:IArchiveService
{
    private readonly DbContext _context;
    private readonly JsonSerializerOptions jsonoptions;

    public ArchiveService(DbContext context)
    {
        jsonoptions=new JsonSerializerOptions
        {
            
            WriteIndented = false,
            ReferenceHandler = ReferenceHandler.Preserve
        };;
        _context = context;
    }

    public async Task<TResult<bool>> ArchiveEntityAsync<TEntity>(Guid Id, params Func<IQueryable<TEntity>, IQueryable<TEntity>>[] includes)
        where TEntity : class, IEntity
    {
        
        var query = _context.Set<TEntity>().AsQueryable();
        
        foreach (var include in includes)
        {
            query = include(query);

        }
            
        var entityWithRelations=await query
            .AsNoTracking()
            .AsSplitQuery()
            .FirstOrDefaultAsync(e => e.Id ==Id);
        
        if (entityWithRelations == null)
            return Result.InternalError<bool>(Error.NotFound("Entity not found"));

        var jsonData = JsonSerializer.Serialize(entityWithRelations, jsonoptions);
        var archiveRecord = new ArchiveRecord
        {
            EntityName = typeof(TEntity).Name,
            EntityId = Id,
            JsonData = jsonData,
            ArchivedAt = DateTime.UtcNow,
        };
        _context.Set<ArchiveRecord>().Add(archiveRecord);
        _context.Set<TEntity>().Remove(entityWithRelations);
        return Result.Success<bool>(true);
    }

    public async Task<TResult<TEntity>> RestoreEntityAsync<TEntity>(Guid id) where TEntity : class, IEntity
    {

        var archiveRecord = await _context.Set<ArchiveRecord>()
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.EntityId == id && a.EntityName == typeof(TEntity).Name);
        
        if (archiveRecord == null)
            return Result.InternalError<TEntity>(Error.NotFound("Archived record not found"));


        var entity = JsonSerializer.Deserialize<TEntity>(archiveRecord.JsonData, jsonoptions);
        if (entity == null)
            return Result.InternalError<TEntity>(Error.Internal($"{typeof(TEntity).Name} Deserialization failed"));
        _context.Set<TEntity>().Add(entity);
        _context.Set<ArchiveRecord>().Remove(archiveRecord);
        return Result.Success(entity);
    }



    public async Task<TResult<bool>> DeleteArchiveAsync<TEntity>(Guid id)
        where TEntity : class, IEntity
    {
        var archiveRecord = await _context.Set<ArchiveRecord>()
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.EntityId == id && a.EntityName == typeof(TEntity).Name);

        if (archiveRecord == null)
            return Result.InternalError<bool>(Error.NotFound("Archived record not found"));

        _context.Set<ArchiveRecord>().Remove(archiveRecord);

        return Result.Success<bool>(true);

    }

    public async Task<TResult<List<TEntity>>> GetArchivesAsync<TEntity>()
        where TEntity : class, IEntity
    {
        var entityName = typeof(TEntity).Name;
        var entities = _context.Set<ArchiveRecord>()
            .AsNoTracking()
            .Where(x=>x.EntityName==entityName)
            .Select(x=>JsonSerializer.Deserialize<TEntity>(x.JsonData, jsonoptions)!)
            .ToList();

        return Result.Success(entities);
    }

}