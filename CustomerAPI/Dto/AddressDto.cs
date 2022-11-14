using System.ComponentModel.DataAnnotations;

namespace CustomerAPI.Dto
{
    public class AddressDto
    {
        public string AddressLine { get; set; }

        [Required(ErrorMessage = "City name is a required field.")]
        public string City { get; set; }

        [Required(ErrorMessage = "Country name is a required field.")]
        public string Country { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "City code is required and it can't be lower than 1")]
        public int CityCode { get; set; }
    }
}
