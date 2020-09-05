using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class Doctor
    {
        public int Id { get; set; }

        [Required()]
        [MaxLength(255)]
        public string Name { get; set; }

        [Required()]
        public string Cpf { get; set; }

        [Required()]
        public string Crm { get; set; }

        [Required()]
        public string[] Expertise { get; set; }
    }

}
