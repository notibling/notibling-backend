using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotiblingBackend.Domain.Entities
{
    public class CompanyType
    {
        [Key]
        public int TypeCompanyId { get; set; }
        public string TypeName { get; set; }
        public string Abbreviation { get; set; }
        public ICollection<Company> Companies { get; set; }
    }
}
