using System;
using System.Linq;
using TShapedFoundation.Utilities.Extensions;

namespace TShapedFoundation.PageObjects.AutomationPractice.Models
{
    public class ProductInfo : IEquatable<ProductInfo>
    {
        public string Name { get; set; }

        public string Desc { get; set; }

        public string Price { get; set; }

        public string[] Colors { get; set; }

        public string Availability { get; set; }

        public bool Equals(ProductInfo other)
        {
            return Name.Equals(other.Name)
                && Desc.Equals(other.Desc)
                && Price.Equals(other.Price)
                && Availability.Equals(other.Availability)
                && StringExtensions.IgnoreOrderEquals(Colors, other.Colors);
        }
    }
}
