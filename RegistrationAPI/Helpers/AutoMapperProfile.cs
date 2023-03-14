﻿using AutoMapper;
using RegistrationAPI.Entities;
using RegistrationAPI.Models;

namespace RegistrationAPI.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RegisterUserDto, User>();

            CreateMap<User, UserDto>();
        }
    }
}
