using System.ComponentModel.DataAnnotations;

namespace Car.Application.ViewModels
{
    public class ResetPasswordVM
    {
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Bu xana boş qala bilməz")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Bu xana boş qala bilməz")]
        [Compare("NewPassword", ErrorMessage = "Şifrəni düzgün daxil edin")]
        public string ConfirmPassword { get; set; }
    }
}
