namespace GameProject.Entities;

public class Dragon : Enemy
{
    private const int DragonHealth = 100;
    private const int DragonDamage = 15;
    private const int DragonExpReward = 80;
    private const int AdditionalFireDamage = 5;
    
    public Dragon() : base("Дракон", DragonHealth, DragonDamage, DragonExpReward)
    {
    }
    
    public Dragon(Dragon other) : base(other)
    {
    }
    
    public override void Attack(Player target)
    {
        Console.WriteLine($"{Name} извергает пламя на {target.Name}!");
        target.TakeDamage(Damage);
        Console.WriteLine($"Огонь дракона наносит дополнительный урон!");
        target.TakeDamage(AdditionalFireDamage);
    }
    
    public override string GetDescription()
    {
        return "Могучий дракон. Очень опасен!";
    }
    
    public override Enemy Clone()
    {
        return new Dragon(this);
    }
}