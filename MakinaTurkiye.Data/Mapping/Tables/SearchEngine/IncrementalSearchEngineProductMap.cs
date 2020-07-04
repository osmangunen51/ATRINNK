﻿using MakinaTurkiye.Entities.Tables.SearchEngine;
using System.Data.Entity.ModelConfiguration;

namespace MakinaTurkiye.Data.Mapping.Tables.SearchEngine
{
    public class IncrementalSearchEngineProductMap : EntityTypeConfiguration<IncrementalSearchEngineProduct>
    {
        public IncrementalSearchEngineProductMap()
        {
            this.ToTable("Incremental_SearchEngine_Product");
            this.HasKey(sel => sel.Id);
            this.Ignore(sel => sel.SearchEngineType);
        }
    }
}
