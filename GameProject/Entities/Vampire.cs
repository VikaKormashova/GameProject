namespace GameProject.Entities;

public class Vampire : Enemy
{
    private const int VampireHealth = 50;
    private const int VampireDamage = 8;
    private const int VampireExpReward = 45;
    private const int LifeStealDivisor = 2;
    
    public Vampire() : base("Вампир", VampireHealth, VampireDamage, VampireExpReward)
    {
    }
    
    public Vampire(Vampire other) : base(other)
    {
    }
    
    public override void Attack(Player target)
    {
        Console.WriteLine($"{Name} кусает {target.Name}!");
        target.TakeDamage(Damage);
        
        int healAmount = Damage / LifeStealDivisor;
        Health += healAmount;
        if (Health > MaxHealth) Health = MaxHealth;
        
        Console.WriteLine($"{Name} восстанавливает {healAmount} здоровья!");
    }
    
    public override string GetDescription()
    {
        return "Ночное создание, восстанавливающее здоровье от атак.";
    }
    
    public override Enemy Clone()
    {
        return new Vampire(this);
    }
}