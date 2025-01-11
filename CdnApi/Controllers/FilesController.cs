using CdnApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Controllers;

namespace CdnApi.Controllers
{
    [Route("files")]
    public class FilesController : BasicControllerTemplate
    {
        private readonly IAwsS3Service _awsS3Service;

        public FilesController(IAwsS3Service awsS3Service)
        {
            this._awsS3Service = awsS3Service;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<string>>> Get()
        {
            var files = await _awsS3Service.GetFiles();
            return Ok(from file in files select file.Key);
        }
    }
}
