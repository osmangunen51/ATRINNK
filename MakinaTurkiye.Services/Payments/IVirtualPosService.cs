using MakinaTurkiye.Entities.Tables.Payments;

namespace MakinaTurkiye.Services.Payments
{
    public interface IVirtualPosService
    {
        VirtualPos GetVirtualPosByVirtualPosId(int virtualPosId);
    }
}
