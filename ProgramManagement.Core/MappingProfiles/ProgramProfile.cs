using AutoMapper;
using ProgramManagement.Core.DTOs.Program;
using ProgramManagement.Persistence.Entity;

namespace ProgramManagement.Core.MappingProfiles;

public class ProgramProfile : Profile
{
    public ProgramProfile()
    {
        CreateMap<AddProgramRequest, Program>().ReverseMap();
        CreateMap<Models.Program, Program>().ReverseMap();
    }
}