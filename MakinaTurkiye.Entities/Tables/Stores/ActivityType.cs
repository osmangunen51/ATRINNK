namespace MakinaTurkiye.Entities.Tables.Stores
{
    public class ActivityType : BaseEntity
    {
        //private ICollection<StoreActivityType> _storeActivityTypes;

        //public ICollection<StoreActivityType> StoreActivityTypes
        //{
        //    get { return _storeActivityTypes ?? (_storeActivityTypes = new List<StoreActivityType>()); }
        //    protected set { _storeActivityTypes = value; }
        //}

        public byte ActivityTypeId { get; set; }
        public string ActivityName { get; set; }
        public byte? Order { get; set; }


    }
}
