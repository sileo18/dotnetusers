﻿using Amazon.S3;
using Amazon.S3.Transfer;

namespace dotnetusers.Services
{
    public class S3Service
    {
        private readonly IAmazonS3 _s3Client;
        private readonly string _bucketName;

        public S3Service(IConfiguration configuration)
        {
            var awsOptions = configuration.GetSection("AWS");
            _bucketName = awsOptions["BucketName"];

            _s3Client = new AmazonS3Client(
                awsOptions["AccessKey"],
                awsOptions["SecretKey"],
                Amazon.RegionEndpoint.GetBySystemName(awsOptions["Region"])
            );
        }

        public async Task<string> UploadFileAsync(Stream fileStream, string fileName, string contentType)
        {
            var uploadRequest = new TransferUtilityUploadRequest
            {
                InputStream = fileStream,
                Key = fileName,
                BucketName = _bucketName,
                ContentType = contentType                
            };

            var fileTransferUtility = new TransferUtility(_s3Client);
            await fileTransferUtility.UploadAsync(uploadRequest);

            // Retorna a URL pública do arquivo
            return $"https://{_bucketName}.s3.amazonaws.com/{fileName}";
        }
    }
}
