using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAKAZE.Models
{
    public class Place
    {
        public string Avatar { get; set; }
        public bool IsPromotion { get; set; }
        public string Name { get; set; }
        public string CareerDescription { get; set; }
        public string Address { get; set; }
        public int Ranking { get; set; }

        public string Distance { get; set; }
        public string OpeningHours { get; set; }
        public string NumberOfCheckIn { get; set; }

    }
}
