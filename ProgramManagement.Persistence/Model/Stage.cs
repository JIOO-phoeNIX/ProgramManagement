using System.ComponentModel.DataAnnotations;

namespace ProgramManagement.Persistence.Model;

/// <summary>
/// This holds the details of the stage type, add extra paramater(s) for other stage types
/// </summary>
public class Stage
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; }
    [Required]
    [StringLength(50)]
    public string Type { get; set; }
    /// <summary>
    /// If stage is of type VideoInterview, add this if question was added for it
    /// </summary>
    public VideoInterviewQuestion? VideoInterviewQuestion { get; set; }
}