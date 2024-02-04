namespace Aurora.Framework.Data.Entities
{
    public class Instance
    {
        public int Id { get; }
        public Guid Token { get; }
        public string Hostname { get; } = string.Empty;
        public DateTime LastRefreshTime { get; private set; } = DateTime.UtcNow;

        public Instance(int id, string hostname)
        {
            Id = id;
            Token = Guid.NewGuid();
            Hostname = hostname;
        }

        public void Refresh()
        {
            LastRefreshTime = DateTime.UtcNow;
        }
    }
}
