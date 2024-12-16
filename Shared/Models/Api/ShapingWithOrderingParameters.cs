using Swashbuckle.AspNetCore.Annotations;

namespace Shared.Models.Api
{
    public class ShapingWithOrderingParameters : ShapingParameters
    {
        [SwaggerParameter(
            "<p>The property you want to sort by asc or desc.</p>" +
            "<p>Supports multiple properties by seperating them with a comma.</p>" +
            "<p></p>" +
            "<p>Example</p>" +
            "<code>" +
            "createdAt desc,updatedAt asc" +
            "</code>"
        )]
        public string OrderBy { get; set; } = "CreatedAt desc";
    }
}
