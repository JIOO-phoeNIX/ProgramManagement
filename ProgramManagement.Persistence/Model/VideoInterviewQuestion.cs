
using System.ComponentModel.DataAnnotations;

namespace ProgramManagement.Persistence.Model;

/// <summary>
/// This holds the video interview stage type question details
/// </summary>
public class VideoInterviewQuestion
{
    [StringLength(1000)]
    public string Question { get; set; }
    [StringLength(1000)]
    public string AdditionalInformation { get; set; }
    [Range(1, int.MaxValue)]
    public int MaxDurationOfVideoInSeconds { get; set; }
    [Range(1, int.MaxValue)]
    public int DeadlineForSubmissionInDays { get; set; }
}