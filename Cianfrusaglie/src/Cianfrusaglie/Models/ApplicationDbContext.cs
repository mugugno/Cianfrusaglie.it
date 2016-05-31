using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;

namespace Cianfrusaglie.Models {
    public class ApplicationDbContext : IdentityDbContext< User, IdentityRole, string > {
        public ApplicationDbContext( DbContextOptions options ) : base( options ) { }

        public DbSet< Category > Categories { get; set; }
        public DbSet< Message > Messages { get; set; }
        public DbSet< Announce > Announces { get; set; }
        public DbSet< AnnounceExchange > AnnounceExchange { get; set; }
        public DbSet< AnnounceCategory > AnnounceCategories { get; set; }
        public DbSet< CategoryFormField > CategoryFormFields { get; set; }
        public DbSet< FormField > FormFields { get; set; }
        public DbSet< AnnounceFormFieldsValues > AnnounceFormFieldsValues { get; set; }
        public DbSet< Gat > Gats { get; set; }
        public DbSet< AnnounceGat > AnnounceGats { get; set; }
        public DbSet< FeedBack > FeedBacks { get; set; }
        public DbSet< Interested > Interested { get; set; }
        public DbSet< FieldDefaultValue > FieldDefaultValues { get; set; }
        public DbSet< ImageUrl > ImageUrls { get; set; }
        public DbSet< UserGatHistogram > UserGatHistograms { get; set; }
        public DbSet< UserCategoryPreferences > UserCategoryPreferenceses { get; set; }
        public DbSet< AnnounceChosen > AnnounceChosenUsers { get; set; }

        protected override void OnModelCreating( ModelBuilder builder ) {
            base.OnModelCreating( builder );
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            builder.Entity< Category >().HasOne( c => c.OverCategory ).WithMany( c => c.SubCategories );

            builder.Entity< Announce >().HasOne( u => u.Author ).WithMany( u => u.PublishedAnnounces ).OnDelete(
                DeleteBehavior.Restrict );

            builder.Entity< AnnounceCategory >().HasKey( x => new {x.AnnounceId, x.CategoryId} );

           builder.Entity< Announce >().HasMany( a => a.AnnounceCategories ).WithOne( ac => ac.Announce ).HasForeignKey(
              a => a.AnnounceId ).IsRequired( true );
            //builder.Entity< AnnounceCategory >().HasOne( pc => pc.Announce ).WithMany( p => p.AnnounceCategories )
            //    .HasForeignKey( pc => pc.AnnounceId );

            builder.Entity< AnnounceCategory >().HasOne( pc => pc.Category ).WithMany( c => c.CategoryAnnounces )
                .HasForeignKey( pc => pc.CategoryId );

            builder.Entity< Message >().HasOne( m => m.Sender ).WithMany( u => u.SentMessages ).OnDelete(
                DeleteBehavior.Restrict );
            builder.Entity< Message >().HasOne( m => m.Receiver ).WithMany( u => u.ReceivedMessages ).OnDelete(
                DeleteBehavior.Restrict );

            //---
            builder.Entity< CategoryFormField >().HasKey( x => new {x.FormFieldId, x.CategoryId} );

            builder.Entity< CategoryFormField >().HasOne( pc => pc.FormField ).WithMany( p => p.CategoriesFormFields )
                .HasForeignKey( pc => pc.FormFieldId );

            builder.Entity< CategoryFormField >().HasOne( pc => pc.Category ).WithMany( c => c.CategoriesFormFields )
                .HasForeignKey( pc => pc.CategoryId );


            builder.Entity< AnnounceFormFieldsValues >().HasKey( x => new {x.FormFieldId, x.AnnounceId} );

            builder.Entity< AnnounceFormFieldsValues >().HasOne( pc => pc.FormField ).WithMany(
                p => p.AnnouncesFormFields ).HasForeignKey( pc => pc.FormFieldId );

            builder.Entity< AnnounceFormFieldsValues >().HasOne( pc => pc.Announce ).WithMany(
                c => c.AnnouncesFormFields ).HasForeignKey( pc => pc.AnnounceId );


            builder.Entity< AnnounceGat >().HasKey( x => new {x.GatId, x.AnnounceId} );

            builder.Entity< AnnounceGat >().HasOne( pc => pc.Announce ).WithMany( p => p.AnnouncesGats ).HasForeignKey(
                pc => pc.AnnounceId );

            builder.Entity< AnnounceGat >().HasOne( pc => pc.Gat ).WithMany( c => c.AnnouncesGats ).HasForeignKey(
                pc => pc.GatId );


            //builder.Entity< FeedBack >().HasKey( f => new {f.AnnounceId, SenderId = f.AuthorId, f.ReceiverId} );
            builder.Entity< FeedBack >().HasOne( u => u.Author ).WithMany( u => u.SentFeedBacks ).OnDelete(
                DeleteBehavior.Restrict );

            builder.Entity< FeedBack >().HasOne( u => u.Receiver ).WithMany( u => u.ReceivedFeedBacks ).OnDelete(
                DeleteBehavior.Restrict );
            builder.Entity< FeedBack >().HasOne( u => u.Announce ).WithMany( u => u.FeedBacks ).HasForeignKey(
                u => u.AnnounceId ).OnDelete( DeleteBehavior.Restrict );


            builder.Entity< Interested >().HasIndex( i => new {i.AnnounceId, i.UserId} ).IsUnique(true);
            builder.Entity< Interested >().HasOne( u => u.Announce ).WithMany( u => u.Interested ).HasForeignKey( i => i.AnnounceId ).OnDelete(
                DeleteBehavior.Restrict );
            builder.Entity< Interested >().HasOne( u => u.User ).WithMany( u => u.InterestedAnnounces ).HasForeignKey( i => i.UserId ).OnDelete(
                DeleteBehavior.Restrict );

            builder.Entity< FieldDefaultValue >().HasOne( f => f.FormField ).WithMany( f => f.DefaultValues ).OnDelete(
                DeleteBehavior.Restrict );


            builder.Entity< ImageUrl >().HasOne( i => i.Announce ).WithMany( a => a.Images ).HasForeignKey( a => a.AnnounceId ).OnDelete(
                DeleteBehavior.Restrict );


            builder.Entity< User >().HasIndex( u => u.UserName ).IsUnique( true );
            builder.Entity< User >().HasIndex( u => u.Email ).IsUnique( true );


           builder.Entity< UserCategoryPreferences >().HasOne( uc => uc.User ).WithMany( u => u.CategoryPreferenceses ).OnDelete(
                DeleteBehavior.Restrict );
      }
    }
}