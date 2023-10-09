using AutoMapper;
using ProgramManagement.Core.DTOs.ApplicationFlow;
using ProgramManagement.Persistence.Entity;

namespace ProgramManagement.Core.MappingProfiles;

public class ApplicationFormProfile : Profile
{
    public ApplicationFormProfile()
    {
        CreateMap<AddApplicationFormRequest, ApplicationForm>().ReverseMap();
    }
}