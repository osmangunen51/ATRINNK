﻿using MakinaTurkiye.Core.Data;
using MakinaTurkiye.Entities.Tables.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakinaTurkiye.Services.Catalog
{
    public class DeletedProductRedirectService : IDeletedProductRedirectService
    {
        #region fields

        private readonly IRepository<DeletedProductRedirect> _deletedProductRedirectRepository;

        #endregion

        public DeletedProductRedirectService(IRepository<DeletedProductRedirect> deletedProductRedirectRepository)
        {
            _deletedProductRedirectRepository = deletedProductRedirectRepository;
        }

        public DeletedProductRedirect GetDeletedProductRedirectByProductId(int productId)
        {
            var query = _deletedProductRedirectRepository.Table;
            return query.FirstOrDefault(x => x.ProductId == productId);

        }

        public void InsertDeletedProductRedirect(DeletedProductRedirect deletedProductRedirect)
        {
            if (deletedProductRedirect == null)
                throw new ArgumentNullException("deletedProductRedirect");
            _deletedProductRedirectRepository.Insert(deletedProductRedirect);
        }
    }
}
