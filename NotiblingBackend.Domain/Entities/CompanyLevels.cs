using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotiblingBackend.Domain.Entities
{
    public class CompanyLevels
    {
        [Key]
        public int LevelCompanyId { get; set; }
        public string LevelName { get; set; }
        public ICollection<Company> Companies { get; set; }
    }
}
