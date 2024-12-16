using Swashbuckle.AspNetCore.Annotations;

namespace Shared.Models.Api
{
    public class ShapingParameters
    {
        [SwaggerParameter("Comma separated list of properties to limit the response body.")]
        public string Fields { get; set; } = "";
    }
}
