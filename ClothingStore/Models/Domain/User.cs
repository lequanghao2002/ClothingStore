using System.ComponentModel.DataAnnotations;

namespace ClothingStore.Models.Domain
{
    public class User
    {
        [Key]
        public int ID_User { get; set; }
        public string? Account { get; set; }
        public string? Password { get; set; }
        public string? Username { get; set; }
        public DateTime Birthday { get; set; }
        public string? Address { get; set; }
        public DateTime DateCreate { get; set; }
        public int ID_Authorize { get; set; }

        // navigation properties: one user just has one authorize
        public Authorities? Authorities { get; set; }

        //navigation properties: one user has many order
        public List<Order>? Order { get; set; }
    }
}
