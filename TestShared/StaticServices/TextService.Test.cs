namespace TestShared.StaticServices
{
    public class TextServiceTest
    {
        [Fact]
        public void GetHash()
        {
            var raw = "abcdefghijklmnopqrstuvwxyz1234567890.-:";

            var hash = Shared.StaticServices.TextService.GetHash(raw);

            Assert.Equal(64, hash.Length);
        }

        [Fact]
        public void GetGuidArray()
        {
            var raw = "4abf7ecd-07f2-4b5c-8f9c-1d98ff8e7be7,b2f3da27-4c18-4d2b-9557-117062eedb3b";

            var guids = Shared.StaticServices.TextService.GetGuidArray(raw);

            Assert.NotNull(guids);
            Assert.Equal(2, guids.Length);
            Assert.Equal("4abf7ecd-07f2-4b5c-8f9c-1d98ff8e7be7", guids[0].ToString());
            Assert.Equal("b2f3da27-4c18-4d2b-9557-117062eedb3b", guids[1].ToString());
        }
    }
}
