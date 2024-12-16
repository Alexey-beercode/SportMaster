﻿using SportMaster.Domain.Enums;
using SportMaster.Domain.Interfaces;

namespace SportMaster.Domain.Entities;

public class User : BaseEntity
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public int Age { get; set; }
    public decimal Height { get; set; }
    public decimal Weight { get; set; }
    public Gender Gender { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}