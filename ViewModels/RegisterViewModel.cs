﻿using System.ComponentModel.DataAnnotations;

namespace BE_CyberShark.ViewModels
{
    public class RegisterViewModel
    {

        public string? Ten { get; set; } = "";

        [Required(ErrorMessage = "Password is a required field.")]
        public string Mat_khau { get; set; } = "";

        [Required(ErrorMessage = "Email is a required field.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
     
        public string Email { get; set; } = "";

        [RegularExpression(@"^[0-9]*$", ErrorMessage = "Invalid phone number.")]


        [Required(ErrorMessage = "Phone is a required field.")]
        public string? So_dien_thoai { get; set; } = "";

        public string? Hinhanh { get; set; } = "";

        public string? Role { get; set; } = "user";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
