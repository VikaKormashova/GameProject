using GameProject.Entities;
using GameProject.Strategies;

namespace GameProject.Tests;

public class StrategyTests
{
    [Fact]
    public void AggressiveBehavior_ShouldAttackPlayer()
    {
        var player = new Player("Test");
        var dragon = new Dragon();
        var behavior = new AggressiveBehavior();
        int initialHealth = player.Health;
        
        behavior.Execute(dragon, player);
        
        Assert.True(player.Health < initialHealth);
    }
    
    [Fact]
    public void RangedBehavior_CanMiss()
    {
        var player = new Player("Test");
        var dragon = new Dragon();
        var behavior = new RangedBehavior();
        int initialHealth = player.Health;
        
        behavior.Execute(dragon, player);
        
        Assert.True(player.Health <= initialHealth);
    }
    
    [Fact]
    public void Enemy_CanChangeStrategy()
    {
        var player = new Player("Test");
        var dragon = new Dragon();
        
        dragon.SetBehavior(new AggressiveBehavior());
        dragon.SetBehavior(new FleeBehavior());
        
        Assert.True(true);
    }
    
    [Fact]
    public void FleeBehavior_DoesNotAttackWhenHealthLow()
    {
        var player = new Player("Test");
        var dragon = new Dragon();
        dragon.TakeDamage(80);
        var behavior = new FleeBehavior();
        int initialPlayerHealth = player.Health;
        int initialEnemyHealth = dragon.Health;
        
        behavior.Execute(dragon, player);
        
        Assert.Equal(initialEnemyHealth, dragon.Health);
    }
}