﻿using Microsoft.AspNetCore.Identity;

namespace Progetto_S18_L5.Models
{
    public class ApplicationRole : IdentityRole
    {
        // navigazione
        public ICollection<ApplicationUserRole> ApplicationUserRole { get; set; }
    }
}
