using GameProject.Entities;

namespace GameProject.Items;

public abstract class Item
{
    public string Name { get; set; }
    public string Description { get; set; }
    
    protected Item(string name, string description)
    {
        Name = name;
        Description = description;
    }
    
    public abstract void Use(Player target);
}