using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Compass.Site.Models;
using CompassSite.Database.Comparers;
using CompassSite.Database.Interfaces;
using CompassSite.Database.Models;

namespace Compass.Site.Services
{
    public class ProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public List<Product> GetProductsByCategory(int categoryId)
        {
            return _productRepository.GetByCategory(categoryId).ToList();
        }

        public List<Product> GetProducts()
        {
            return _productRepository.GetAll().ToList();
        }
        public ProductsViewModel Paginate(List<Product> products, int page)
        {
            PageInfo pageInfo = new PageInfo(products) { PageNumber = page };
            List<Product> productsPerPage = products.Skip((page - 1) * pageInfo.PageSize).Take(pageInfo.PageSize).ToList();
            ProductsViewModel productsViewModel = new ProductsViewModel
            {
                PageInfo = pageInfo,
                Products = productsPerPage,
                Categories = _productRepository.GetAll().Select(t=>t.Category).Distinct(new CategoryComparer()).ToList()
            };
            return productsViewModel;
        }
    }
}
