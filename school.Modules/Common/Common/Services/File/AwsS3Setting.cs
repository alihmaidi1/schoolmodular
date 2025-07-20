namespace Common.Services.File;

public class AwsS3Setting
{
    
    public string AccessKey { get; set; }
    
    public string SecretKey { get; set; }
    
    public string Region { get; set; }
    
    public string BucketName { get; set; }
    
    public string ImageContainer { get; set; }
    
    public string PresignedUrlExpiration { get; set; }
    
    public string VideoContainer { get; set; }
    
}