using ProgramManagement.Persistence.Model;

namespace ProgramManagement.Persistence.Entity;

public class WorkFlow
{
    public string id { get; set; }
    public List<Stage> Stages { get; set; } = new List<Stage>();
    public DateTime DateCreated { get; set; }
    public DateTime? DateLastUpdated { get; set; }
}