using System;
using System.Collections.Generic;

namespace Ecommerce.Data
{
    public partial class ProductAttributeLookup
    {
        public int AttributeId { get; set; }
        public int ProdCatId { get; set; }
        public string AttributeName { get; set; }

        public virtual ProductCategory ProdCat { get; set; }
    }
}
