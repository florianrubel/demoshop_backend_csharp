using Microsoft.AspNetCore.Mvc.ModelBinding;
using Moq;
using Shared.Helpers;

namespace TestShared.Helpers
{
    public class ArrayModelBinderTest
    {
        [Fact]
        public async Task BindModelAsync_BindsCommaSeparatedStringToArray()
        {
            // Arrange
            var binder = new ArrayModelBinder();
            var valueProvider = new Mock<IValueProvider>();
            valueProvider.Setup(vp => vp.GetValue("test")).Returns(new ValueProviderResult("4abf7ecd-07f2-4b5c-8f9c-1d98ff8e7be7,b2f3da27-4c18-4d2b-9557-117062eedb3b"));

            var metadataProvider = new EmptyModelMetadataProvider();
            var modelMetadata = metadataProvider.GetMetadataForType(typeof(List<Guid>));

            var bindingContext = new DefaultModelBindingContext
            {
                ModelName = "test",
                ModelMetadata = modelMetadata,
                ModelState = new ModelStateDictionary(),
                ValueProvider = valueProvider.Object,

            };

            // Act
            await binder.BindModelAsync(bindingContext);

            // Assert
            Assert.True(bindingContext.Result.IsModelSet);
            Assert.NotNull(bindingContext.Model);
            Assert.IsType<Guid[]>(bindingContext.Model);

            var model = (bindingContext.Model as Guid[]).ToList<Guid>();
            Assert.Equal(2, model.Count);
            Assert.Equal("4abf7ecd-07f2-4b5c-8f9c-1d98ff8e7be7", model[0].ToString());
            Assert.Equal("b2f3da27-4c18-4d2b-9557-117062eedb3b", model[1].ToString());
        }
    }
}
