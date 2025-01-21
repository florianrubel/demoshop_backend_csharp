namespace SharedProducts.Models.Products
{
    public abstract class AbstractPatchProductVariantRelation<ValueType>
    {
        public ValueType? Value { get; set; }
    }
}
