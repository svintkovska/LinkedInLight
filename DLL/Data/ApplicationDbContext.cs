using DLL.Models;
using DLL.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{

		}
		public DbSet<ApplicationUser> ApplicationUsers { get; set; }
		public DbSet<Experience> Experiences { get; set; }
		public DbSet<Education> Educations { get; set; }
		public DbSet<Skill> Skills { get; set; }
		public DbSet<Company> Companies { get; set; }
		public DbSet<Industry> Industries { get; set; }
		public DbSet<Post> Posts { get; set; }


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<IdentityRole>().HasData(
				new IdentityRole { Id = "1", Name = RoleConstants.User, NormalizedName = RoleConstants.User.ToUpper() },
				new IdentityRole { Id = "2", Name = RoleConstants.Founder, NormalizedName = RoleConstants.Founder.ToUpper() },
				new IdentityRole { Id = "3", Name = RoleConstants.Recruiter, NormalizedName = RoleConstants.Recruiter.ToUpper() }
				);
		}
	}
 }
