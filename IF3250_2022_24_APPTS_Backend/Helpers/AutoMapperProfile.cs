﻿namespace IF3250_2022_24_APPTS_Backend.Helpers;

using AutoMapper;
using IF3250_2022_24_APPTS_Backend.Entities;
using IF3250_2022_24_APPTS_Backend.Models.User;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // User -> AuthenticateResponse
        CreateMap<Applicant, AuthenticateResponse>();

        // RegisterRequest -> User
        CreateMap<RegisterRequest, Applicant>();

        // UpdateRequest -> User
        CreateMap<UpdateRequest, Applicant>()
            .ForAllMembers(x => x.Condition(
                (src, dest, prop) =>
                {
                    // ignore null & empty string properties
                    if (prop == null) return false;
                    if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

                    return true;
                }
            ));
    }
}