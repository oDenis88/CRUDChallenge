using System.ComponentModel.DataAnnotations;

namespace ATS.Web.DTOs
{
    public class personDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email{ get; set; }
    }
}