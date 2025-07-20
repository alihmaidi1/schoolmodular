using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Options;
using Shared.Domain.OperationResult;

namespace Common.Services.File;

public class AwsStorageService: IAwsStorageService
{
    
    private readonly AmazonS3Client _s3Client;
    private readonly AwsS3Setting _settings;

    public AwsStorageService(IOptions<AwsS3Setting> settings)
    {
        _settings = settings.Value;
        _s3Client = new AmazonS3Client(_settings.AccessKey, _settings.SecretKey, Amazon.RegionEndpoint.GetBySystemName(_settings.Region));

    }

    public async Task<TResult<string>> GenerateImageUploadUrl(string fileName)
    {
        var request = new GetPreSignedUrlRequest
        {
            BucketName = _settings.BucketName,
            Key = $"{_settings.ImageContainer}/{Guid.NewGuid()}/{fileName}",
            Verb = HttpVerb.PUT,
            Expires = DateTime.UtcNow.AddSeconds(Double.Parse(_settings.PresignedUrlExpiration))
        };
        return Result.Success(_s3Client.GetPreSignedURL(request));
    }

    
    
    public async Task<TResult<ChunkUploadResponse>> InitiateChunkedVideoUpload(string fileName)
    {
        var initiateRequest = new InitiateMultipartUploadRequest
        {
        BucketName = _settings.BucketName,
        Key = $"{_settings.VideoContainer}/{Guid.NewGuid()}/{fileName}"
        };

        var response = await _s3Client.InitiateMultipartUploadAsync(initiateRequest);
        return Result.Success(new ChunkUploadResponse
        {
            UploadId = response.UploadId,
            Key = response.Key
        });

    }

    public async Task<string> UploadPartAsync(string uploadId, string fileName, int partNumber, Stream chunkStream)
    {
        var request = new UploadPartRequest
        {
            BucketName = _settings.BucketName,
            Key = fileName,
            UploadId = uploadId,
            PartNumber = partNumber,
            InputStream = chunkStream,
            PartSize = chunkStream.Length
        };

        var response = await _s3Client.UploadPartAsync(request);
        return response.ETag;
    }

    public async Task CompleteMultipartUploadAsync(string uploadId, string fileName, List<Amazon.S3.Model.PartETag> partETags)
    {
        var request = new CompleteMultipartUploadRequest
        {
            BucketName = _settings.BucketName,
            Key = fileName,
            UploadId = uploadId,
            PartETags = partETags
        };

        await _s3Client.CompleteMultipartUploadAsync(request);
    }

    public async Task AbortMultipartUploadAsync(string uploadId, string fileName)
    {
        var request = new AbortMultipartUploadRequest
        {
            BucketName = _settings.BucketName,
            Key = fileName,
            UploadId = uploadId
        };

        await _s3Client.AbortMultipartUploadAsync(request);
        
    }
}