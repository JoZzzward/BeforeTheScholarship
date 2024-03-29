﻿using System.Text.RegularExpressions;
using FluentValidation;

namespace BeforeTheScholarship.Web.Pages.Auth.Models;

public class PasswordRecoveryRequest
{
    public string Email { get; set; }
    public string Token { get; set; }
    public string NewPassword { get; set; }
}

public class PasswordRecoveryRequestValidator : AbstractValidator<PasswordRecoveryRequest>
{
    public PasswordRecoveryRequestValidator()
    {
        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("Incorrect email.")
            .MaximumLength(50).WithMessage("Email length must be less than 50");

        RuleFor(x => x.Token)
            .Length(230, 250).WithMessage("Token must have correct length");

        // Checks if password was 8-30 symbols and contains minimum 1 lowercase letter 
        RuleFor(x => x.NewPassword)
            .Must(x => new Regex("^(?=.*\\d)(?=.*[a-zA-Z]).{8,30}$").Matches(x).Any())
            .WithMessage("Password must be 8 symbols or more")
            .Must(x => new Regex("^(?=.*\\d)(?=.*[a-zA-Z]).{8,30}$").Matches(x).Any())
            .WithMessage("Password must have minimum 1 lowercase letter");
    }
}