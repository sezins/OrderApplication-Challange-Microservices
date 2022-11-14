using System.ComponentModel.DataAnnotations;

namespace CustomerAPI.Entities
{
    public class Address
    {
        public string AddressLine { get; set; }

        [Required(ErrorMessage = "City name is a required field.")]
        public string City { get; set; }

        [Required(ErrorMessage = "Country name is a required field.")]
        public string Country { get; set; }

        [Required(ErrorMessage = "CityCode is a required field.")]
        public int CityCode { get; set; }
    }
}
