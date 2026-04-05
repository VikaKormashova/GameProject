namespace GameProject.Entities;

public abstract class Entity
{
    public string Name { get; set; }
    public int Health { get; set; }
    public int MaxHealth { get; set; }
    
    protected Entity(string name, int maxHealth)
    {
        Name = name;
        MaxHealth = maxHealth;
        Health = maxHealth;
    }
    
    public virtual void TakeDamage(int damage)
    {
        Health -= damage;
        if (Health < 0) Health = 0;
    }
    
    public bool IsAlive() => Health > 0;
}