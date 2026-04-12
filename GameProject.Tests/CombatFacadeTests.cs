using GameProject.Entities;
using GameProject.Combat;

namespace GameProject.Tests;

public class CombatFacadeTests
{
    [Fact]
    public void ProcessAttack_ShouldReduceEnemyHealth()
    {
        // Arrange
        var facade = new CombatFacade();
        var player = new Player("Test");
        var dragon = new Dragon();
        int initialHealth = dragon.Health;
        
        // Act
        facade.ProcessAttack(player, dragon, 15);
        
        // Assert
        Assert.True(dragon.Health < initialHealth);
    }
    
    [Fact]
    public void ProcessEnemyAttack_ShouldReducePlayerHealth()
    {
        // Arrange
        var facade = new CombatFacade();
        var player = new Player("Test");
        var dragon = new Dragon();
        int initialHealth = player.Health;
        
        // Act
        facade.ProcessEnemyAttack(dragon, player);
        
        // Assert
        Assert.True(player.Health < initialHealth);
    }
    
    [Fact]
    public void MultipleAttacks_ShouldStackDamage()
    {
        // Arrange
        var facade = new CombatFacade();
        var player = new Player("Test");
        var dragon = new Dragon();
        int initialHealth = dragon.Health;
        
        // Act
        facade.ProcessAttack(player, dragon, 10);
        int afterFirst = dragon.Health;
        facade.ProcessAttack(player, dragon, 10);
        int afterSecond = dragon.Health;
        
        // Assert
        Assert.True(afterFirst < initialHealth);
        Assert.True(afterSecond < afterFirst);
    }
}