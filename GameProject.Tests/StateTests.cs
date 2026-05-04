using GameProject.Core;
using GameProject.Entities;
using GameProject.States;

namespace GameProject.Tests;

public class StateTests
{
    [Fact]
    public void MenuState_EnterKey_ShouldSwitchToGameState()
    {
        var menuState = new MenuState();
        var result = menuState.HandleInput(ConsoleKey.Enter);
        
        Assert.IsType<GameState>(result);
    }
    
    [Fact]
    public void GameState_EscKey_ShouldSwitchToPauseState()
    {
        var gameState = new GameState();
        var result = gameState.HandleInput(ConsoleKey.Escape);
        
        Assert.IsType<PauseState>(result);
    }
    
    [Fact]
    public void PauseState_EscKey_ShouldSwitchToGameState()
    {
        var pauseState = new PauseState();
        var result = pauseState.HandleInput(ConsoleKey.Escape);
        
        Assert.IsType<GameState>(result);
    }
    
    [Fact]
    public void PauseState_MKey_ShouldSwitchToMenuState()
    {
        var pauseState = new PauseState();
        var result = pauseState.HandleInput(ConsoleKey.M);
        
        Assert.IsType<MenuState>(result);
    }
    
    [Fact]
    public void GameOverState_RKey_ShouldSwitchToGameState()
    {
        var gameOverState = new GameOverState();
        var result = gameOverState.HandleInput(ConsoleKey.R);
        
        Assert.IsType<GameState>(result);
    }
    
    [Fact]
    public void GameState_WhenHealthZero_ShouldSwitchToGameOverState()
    {
        var gameManager = GameManager.Instance;
        gameManager.StartNewGame("Test");
        gameManager.CurrentPlayer!.TakeDamage(100);
        
        var gameState = new GameState();
        
        var result = gameState.HandleInput(ConsoleKey.D);
        
        Assert.IsType<GameOverState>(result);
    }
}