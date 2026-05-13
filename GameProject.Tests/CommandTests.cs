using GameProject.Core;
using GameProject.Entities;
using GameProject.Commands;

namespace GameProject.Tests;

public class CommandTests
{
    [Fact]
    public void MoveCommand_ShouldChangePosition()
    {
        var player = new Player("Test");
        var command = new MoveCommand(player, 5, 3);
        
        command.Execute();
        
        Assert.Equal(5, player.PositionX);
        Assert.Equal(3, player.PositionY);
    }
    
    [Fact]
    public void MoveCommand_Undo_ShouldRestorePosition()
    {
        var player = new Player("Test");
        var command = new MoveCommand(player, 5, 3);
        
        command.Execute();
        command.Undo();
        
        Assert.Equal(0, player.PositionX);
        Assert.Equal(0, player.PositionY);
    }
    
    [Fact]
    public void UsePotionCommand_ShouldHealPlayer()
    {
        var player = new Player("Test");
        player.TakeDamage(50);
        var command = new UsePotionCommand(player, 30);
        
        command.Execute();
        
        Assert.Equal(80, player.Health);
    }
    
    [Fact]
    public void UsePotionCommand_Undo_ShouldDamagePlayer()
    {
        var player = new Player("Test");
        player.TakeDamage(50);
        var command = new UsePotionCommand(player, 30);
        
        command.Execute();
        command.Undo();
        
        Assert.Equal(50, player.Health);
    }
}