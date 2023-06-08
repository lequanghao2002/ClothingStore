using ClothingStore.Models.Domain;
using ClothingStore.Models.Users;

namespace ClothingStore.Repositories.Users
{
    public interface IUserRepository
    {
        Task<List<UserDTO>> GetAllUser();

        Task<GetUserAPDTO> GetUserByAccPass(string account, string password);

        Task<RegisterUserDTO> RegisterUser(RegisterUserDTO registerUserDTO);


        LoginUser LoginUser(LoginUser loginUser);

        Task<UpdateUserDTO> UpdateUser(string Account, string Password, UpdateUserDTO updateUserDTO);

        Task<AddAuthorizeUserDTO> AddAuthorizeUser(int id, AddAuthorizeUserDTO addAuthorizeUserDTO);

        Task<User> DeleteUser(int id);



    }
}
