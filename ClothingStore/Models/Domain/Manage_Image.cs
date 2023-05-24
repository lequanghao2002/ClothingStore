using System.ComponentModel.DataAnnotations;

namespace ClothingStore.Models.Domain
{
    public class Manage_Image
    {
        [Key]
        public int ID_MI { get; set; }
        public string? ImageName { get; set; }  
        public string? Image_url { get; set;}

        //navigation properties: one product has many image
        public Product? Product { get; set; }
    }
}
