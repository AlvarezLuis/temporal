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

namespace Impexium.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
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
        public async Task<IActionResult> Create(ProductRequest request)
        {
            try
            {
                //if (!ModelState.IsValid || request.Id != null)
                //{
                //    return BadRequest(Result.Response.BuildResponse(StatusCodes.Status400BadRequest, GetErrorList()));
                //}
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

        [HttpPost("Update")]
        public async Task<IActionResult> Update(ProductRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(Result.Response.BuildResponse(StatusCodes.Status400BadRequest, GetErrorList()));
                }
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

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(ProductRequest request)
        {
            try
            {
                if (!ModelState.IsValid || request.Id != null)
                {
                    return BadRequest(Result.Response.BuildResponse(StatusCodes.Status400BadRequest, GetErrorList()));
                }
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

        private List<string> GetErrorList()
        {
            return ModelState.Values.Aggregate(
               new List<string>(),
               (a, c) =>
               {
                   a.AddRange(c.Errors.Select(r => r.ErrorMessage));
                   return a;
               },
               a => a
            );
        }
    }
}
