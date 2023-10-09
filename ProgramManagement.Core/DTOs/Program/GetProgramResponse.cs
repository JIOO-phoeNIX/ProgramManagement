using ProgramManagement.Core.Models;

namespace ProgramManagement.Core.DTOs.Program;

public class GetProgramResponse
{
    public Models.Program Program { get; set; } = new Models.Program();
}