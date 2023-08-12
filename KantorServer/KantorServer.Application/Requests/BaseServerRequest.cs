namespace KantorServer.Application.Requests
{
    public abstract class BaseServerRequest
    {
        public string SynchronizationKey { get; set; }

        public BaseServerRequest()
        {
            SynchronizationKey = string.Empty;
        }
    }
}
