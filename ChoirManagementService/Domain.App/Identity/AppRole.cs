﻿using System;
using System.ComponentModel.DataAnnotations;
using Contracts.Domain;
using Microsoft.AspNetCore.Identity;

namespace Domain.App.Identity
{
    public class AppRole : IdentityRole<Guid>
    {

    }

}