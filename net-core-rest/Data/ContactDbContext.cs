using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace net_core_rest.Data
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class ContactDbContext : DbContext
    {
        public DbSet<Models.Contact> Contacts { get; set; }
        public ContactDbContext(DbContextOptions<ContactDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.Contact>()
                .Property(p => p.Name)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Models.Contact>()
                .Property(p => p.Email)
                .HasMaxLength(100)
                .IsRequired();

            modelBuilder.Entity<Models.Contact>()
                .HasIndex(p => p.Email)
                .IsUnique();

            modelBuilder.Entity<Models.Contact>()
                .Property(p => p.Phone)
                .HasMaxLength(20)
                .IsRequired();

            modelBuilder.Entity<Models.Contact>()
                .Property(p => p.Address)
                .HasMaxLength(250);

            modelBuilder.Entity<Models.Contact>()
                .Property(p => p.Notes)
                .HasMaxLength(250);

            modelBuilder.Entity<Models.Contact>()
                .HasData(
                new Models.Contact { Id = 1, Name = "Rhody Farquhar", Email = "rfarquhar0@va.gov", Phone = "2906036054", URL = "http://prweb.com", Photo = "https://robohash.org/illumetrerum.png?size=100x100&set=set1", Address = "032 Waubesa Avenue", Notes = "vel augue vestibulum rutrum rutrum neque aenean auctor gravida sem praesent id massa id" },
                new Models.Contact { Id = 2, Name = "Farand Valde", Email = "fvalde1@reference.com", Phone = "7504488204", URL = "https://uol.com.br", Photo = "https://robohash.org/voluptatemsimiliqueculpa.png?size=100x100&set=set1", Address = "7 Paget Crossing", Notes = "eleifend donec ut dolor morbi vel lectus in quam fringilla rhoncus mauris enim leo rhoncus" },
                new Models.Contact { Id = 3, Name = "Sarene Cowdery", Email = "scowdery2@behance.net", Phone = "2518926895", URL = "https://multiply.com", Photo = "https://robohash.org/illoetsint.png?size=100x100&set=set1", Address = "52256 North Circle", Notes = "turpis enim blandit mi in porttitor pede justo eu massa donec" },
                new Models.Contact { Id = 4, Name = "Tasia Oldridge", Email = "toldridge3@unc.edu", Phone = "2552926925", URL = "http://icio.us", Photo = "https://robohash.org/eumquamexplicabo.png?size=100x100&set=set1", Address = "11 Merrick Point", Notes = "congue elementum in hac habitasse platea dictumst morbi vestibulum velit id pretium iaculis diam erat fermentum" },
                new Models.Contact { Id = 5, Name = "Raina Pigney", Email = "rpigney4@weibo.com", Phone = "1665350879", URL = "https://imdb.com", Photo = "https://robohash.org/ametquodiusto.png?size=100x100&set=set1", Address = "50 Petterle Hill", Notes = "interdum mauris non ligula pellentesque ultrices phasellus id sapien in sapien" },
                new Models.Contact { Id = 6, Name = "Rhianna Dermot", Email = "rdermot5@answers.com", Phone = "1657592814", URL = "https://clickbank.net", Photo = "https://robohash.org/iustoexcepturiquia.png?size=100x100&set=set1", Address = "6565 Sherman Trail", Notes = "lacus curabitur at ipsum ac tellus semper interdum mauris ullamcorper purus sit amet" },
                new Models.Contact { Id = 7, Name = "Eduard Oakden", Email = "eoakden6@cpanel.net", Phone = "9512879955", URL = "https://army.mil", Photo = "https://robohash.org/ducimuslaboriosamqui.png?size=100x100&set=set1", Address = "9 2nd Road", Notes = "venenatis turpis enim blandit mi in porttitor pede justo eu massa donec dapibus duis at" },
                new Models.Contact { Id = 8, Name = "Gaby Berndtssen", Email = "gberndtssen7@hubpages.com", Phone = "3003138365", URL = "http://quantcast.com", Photo = "https://robohash.org/eteosblanditiis.png?size=100x100&set=set1", Address = "2925 Aberg Circle", Notes = "magna at nunc commodo placerat praesent blandit nam nulla integer pede justo lacinia eget tincidunt eget tempus" },
                new Models.Contact { Id = 9, Name = "Gill Hunnable", Email = "ghunnable8@dot.gov", Phone = "7078567528", URL = "http://boston.com", Photo = "https://robohash.org/quovoluptatemvoluptas.png?size=100x100&set=set1", Address = "03 Johnson Terrace", Notes = "mollis molestie lorem quisque ut erat curabitur gravida nisi at nibh in hac habitasse" },
                new Models.Contact { Id = 10, Name = "Bethanne Agius", Email = "bagius9@oakley.com", Phone = "5969400750", URL = "http://google.it", Photo = "https://robohash.org/harumvelitaut.png?size=100x100&set=set1", Address = "8 American Drive", Notes = "vestibulum vestibulum ante ipsum primis in faucibus orci luctus et" }
                );

            base.OnModelCreating(modelBuilder);
        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
