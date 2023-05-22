﻿namespace Trinnk.Api.View
{
    public class District
    {
        public int DistrictId { get; set; }
        public int? CityId { get; set; }
        public int? LocalityId { get; set; }
        public string DistrictName { get; set; }
        public string ZipCode { get; set; }
    }
}