namespace MakinaTurkiye.Services
{
    public interface ICachingSupported
    {
        bool CachingGetOrSetOperationEnabled { get; set; }

        bool CachingRemoveOperationEnabled { get; set; }
    }
}
