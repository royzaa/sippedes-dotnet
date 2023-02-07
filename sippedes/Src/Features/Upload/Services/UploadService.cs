using System.Net;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Options;
using sippedes.Commons.Utils;
using sippedes.Cores.Model;
using sippedes.Features.Upload.Dto;

namespace sippedes.Features.Upload.Services;

public class UploadService : IUploadService
{
    private readonly string _bucketName;
    private readonly IAmazonS3 _awsS3Client;

    public UploadService(IOptions<AwsS3ConfigurationModel> awsS3ConfigurationModel)
    {
        _bucketName = awsS3ConfigurationModel.Value.BucketName;

        var s3ClientConfig = new AmazonS3Config
        {
            ServiceURL = awsS3ConfigurationModel.Value.EndpointUrl, UseHttp = true,
        };

        _awsS3Client = new AmazonS3Client(awsAccessKeyId: awsS3ConfigurationModel.Value.AwsAccessKey,
            awsSecretAccessKey: awsS3ConfigurationModel.Value.AwsSecretAccessKey,
            clientConfig: s3ClientConfig);
    }

    public async Task<UploadSignatureRes> UploadFileSignatureAsync(IFormFile file)
    {
        var presignedUrl = String.Empty;
        var filePath = String.Empty;
        
        try
        {
            using (var newMemoryStream = new MemoryStream())
            {
                file.CopyTo(newMemoryStream);

                var randomCode = GeneratorUtils.GenerateRondomAlphaNumeric();

                filePath = $@"signature/{file.FileName}-{randomCode}";

                var request = new PutObjectRequest
                {
                    BucketName = _bucketName,
                    Key = filePath,
                    InputStream = newMemoryStream,
                    ContentType = file.ContentType
                };
                

                var response = await _awsS3Client.PutObjectAsync(request);

                if (response.HttpStatusCode != HttpStatusCode.OK) throw new AWSCommonRuntimeException("Failed upload");

                var validityInHour = 12;
                presignedUrl = GeneratePresignedUrl(new PresignedUrlReqDto
                {
                    BucketName = _bucketName,
                    ObjectKey = filePath,
                    Duration = validityInHour
                });
            }
        }
        catch (AmazonS3Exception e)
        {
            Console.WriteLine($"Upload to S3 Error: {e.Message}");
            throw new Exception(e.Message);
        }

        return new UploadSignatureRes
        {
            Url = presignedUrl,
            FilePath = filePath
        };
    }

    public string GeneratePresignedUrl(PresignedUrlReqDto payload)
    {
        string urlString = string.Empty;
        try
        {
            var request = new GetPreSignedUrlRequest()
            {
                BucketName = payload.BucketName,
                Key = payload.ObjectKey,
                Expires = DateTime.UtcNow.AddHours(payload.Duration),
            };
            urlString = _awsS3Client.GetPreSignedURL(request);
        }
        catch (AmazonS3Exception ex)
        {
            Console.WriteLine($"Error:'{ex.Message}'");
            throw new Exception(ex.Message);
        }

        return urlString;
    }
}