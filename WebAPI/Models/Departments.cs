using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class Departments
    {

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
