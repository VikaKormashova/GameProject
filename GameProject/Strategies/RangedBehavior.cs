using GameProject.Entities;

namespace GameProject.Strategies;

public class RangedBehavior : IEnemyBehavior
{
    private Random _random = new Random();
    private const int MissChancePercent = 20;
    
    public void Execute(Enemy enemy, Player player)
    {
        Console.WriteLine($"{enemy.Name} стреляет издалека!");
        
        if (_random.Next(100) < MissChancePercent)
        {
            Console.WriteLine($"Промах! {enemy.Name} не попал!");
            return;
        }
        
        enemy.Attack(player);
    }
    
    public string GetDescription()
    {
        return "Дальнобойный (стреляет, иногда промахивается)";
    }
}