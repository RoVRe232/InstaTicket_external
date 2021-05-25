using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InstaTicket.Models
{
    public class Admin:User
    {
        [StringLength(2048)]
        public string ActionsHistory { get; set; }
    }
}
