using System.ComponentModel.DataAnnotations;

namespace ProgramManagement.Core.DTOs.Program;

public class AddProgramRequest
{
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
    public string? Start { get; set; }
    [Required]
    [StringLength(100)]
    public string Open { get; set; }
    [Required]
    [StringLength(100)]
    public string Close { get; set; }
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
}