using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartJobAssistant.Core.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartJobAssistant.Repository.Identity
{
	public class StoreIdentityDbContext:IdentityDbContext<AppUser>
	{
        public StoreIdentityDbContext(DbContextOptions<StoreIdentityDbContext> options):base(options) { }



    }
}
