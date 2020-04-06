using MakinaTurkiye.Entities.Tables.Media;
using System.Collections.Generic;

namespace MakinaTurkiye.Services.Media
{
    public interface IPictureService : ICachingSupported
    {
        Picture GetPictureByPictureId(int pictureId);

        IList<Picture> GetPicturesByProductId(int productId, bool setCache = true);

        Picture GetFirstPictureByProductId(int productId);

        IList<Picture> GetPictureByMainPartyId(int mainPartyId);

        IList<Picture> GetPictureByMainPartyIdWithStoreImageType(int mainPartyId, StoreImageTypeEnum storeImageType);
        IList<Picture> GetPictureByStoreCertificateId(int storeCertificateId);
        IList<Picture> GetPictureByStoreDealerIds(List<int> storeDealerIds);
        void DeletePicture(Picture picture);

        void UpdatePicture(Picture picture);

        void InsertPicture(Picture picture);
        
    }
}
