namespace KantorClient.Model
{
    public class UserSession : BaseModel
    {
        // Id z bazy servera
        public long UserId { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? LastAction { get; set; }
        public string SynchronizationKey { get; set; }
        public decimal Cash { get; set; }
        public string UserPermission { get; set; }
        public long KantorId { get; set; }
    }
}
