using ClothingStore.Models.Categories;
using ClothingStore.Models.Domain;

namespace ClothingStore.Repositories
{
    public interface ICategoryRepository
    {
        public Task<List<GetCategoryDTO>> GetAll();
        public Task<CreateCategoryDTO> Create(CreateCategoryDTO createCategoryDTO);
        public Task<CreateCategoryDTO> Update(CreateCategoryDTO createCategoryDTO, int id);
        public Task<Category> Delete(int id);
    }
}
