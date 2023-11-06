using System;
using System.ComponentModel.DataAnnotations;

namespace BE_cybershark.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is a required field.")]
        [StringLength(45)]
        public string? Ten { get; set; } = "";

        [Required(ErrorMessage = "Password is a required field.")]
        [StringLength(64)]
        public string Mat_khau { get; set; } = "";

        [Required(ErrorMessage = "Email is a required field.")]
        [StringLength(255)]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; } = "";

        [StringLength(15)]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Invalid phone number.")]
        public string So_dien_thoai { get; set; } = "";

        [StringLength(255)]
        public string Hinhanh { get; set; } = "";

        [StringLength(255)]
        public string Role { get; set; } = "";

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
