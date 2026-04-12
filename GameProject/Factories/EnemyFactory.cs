using GameProject.Entities;

namespace GameProject.Factories;

public abstract class EnemyFactory
{
    public abstract Enemy CreateEnemy();
    
    public Enemy SpawnEnemy()
    {
        var enemy = CreateEnemy();
        Console.WriteLine($"Появился враг: {enemy.Name}! {enemy.GetDescription()}");
        return enemy;
    }
}