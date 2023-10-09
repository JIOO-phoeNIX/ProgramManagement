using ProgramManagement.Core.DTOs.ApplicationFlow;
using ProgramManagement.Core.DTOs.WorkFlow;

namespace ProgramManagement.Core.DTOs.Program;

public class SummaryResponse
{
    public Models.Program Program { get; set; } = new Models.Program();
    public AddApplicationFormRequest ApplicationForm { get; set; } = new AddApplicationFormRequest();
    public AddWorkFlowRequest WorkFlow { get; set; } = new AddWorkFlowRequest();

}