using System.Text;

namespace Trinnk.Entities.Tables.Common
{
    public static class AddressExtensions
    {
        public static string GetFullAddress(this Address address)
        {
            StringBuilder sb = new StringBuilder();
            if (address.Town != null)
            {
                sb.AppendFormat("{0} ", address.Town.TownName);
            }
            sb.AppendFormat("{0} ", address.Avenue);
            if (!string.IsNullOrEmpty(address.Street))
            {
                sb.AppendFormat("{0} ", address.Street);
            }
            if (!string.IsNullOrEmpty(address.ApartmentNo))
            {
                sb.AppendFormat("No: {0} ", address.ApartmentNo);
            }
            if (!string.IsNullOrWhiteSpace(address.DoorNo))
            {
                sb.AppendFormat("/ {0} ", address.DoorNo);
            }

            if (address.Town != null && address.Town.District != null)
            {
                sb.AppendFormat("{0} ", address.Town.District.DistrictName);
            }

            if (address.Locality != null)
            {
                sb.AppendFormat("{0} ", address.Locality.LocalityName);
            }
            if (address.City != null)
            {
                sb.AppendFormat("{0} ", address.City.CityName);
            }
            if (address.Country != null)
            {
                sb.AppendFormat("/ {0} ", address.Country.CountryName);
            }
            return sb.ToString();
        }

        public static string GetLocation(this Address address)
        {
            if (address.Locality != null && address.City != null)
            {
                return string.Format("{0} / {1} / {2}", address.Town != null ? address.Town.TownName : "", address.Locality.LocalityName, address.City.CityName, address.Country.CountryName);
            }
            return string.Empty;
        }
        public static string GetAddressEdit(this Address address)
        {


            if (address != null)
            {
                var builder = new StringBuilder();

                if (address.Town != null)
                {
                    builder.AppendFormat("{0} ", address.Town.TownName);
                }

                builder.AppendFormat("{0} ", address.Avenue);

                if (!string.IsNullOrWhiteSpace(address.Street))
                {
                    builder.AppendFormat("{0} ", address.Street);
                }

                if (!string.IsNullOrWhiteSpace(address.ApartmentNo))
                {
                    builder.AppendFormat("No: {0} ", address.ApartmentNo);
                }

                if (!string.IsNullOrWhiteSpace(address.DoorNo))
                {
                    builder.AppendFormat("/ {0} ", address.DoorNo);
                }


                builder.AppendFormat("{1} {0} {2} / {3}", address.Locality != null ? address.Locality.LocalityName : "", address.Town != null ? address.Town.District != null ? address.Town.District.ZipCode : "" : "", address.City != null ? address.City.CityName : "-", address.Country != null ? address.Country.CountryName : "-");

                return builder.ToString();
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
