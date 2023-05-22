namespace Trinnk.Entities.Tables.Stores
{
    public class StoreActivityType : BaseEntity
    {

        public int StoreActivityTypeId { get; set; }
        public byte ActivityTypeId { get; set; }
        public int StoreId { get; set; }

        public virtual ActivityType ActivityType { get; set; }


    }
}
