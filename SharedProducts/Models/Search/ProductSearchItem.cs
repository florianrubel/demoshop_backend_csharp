using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SharedProducts.Models.Search
{
    public class ProductSearchItem
    {
        [BsonId]
        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid Id { get; set; }

        [BsonGuidRepresentation(GuidRepresentation.Standard)]
        public Guid ProductId { get; set; }

        public string Name { get; set; }

        public Dictionary<string, string> Description { get; set; }

        public string ListPicture { get; set; }

        public List<string> Pictures { get; set; }

        public int PriceInCents { get; set; }

        public Dictionary<string, bool> BooleanProperties { get; set; } = new Dictionary<string, bool>();

        public Dictionary<string, double> NumericProperties { get; set; } = new Dictionary<string, double>();

        public Dictionary<string, string> StringProperties { get; set; } = new Dictionary<string, string>();
    }
}
