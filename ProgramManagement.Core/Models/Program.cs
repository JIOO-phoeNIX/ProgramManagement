
namespace ProgramManagement.Core.Models;

public class Program
{
    public string Title { get; set; }
    public string? Summary { get; set; }
    public string Description { get; set; }
    public string? Skills { get; set; }
    public string? Benefits { get; set; }
    public string? Criteria { get; set; }
    public string Type { get; set; }
    public string? Duration { get; set; }
    public string Location { get; set; }
    public bool IsFullyRemote { get; set; }
    public string? MinQualifications { get; set; }
    public int MaxNoOfApplication { get; set; }
    public string StartDate { get; set; }
    public string DateOpen { get; set; }
    public string DateClose { get; set; }
}