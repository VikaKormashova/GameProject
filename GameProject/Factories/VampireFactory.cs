using GameProject.Entities;

namespace GameProject.Factories;

public class VampireFactory : EnemyFactory
{
    public override Enemy CreateEnemy()
    {
        return new Vampire();
    }
}