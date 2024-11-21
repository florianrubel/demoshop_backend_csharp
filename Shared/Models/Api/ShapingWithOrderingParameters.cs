namespace Shared.Models.Api
{
    public class ShapingWithOrderingParameters : ShapingParameters, IShapingWithOrderingParameters
    {
        public string OrderBy { get; set; } = "CreatedAt desc";
    }
}
