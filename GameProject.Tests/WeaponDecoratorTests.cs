using GameProject.Weapons;

namespace GameProject.Tests;

public class WeaponDecoratorTests
{
    [Fact]
    public void Sword_BaseDamage_ShouldBe10()
    {
        // Arrange
        var sword = new Sword(10);
        
        // Act
        int damage = sword.GetDamage();
        
        // Assert
        Assert.Equal(10, damage);
    }
    
    [Fact]
    public void FireDamageDecorator_ShouldAddBonus()
    {
        // Arrange
        IWeapon sword = new Sword(10);
        IWeapon fireSword = new FireDamageDecorator(sword, 5);
        
        // Act
        int damage = fireSword.GetDamage();
        
        // Assert
        Assert.Equal(15, damage);
    }
    
    [Fact]
    public void IceDamageDecorator_ShouldAddBonus()
    {
        // Arrange
        IWeapon sword = new Sword(10);
        IWeapon iceSword = new IceDamageDecorator(sword, 3);
        
        // Act
        int damage = iceSword.GetDamage();
        
        // Assert
        Assert.Equal(13, damage);
    }
    
    [Fact]
    public void CriticalStrikeDecorator_ShouldMultiplyDamage()
    {
        // Arrange
        IWeapon sword = new Sword(10);
        IWeapon critSword = new CriticalStrikeDecorator(sword, 2.0);
        
        // Act
        int damage = critSword.GetDamage();
        
        // Assert
        Assert.Equal(20, damage);
    }
    
    [Fact]
    public void MultipleDecorators_ShouldCombineCorrectly()
    {
        // Arrange
        IWeapon sword = new Sword(10);
        IWeapon fireSword = new FireDamageDecorator(sword, 5);
        IWeapon iceFireSword = new IceDamageDecorator(fireSword, 3);
        
        // Act
        int damage = iceFireSword.GetDamage();
        
        // Assert: 10 + 5 + 3 = 18
        Assert.Equal(18, damage);
    }
    
    [Fact]
    public void DecoratorOrder_ShouldMatter()
    {
        // Arrange: сначала крит (x2), потом огонь (+5)
        IWeapon sword = new Sword(10);
        IWeapon critFirst = new CriticalStrikeDecorator(sword, 2.0);
        IWeapon fireThenCrit = new FireDamageDecorator(critFirst, 5);
        
        // Act
        int damage = fireThenCrit.GetDamage();
        
        // Assert: (10 * 2) + 5 = 25
        Assert.Equal(25, damage);
    }
    
    [Fact]
    public void Description_ShouldReflectAllModifiers()
    {
        // Arrange
        IWeapon sword = new Sword(10);
        IWeapon fireSword = new FireDamageDecorator(sword, 5);
        IWeapon iceFireSword = new IceDamageDecorator(fireSword, 3);
        
        // Act
        string description = iceFireSword.GetDescription();
        
        // Assert
        Assert.Contains("Огонь", description);
        Assert.Contains("Лёд", description);
    }
}