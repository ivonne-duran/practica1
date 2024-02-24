using Microsoft.EntityFrameworkCore;



namespace practica1.Models
{
    public class EstadosEquipoContext: DbContext
    {
        public EstadosEquipoContext ( DbContextOptions<EstadosEquipoContext> options ) : base( options )
        { } 
        public DbSet<EstadosEquipo> EstadosEquipo { get; set; }
    }

}
