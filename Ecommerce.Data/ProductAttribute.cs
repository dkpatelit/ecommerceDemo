using System;
using System.Collections.Generic;

namespace Ecommerce.Data
{
    public partial class ProductAttribute
    {
        public long ProductId { get; set; }
        public int AttributeId { get; set; }
        public string AttributeValue { get; set; }

        public virtual ProductAttributeLookup Attribute { get; set; }
        public virtual Product Product { get; set; }
    }
}
