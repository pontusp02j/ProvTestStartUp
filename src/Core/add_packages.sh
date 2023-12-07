#!/bin/bash

# Adding NuGet packages to the .NET project
dotnet add package AutoMapper --version 12.0.1
dotnet add package AutoMapper.Extensions.Microsoft.DependencyInjection --version 12.0.1
dotnet add package BCrypt.Net-Next --version 4.0.3
dotnet add package FastEndpoints --version 5.10.0
dotnet add package FluentValidation.AspNetCore --version 11.3.0
dotnet add package MediatR --version 12.0.1
dotnet add package Microsoft.AspNetCore.Cors --version 2.2.0
dotnet add package Microsoft.AspNetCore.Http.Features --version 5.0.17
dotnet add package Microsoft.AspNetCore.Session --version 2.2.0
dotnet add package Microsoft.EntityFrameworkCore --version 7.0.5
dotnet add package Microsoft.EntityFrameworkCore.Design --version 7.0.5
dotnet add package Microsoft.EntityFrameworkCore.Tools --version 7.0.5
dotnet add package Microsoft.Extensions.DependencyInjection --version 7.0.0
dotnet add package Microsoft.IdentityModel.Tokens --version 6.30.1
dotnet add package Newtonsoft.Json --version 13.0.3
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL --version 7.0.4
dotnet add package System.IdentityModel.Tokens.Jwt --version 6.30.1
dotnet add package CacheManager.Microsoft.Extensions.Caching.Memory --version 2.0.0-beta-1629

echo "Packages added successfully."
