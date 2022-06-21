namespace NeoSistem.MakinaTurkiye.Management.Models
{
    public class ConstantModel
    {
        public short ConstantId { get; set; }
        public byte ConstantType { get; set; }
        public string ConstantName { get; set; }
        public string ConstantPropertie { get; set; }
        public string ConstantTitle { get; set; }
        public string ConstantMailContent { get; set; }
        public int Order { get; set; }

        public bool MemberDescriptionIsOpened { get; set; } = false;
    }

}