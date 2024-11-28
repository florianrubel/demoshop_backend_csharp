namespace ProductCacheApi.Cache
{
    public interface IProductCacheFactory
    {
        Task Build();

        Task BuildByBooleanProperty(Guid booleanProperty);
        Task BuildByNumericProperty(Guid numericProperty);
        Task BuildByStringProperty(Guid stringProperty);

        Task BuildByBooleanProperties(IEnumerable<Guid> booleanProperties);
        Task BuildByNumericProperties(IEnumerable<Guid> numericProperties);
        Task BuildByStringProperties(IEnumerable<Guid> stringProperties);

        Task BuildByProduct(Guid product);
        Task BuildByProductVariant(Guid productVariant);
        Task BuildByProducts(IEnumerable<Guid> products);
        Task BuildByProductVariants(IEnumerable<Guid> productVariants);

        Task BuildByProductVariantBooleanProperty(Guid productVariantBooleanProperty);
        Task BuildByProductVariantNumericProperty(Guid productVariantNumericProperty);
        Task BuildByProductVariantStringProperty(Guid productVariantStringProperty);

        Task BuildByProductVariantBooleanProperties(IEnumerable<Guid> productVariantBooleanProperties);
        Task BuildByProductVariantNumericProperties(IEnumerable<Guid> productVariantNumericProperties);
        Task BuildByProductVariantStringProperties(IEnumerable<Guid> productVariantStringProperties);
    }
}