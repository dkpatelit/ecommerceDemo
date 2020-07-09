using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ecommerce.Data;

namespace Ecommerce.Repository
{
    public interface IProductRepository
    {
        ProductViewModel getProductDetails(int productId);
        
        List<ProductViewModel> getProductsByPage(int pageSize, int currentPage);

        int getProductsCount();

        List<ProductCategory> getCategories();

        List<ProductAttributeLookup> getAttributesByCategoryId(int categoryId);

        void createProduct(ProductViewModel model);

        void editProduct(ProductViewModel model);
    }
}