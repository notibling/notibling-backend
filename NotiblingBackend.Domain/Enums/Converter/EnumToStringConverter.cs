using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotiblingBackend.Domain.Enums.Converter
{
    public class EnumToStringConverter<TEnum> : ValueConverter<TEnum, string> where TEnum : struct, Enum
    {
        public EnumToStringConverter()
            : base(
                  role => role.ToString(),
                  roleString => (TEnum)Enum.Parse(typeof(TEnum), roleString))
        { }
    }
}
