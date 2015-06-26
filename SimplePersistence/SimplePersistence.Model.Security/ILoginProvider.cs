namespace SimplePersistence.Model.Security
{
    public interface ILoginProvider : IEntity<string>
    {
        string Name { get; set; }

        string Description { get; set; }
    }
}
