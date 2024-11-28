namespace Shared.Models.Api
{
    public interface IShapingWithOrderingParameters : IShapingParameters
    {
        string OrderBy { get; set; }
    }
}