using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HikerTrails.Models
{
    public class Hikes
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Cost { get; set; }

        public string Difficulty { get; set; }

        public int MinimumAge { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public double StartLongitude { get; set; }

        public double StartLatitude { get; set; }

        public double HikeRating { get; set; }

        public int MaximumParticipants { get; set; }

    }
}