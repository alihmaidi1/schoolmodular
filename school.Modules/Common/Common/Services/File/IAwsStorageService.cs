using Shared.Domain.OperationResult;

namespace Common.Services.File;

public interface IAwsStorageService
{
    
    Task<TResult<string>> GenerateImageUploadUrl(string fileName);
    Task<TResult<ChunkUploadResponse>> InitiateChunkedVideoUpload(string fileName);

    
    public Task<string> UploadPartAsync(string uploadId,string fileName,int partNumber,Stream chunkStream);

    public Task CompleteMultipartUploadAsync(string uploadId, string fileName, List<Amazon.S3.Model.PartETag> partETags);


    public Task AbortMultipartUploadAsync(string uploadId, string fileName);

}