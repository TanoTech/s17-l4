using System.Data;
using Microsoft.EntityFrameworkCore;

namespace s17_l3.Models
{
    public class ScarpeDbContext : DbContext
    {
        public ScarpeDbContext(DbContextOptions<ScarpeDbContext> options) : base(options)
        {
        }

        public DbSet<Scarpa> Scarpe { get; set; }

        public IEnumerable<Scarpa> GetAll()
        {
            return Scarpe.ToList();
        }

        public Scarpa? GetById(int id)
        {
            for (int i = 0; i < Scarpe.Count(); i++)
            {
                var scarpa = Scarpe.ElementAt(i);
                if (scarpa.ID == id)
                {
                    return scarpa;
                }
            }
            return null;
        }

        public Scarpa? Modify(Scarpa scarpa)
        {
            for (int i = 0; i < Scarpe.Count(); i++)
            {
                var existingScarpa = Scarpe.ElementAt(i);
                if (existingScarpa.ID == scarpa.ID)
                {
                    existingScarpa.Nome = scarpa.Nome;
                    existingScarpa.Prezzo = scarpa.Prezzo;
                    existingScarpa.Descrizione = scarpa.Descrizione;
                    SaveChanges();
                    return existingScarpa;
                }
            }
            return null;
        }

        public Scarpa? hardDelete(int? idCancella)
        {
            for (int i = 0; i < Scarpe.Count(); i++)
            {
                var scarpa = Scarpe.ElementAt(i);
                if (scarpa.ID == idCancella)
                {
                    Scarpe.Remove(scarpa);
                    SaveChanges();
                    return scarpa;
                }
            }
            return null;
        }
    }
}