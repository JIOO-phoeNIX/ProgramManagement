using System.ComponentModel.DataAnnotations;

namespace ProgramManagement.Persistence.Model;

public class Question
{
    [Required]
    [StringLength(50)]
    public string Type { get; set; }
    [Required]
    [StringLength(50)]
    public string Section { get; set; }
    [Required]
    [StringLength(1000)]
    public string QuestionText { get; set; }
    public string? Choices { get; set; }
    public bool? EnableOtherOption { get; set; }
    public int MaxChoiceAllowed { get; set; }
    public bool? DisqualifyIfNo { get; set; }
}