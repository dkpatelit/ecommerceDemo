using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Ecommerce.Data;
using Ecommerce.Repository;
using Ecommerce.Web.ViewModels;

namespace Ecommerce.Web.Controllers
{
    public class ProductController : Controller
    {
        private IProductRepository _productRepository;

        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IActionResult Index()
        {
            var count = _productRepository.getProductsCount();
            return View(count);
        }

        public IActionResult createProduct()
        {
            var items = _productRepository.getCategories();
            var result = items.Select(i => new SelectListItem { Text = i.CategoryName, Value = i.ProdCatId.ToString(), Selected = false }).ToList();
            return View(result);
        }

        [HttpPost]
        public IActionResult createProduct(ProductViewModel model)
        {
            _productRepository.createProduct(model);
            return Ok();
        }

        [HttpGet("Product/edit/{productId}")]
        public IActionResult editProduct(int productId)
        {
            ViewData["productId"] = productId;
            var items = _productRepository.getCategories();
            var result = items.Select(i => new SelectListItem { Text = i.CategoryName, Value = i.ProdCatId.ToString(), Selected = false }).ToList();
            return View(result);
        }

         [HttpPost("Product/edit")]
        public IActionResult editProductPost(ProductViewModel model)
        {
            _productRepository.editProduct(model);
            return Ok();
        }

        [HttpGet("Product/getdetails/{productId}")]
        public IActionResult getProductDetails(int productId)
        {
            var model = _productRepository.getProductDetails(productId);
            return new ObjectResult(model);
        }

        public IActionResult getProducts(int pageSize, int currentPage)
        {
            var result = _productRepository.getProductsByPage(pageSize, currentPage);
            return Ok(result);
        }


    }
}