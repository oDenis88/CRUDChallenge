using System;
using System.ComponentModel.DataAnnotations;

namespace ATS.Domain.Models
{
    public class Person : BaseEntity
    {
 
        [Required]
        public string Name { get;  set; }
        [Required]
        public string Email{ get;  set; }
        public string? Phone { get;  set; }
        public string? Address { get;  set; }
        public string? Profession { get;  set; }
        public string? UrlLinkedin { get;  set; }
 
    }
}