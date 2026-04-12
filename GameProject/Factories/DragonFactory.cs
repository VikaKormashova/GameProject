using GameProject.Entities;

namespace GameProject.Factories;

public class DragonFactory : EnemyFactory
{
    public override Enemy CreateEnemy()
    {
        return new Dragon();
    }
}