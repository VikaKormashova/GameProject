using GameProject.Entities;

namespace GameProject.Entities;

public class Enemy : Entity
{
    public int Damage { get; set; }
    public int ExperienceReward { get; set; }
    
    public Enemy(string name, int health, int damage, int expReward) 
        : base(name, health)
    {
        Damage = damage;
        ExperienceReward = expReward;
    }
    
    public void Attack(Player target)
    {
        Console.WriteLine($"{Name} атакует {target.Name} и наносит {Damage} урона!");
        target.TakeDamage(Damage);
    }
}