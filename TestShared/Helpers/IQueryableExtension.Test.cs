using Shared.Entities;
using Shared.Helpers;

namespace TestShared.Helpers
{
    public class IQueryableExtensionTest
    {
        [Fact]
        public void ApplySort_SortsCorrectly_BySingleFieldAscending()
        {
            var data = new List<UuidBaseEntity>().AsQueryable();

            Assert.Equal("createdAt descending", data.GetSortString(""));
            Assert.Equal("createdAt ascending", data.GetSortString("createdAt"));
            Assert.Equal("createdAt descending", data.GetSortString("createdAt desc"));
            Assert.Equal("createdAt ascending,updatedAt descending", data.GetSortString("createdAt, updatedAt desc"));
            Assert.Equal("createdAt descending,updatedAt ascending", data.GetSortString("createdAt desc, updatedAt"));
            Assert.Equal("createdAt descending", data.GetSortString("createdAt desc, property2"));
        }
    }
}
