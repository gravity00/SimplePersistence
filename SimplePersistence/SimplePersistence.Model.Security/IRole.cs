namespace SimplePersistence.Model.Security
{
    public interface IRole : IEntity<string>
    {
        string Name { get; set; }

        string Description { get; set; }
    }
}