using Trinnk.Entities.Tables.Videos;
using System.Data.Entity.ModelConfiguration;

namespace Trinnk.Data.Mapping.Tables.Videos
{
    public class VideoMap : EntityTypeConfiguration<Video>
    {
        public VideoMap()
        {
            this.ToTable("Video");
            this.Ignore(v => v.Id);
            this.HasKey(v => v.VideoId);
            this.Property(v => v.VideoId).HasColumnName("VideoId");

            //this.HasRequired(v => v.Product).WithMany(p => p.Videos).HasForeignKey(v => v.ProductId);

        }
    }
}
