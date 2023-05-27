using ClothingStore.Models.Domain;
using ClothingStore.Models.DTO;

namespace ClothingStore.Repositories
{
    public interface IManage_ImageRepository
    {
        Task<List<ImageDTO>> GetAllImage();
        Task<AddImageDTO> AddImage (AddImageDTO imageDTO);

        Task<ImageNoIdDTO> UpdateImage(int id, ImageNoIdDTO imageNoIdDTO);

        Task<Manage_Image>? DeleteImage(int id);

    }
}
