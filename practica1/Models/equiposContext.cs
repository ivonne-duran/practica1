using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Extensions.Options;

namespace practica1.Models
{
    public class equiposContext : DbContext
    {
      public  equiposContext(DbContextOptions<equiposContext> options): base(options) { }
      public DbSet<equipos> equipos {  get; set; }
    }
    
   
    
}
