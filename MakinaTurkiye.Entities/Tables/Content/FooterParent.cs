using System.Collections.Generic;

namespace MakinaTurkiye.Entities.Tables.Content
{
    public class FooterParent:BaseEntity
    {
        private ICollection<FooterContent> _footerContents;
        public int FooterParentId { get; set; }
        public string FooterParentName { get; set; }
        public int DisplayOrder { get; set; }

        public int LanguageId { get; set; }

        public virtual ICollection<FooterContent> FooterContents
        {
            get { return _footerContents ?? (_footerContents = new List<FooterContent>()); }
            protected set { _footerContents = value; }
        }
    }
}
