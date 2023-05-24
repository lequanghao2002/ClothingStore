using System.ComponentModel.DataAnnotations;

namespace ClothingStore.Models.Domain
{
    public class Authorities
    {
        [Key]
        public int ID_Authorize { get; set; }
        public string? Authorize { get; set; }

        // navigation properties: one user just has one authorize
        public List<User>? User { get; set; }
    }
}
