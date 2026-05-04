using GameProject.Entities;

namespace GameProject.Tests;

public class ObserverTests
{
    private class TestObserver
    {
        public int HealthValue { get; private set; }
        public int ExpValue { get; private set; }
        public int LevelValue { get; private set; }
        public bool DeathNotified { get; private set; }
        
        public TestObserver(Player player)
        {
            player.OnHealthChanged += (health, max) => HealthValue = health;
            player.OnExperienceChanged += (exp, max) => ExpValue = exp;
            player.OnLevelUp += (level) => LevelValue = level;
            player.OnPlayerDeath += () => DeathNotified = true;
        }
    }
    
    [Fact]
    public void Player_HealthChanged_EventShouldFire()
    {
        var player = new Player("Test");
        var observer = new TestObserver(player);
        
        player.TakeDamage(30);
        
        Assert.Equal(70, observer.HealthValue);
    }
    
    [Fact]
    public void Player_ExperienceChanged_EventShouldFire()
    {
        var player = new Player("Test");
        var observer = new TestObserver(player);
        
        player.GainExperience(50);
        
        Assert.Equal(50, observer.ExpValue);
    }
    
    [Fact]
    public void Player_LevelUp_EventShouldFire()
    {
        var player = new Player("Test");
        var observer = new TestObserver(player);
        
        player.GainExperience(120);
        
        Assert.Equal(2, observer.LevelValue);
    }
    
    [Fact]
    public void Player_Death_EventShouldFire()
    {
        var player = new Player("Test");
        var observer = new TestObserver(player);
        
        player.TakeDamage(200);
        
        Assert.True(observer.DeathNotified);
    }
}