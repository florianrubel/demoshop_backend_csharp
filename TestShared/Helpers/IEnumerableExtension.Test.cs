using Shared.Helpers;
using TestShared.Models.Helpers;

namespace TestShared.Helpers
{

    public class IEnumerableExtensionTest
    {
        [Fact]
        public void ShapeData()
        {
            var testObjList = new List<TestObj> { new TestObj(), new TestObj() };

            foreach (var testObj in testObjList)
            {
                var shapedGood = testObj.ShapeData("prop2,prop3,prop6").ToDictionary();
                Assert.Equal(3, shapedGood.Keys.Count);
                Assert.True(shapedGood.ContainsKey("Prop2"));
                Assert.True(shapedGood.ContainsKey("Prop3"));
                Assert.True(shapedGood.ContainsKey("Prop6"));

                var shapedbad = testObj.ShapeData("prop1,prop4,prop7").ToDictionary();
                Assert.Equal(2, shapedbad.Keys.Count);
                Assert.True(shapedbad.ContainsKey("Prop1"));
                Assert.True(shapedbad.ContainsKey("Prop4"));
                Assert.False(shapedbad.ContainsKey("Prop7"));
            }
        }
    }
}
