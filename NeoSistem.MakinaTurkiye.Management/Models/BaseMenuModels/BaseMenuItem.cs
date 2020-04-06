using MakinaTurkiye.Entities.Tables.Content;
using System;
using System.Collections.Generic;

namespace NeoSistem.MakinaTurkiye.Management.Models.BaseMenuModels
{
    public class BaseMenuItem
    {
        public BaseMenuItem()
        {
            this.BaseMenuCategories = new List<BaseMenuCategory>();
            this.ImagePaths = new List<string>();
        }
        public int BaseMenuId { get; set; }
        public string BaseMenuName { get; set; }
        public DateTime CreatedTime { get; set; }
        public bool Active { get; set; }
        public byte Order { get; set; }
        public byte? HomePageOrder { get; set; }
        public List<BaseMenuCategory> BaseMenuCategories { get; set; }
        public List<string> ImagePaths { get; set; }
        
    }
}