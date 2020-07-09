
using System;
using System.Collections.Generic;
using Ecommerce.Data;

namespace Ecommerce.Web.ViewModels
{

    public class ProductViewModel2
    {
        public long ProductId { get; set; }
        public string productName { get; set; }
        public string productDescription { get; set; }
        public int categoryId { get; set; }

        public List<Ecommerce.Data.AttributeViewModel> attributes { get; set; }

    }

    // public class attibuteViewModel
    // {
    //     public int attributeId { get; set; }
    //     public string attributeName { get; set; }
    //     public string attributeValue { get; set; }
    // }


}