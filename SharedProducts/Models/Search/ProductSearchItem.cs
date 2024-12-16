namespace SharedProducts.Models.Search
{
    public class ProductSearchItem
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public string Name { get; set; }

        public Dictionary<string, string> Description { get; set; }

        public string ListPicture { get; set; }

        public List<string> Pictures { get; set; }

        public int PriceInCents { get; set; }

        public Dictionary<string, bool> BooleanProperties { get; set; } = new Dictionary<string, bool>();

        public Dictionary<string, double> NumericProperties { get; set; } = new Dictionary<string, double>();

        public Dictionary<string, string> StringProperties { get; set; } = new Dictionary<string, string>();

        public List<string> Colors { get; set; } = new List<string>();

        public List<string> Sizes { get; set; } = new List<string>();
    }
}
