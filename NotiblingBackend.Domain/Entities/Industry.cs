using System.ComponentModel.DataAnnotations;

namespace NotiblingBackend.Domain.Entities
{
    public class Industry
    {
        [Key]
        public int IndustryId { get; set; }
        public string IndustryName { get; set; }
        public ICollection<Company> Companies { get; set; }
    }
}