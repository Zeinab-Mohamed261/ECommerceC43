﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObject.IdentityDtos
{
    public class LoginDto
    {
        [EmailAddress]
        public string Email { get; set; } = default!; // default value is null
        public string Password { get; set; } = default!; // default value is null
    }
}
