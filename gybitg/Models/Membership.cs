using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace gybitg.Models
{
    public class Membership
    {
        [Key]
        public int Id { get; set; }

        public string Type { get; set; }

    }
}
