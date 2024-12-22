using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotiblingBackend.Domain.Enums
{
    public enum UserRole
    {
        Admin, //Adminstrador
        Staff, //Equipo (No se si es necesario)
        Client, //Cliente normal el que puede comprar
        Entrepreneur, //Emprendedor quien puede ofrecer solo productos pero no es una empresa
        Professional, //Profesional quien puede ofrecer solo servicios (Puede ser llamado tambien ServiceProvider)
        BusinessOwner, //Hibrido entre emprendedor y profesional, es decir puedo ofrecer productos y servicios
        Company //Empresa la cual ofrece todos sus productos con beneficios.
    }
}
