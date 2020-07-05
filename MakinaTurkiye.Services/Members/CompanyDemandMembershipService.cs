﻿using MakinaTurkiye.Core.Data;
using MakinaTurkiye.Entities.Tables.Members;

namespace MakinaTurkiye.Services.Members
{
    public class CompanyDemandMembershipService : ICompanyDemandMembershipService
    {
        #region Fields

        private readonly IRepository<CompanyDemandMembership> _companyDemandMembershipRepository;

        #endregion

        #region Ctor

        public CompanyDemandMembershipService(IRepository<CompanyDemandMembership> companyDemandMembershipRepository)
        {
            this._companyDemandMembershipRepository = companyDemandMembershipRepository;
        }

        #endregion

        #region Methods

        public void AddCompanyDemandMembership(CompanyDemandMembership companyDemandMembership)
        {
            _companyDemandMembershipRepository.Insert(companyDemandMembership);
        }

        #endregion 
    }
}
