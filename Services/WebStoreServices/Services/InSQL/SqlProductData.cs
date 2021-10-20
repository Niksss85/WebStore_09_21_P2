using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebStore.DAL.Context;
using WebStore.Domain;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Services;

namespace WebStore.Services.InSQL
{
    public class SqlProductData : IProductData
    {
        private readonly WebStoreDB _db;

        public SqlProductData(WebStoreDB db) => _db = db;

        public IEnumerable<Section> GetSections() => _db.Sections;

        public IEnumerable<Brand> GetBrands() => _db.Brands;

        public ProductsPage GetProducts(ProductFilter Filter = null)
        {
            IQueryable<Product> query = _db.Products
               .Include(p => p.Brand)
               .Include(p => p.Section);

            if (Filter?.Ids?.Length > 0)
                query = query.Where(product => Filter.Ids.Contains(product.Id));
            else
            {
                if (Filter?.SectionId is { } section_id)
                    query = query.Where(product => product.SectionId == section_id);

                if (Filter?.BrandId is { } brand_id)
                    query = query.Where(product => product.BrandId == brand_id);
            }
            var total_count = query.Count();

            if (Filter is { PageSize: > 0 and var page_size, Page: > 0 and var page_number })
                query = query
                   .OrderBy(p => p.Order) 
                   .Skip((page_number - 1) * page_size)
                   .Take(page_size);

            return new ProductsPage(query, total_count);
        }

        public Product GetProductById(int Id) => _db.Products
           .Include(p => p.Brand)
           .Include(p => p.Section)
           .SingleOrDefault(p => p.Id == Id);

        public Section GetSection(int id) => _db.Sections
            .Include(s => s.Products)
            .SingleOrDefault(s => s.Id == id);

        public Brand GetBrand(int id) => _db.Brands
            .Include(b => b.Products)
            .SingleOrDefault(b => b.Id == id);
    }
}
