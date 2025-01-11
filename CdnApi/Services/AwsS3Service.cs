using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using CdnApi.Models.Aws;
using Microsoft.Extensions.Options;

namespace CdnApi.Services
{
    public class AwsS3Service : IAwsS3Service
    {
        private readonly IAmazonS3 _client;
        private readonly string _bucketName;

        public AwsS3Service(IOptions<AwsSettings> awsSettings)
        {
            _client = new AmazonS3Client(awsSettings.Value.AccessKey, awsSettings.Value.SecredAccessKey, RegionEndpoint.EUCentral1);
            _bucketName = awsSettings.Value.BucketName;
        }

        public async Task<IEnumerable<S3Object>> GetFiles()
        {
            var result = await _client.ListObjectsAsync(_bucketName);
            return result.S3Objects;
        }
    }
}
