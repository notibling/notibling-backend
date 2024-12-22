using NotiblingBackend.Domain.Enums;
using NotiblingBackend.Utilities.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotiblingBackend.Domain.Entities
{
    public class Company : User
    {
        private string _companyName;
        private string _legalName;
        private string _rut;
        private DateOnly _foundationDate;
        private string? _webSite;
        private int _industryId;
        private int _companyLevelId;
        private int _companyTypeId;

        public Guid CompanyId { get; private set; }

        [Required]
        //[StringLength(50, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 100 caracteres.")]
        public string CompanyName
        {
            get => _companyName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("The company name cannot be empty.");

                _companyName = value.Trim();
            }
        }

        [Required]
        //[StringLength(50, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 100 caracteres.")]
        //Razón Social
        public string LegalName
        {
            get => _legalName;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("The company's legal name cannot be empty.");
                _legalName = value.Trim();
            }
        }

        [Required]
        //[RegularExpression(@"^\d{7,8}-[0-9kK]$", ErrorMessage = "Formato de RUT inválido.")]
        public string Rut
        {
            get => _rut;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException("The company's RUT or TIN number cannot be empty.");
                if (value.Length < 10 || value.Length > 12)
                    throw new CompanyException("El RUT debe de tener entre 10  y 12 numeros");
                _rut = value.Trim();
            }
        }
        [Required]
        public DateOnly FoundationDate
        {
            get => _foundationDate;
            set
            {
                if (value > DateOnly.FromDateTime(DateTime.Now))
                    throw new ArgumentException("The company's foundation date cannot be later than the current date.");

                if (value == DateOnly.MinValue)
                    throw new ArgumentNullException("The company's foundation date is not valid.");
                _foundationDate = value;
            }
        }
        public string? WebSite
        {
            get => _webSite;
            set
            {
                if (!string.IsNullOrWhiteSpace(value) && !Uri.IsWellFormedUriString(value, UriKind.Absolute))
                    throw new ArgumentException("El sitio web debe ser una URL válida.");
                _webSite = value?.Trim();
            }
        }

        [Required]
        public int IndustryId
        {
            get => _industryId; set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("IndustryId must be a positive number.");
                _industryId = value;
            }
        }
        public Industry Industry { get; set; }
        [Required]
        public int CompanyLevelId 
        {
            get => _companyLevelId; set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("CompanyLevelId must be a positive number.");
                _companyLevelId = value;
            }
        }
        public CompanyLevels CompanyLevels { get; set; }
        [Required]
        public int CompanyTypeId
        {
            get => _companyTypeId; set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("CompanyTypeId must be a positive number.");
                _companyTypeId = value;
            }
        }
        public CompanyType CompanyType { get; set; }
        [Required]
        public CompanyRole CompanyRole { get; set; }

        public Company()
        {
            //UserName = GenerateUserName();
            CompanyRole = CompanyRole.Free;
            UserRole = UserRole.Company;
        }

        public override string GenerateUserName()
        {

            if (_legalName.Length < 5)
            {
                Random random = new Random();
                int number = random.Next(1000, 10000);
                return $"{_legalName.ToLower()}{number}";
            }

            return _legalName.ToLower();
        }

        public void Validate()
        {
            // Lanzará excepciones si alguna propiedad no es válida.
            _ = CompanyName;
            _ = LegalName;
            _ = Rut;
            _ = WebSite;
            _ = LastName;
        }
    }
}
