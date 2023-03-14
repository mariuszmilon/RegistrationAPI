namespace RegistrationAPI.Models
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string PersonalIdNumber { get; set; }
        public decimal? AveragePowerConsumption { get; set; }
    }
}
