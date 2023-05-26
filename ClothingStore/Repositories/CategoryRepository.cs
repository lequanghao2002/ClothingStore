using ClothingStore.Data;
using ClothingStore.Models.Categories;
using ClothingStore.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _appDbContext;

        public CategoryRepository(AppDbContext appDbContext) {
            _appDbContext = appDbContext;
        }
        public async Task<List<GetCategoryDTO>> GetAll()
        {
            var categoryDomainList = await _appDbContext.Category!.Select(category => new GetCategoryDTO{
                ID_Category = category.ID_Category,
                NameCategory = category.NameCategory,
            }).ToListAsync();

            return categoryDomainList;
        }

        public async Task<CreateCategoryDTO> Create(CreateCategoryDTO createCategoryDTO)
        {
            var categoryDomain = new Category
            {
                NameCategory = createCategoryDTO.NameCategory
            };
            _appDbContext.Category!.Add(categoryDomain);
            await _appDbContext.SaveChangesAsync();

            return createCategoryDTO;
        }

        public async Task<CreateCategoryDTO> Update(CreateCategoryDTO createCategoryDTO, int id)
        {
            var categoryDomain = _appDbContext.Category!.FirstOrDefault(m => m.ID_Category == id);
            if (categoryDomain != null)
            {
                categoryDomain.NameCategory = createCategoryDTO.NameCategory;
                await _appDbContext.SaveChangesAsync();
            }
            else
            {
                return null!;
            }
            return createCategoryDTO;
        }

        public async Task<Category> Delete(int id)
        {
            var categoryDomain = _appDbContext.Category!.SingleOrDefault(m => m.ID_Category == id);

            if(categoryDomain != null)
            {
                _appDbContext.Remove(categoryDomain);
                await _appDbContext.SaveChangesAsync();
            }
            else
            {
                return null!;
            }
            return categoryDomain!;
        }
        
    }
}
