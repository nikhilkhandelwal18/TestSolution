using BusinessEntities;
using BusinessServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AttributeRouting;
using AttributeRouting.Web.Http;
using WebAPI.Filters;
using WebAPI.ActionFilters;

namespace WebAPI.Controllers
{
    //[ApiAuthenticationFilter]
    //[AttributeRouting.RoutePrefix("v1/Products/Product")]

    [AuthorizationRequired]
    [AttributeRouting.RoutePrefix("v1/Products/Product")]
    public class ProductController : ApiController
    {
        private readonly IProductServices _productServices;

        #region Public Constructor

        /// <summary>
        /// Public constructor to initialize product service instance
        /// </summary>
        public ProductController(IProductServices productServices)
        {
            _productServices = productServices; //new ProductServices();
        }

        #endregion

        // GET: api/Product
        public HttpResponseMessage Get()
        {
            var products = _productServices.GetAllProducts();
            if (products != null)
            {
                var productEntities = products as List<ProductEntity> ?? products.ToList();
                if (productEntities.Any())
                    return Request.CreateResponse(HttpStatusCode.OK, productEntities);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "Products not found.");
        }

        // GET: api/Product/5
        [GET("ProductId/{id?}")]
        [GET("product/id/{id?}")]
        [GET("myproduct/id/{id?}")]
        [GET("particularproduct/{id?}")]
        [GET("myproduct/{id:range(1, 3)}")]
        public HttpResponseMessage Get(int id)
        {
            var product = _productServices.GetProductById(id);
            if (product != null)
                return Request.CreateResponse(HttpStatusCode.OK, product);
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No product found for this id");
        }

        // POST: api/Product
        public int Post([FromBody]ProductEntity productEntity)
        {
            return _productServices.CreateProduct(productEntity);
        }

        // PUT: api/Product/5
        public bool Put(int id, [FromBody]ProductEntity productEntity)
        {
            if (id > 0)
            {
                return _productServices.UpdateProduct(id, productEntity);
            }
            return false;
        }

        // DELETE: api/Product/5
        public bool Delete(int id)
        {
            if (id > 0)
                return _productServices.DeleteProduct(id);
            return false;
        }
    }
}
