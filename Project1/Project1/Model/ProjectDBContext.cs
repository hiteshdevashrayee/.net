using Microsoft.EntityFrameworkCore;

namespace Project1.Model
{
    public class ProjectDBContext : DbContext
    {
        DbSet<Subscribers> Subscribers;
    }
}
