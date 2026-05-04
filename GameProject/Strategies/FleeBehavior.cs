using GameProject.Entities;

namespace GameProject.Strategies;

public class FleeBehavior : IEnemyBehavior
{
    private const int HealthThresholdPercent = 30;
    
    public void Execute(Enemy enemy, Player player)
    {
        int healthPercent = (enemy.Health * 100) / enemy.MaxHealth;
        
        if (healthPercent < HealthThresholdPercent)
        {
            Console.WriteLine($"{enemy.Name} в панике убегает! (Здоровье: {enemy.Health}/{enemy.MaxHealth})");
            Console.WriteLine($"{enemy.Name} скрывается в темноте...");
        }
        else
        {
            Console.WriteLine($"{enemy.Name} нервничает, но пока не убегает.");
            enemy.Attack(player);
        }
    }
    
    public string GetDescription()
    {
        return "Трусливый (убегает при здоровье < 30%)";
    }
}