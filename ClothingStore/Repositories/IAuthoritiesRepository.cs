using ClothingStore.Models.Domain;
using ClothingStore.Models.DTO;

namespace ClothingStore.Repositories
{
    public interface IAuthoritiesRepository
    {
        Task<List<AuthorizeDTO>> GetAllAuthorize();
        Task<CreateAuthorizeDTO> CreateAuthorize(CreateAuthorizeDTO createAuthorizeDTO);
        Task<AuthorizeNoIdDTO> UpdateAuthorize(int id, AuthorizeNoIdDTO authorizeNoIdDTO);

        Task<Authorities> DeleteAuthorize (int id); 
    }
}
