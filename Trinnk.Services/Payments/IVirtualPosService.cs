using Trinnk.Entities.Tables.Payments;

namespace Trinnk.Services.Payments
{
    public interface IVirtualPosService
    {
        VirtualPos GetVirtualPosByVirtualPosId(int virtualPosId);
    }
}
