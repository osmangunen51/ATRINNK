using System;

namespace NeoSistem.Trinnk.Management.Models
{
    public class TypeVariables
    {
        [Flags]
        public enum PhoneType
        {
            Tel = 1,
            Fax = 2,
            Cep = 3,
            Diger = 4
        }

        [Flags]
        public enum AddressType
        {
            Ev = 1,
            Is = 2,
            Fatura = 3,
            Diger = 4
        }
    }
}