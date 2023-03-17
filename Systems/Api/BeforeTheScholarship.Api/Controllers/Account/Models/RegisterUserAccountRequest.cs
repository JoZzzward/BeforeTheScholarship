﻿using AutoMapper;
using BeforeTheScholarship.Services.UserAccountService.Models;

namespace BeforeTheScholarship.Api.Controllers.Accounts;

public class RegisterUserAccountRequest
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}

public class RegisterUserAccountRequestProfile : Profile
{
    public RegisterUserAccountRequestProfile()
    {
        CreateMap<RegisterUserAccountRequest, RegisterUserAccountModel>();
    }
}

