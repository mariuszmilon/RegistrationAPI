﻿namespace RegistrationAPI.Models
{
    public class RegisterUserDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string PersonalIdNumber { get; set; }
        public decimal? AveragePowerConsumption { get; set; }

    }
}