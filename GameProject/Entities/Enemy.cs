using GameProject.Strategies;

namespace GameProject.Entities;

public abstract class Enemy
{
    public string Name { get; protected set; }
    public int Health { get; protected set; }
    public int MaxHealth { get; protected set; }
    public int Damage { get; protected set; }
    public int ExperienceReward { get; protected set; }
    
    private IEnemyBehavior? _behavior;
    
    protected Enemy(string name, int maxHealth, int damage, int expReward)
    {
        Name = name;
        MaxHealth = maxHealth;
        Health = maxHealth;
        Damage = damage;
        ExperienceReward = expReward;
    }
    
    protected Enemy(Enemy other)
    {
        Name = other.Name;
        MaxHealth = other.MaxHealth;
        Health = other.Health;
        Damage = other.Damage;
        ExperienceReward = other.ExperienceReward;
    }
    
    public void SetBehavior(IEnemyBehavior behavior)
    {
        _behavior = behavior;
        Console.WriteLine($"{Name} теперь использует стратегию: {behavior.GetDescription()}");
    }
    
    public void Act(Player player)
    {
        if (_behavior == null)
        {
            Console.WriteLine($"{Name} в замешательстве и ничего не делает!");
            return;
        }
        
        _behavior.Execute(this, player);
    }
    
    public abstract void Attack(Player target);
    public abstract string GetDescription();
    
    public virtual void TakeDamage(int amount)
    {
        Health -= amount;
        if (Health < 0) Health = 0;
    }
    
    public bool IsAlive() => Health > 0;
    
    public abstract Enemy Clone();
}