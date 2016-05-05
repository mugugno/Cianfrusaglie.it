﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;

namespace Cianfrusaglie.Models{
    public class ApplicationDbContext : IdentityDbContext<User>{
        public DbSet<Category> Categories { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Announce> Announces { get; set; } 
        public DbSet<AnnounceCategory> AnnounceCategories { get; set; } 
        public DbSet<CategoryFormField> CategoryFormFields { get; set; } 
        public DbSet<FormField> FormFields { get; set; } 
        public DbSet<AnnounceFormFieldsValues> AnnounceFormFieldsValues { get; set; } 

        protected override void OnModelCreating(ModelBuilder builder){
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            builder.Entity<Category>().HasIndex(c => c.Name).IsUnique(true);
            builder.Entity<Category>().HasOne(c => c.OverCategory).WithMany(c => c.SubCategories);

         builder.Entity<Announce>().HasOne( u => u.Author ).WithMany( u => u.PublishedAnnounces ).OnDelete( DeleteBehavior.Restrict );

         builder.Entity<AnnounceCategory>().HasKey( x => new { x.AnnounceId, x.CategoryId } );

         builder.Entity<AnnounceCategory>()
             .HasOne( pc => pc.Announce )
             .WithMany( p => p.AnnounceCategories )
             .HasForeignKey( pc => pc.AnnounceId );

         builder.Entity<AnnounceCategory>()
             .HasOne( pc => pc.Category )
             .WithMany( c => c.CategoryAnnounces )
             .HasForeignKey( pc => pc.CategoryId );

         builder.Entity< Message >().HasOne( m => m.Sender ).WithMany( u => u.SentMessages ).OnDelete( DeleteBehavior.Restrict );
            builder.Entity< Message >().HasOne( m => m.Receiver ).WithMany( u => u.ReceivedMessages ).OnDelete( DeleteBehavior.Restrict );

         //---
         builder.Entity<CategoryFormField>().HasKey( x => new { x.FormFieldId, x.CategoryId } );

         builder.Entity<CategoryFormField>()
             .HasOne( pc => pc.FormField )
             .WithMany( p => p.CategoriesFormFields )
             .HasForeignKey( pc => pc.FormFieldId );

         builder.Entity<CategoryFormField>()
             .HasOne( pc => pc.Category )
             .WithMany( c => c.CategoriesFormFields )
             .HasForeignKey( pc => pc.CategoryId );


         builder.Entity<AnnounceFormFieldsValues>().HasKey( x => new { x.FormFieldId, x.AnnounceId } );

         builder.Entity<AnnounceFormFieldsValues>()
             .HasOne( pc => pc.FormField )
             .WithMany( p => p.AnnouncesFormFields )
             .HasForeignKey( pc => pc.FormFieldId );

         builder.Entity<AnnounceFormFieldsValues>()
             .HasOne( pc => pc.Announce )
             .WithMany( c => c.AnnouncesFormFields )
             .HasForeignKey( pc => pc.AnnounceId );
      }
    }
}
