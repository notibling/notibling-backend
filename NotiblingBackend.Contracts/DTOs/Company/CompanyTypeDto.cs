﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotiblingBackend.Contracts.DTOs.Company
{
    public class CompanyTypeDto
    {
        public int TypeCompanyId { get; set; }
        public string TypeName { get; set; }
        public string Abbreviation { get; set; }
    }
}
