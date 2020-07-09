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
    public class AttributesController : Controller
    {
        private IProductRepository _productRepository;

        public AttributesController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet("getbycategory/{categoryId}")]
        public IActionResult Index(int categoryId)
        {
            var data = _productRepository.getAttributesByCategoryId(categoryId);
            return new ObjectResult(data);
        }


    }
}