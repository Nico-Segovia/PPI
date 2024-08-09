using System.ComponentModel.DataAnnotations;

namespace OrdenesInversionAPI.Models
{
    public class Usuario
    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string PasswordHash { get; set; }
    }
}
