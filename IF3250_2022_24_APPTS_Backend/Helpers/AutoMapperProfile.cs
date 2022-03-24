namespace IF3250_2022_24_APPTS_Backend.Helpers;

using AutoMapper;
using IF3250_2022_24_APPTS_Backend.Entities;
using IF3250_2022_24_APPTS_Backend.Models.User;
using IF3250_2022_24_APPTS_Backend.Models.JobOpening;
using IF3250_2022_24_APPTS_Backend.Models.JobApplication;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        // User -> AuthenticateResponse
        CreateMap<User, AuthenticateResponse>();

        // RegisterRequest -> User
        CreateMap<RegisterRequest, User>();

        // UpdateRequest -> User
        CreateMap<UpdateRequest, User>()
            .ForAllMembers(x => x.Condition(
                (src, dest, prop) =>
                {
                    // ignore null & empty string properties
                    if (prop == null) return false;
                    if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

                    return true;
                }
            ));

        // AddRequest -> JobOpening
        CreateMap<AddJobOpeningRequest, JobOpening>();

        // AddRequest -> JobApplication
        CreateMap<AddJobApplicationRequest, JobApplication>();

        // JobApplication -> JobApplicationResponse
        CreateMap<JobApplication, JobApplicationResponse>();

        // UpdateRequest -> JobApplication
        CreateMap<UpdateJobApplicationRequest, JobApplication>();
    }
}