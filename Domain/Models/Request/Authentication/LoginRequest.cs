﻿namespace Domain.Models.Request.Authentication;

public class LoginRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}
