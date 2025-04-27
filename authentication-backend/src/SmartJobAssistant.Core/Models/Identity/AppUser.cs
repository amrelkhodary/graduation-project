using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartJobAssistant.Core.Models.Identity
{
	public class AppUser:IdentityUser
	{
        public string DisplayNamr { get; set; }
    }
}
