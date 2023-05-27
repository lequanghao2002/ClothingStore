using ClothingStore.Data;
using ClothingStore.Models.Domain;
using ClothingStore.Models.Products;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ClothingStore.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _appDbContext;

        public ProductRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<List<GetProductDTO>> GetAll(string? filter, string? sortBy, bool isAcending = true, int page = 1, int pageSize = 10)
        {
            var allProductDomain = _appDbContext.Product.AsQueryable();

            if (!string.IsNullOrEmpty(filter))
            {
                allProductDomain = allProductDomain.Where(m => m.NameProduct.Contains(filter));
            }

            if (!string.IsNullOrEmpty(sortBy))
            {
                if (sortBy.Equals("Id", StringComparison.OrdinalIgnoreCase))
                {
                    allProductDomain = isAcending ? allProductDomain.OrderBy(m => m.ID_Product) : allProductDomain.OrderByDescending(m => m.ID_Product);
                }

                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    allProductDomain = isAcending ? allProductDomain.OrderBy(m => m.NameProduct) : allProductDomain.OrderByDescending(m => m.NameProduct);
                }

                if (sortBy.Equals("Price", StringComparison.OrdinalIgnoreCase))
                {
                    allProductDomain = isAcending ? allProductDomain.OrderBy(m => m.Price) : allProductDomain.OrderByDescending(m => m.Price);
                }
            }

            allProductDomain = allProductDomain.Skip((page - 1) * pageSize).Take(pageSize);

            var productDomainList = await allProductDomain.Select(product => new GetProductDTO
            {
                ID_Product = product.ID_Product,
                NameProduct = product.NameProduct,
                ProductDetail = product.ProductDetail,
                Price = product.Price,
                Discount = product.Discount,
                ID_Category = product.ID_Category,
                ID_MI = product.ID_MI,
            }).ToListAsync();

            return productDomainList;
        }

        public async Task<GetProductDTO> GetById(int id)
        {
            var productById = await _appDbContext.Product.SingleOrDefaultAsync(m => m.ID_Product == id);
            var productDomain = new GetProductDTO
            {
                ID_Product = productById.ID_Product,
                NameProduct = productById.NameProduct,
                ProductDetail = productById.ProductDetail,
                Price = productById.Price,
                Discount = productById.Discount,
                ID_Category = productById.ID_Category,
                ID_MI = productById.ID_MI,
            };
            return productDomain;
        }

        public async Task<List<GetProductDTO>> GetByCategory(int id)
        {
            var productDomain = await _appDbContext.Product
                .Where(m => m.ID_Category == id)
                .Select(product => new GetProductDTO
                {
                    ID_Product = product.ID_Product,
                    NameProduct = product.NameProduct,
                    ProductDetail = product.ProductDetail,
                    Price = product.Price,
                    Discount = product.Discount,
                    ID_Category = product.ID_Category,
                    ID_MI = product.ID_MI,
                }).ToListAsync();

            return productDomain;
        }

        public async Task<CreateProductDTO> Create(CreateProductDTO createProductDTO)
        {
            var productDomain = new Product
            {
                NameProduct = createProductDTO.NameProduct,
                ProductDetail = createProductDTO.ProductDetail,
                Price = createProductDTO.Price,
                Discount = createProductDTO.Discount,
                ID_Category = createProductDTO.ID_Category,
                ID_MI = createProductDTO.ID_MI,
            };
            await _appDbContext.AddAsync(productDomain);
            await _appDbContext.SaveChangesAsync();

            return createProductDTO;
        }

        public async Task<CreateProductDTO> Update(CreateProductDTO createProductDTO, int id)
        {
            var productDomain = _appDbContext.Product!.SingleOrDefault(m => m.ID_Product == id);

            if (productDomain != null)
            {
                productDomain.NameProduct = createProductDTO.NameProduct;
                productDomain.ProductDetail = createProductDTO.ProductDetail;
                productDomain.Price = createProductDTO.Price;
                productDomain.Discount = createProductDTO.Discount;
                productDomain.ID_Category = createProductDTO.ID_Category;
                productDomain.ID_MI = createProductDTO.ID_MI;

                await _appDbContext.SaveChangesAsync();
            }
            else
            {
                return null!;
            }

            return createProductDTO;
        }

        public async Task<Product> Delete(int id)
        {
            var productDomain = _appDbContext.Product!.SingleOrDefault(m => m.ID_Product == id);
            if (productDomain != null)
            {
                _appDbContext.Remove(productDomain);
                await _appDbContext.SaveChangesAsync();
            }
            else
            {
                return null!;
            }

            return productDomain;
        }


    }
}
