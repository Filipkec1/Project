namespace Project.Core.Models.Contracts
{
    /// <summary>
    /// Defines extension for entity that has name attribute.
    /// </summary>
    public interface IHasName
    {
        string Name { get; set; }
    }
}
