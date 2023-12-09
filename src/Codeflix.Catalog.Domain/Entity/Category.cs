namespace Codeflix.Catalog.Domain.Entity;
public class Category
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public bool isActive { get; private set; }
    public DateTime CreatedAt { get; private set; }

    public Category(string name, string description)
    {
        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        isActive = true;
        CreatedAt = DateTime.Now;
    }
}
        