using ClothingStore.Data;
using ClothingStore.Models.Domain;
using ClothingStore.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace ClothingStore.Repositories
{
    public class AuthorizeRepository : IAuthoritiesRepository
    {
        private readonly AppDbContext _appDbContext;
        public AuthorizeRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task<List<AuthorizeDTO>> GetAllAuthorize()
        {
            var AllAuthorize = _appDbContext.Authorities.AsQueryable();
            var ListAuthorize = await AllAuthorize.Select(authorize => new AuthorizeDTO
            {
                ID_Authorize = authorize.ID_Authorize,
                Authorize = authorize.Authorize
            }).ToListAsync();
            return ListAuthorize;
        }

        public async Task<CreateAuthorizeDTO> CreateAuthorize(CreateAuthorizeDTO createAuthorizeDTO)
        {
            var authorize = new Authorities
            {
                Authorize = createAuthorizeDTO.Authorize
            };
            await _appDbContext.Authorities.AddAsync(authorize);
            await _appDbContext.SaveChangesAsync();
            return createAuthorizeDTO;
        }

        public async Task<AuthorizeNoIdDTO> UpdateAuthorize(int id, AuthorizeNoIdDTO authorizeNoIdDTO)
        {
            var AuthorizeDomain = _appDbContext.Authorities!.FirstOrDefault(a => a.ID_Authorize == id);
            if (AuthorizeDomain != null)
            {
                AuthorizeDomain.Authorize = authorizeNoIdDTO.Authorize;
                await _appDbContext.SaveChangesAsync();
            }
            return authorizeNoIdDTO;
        }

        public async Task<Authorities> DeleteAuthorize(int id)
        {
            var AuthorizeDomain = _appDbContext.Authorities!.SingleOrDefault(a => a.ID_Authorize == id);
            if (AuthorizeDomain != null)
            {
                _appDbContext.Authorities.Remove(AuthorizeDomain);
                await _appDbContext.SaveChangesAsync();
            }
            else return null!;
            return AuthorizeDomain;
        }
    }
}
