﻿using Shared.Constants;
using Shared.Entities;
using System.ComponentModel.DataAnnotations;

namespace SharedProducts.Entities.Products
{
    public class Product : UuidBaseEntity
    {
        [MaxLength(InputSizes.DEFAULT_TEXT_MAX_LENGTH)]
        public string Name { get; set; }

        public Dictionary<string, string> DescriptionLocalized { get; set; }

        public string ListPicture { get; set; }

        public List<string> Pictures { get; set; } = new List<string>();

        public int DefaultPriceInCents { get; set; }
    }
}
