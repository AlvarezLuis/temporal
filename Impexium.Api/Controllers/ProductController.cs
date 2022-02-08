using Impexium.Entities.Interfaces;
using Impexium.Entities.Request;
using Result = Impexium.Entities.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Impexium.Domain.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Impexium.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("List")]
        public async Task<IActionResult> List()
        {
            try
            {
                return new JsonResult(Result.Response.BuildResponse(StatusCodes.Status200OK, await _productService.GetAllProducts()));
            }
            catch (Exception ex)
            {
                Response.StatusCode = StatusCodes.Status500InternalServerError;
                return new JsonResult(Result.Response.BuildResponse(StatusCodes.Status500InternalServerError, ex.Message));
            }
        }

        
        [HttpPut("Create")]
        [ServiceFilter(typeof(ActionFilter))]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Create(ProductRequest request)
        {
            try
            {                
                Response.StatusCode = StatusCodes.Status201Created;
                var product = request.ConvertToProduct();
                await _productService.Add(product);
                return new JsonResult(Result.Response.BuildResponse(StatusCodes.Status201Created, product));
            }
            catch (Exception ex)
            {
                Response.StatusCode = StatusCodes.Status500InternalServerError;
                return new JsonResult(Result.Response.BuildResponse(StatusCodes.Status500InternalServerError, ex.Message));
            }
        }

        [ServiceFilter(typeof(ActionFilter))]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("Update")]
        public async Task<IActionResult> Update(ProductRequest request)
        {
            try
            {                
                var product = request.ConvertToProduct();
                await _productService.Update(product);
                return new JsonResult(Result.Response.BuildResponse(StatusCodes.Status200OK, product));
            }
            catch (Exception ex)
            {
                Response.StatusCode = StatusCodes.Status500InternalServerError;
                return new JsonResult(Result.Response.BuildResponse(StatusCodes.Status500InternalServerError, ex.Message));
            }
        }

        [ServiceFilter(typeof(ActionFilter))]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(ProductRequest request)
        {
            try
            {
                var product = request.ConvertToProduct();
                await _productService.Remove(product);
                return new JsonResult(Result.Response.BuildResponse(StatusCodes.Status200OK, product));
            }
            catch (Exception ex)
            {
                
                Response.StatusCode = StatusCodes.Status500InternalServerError;
                return new JsonResult(Result.Response.BuildResponse(StatusCodes.Status500InternalServerError, new List<string>() { ex.Message }));
            }
        }
    }
}
