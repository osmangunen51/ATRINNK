﻿using MakinaTurkiye.Caching;
using MakinaTurkiye.Core.Data;
using MakinaTurkiye.Data;
using Nest;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.Linq;
namespace MakinaTurkiye.Services.Pinterest
{
    public partial class PinterestService : IPinterestService
    {

        #region Fields

        private readonly IDbContext _dbContext;
        private readonly IDataProvider _dataProvider;

        #endregion

        #region Ctor

        public PinterestService(IDbContext dbContext,IDataProvider dataProvider)
        {
            this._dbContext = dbContext;
            this._dataProvider = dataProvider;
        }

        #endregion

        #region Methods

        public void Islem(string Text)
        {
            
        }
        #endregion
        
    }
}
