using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HikerTrails.Models
{
    public class HikerTrailsContext : DbContext
    {
        public HikerTrailsContext() : base("DefaultConnection")
        {

        }

        public DbSet<Hikes> Hikes { get; set; }

        public DbSet<HikerTripDetails> HikerTripDetails { get; set; }
    }
}