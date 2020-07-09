using System;
using System.Collections.Generic;

namespace Ecommerce.Data
{
    public class ProductViewModel
    {

        public long productId { get; set; }
        public string productName { get; set; }
        public string productDescription { get; set; }
        public int categoryId { get; set; }

        public string categoryName { get; set; }

        public List<AttributeViewModel> attributes { get; set; }

    }

    public static class ProductViewModelExtensions
    {

        public static Product ToProduct(this ProductViewModel model)
        {
            Product productObj = new Product()
            {
                ProductId = model.productId,
                ProdName = model.productName,
                ProdDescription = model.productDescription,
                ProdCatId = model.categoryId
            };

            return productObj;
        }

        public static List<ProductAttribute> ToProductAttributes(this ProductViewModel model)
        {
            List<ProductAttribute> productAttributes = new List<ProductAttribute>();

            if (model.attributes != null)
            {
                foreach (var item in model.attributes)
                {
                    productAttributes.Add(new ProductAttribute()
                                                {
                                                    AttributeId = item.AttributeId,
                                                    AttributeValue = item.AttributeValue ?? ""
                                                }
                                        );
                }
            }

            return productAttributes;
        }
    }
}
