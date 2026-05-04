namespace GameProject.Entities;

public class Ghost : Enemy
{
    private const int GhostHealth = 35;
    private const int GhostDamage = 5;
    private const int GhostExpReward = 25;
    private const int DodgeChancePercent = 40;
    private Random _random = new Random();
    
    public Ghost() : base("Призрак", GhostHealth, GhostDamage, GhostExpReward)
    {
    }
    
    public override void Attack(Player target)
    {
        if (_random.Next(100) < DodgeChancePercent)
        {
            Console.WriteLine($"{Name} стал неосязаемым и атакует из тени!");
        }
        else
        {
            Console.WriteLine($"{Name} атакует {target.Name}!");
        }
        target.TakeDamage(Damage);
    }
    
    public override string GetDescription()
    {
        return "Таинственное создание. Может стать неосязаемым.";
    }
    
    public override Enemy Clone()
    {
        return new Ghost(this);
    }
    
    private Ghost(Ghost other) : base(other)
    {
        _random = new Random();
    }
}