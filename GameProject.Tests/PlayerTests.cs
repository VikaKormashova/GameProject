using GameProject.Entities;

namespace GameProject.Tests;

public class PlayerTests
{
    [Fact]
    public void TakeDamage_ShouldReduceHealth()
    {
        // Arrange
        var player = new Player("Hero");
        
        // Act
        player.TakeDamage(30);
        
        // Assert
        Assert.Equal(70, player.Health);
    }

    [Fact]
    public void TakeDamage_MoreThanHealth_ShouldNotGoBelowZero()
    {
        // Arrange
        var player = new Player("Hero");
        
        // Act
        player.TakeDamage(200);
        
        // Assert
        Assert.Equal(0, player.Health);
        Assert.False(player.IsAlive());
    }

    [Fact]
    public void GainExperience_ShouldIncreaseExperience()
    {
        // Arrange
        var player = new Player("Hero");
        
        // Act
        player.GainExperience(50);
        
        // Assert
        Assert.Equal(50, player.Experience);
    }

    [Fact]
    public void GainExperience_WhenReachingThreshold_ShouldLevelUp()
    {
        // Arrange
        var player = new Player("Hero");
        
        // Act
        player.GainExperience(100);
        
        // Assert
        Assert.Equal(0, player.Experience);
        Assert.Equal(2, player.Level);
        Assert.Equal(110, player.Health);
    }

    [Fact]
    public void Heal_ShouldRestoreHealth()
    {
        // Arrange
        var player = new Player("Hero");
        player.TakeDamage(50);
        
        // Act
        player.Heal(30);
        
        // Assert
        Assert.Equal(80, player.Health);
    }

    [Fact]
    public void Heal_ShouldNotExceedMaxHealth()
    {
        // Arrange
        var player = new Player("Hero");
        player.TakeDamage(30);
        
        // Act
        player.Heal(50);
        
        // Assert
        Assert.Equal(100, player.Health);
    }

    [Fact]
    public void Heal_WhenDead_ShouldDoNothing()
    {
        // Arrange
        var player = new Player("Hero");
        player.TakeDamage(200);
        
        // Act
        player.Heal(50);
        
        // Assert
        Assert.Equal(0, player.Health);
        Assert.False(player.IsAlive());
    }
}