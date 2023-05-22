using System;

namespace Trinnk.Entities.Tables.Content
{
    public class BaseMenuImage : BaseEntity
    {
        public int BaseMenuImageId { get; set; }
        public int BaseMenuId { get; set; }
        public string MenuImagePath { get; set; }
        public string Url { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool Active { get; set; }

        public virtual BaseMenu BaseMenu { get; set; }
    }
}
