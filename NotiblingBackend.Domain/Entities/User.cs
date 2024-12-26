using NotiblingBackend.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using NotiblingBackend.Utilities.Exceptions;
using System.Globalization;
using System.Text.RegularExpressions;
using TimeZoneConverter;
using NodaTime;
using NotiblingBackend.Utilities.Security;

namespace NotiblingBackend.Domain.Entities
{
    //[Microsoft.EntityFrameworkCore.Index(nameof(Email), IsUnique = true)]
    public abstract partial class User
    {
        private string _firstName;
        private string _lastName;
        private string _email;
        private string _password;
        private string _userName;
        private string _country;
        private string _phone;
        private DateTime _createdAt;
        private DateTime? _updatedAt;
        private string _timeZone;
        private bool _isDeleted;
        private DateTime? _deletedAt;

        public int Id { get; private set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        //[RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "El nombre solo puede contener letras y espacios.")]
        //[StringLength(50, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 50 caracteres.")]
        public string FirstName
        {
            get => _firstName; set
            {
                if (value.Trim().Length < 2 || value.Trim().Length > 50)
                    throw new ArgumentException("El nombre debe tener entre 2 y 50 caracteres.");
                if (!IsValidOnlyText(value.Trim()))
                    throw new UserException("El nombre solo puede contener letras y espacios.");
                _firstName = value.Trim();
            }
        }

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        //[RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "El nombre solo puede contener letras y espacios.")]
        //[StringLength(50, MinimumLength = 2, ErrorMessage = "El apellido debe tener entre 2 y 50 caracteres.")]
        public string LastName
        {
            get => _lastName;
            set
            {
                if (value.Trim().Length < 2 || value.Trim().Length > 50)
                    throw new ArgumentException("El apellido debe tener entre 2 y 50 caracteres.");
                if (!IsValidOnlyText(value.Trim()))
                    throw new UserException("El apellido solo puede contener letras y espacios.");
                _lastName = value.Trim();
            }
        }

        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "Correo electrónico inválido.")]
        public string Email
        {
            get => _email;
            set
            {
                try
                {
                    IsValidEmail(value.Trim());
                }
                catch (RegexMatchTimeoutException ex)
                {
                    throw new UserException($"La validación del correo electrónico tomó demasiado tiempo. {ex.Message}");
                }
                catch (ArgumentException ex)
                {
                    throw new UserException($"Error: {ex.Message}");
                }
                catch (Exception ex)
                {
                    throw new UserException($"Se produjo un error inesperado al validar el correo electrónico: {ex.Message}");
                }
                _email = value.Trim();
            }
        }

        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        public string Password
        {
            get => _password;
            set
            {
                if (!IsValidPassword(value))
                    throw new CompanyException("The password must be at least 8 characters long, contain an uppercase letter, a lowercase letter, a number, and a special character.");


                _password = PasswordEncryptor.HashPassword(value.Trim());
            }
        }

        [RegularExpression(@"^[a-zA-Z0-9_.-]+$", ErrorMessage = "El nombre de usuario solo puede contener letras, números, guiones y puntos.")]
        [Required(ErrorMessage = "El nombre de usuario es obligatorio.")]
        public string UserName
        {
            get => _userName;
            set
            {
                if (!IsValidUserName(value))
                    throw new UserException("El nombre de usuario solo puede contener letras, números, guiones o puntos.");
                if (value.Length < 5)
                    throw new UserException("El nombre de usuario tiene que ser mayor a 5 caracteres.");
                _userName = value.Trim();
            }
        }

        public string Country { get => _country; set => _country = value; }
        public string Phone
        {
            get => _phone;
            set
            {
                if (!IsValidPhoneNumber(value))
                    throw new UserException("El número de teléfono proporcionado no es válido.");
                _phone = value.Trim();
            }
        }
        public UserRole UserRole { get; set; }
        public DateTime CreatedAt
        {
            get => _createdAt;
            private set => _createdAt = value;
        }
        public DateTime? UpdatedAt
        {
            get => _updatedAt;
            private set => _updatedAt = value;
        }
        public string TimeZone
        {
            get => _timeZone;
            set
            {
                if (!IsValidTimeZone(value.Trim()))
                    throw new UserException("Zona horaria invalida.");
                _timeZone = value.Trim();
            }
        }
        public AccountStatus AccountStatus { get; private set; }
        public bool IsDeleted
        {
            get => _isDeleted;
            internal set
            {
                _isDeleted = value;
            }
        }
        public DateTime? DeletedAt
        {
            get => _deletedAt;
            private set
            {
                _deletedAt = value;
            }
        }

        protected User()
        {
            var utcNow = DateTime.UtcNow; // Obtienes la hora actual en UTC
            var specifiedUtcNow = DateTime.SpecifyKind(utcNow, DateTimeKind.Unspecified); // Eliminas el Kind de UTC
            _updatedAt = specifiedUtcNow;
            _createdAt = specifiedUtcNow; // Asignas la fecha convertida sin zona horaria
        }

        public virtual string GenerateUserName()
        {
            if (FirstName.Length > 4 && LastName.Length > 4)
            {
                return $"{FirstName[..5].ToLower()}.{LastName[..5].ToLower()}";
            }

            string userName = $"{FirstName.ToLower()}.{LastName.ToLower()}";
            return userName[..10];
        }

        #region Registration Date
        public void UpdateModifiedDate()
        {
            var utcNow = DateTime.UtcNow; // Obtienes la hora actual en UTC
            var specifiedUtcNow = DateTime.SpecifyKind(utcNow, DateTimeKind.Unspecified); // Eliminas el Kind de UTC
            UpdatedAt = specifiedUtcNow;
        }

        public void DeletedModifiedDate()
        {
            var utcNow = DateTime.UtcNow; // Obtienes la hora actual en UTC
            var specifiedUtcNow = DateTime.SpecifyKind(utcNow, DateTimeKind.Unspecified); // Eliminas el Kind de UTC
            DeletedAt = specifiedUtcNow;
        }


        public DateTime GetLocalTime(DateTime utcTime)
        {

            var windowsTimeZoneId = TZConvert.IanaToWindows(TimeZone);

            if (!TimeZoneInfo.GetSystemTimeZones().Any(tz => tz.Id == windowsTimeZoneId))
                throw new InvalidTimeZoneException("Zona horaria no válida.");

            try
            {
                var timeZone = TimeZoneInfo.FindSystemTimeZoneById(TimeZone);
                return TimeZoneInfo.ConvertTimeFromUtc(utcTime, timeZone);
            }
            catch (TimeZoneNotFoundException)
            {
                // Manejo de error: si la zona horaria no es válida, puedes retornar UTC o manejarlo como prefieras
                return utcTime;
            }
            catch (InvalidTimeZoneException)
            {
                return utcTime;
            }
        }
        #endregion

        public string GetUserRole()
        {
            return UserRole.ToString();
        }

        #region Validations
        //public void Validate()
        //{
        //    if (!IsValidOnlyText(FirstName))
        //        throw new UserException("El nombre solo puede contener letras y espacios.");

        //    if (!IsValidOnlyText(LastName))
        //        throw new UserException("El apellido solo puede contener letras y espacios.");

        //    try
        //    {
        //        IsValidEmail(Email);
        //    }
        //    catch (RegexMatchTimeoutException ex)
        //    {
        //        throw new UserException($"La validación del correo electrónico tomó demasiado tiempo. {ex.Message}");
        //    }
        //    catch (ArgumentException ex)
        //    {
        //        throw new UserException($"Error: {ex.Message}");
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new UserException($"Se produjo un error inesperado al validar el correo electrónico: {ex.Message}");
        //    }
        //    if (!IsValidPassword(Password))
        //        throw new UserException("La contraseña debe tener al menos 8 caracteres, una mayúscula, una minúscula, un número y un carácter especial.");

        //    if (!string.IsNullOrEmpty(Phone) && !IsValidPhoneNumber(Phone))
        //        throw new UserException("El número de teléfono proporcionado no es válido.");

        //    if (!IsValidUserName(UserName))
        //        throw new UserException("El nombre de usuario solo puede contener letras, números, guiones o puntos.");
        //    if (UserName.Length < 5)
        //        throw new UserException("El nombre de usuario tiene que ser mayor a 5 caracteres.");

        //    if (!IsValidTimeZone(TimeZone))
        //        throw new UserException("Zona horaria invalida.");

        //}

        private static bool IsValidOnlyText(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return false;

            string regexPattern = @"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$";
            return Regex.IsMatch(input, regexPattern);
        }

        private static void IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("El correo electrónico está vacío.");

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapper,
                                      RegexOptions.None, TimeSpan.FromMilliseconds(200));

                // Examines the domain part of the email and normalizes it.
                static string DomainMapper(Match match)
                {
                    // Use IdnMapping class to convert Unicode domain names.
                    var idn = new IdnMapping();

                    // Pull out and process domain name (throws ArgumentException on invalid)
                    string domainName = idn.GetAscii(match.Groups[2].Value);

                    return match.Groups[1].Value + domainName;
                }
            }
            catch (RegexMatchTimeoutException)
            {
                throw;
            }
            catch (ArgumentException)
            {
                throw;
            }

            // Valida el formato del correo
            if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)))
            {
                throw new ArgumentException("El formato del correo electrónico no es válido.");
            }
        }

        private static readonly Regex PasswordRegex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\w\d\s])[A-Za-z\d^\w\d\s\S]{8,}$", RegexOptions.Compiled);
        private static bool IsValidPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return false;

            return PasswordRegex.IsMatch(password);
        }

        private static bool IsValidPhoneNumber(string? phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return false;

            var regex = new Regex(@"^(2\d{7}|4\d{7}|09[1-9]\d{6})$");
            return regex.IsMatch(phone.Trim());
        }

        private static bool IsValidUserName(string userName)
        {
            if (string.IsNullOrEmpty(userName)) return false;

            string regexPattern = @"^[a-zA-Z0-9_.-]+$";
            return Regex.IsMatch(userName, regexPattern);
        }

        public static bool IsValidTimeZone(string timeZoneId)
        {
            try
            {
                var tz = DateTimeZoneProviders.Tzdb[timeZoneId];
                return tz != null;
            }
            catch (KeyNotFoundException)
            {
                return false;
            }
        }
        #endregion

    }
}
