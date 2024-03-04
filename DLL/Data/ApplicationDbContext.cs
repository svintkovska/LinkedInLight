﻿using Domain.Models;
using DLL.Constants;
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
		public DbSet<Comment> Comments { get; set; }
		public DbSet<Like> Likes { get; set; }
		public DbSet<Connection> Connections { get; set; }
		public DbSet<ConnectionRequest> ConnectionRequests { get; set; }
		public DbSet<Message> Messages { get; set; }
		public DbSet<Notification> Notifications { get; set; }
		public DbSet<JobPosting> JobPostings { get; set; }
		public DbSet<JobApplication> JobApplications { get; set; }
		public DbSet<ScreeningQuestion> ScreeningQuestions { get; set; }
		public DbSet<ScreeningAnswer> ScreeningAnswers { get; set; }
		public DbSet<Language> Languages { get; set; }


		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);
			modelBuilder.Entity<IdentityRole>().HasData(
				new IdentityRole { Id = "1", Name = RoleConstants.ADMIN, NormalizedName = RoleConstants.ADMIN.ToUpper() },
				new IdentityRole { Id = "2", Name = RoleConstants.MODERATOR, NormalizedName = RoleConstants.MODERATOR.ToUpper() },
				new IdentityRole { Id = "3", Name = RoleConstants.AUTHORIZED_USER, NormalizedName = RoleConstants.AUTHORIZED_USER.ToUpper() },
				new IdentityRole { Id = "4", Name = RoleConstants.UNAUTHORIZED_USER, NormalizedName = RoleConstants.UNAUTHORIZED_USER.ToUpper() },
				new IdentityRole { Id = "5", Name = RoleConstants.FOUNDER, NormalizedName = RoleConstants.FOUNDER.ToUpper() },
				new IdentityRole { Id = "6", Name = RoleConstants.RECRUITER, NormalizedName = RoleConstants.RECRUITER.ToUpper() }
				);
			modelBuilder.Entity<ApplicationUser>()
			   .HasMany(u => u.Experiences)
			   .WithOne(e => e.ApplicationUser)
			   .HasForeignKey(e => e.ApplicationUserId);
			modelBuilder.Entity<ApplicationUser>()
			   .HasMany(u => u.Educations)
			   .WithOne(e => e.ApplicationUser)
			   .HasForeignKey(e => e.ApplicationUserId);
			modelBuilder.Entity<Connection>()
				.HasOne(c => c.User)
				.WithMany(u => u.Connections)
				.HasForeignKey(c => c.UserId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Connection>()
				.HasOne(c => c.ConnectedUser)
				.WithMany()
				.HasForeignKey(c => c.ConnectedUserId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<ConnectionRequest>()
				.HasOne(cr => cr.Receiver)
				.WithMany(u => u.ReceivedConnectionRequests)
				.HasForeignKey(cr => cr.ReceiverId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<ConnectionRequest>()
				.HasOne(cr => cr.Sender)
				.WithMany(u => u.SentConnectionRequests)
				.HasForeignKey(cr => cr.SenderId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Post>()
			   .HasOne(p => p.ApplicationUser)
			   .WithMany(u => u.Posts)
			   .HasForeignKey(p => p.ApplicationUserId)
			   .OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Post>()
				.HasMany(p => p.Comments)
				.WithOne(c => c.Post)
				.HasForeignKey(c => c.PostId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<Post>()
				.HasMany(p => p.Likes)
				.WithOne(l => l.Post)
				.HasForeignKey(l => l.PostId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<Comment>()
				.HasOne(c => c.ApplicationUser)
				.WithMany(u => u.Comments)
				.HasForeignKey(c => c.ApplicationUserId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Like>()
				.HasOne(l => l.Post)
				.WithMany(p => p.Likes)
				.HasForeignKey(l => l.PostId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Like>()
				.HasOne(l => l.Comment)
				.WithMany(c => c.Likes)
				.HasForeignKey(l => l.CommentId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<Message>()
			   .HasOne(m => m.Sender)
			   .WithMany()
			   .HasForeignKey(m => m.SenderId)
			   .OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Message>()
				.HasOne(m => m.Receiver)
				.WithMany()
				.HasForeignKey(m => m.ReceiverId)
				.OnDelete(DeleteBehavior.Restrict);
			modelBuilder.Entity<Notification>()
			   .HasOne(n => n.ApplicationUser)
			   .WithMany(u => u.Notifications)
			   .HasForeignKey(n => n.ApplicationUserId)
			   .OnDelete(DeleteBehavior.Restrict);



			modelBuilder.Entity<JobPosting>()
				.HasOne(j => j.Company)
				.WithMany()
				.HasForeignKey(j => j.CompanyId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<JobPosting>()
				.HasOne(j => j.Recruiter)
				.WithMany(u => u.JobPostings)
				.HasForeignKey(j => j.RecruiterId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<JobPosting>()
				.HasOne(j => j.Industry)
				.WithMany()
				.HasForeignKey(j => j.IndustryId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<JobApplication>()
				.HasOne(a => a.JobPosting)
				.WithMany(j => j.Applications)
				.HasForeignKey(a => a.JobPostingId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<JobApplication>()
				.HasOne(a => a.Candidate)
				.WithMany()
				.HasForeignKey(a => a.CandidateId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<ScreeningQuestion>()
				.HasOne(q => q.JobPosting)
				.WithMany(j => j.ScreeningQuestions)
				.HasForeignKey(q => q.JobPostingId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<ScreeningAnswer>()
				.HasOne(a => a.ScreeningQuestion)
				.WithMany()
				.HasForeignKey(a => a.ScreeningQuestionId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<ScreeningAnswer>()
				.HasOne(a => a.Candidate)
				.WithMany()
				.HasForeignKey(a => a.CandidateId)
				.OnDelete(DeleteBehavior.Restrict);
		}
	}
}
