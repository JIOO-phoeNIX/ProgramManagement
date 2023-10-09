using AutoMapper;
using ProgramManagement.Core.DTOs.WorkFlow;
using ProgramManagement.Persistence.Entity;

namespace ProgramManagement.Core.MappingProfiles;

public class WorkFlowProfile : Profile
{
    public WorkFlowProfile()
    {
        CreateMap<AddWorkFlowRequest, WorkFlow>().ReverseMap();
    }
}