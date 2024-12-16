﻿using SharedProducts.Entities.Products.Properties;
using Shared.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedProducts.Entities.Products
{
    public class ProductVariantNumericProperty : UuidBaseEntity
    {
        [ForeignKey(nameof(ProductVariantId))]
        public Guid ProductVariantId { get; set; }
        public virtual ProductVariant ProductVariant { get; set; }

        [ForeignKey(nameof(PropertyId))]
        public Guid PropertyId { get; set; }
        public virtual NumericProperty Property { get; set; }

        public double Value { get; set; }
    }
}