using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Panel.Models
{
    public class Kisi
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Lütfen Email giriniz.")]
        [EmailAddress(ErrorMessage = "Lütfen mail adresinizi düzgün giriniz")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Lütfen Şifre giriniz.")]
        public string Password { get; set; }
    }
}
