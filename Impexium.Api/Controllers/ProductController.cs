using Impexium.Entities.Interfaces;
using Impexium.Entities.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Impexium.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            this._productService = productService;
        }

        [HttpGet("List")]
        public ActionResult List()
        {
            try
            {
                return new JsonResult(_productService.GetAllProducts());
            }
            catch (Exception ex)
            {
                Response.StatusCode = StatusCodes.Status500InternalServerError;
                return new JsonResult(new Response() { StatusCode = StatusCodes.Status500InternalServerError, Error = ex.Message });
            }
        }

        [HttpPost("create")]
        public ActionResult Create()
        {
            try
            {
                Response.StatusCode = StatusCodes.Status201Created;
                return new JsonResult(_productService.GetAllProducts());
            }
            catch (Exception ex)
            {
                Response.StatusCode = StatusCodes.Status500InternalServerError;
                return new JsonResult(new Response() { StatusCode = StatusCodes.Status500InternalServerError, Error = ex.Message });
            }
        }

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }
    }
}
