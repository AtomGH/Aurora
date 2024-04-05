namespace Aurora.Library.Framework
{
    /// <summary>
    /// 
    /// </summary>
    public class InstanceContext
    {
        public int Id { get; private set; }
        public Guid Token { get; private set; }
        public string Hostname { get; private set; } = string.Empty;

        public void Initialize(InstanceInformation instance)
        {
            Id = instance.Id;
            Token = instance.Token;
            Hostname = "";
        }
    }
}
