using ProgramManagement.Persistence.Model;
using System.ComponentModel.DataAnnotations;

namespace ProgramManagement.Core.DTOs.WorkFlow;

public class AddWorkFlowRequest
{
    [Required]
    public string ProgramId { get; set; }
    public List<Stage> Stages { get; set; } = new List<Stage>();
}