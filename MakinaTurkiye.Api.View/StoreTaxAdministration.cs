namespace MakinaTurkiye.Api.View
{
    public class StoreTaxAdministrationItem
    {
        public string Value { get; set; }
        public bool ProfileState { get; set; }
    }

    public class StoreTaxAdministration
    {
        public StoreTaxAdministrationItem Name { get; set; } = new StoreTaxAdministrationItem();
        public StoreTaxAdministrationItem No { get; set; } = new StoreTaxAdministrationItem();
        public StoreTaxAdministrationItem MersisNo { get; set; } = new StoreTaxAdministrationItem();
        public StoreTaxAdministrationItem TicaretSicilNo { get; set; } = new StoreTaxAdministrationItem();
        public int MainPartyId { get; set; } = 0;
    }
}