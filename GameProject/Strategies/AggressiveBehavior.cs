using GameProject.Entities;

namespace GameProject.Strategies;

public class AggressiveBehavior : IEnemyBehavior
{
    public void Execute(Enemy enemy, Player player)
    {
        Console.WriteLine($"{enemy.Name} яростно атакует!");
        enemy.Attack(player);
    }
    
    public string GetDescription()
    {
        return "Агрессивный (атакует в ближнем бою)";
    }
}