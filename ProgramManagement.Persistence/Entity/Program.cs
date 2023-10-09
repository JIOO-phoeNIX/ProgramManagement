using System.ComponentModel.DataAnnotations;

namespace ProgramManagement.Persistence.Entity;

public class Program
{
    public string id { get; set; }
    [Required]
    [StringLength(500)]
    public string Title { get; set; }
    [StringLength(1000)]
    public string? Summary { get; set; }
    [Required]
    [StringLength(1000)]
    public string Description { get; set; }
    [StringLength(1000)]
    public string? Skills { get; set; }
    [StringLength(1000)]
    public string? Benefits { get; set; }
    [StringLength(1000)]
    public string? Criteria { get; set; }
    [Required]
    [StringLength(100)]
    public string Type { get; set; }
    [StringLength(100)]
    public string? Duration { get; set; }
    [Required]
    [StringLength(100)]
    public string Location { get; set; }
    public bool IsFullyRemote { get; set; }
    [StringLength(100)]
    public string? MinQualifications { get; set; }
    [Range(1, int.MaxValue)]
    public int MaxNoOfApplication { get; set; }
    [StringLength(100)]
    public DateTime? StartDate { get; set; }
    [Required]
    [StringLength(100)]
    public DateTime DateOpen { get; set; }
    [Required]
    [StringLength(100)]
    public DateTime DateClose { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime? DateLastUpdated { get; set; }
    public string? ApplicationFormId { get; set; }
    public string? WorkFlowId { get; set; }
}