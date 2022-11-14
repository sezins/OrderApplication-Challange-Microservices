using System.ComponentModel.DataAnnotations;

namespace CustomerAPI.Dto
{
    public class CustomerDto
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "Customer name is a required field.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email is a required field.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "FullAdress is a required field.")]
        public string FullAdress { get; set; }
    }
}
