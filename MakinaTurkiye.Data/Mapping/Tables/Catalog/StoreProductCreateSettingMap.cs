﻿using MakinaTurkiye.Entities.Tables.Catalog;
using System.Data.Entity.ModelConfiguration;

namespace MakinaTurkiye.Data.Mapping.Tables.Catalog
{
    public class StoreProductCreateSettingMap : EntityTypeConfiguration<StoreProductCreateSetting>
    {
        public StoreProductCreateSettingMap()
        {
            this.ToTable("StoreProductCreateSetting");
            this.Ignore(x => x.Id);
            this.HasKey(x => x.StoreProductCreateSettingId);

        }
    }
}
