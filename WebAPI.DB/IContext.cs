using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.DB.CommonAppModel;
using WebAPI.DB.Entities;
using WebAPI.DB.Guide;

namespace WebAPI.DB
{
    public interface IContext
    {
        DbSet<SwaggerLogin> SwaggerLogins { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<BelongToBook> BelongToBooks { get; set; }
        DbSet<Book> Books { get; set; }
        DbSet<AddedAttribute> AddedAttributes { get; set; }
        DbSet<BelongToEvent> BelongToEvents { get; set; }
        DbSet<BelongToScheme> BelongToSchemes { get; set; }
        DbSet<BelongToTimeline> BelongToTimelines { get; set; }
        DbSet<Answer> Answers { get; set; }
        DbSet<Character> Characters { get; set; }
        DbSet<Connection> Connections { get; set; }
        DbSet<Event> Events { get; set; }
        DbSet<BelongToGallery> BelongToGalleries { get; set; }
        DbSet<Picture> Pictures { get; set; }
        DbSet<Scheme> Schemes { get; set; }
        DbSet<Timeline> Timelines { get; set; }
        DbSet<NumberBlock> NumberBlocks { get; set; }
        DbSet<Question> Questions { get; set; }
        DbSet<Sex> Sex { get; set; }
        DbSet<TypeBelongToBook> TypeBelongToBooks { get; set; }
        DbSet<TypeConnection> TypeConnections { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        // Другие методы, которые вам могут понадобиться
    }
}
