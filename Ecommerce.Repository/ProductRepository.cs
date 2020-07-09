using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ecommerce.Data;

namespace Ecommerce.Repository
{
    public class ProductRepository : IProductRepository
    {
        ECommerceDemoContext _db;
        public ProductRepository(ECommerceDemoContext db)
        {
            _db = db;
        }

        public List<ProductViewModel> getProductsByPage(int pageSize, int currentPage)
        {
            var result = _db.Product.Include(i => i.ProdCat).Skip((currentPage - 1) * pageSize).Take(pageSize)
                            .Select(i => new ProductViewModel
                            {
                                productName = i.ProdName,
                                productId = i.ProductId,
                                productDescription = i.ProdDescription,
                                categoryId = i.ProdCatId,
                                categoryName = i.ProdCat.CategoryName
                            }).ToList();
            return result;
        }

        public ProductViewModel getProductDetails(int productId)
        {
            ProductViewModel model = new ProductViewModel();

            model = _db.Product.Where(i => i.ProductId == productId)
                            .Select(i => new ProductViewModel
                            {
                                productName = i.ProdName,
                                productId = i.ProductId,
                                productDescription = i.ProdDescription,
                                categoryId = i.ProdCatId
                            }).FirstOrDefault();

            model.attributes = GetProductAttributesByProductId(productId);

            return model;
        }

        private List<AttributeViewModel> GetProductAttributesByProductId(long productId)
        {
            var qry = from l in _db.ProductAttributeLookup
                      join a in (
                              from a in _db.ProductAttribute
                              where a.ProductId == productId
                              select a) on l.AttributeId equals a.AttributeId into a2
                      from a3 in a2.DefaultIfEmpty()
                      join p in _db.Product on l.ProdCatId equals p.ProdCatId
                      where p.ProductId == productId
                      select new AttributeViewModel
                      {
                          AttributeValue = a3.AttributeValue ?? "",
                          AttributeName = l.AttributeName,
                          AttributeId = l.AttributeId
                      };
            var result = qry.ToList();

            return result;
        }

        public int getProductsCount()
        {
            return _db.Product.Count();
        }

        public List<ProductCategory> getCategories()
        {
            var result = _db.ProductCategory.ToList();
            return result;
        }

        public List<ProductAttributeLookup> getAttributesByCategoryId(int categoryId)
        {
            var result = _db.ProductAttributeLookup.Where(i => i.ProdCatId == categoryId).ToList();
            return result;
        }

        public void createProduct(ProductViewModel model)
        {
            Product productObj = model.ToProduct();
            List<ProductAttribute> productAttributes = model.ToProductAttributes();

            createProduct(productObj, productAttributes);
        }

        private void createProduct(Product product, List<ProductAttribute> productAttributes)
        {
            _db.Product.Add(product);
            _db.SaveChanges();

            foreach (ProductAttribute item in productAttributes)
            {
                var paraList = new[] {
                    new SqlParameter("@productId", product.ProductId),
                    new SqlParameter("@attributeId", item.AttributeId),
                    new SqlParameter("@attributeValue", item.AttributeValue)
                };
                var affectedRows = _db.Database.ExecuteSqlRaw("INSERT INTO [dbo].[ProductAttribute] VALUES (@productId, @attributeId, @attributeValue)", paraList);
            }
        }

        public void editProduct(ProductViewModel model)
        {
            Product productObj = model.ToProduct();
            List<ProductAttribute> productAttributes = model.ToProductAttributes();

            editProduct(productObj, productAttributes);
        }

        private void editProduct(Product product, List<ProductAttribute> productAttributes)
        {
            updateProductEntity(product);
            deleteAttributeRecords(product.ProductId);
            addAttributesRecords(product, productAttributes);
        }

        private void updateProductEntity(Product product)
        {
            _db.Product.Attach(product);
            _db.Entry(product).State = EntityState.Modified;
            _db.SaveChanges();
        }

        private void deleteAttributeRecords(long productId)
        {
            //Delete existing attribute records
            var paraDeleteList = new[] {
                    new SqlParameter("@productId", productId)
                };
            var deletedRows = _db.Database.ExecuteSqlRaw("delete from [dbo].[ProductAttribute] where ProductId = @productId", paraDeleteList);
        }

        private void addAttributesRecords(Product product, List<ProductAttribute> productAttributes)
        {
            //Add new attributes
            foreach (ProductAttribute item in productAttributes)
            {
                var paraList = new[] {
                    new SqlParameter("@productId", product.ProductId),
                    new SqlParameter("@attributeId", item.AttributeId),
                    new SqlParameter("@attributeValue", item.AttributeValue)
                };

                var affectedRows = _db.Database.ExecuteSqlRaw("INSERT INTO [dbo].[ProductAttribute] VALUES (@productId, @attributeId, @attributeValue)", paraList);
            }
        }

    }
}
