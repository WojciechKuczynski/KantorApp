namespace KantorClient.Model
{
    public abstract class BaseModel
    {
        public long Id { get; set; }
        public DateTime? LastUpdate { get; set; }
    }
}
