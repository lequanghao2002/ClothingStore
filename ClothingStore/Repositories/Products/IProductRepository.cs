using ClothingStore.Models.Domain;
using ClothingStore.Models.Products;

namespace ClothingStore.Repositories.Products
{
    public interface IProductRepository
    {
        public Task<List<GetProductDTO>> GetAll(string? filter, string? sortBy, bool isAcending = true, int page = 1, int pageSize = 10);
        public Task<GetProductDTO> GetById(int id);
        public Task<List<GetProductDTO>> GetByCategory(int id);
        public Task<CreateProductDTO> Create(CreateProductDTO createProductDTO);
        public Task<CreateProductDTO> Update(CreateProductDTO createProductDTO, int id);
        public Task<Product> Delete(int id);
    }
}
