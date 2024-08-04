using System.ComponentModel.DataAnnotations;

namespace Car.Application.ViewModels
{
    public class AdminVM
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Bu xana boş qala bilməz")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Bu xana boş qala bilməz")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Username { get; set; }
        [Required(ErrorMessage = "Bu xana boş qala bilməz")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Bu xana boş qala bilməz")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Şifrəni düzgün daxil edin")]
        public string CheckPassword { get; set; }
        public bool IsRemember { get; set; }
        public bool Status { get; set; }
        public DateTime Created { get; set; }
        public string Role { get; set; }
    }
}
