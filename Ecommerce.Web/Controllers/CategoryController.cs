using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Ecommerce.Data;
using Ecommerce.Repository;

namespace Ecommerce.Web.Controllers
{
    [Route("[controller]")]
    public class CategoryController : Controller
    {
        private IProductRepository _productRepository;

        public CategoryController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            var data = _productRepository.getCategories();
            return new ObjectResult(data);
        }


    }
}