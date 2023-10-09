﻿using ProgramManagement.Persistence.Model;
using System.ComponentModel.DataAnnotations;

namespace ProgramManagement.Core.DTOs.ApplicationFlow;

public class AddApplicationFormRequest
{
    [Required]
    public string ProgramId { get; set; }
    [Required]
    [StringLength(1000)]
    public string ImageUrl { get; set; }
    public PersonalInformationChecker? Phone { get; set; }
    public PersonalInformationChecker? Nationality { get; set; }
    public PersonalInformationChecker? CurrentResidence { get; set; }
    public PersonalInformationChecker? IdNumber { get; set; }
    public PersonalInformationChecker? DOB { get; set; }
    public PersonalInformationChecker? Gender { get; set; }
    public ProfileChecker? Education { get; set; }
    public ProfileChecker? Experience { get; set; }
    public ProfileChecker? Resume { get; set; }
    public List<Question> Questions { get; set; } = new List<Question>();
}