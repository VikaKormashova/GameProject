namespace GameProject.Entities;

public class Vampire : Enemy
{
    public Vampire() : base("Вампир", 50, 8, 45)
    {
    }
    
    public override void Attack(Player target)
    {
        Console.WriteLine($"{Name} кусает {target.Name}!");
        target.TakeDamage(Damage);
        
        int healAmount = Damage / 2;
        Health += healAmount;
        if (Health > MaxHealth) Health = MaxHealth;
        
        Console.WriteLine($"{Name} восстанавливает {healAmount} здоровья!");
    }
    
    public override string GetDescription()
    {
        return "Ночное создание, восстанавливающее здоровье от атак.";
    }
}