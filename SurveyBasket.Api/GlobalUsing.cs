global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore;

global using Mapster;
global using MapsterMapper;
global using FluentValidation;
global using FluentValidation.AspNetCore;

global using System.Reflection;

global using SurveyBasket.Api;
global using SurveyBasket.Api.Entities;
global using SurveyBasket.Api.Persistence;
global using SurveyBasket.Api.Authentication;
global using SurveyBasket.Api.Services.Authentication;
global using SurveyBasket.Api.Services.Polls;
global using SurveyBasket.Api.Contracts.Authentication;

global using Microsoft.Extensions.Options;
global using Microsoft.IdentityModel.Tokens;
global using System.IdentityModel.Tokens.Jwt;
global using System.Security.Claims;
