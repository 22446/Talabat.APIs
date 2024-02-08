﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities
{
    public class AppUser : IdentityUser
    {
        public string DisplayName { get; set; }
        public Address address { get; set; }
    }
}
