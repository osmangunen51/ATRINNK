using MakinaTurkiye.Entities.Tables.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakinaTurkiye.Services.Catalog
{
    public interface ICertificateTypeService : ICachingSupported
    {
        void InsertCertificateType(CertificateType certficateType);
        void UpdateCertificateType(CertificateType certificateType);
        void DeleteCertificateType(CertificateType certificateType);
        IList<CertificateType> GetCertificateTypes();
        CertificateType GetCertificateTypeByCertificateTypeId(int certificateTypeId);
        IList<CertificateType> GetCertificatesByIds(List<int> ids);

        void InsertCertificateTypeProduct(CertificateTypeProduct certificateTypeProduct);
        void DeleteCertificateTypeProduct(CertificateTypeProduct certificateTypeProduct);
        void UpdateCertificateTypeProduct(CertificateTypeProduct certificateTypeProduct);
        IList<CertificateTypeProduct> GetCertificateTypeProductsByProductId(int productId, bool setCache = true);
        CertificateTypeProduct GetCertificateTypeProductsByStoreCertificateId(int storeCertficiateId);


    }
}
