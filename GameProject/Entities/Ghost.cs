namespace GameProject.Entities;

public class Ghost : Enemy
{
    private Random _random = new Random();
    
    public Ghost() : base("Призрак", 35, 5, 25)
    {
    }
    
    public override void Attack(Player target)
    {
        if (_random.Next(100) < 40)
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
}