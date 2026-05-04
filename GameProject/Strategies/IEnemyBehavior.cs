using GameProject.Entities;

namespace GameProject.Strategies;

public interface IEnemyBehavior
{
    void Execute(Enemy enemy, Player player);
    string GetDescription();
}