﻿using System;
using System.Collections.Generic;

namespace NeoSistem.Trinnk.Web.Models.Products
{
    public class MTProductComplainModel
    {
        public MTProductComplainModel()
        {
            this.ComplainTypeItemModels = new List<MTProductComplainTypeItemModel>();
        }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductCityName { get; set; }
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public string UserEmail { get; set; }
        public string UserPhone { get; set; }
        public string UserComment { get; set; }
        public Int16 IsMember { get; set; }

        public List<MTProductComplainTypeItemModel> ComplainTypeItemModels { get; set; }
    }
}