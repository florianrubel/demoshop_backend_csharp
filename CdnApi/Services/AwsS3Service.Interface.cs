
using Amazon.S3.Model;

namespace CdnApi.Services
{
    public interface IAwsS3Service
    {
        Task<IEnumerable<S3Object>> GetFiles();
    }
}