namespace GameProject.Entities;

public abstract class Enemy
{
    public string Name { get; protected set; }
    public int Health { get; protected set; }
    public int MaxHealth { get; protected set; }
    public int Damage { get; protected set; }
    public int ExperienceReward { get; protected set; }
    
    protected Enemy(string name, int maxHealth, int damage, int expReward)
    {
        Name = name;
        MaxHealth = maxHealth;
        Health = maxHealth;
        Damage = damage;
        ExperienceReward = expReward;
    }
    
    public abstract void Attack(Player target);
    public abstract string GetDescription();
    
    public virtual void TakeDamage(int amount)
    {
        Health -= amount;
        if (Health < 0) Health = 0;
    }
    
    public bool IsAlive() => Health > 0;
}