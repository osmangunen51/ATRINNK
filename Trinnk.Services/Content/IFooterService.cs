using Trinnk.Entities.Tables.Content;
using System.Collections.Generic;

namespace Trinnk.Services.Content
{
    public interface IFooterService
    {
        IList<FooterContent> GetAllFooterContent();
        IList<FooterParent> GetAllFooterParent();
        FooterParent GetFooterParentByFooterParentId(int footerParentId);
        FooterContent GetFooterContentByFooterContentId(int footerContentId);
        IList<FooterContent> GetFooterContentsByFooterParentId(int footerParentId);
        void InsertFooterParent(FooterParent footerParent);
        void InsertFooterContent(FooterContent footerContent);
        void UpdateFooterContent(FooterContent footerContent);
        void UpdateFooterParent(FooterParent footerParent);
        void DeleteFooterParent(FooterParent footerParent);
        void DeleteFooterContent(FooterContent footerContent);
    }
}
