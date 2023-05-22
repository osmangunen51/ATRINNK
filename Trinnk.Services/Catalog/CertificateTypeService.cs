using Trinnk.Caching;
using Trinnk.Core.Data;
using Trinnk.Entities.Tables.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Trinnk.Services.Catalog
{
    public class CertificateTypeService : BaseService, ICertificateTypeService
    {
        #region Constants
        private const string CERTIFICATE_CERTIFICATETYPES_KEY = "certifcatetype.certificatetypes";
        private const string CERTIFICATE_CERTIFICATEPRODUCTS_BY_KEY = "certifcatetype.certificatetypeproduct.byproductId-{0}";

        #endregion

        #region Field
        readonly IRepository<CertificateType> _certificateTypeRepository;
        readonly IRepository<CertificateTypeProduct> _certificateTypeProductRepository;
        readonly ICacheManager _cacheManager;
        #endregion
        #region Ctor
        public CertificateTypeService(IRepository<CertificateType> certificateTypeRepository,
            IRepository<CertificateTypeProduct> certificateTypeProductRepository,
            ICacheManager cacheManager) : base(cacheManager)
        {
            _certificateTypeRepository = certificateTypeRepository;
            _certificateTypeProductRepository = certificateTypeProductRepository;
            _cacheManager = cacheManager;

        }
        #endregion
        #region Methods
        public void DeleteCertificateType(CertificateType certificateType)
        {
            if (certificateType == null)
                throw new ArgumentNullException("certificateType");
            _certificateTypeRepository.Delete(certificateType);
            _cacheManager.Remove(CERTIFICATE_CERTIFICATETYPES_KEY);
        }



        public CertificateType GetCertificateTypeByCertificateTypeId(int certificateTypeId)
        {
            if (certificateTypeId == 0)
                throw new ArgumentNullException("certificateTypeId");
            var query = _certificateTypeRepository.Table;
            return query.FirstOrDefault(x => x.CertificateTypeId == certificateTypeId);
        }



        public IList<CertificateType> GetCertificateTypes()
        {

            string key = string.Format(CERTIFICATE_CERTIFICATETYPES_KEY);
            return _cacheManager.Get(key, () =>
            {
                var query = _certificateTypeRepository.TableNoTracking;
                return query.OrderBy(x => x.Order).ThenBy(x => x.Name).ToList();
            });

        }

        public void InsertCertificateType(CertificateType certficateType)
        {
            if (certficateType == null)
                throw new ArgumentNullException("certificateType");
            _certificateTypeRepository.Insert(certficateType);
            _cacheManager.Remove(CERTIFICATE_CERTIFICATETYPES_KEY);
        }



        public void UpdateCertificateType(CertificateType certificateType)
        {
            if (certificateType == null)
                throw new ArgumentNullException("certificateType");
            _certificateTypeRepository.Update(certificateType);
            _cacheManager.Remove(CERTIFICATE_CERTIFICATETYPES_KEY);
        }
        public IList<CertificateTypeProduct> GetCertificateTypeProductsByProductId(int productId, bool setCache = true)
        {
            if (productId == 0)
                throw new ArgumentNullException("productId");
            if (setCache == true)
            {
                var key = string.Format(CERTIFICATE_CERTIFICATEPRODUCTS_BY_KEY, productId);
                return _cacheManager.Get(key, () =>
                {
                    var query = _certificateTypeProductRepository.TableNoTracking;
                    query = query.Where(x => x.ProductId == productId);
                    return query.ToList();
                });
            }
            else
            {
                var query = _certificateTypeProductRepository.Table;
                query = query.Where(x => x.ProductId == productId);
                return query.ToList();
            }


        }
        public void UpdateCertificateTypeProduct(CertificateTypeProduct certificateTypeProduct)
        {
            if (certificateTypeProduct == null)
                throw new ArgumentNullException("certificateTypeProduct");
            _certificateTypeProductRepository.Update(certificateTypeProduct);
            var key = string.Format(CERTIFICATE_CERTIFICATEPRODUCTS_BY_KEY, certificateTypeProduct.ProductId);
            _cacheManager.Remove(key);
        }

        public void InsertCertificateTypeProduct(CertificateTypeProduct certificateTypeProduct)
        {
            if (certificateTypeProduct == null)
                throw new ArgumentNullException("certificateTypeProduct");
            _certificateTypeProductRepository.Insert(certificateTypeProduct);


            var key = string.Format(CERTIFICATE_CERTIFICATEPRODUCTS_BY_KEY, certificateTypeProduct.ProductId);
            _cacheManager.Remove(key);
        }
        public void DeleteCertificateTypeProduct(CertificateTypeProduct certificateTypeProduct)
        {
            if (certificateTypeProduct == null)
                throw new ArgumentNullException("certificateTypeProduct");
            _certificateTypeProductRepository.Delete(certificateTypeProduct);
            var key = string.Format(CERTIFICATE_CERTIFICATEPRODUCTS_BY_KEY, certificateTypeProduct.ProductId);
            _cacheManager.Remove(key);
        }

        public IList<CertificateType> GetCertificatesByIds(List<int> ids)
        {
            if (ids.Count == 0)
                throw new ArgumentNullException("ids");
            var query = _certificateTypeRepository.Table;
            return query.Where(x => ids.Contains(x.CertificateTypeId)).OrderBy(x => x.Order).ToList();
        }

        public IList<CertificateTypeProduct> GetCertificateTypeProductsByStoreCertificateId(int storeCertificateId)
        {
            if (storeCertificateId == 0)
                throw new ArgumentNullException("storeMainPartyId");
            var query = _certificateTypeProductRepository.Table;
            return query.Where(x => x.StoreCertificateId == storeCertificateId).ToList();
        }

        public IList<CertificateTypeProduct> GetCertificateTypeProductsByCerticateTypeId(int certificateTypeId)
        {
            var query = _certificateTypeProductRepository.Table;
            return query.Where(x => x.CertificateTypeId == certificateTypeId).ToList();
        }
        #endregion
    }
}
