using ProgramManagement.Core.DTOs.ApplicationFlow;
using ProgramManagement.Core.DTOs.Program;
using ProgramManagement.Core.DTOs.WorkFlow;
using ProgramManagement.Core.Helpers;

namespace ProgramManagement.Core.Interfaces;

public interface IProgramInterface
{
    public Task<ApiResponse> AddProgramAsync(AddProgramRequest request);
    public Task<ApiResponse> GetProgramAsync(string id);
    public Task<ApiResponse> UpdateProgramAsync(UpdateProgramRequest request);
    public Task<ApiResponse> AddApplicationFormAsync(AddApplicationFormRequest request);
    public Task<ApiResponse> GetApplicationFormAsync(string id);
    public Task<ApiResponse> AddAddWorkFlowAsync(AddWorkFlowRequest request);
    public Task<ApiResponse> GetWorkFlowAsync(string id);
    public Task<ApiResponse> GetSummaryAsync(string programId);
}