using Blogger_Web.Infrastructure.Core;
using ClothingStore.Models.Domain;
using ClothingStore.Models.Products;

namespace ClothingStore.Repositories.Products
{
    public interface IProductRepository
    {
        public Task<PaginationSet<GetProductDTO>> GetAll(string? filter, string? sortBy, bool isAcending = true, int page = 0, int pageSize = 6);
        public Task<GetProductDTO> GetById(int id);
        public Task<List<GetProductDTO>> GetByCategory(int id);
        public Task<CreateProductDTO> Create(CreateProductDTO createProductDTO);
        public Task<CreateProductDTO> Update(CreateProductDTO createProductDTO, int id);
        public Task<Product> Delete(int id);
    }
}
