namespace MakinaTurkiye.Caching
{
    public interface ICachingOperation
    {
        bool SetOperationEnabled { get; set; }

        bool GetOperationEnabled { get; set; }

        bool RemoveOperationEnabled { get; set; }

        bool AllOperationEnabled { get; set; }
    }
}
