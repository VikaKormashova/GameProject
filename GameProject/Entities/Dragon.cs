namespace GameProject.Entities;

public class Dragon : Enemy
{
    public Dragon() : base("Дракон", 100, 15, 80)
    {
    }
    
    private Dragon(Dragon other) : base(other)
    {
    }
    
    public override void Attack(Player target)
    {
        Console.WriteLine($"{Name} извергает пламя на {target.Name}!");
        target.TakeDamage(Damage);
        Console.WriteLine($"Огонь дракона наносит дополнительный урон!");
        target.TakeDamage(5);
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