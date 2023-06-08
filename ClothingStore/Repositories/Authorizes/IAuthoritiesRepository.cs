using ClothingStore.Models.Authorize;
using ClothingStore.Models.Domain;

namespace ClothingStore.Repositories.Authorize
{
    public interface IAuthoritiesRepository
    {
        Task<List<AuthorizeDTO>> GetAllAuthorize();
        Task<CreateAuthorizeDTO> CreateAuthorize(CreateAuthorizeDTO createAuthorizeDTO);
        Task<AuthorizeNoIdDTO> UpdateAuthorize(int id, AuthorizeNoIdDTO authorizeNoIdDTO);

        Task<Authorities> DeleteAuthorize(int id);
    }
}
