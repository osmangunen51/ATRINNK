﻿namespace NeoSistem.MakinaTurkiye.Web.Models
{
  using System;
  using System.Collections.Generic;
  using System.Web.Mvc;
  using EnterpriseEntity.Extensions.Data;
  using System.Linq;
  using System.ComponentModel;
  using NeoSistem.MakinaTurkiye.Web.Models.ViewModels;
    using global::MakinaTurkiye.Entities.Tables.Stores;

    public class StoreModel2
  {

    public string GeneralText { get; set; }
    public string HistoryText { get; set; }
    public string FounderText { get; set; }
    public string PhilosophyText { get; set; }

    public string StoreTypeName { get; set; }
    public string StoreEmployeesCountName { get; set; }
    public string StoreEndorsementName { get; set; }
    public string StoreCapitalName { get; set; }

    public IEnumerable<ActivityType> ActivityItems { get; set; }
    public IEnumerable<StoreActivityType> StoreActivityItems { get; set; }

    public int MainPartyId { get; set; }

    public string StoreName { get; set; }

    public string StoreLogo { get; set; }
    public string StoreAbout { get; set; }

  }
}