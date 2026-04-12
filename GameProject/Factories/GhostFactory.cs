using GameProject.Entities;

namespace GameProject.Factories;

public class GhostFactory : EnemyFactory
{
    public override Enemy CreateEnemy()
    {
        return new Ghost();
    }
}