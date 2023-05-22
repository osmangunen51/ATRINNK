using Trinnk.Entities.Tables.Media;
using System.Data.Entity.ModelConfiguration;

namespace Trinnk.Data.Mapping.Tables.Media
{
    public class PictureMap : EntityTypeConfiguration<Picture>
    {
        public PictureMap()
        {
            this.ToTable("Picture");
            this.Ignore(p => p.Id);
            this.HasKey(p => p.PictureId);
        }
    }
}
