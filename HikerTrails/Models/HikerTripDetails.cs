using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HikerTrails.Models
{
    public class HikerTripDetails
    {
        [Key]
        public int Id { get; set; }

        public int HikeId { get; set; }

        public string UserID { get; set; }

        public float UserRating { get; set; }
    }
}