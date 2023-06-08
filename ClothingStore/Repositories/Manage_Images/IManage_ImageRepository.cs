using ClothingStore.Models.Domain;
using ClothingStore.Models.Manage_Image;

namespace ClothingStore.Repositories.Manage_Images
{
    public interface IManage_ImageRepository
    {
        Task<List<ImageDTO>> GetAllImage();
        Task<AddImageDTO> AddImage(AddImageDTO imageDTO);

        Task<ImageNoIdDTO> UpdateImage(int id, ImageNoIdDTO imageNoIdDTO);

        Task<Manage_Image>? DeleteImage(int id);

    }
}
