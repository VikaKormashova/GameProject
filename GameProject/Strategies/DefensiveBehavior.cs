using GameProject.Entities;

namespace GameProject.Strategies;

public class DefensiveBehavior : IEnemyBehavior
{
    private const int BlockChancePercent = 50;
    private Random _random = new Random();
    
    public void Execute(Enemy enemy, Player player)
    {
        Console.WriteLine($"{enemy.Name} занимает защитную стойку!");
        
        Console.WriteLine($"{enemy.Name} блокирует часть урона!");
        enemy.Attack(player);
        
        if (_random.Next(100) < BlockChancePercent)
        {
            Console.WriteLine($"Контратака {enemy.Name}!");
            enemy.Attack(player);
        }
    }
    
    public string GetDescription()
    {
        return "Защитный (блокирует и контратакует)";
    }
}