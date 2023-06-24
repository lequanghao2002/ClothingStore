using Blogger_Web.Infrastructure.Core;
using ClothingStore.Data;
using ClothingStore.Models.Domain;
using ClothingStore.Models.Products;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ClothingStore.Repositories.Products
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _appDbContext;

        public ProductRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<PaginationSet<GetProductDTO>> GetAll(string? filter, string? sortBy, bool isAcending = true, int page = 0, int pageSize = 6)
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

            var productDomainList = await allProductDomain.Select(product => new GetProductDTO
            {
                ID_Product = product.ID_Product,
                NameProduct = product.NameProduct,
                ProductDetail = product.ProductDetail,
                Price = product.Price,
                Discount = product.Discount,
                ID_Category = product.ID_Category,
                ID_MI = product.ID_MI,
                Image_Url = product.Manage_Images.Image_url,
                Category_Name = product.Categories.NameCategory
            }).OrderByDescending(p => p.ID_Product).ToListAsync();

            var totalCount = productDomainList.Count();
            var listProductPagination = productDomainList.Skip(page * pageSize).Take(pageSize);

            PaginationSet<GetProductDTO> paginationSet = new PaginationSet<GetProductDTO>()
            {
                List = listProductPagination,
                Page = page,
                TotalCount = totalCount,
                PagesCount = (int)Math.Ceiling((decimal)totalCount / pageSize),
            };

            return paginationSet;
        }

        public async Task<GetProductDTO> GetById(int id)
        {
            var productById = await _appDbContext.Product.Select(productById => new GetProductDTO
            {
                ID_Product = productById.ID_Product,
                NameProduct = productById.NameProduct,
                ProductDetail = productById.ProductDetail,
                Price = productById.Price,
                Discount = productById.Discount,
                ID_Category = productById.ID_Category,
                ID_MI = productById.ID_MI,
                Image_Url = productById.Manage_Images.Image_url,
                Category_Name = productById.Categories.NameCategory
            }).FirstOrDefaultAsync(p => p.ID_Product == id);
            
            return productById;
        }

        public async Task<List<GetProductDTO>> GetByCategory(int id)
        {
            var productDomain = await _appDbContext.Product!
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
            var imageDomain = new Manage_Image
            {
                Image_url = createProductDTO.Image_Url
            };
            
            await _appDbContext.Manage_Image.AddAsync(imageDomain);
            await _appDbContext.SaveChangesAsync();

            var productDomain = new Product
            {
                NameProduct = createProductDTO.NameProduct,
                ProductDetail = createProductDTO.ProductDetail,
                Price = createProductDTO.Price,
                Discount = createProductDTO.Discount,
                ID_Category = createProductDTO.ID_Category,
                ID_MI = imageDomain.ID_MI,
            };
            await _appDbContext.AddAsync(productDomain);
            await _appDbContext.SaveChangesAsync();

            return createProductDTO;
        }

        public async Task<CreateProductDTO> Update(CreateProductDTO createProductDTO, int id)
        {
            var productDomain = _appDbContext.Product!.SingleOrDefault(m => m.ID_Product == id);

            var image = await _appDbContext.Manage_Image.FirstOrDefaultAsync(i => i.Image_url == createProductDTO.Image_Url);

            if(image != null)
            {
                if (productDomain != null)
                {
                    productDomain.NameProduct = createProductDTO.NameProduct;
                    productDomain.ProductDetail = createProductDTO.ProductDetail;
                    productDomain.Price = createProductDTO.Price;
                    productDomain.Discount = createProductDTO.Discount;
                    productDomain.ID_Category = createProductDTO.ID_Category;
                    productDomain.ID_MI = image.ID_MI;

                    await _appDbContext.SaveChangesAsync();
                }
                else
                {
                    return null!;
                }

            }
            else
            {
                var imageDomain = new Manage_Image
                {
                    Image_url = createProductDTO.Image_Url
                };

                await _appDbContext.Manage_Image.AddAsync(imageDomain);
                await _appDbContext.SaveChangesAsync();

                if (productDomain != null)
                {
                    productDomain.NameProduct = createProductDTO.NameProduct;
                    productDomain.ProductDetail = createProductDTO.ProductDetail;
                    productDomain.Price = createProductDTO.Price;
                    productDomain.Discount = createProductDTO.Discount;
                    productDomain.ID_Category = createProductDTO.ID_Category;
                    productDomain.ID_MI = imageDomain.ID_MI;

                    await _appDbContext.SaveChangesAsync();
                }
                else
                {
                    return null!;
                }
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
