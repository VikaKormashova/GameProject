using GameProject.Core;
using GameProject.Entities;
using GameProject.UI;

namespace GameProject.States;

public class GameState : IGameState
{
    private ConsoleHUD? _hud;
    private bool _initialized;
    
    public void Enter()
    {
        if (!_initialized)
        {
            var gameManager = GameManager.Instance;
            gameManager.StartNewGame("Hero");
            gameManager.CreateTestEnemies();
            _hud = new ConsoleHUD(gameManager.CurrentPlayer!);
            _initialized = true;
        }
        
        Console.Clear();
        Console.WriteLine("╔══════════════════════════════════════╗");
        Console.WriteLine("║           TURN-BASED RPG             ║");
        Console.WriteLine("║             ИГРА ЗАПУЩЕНА             ║");
        Console.WriteLine("╚══════════════════════════════════════╝\n");
        
        _hud?.Render();
        ShowControls();
    }
    
    public void Exit()
    {
        Console.WriteLine("Выход из игры...");
    }
    
    public IGameState? HandleInput(ConsoleKey key)
    {
        var gameManager = GameManager.Instance;
        
        if (key == ConsoleKey.Escape)
        {
            return new PauseState();
        }
        
        if (key == ConsoleKey.D && gameManager.CurrentPlayer != null)
        {
            gameManager.CurrentPlayer.TakeDamage(10);
            _hud?.Render();
            
            if (!gameManager.CurrentPlayer.IsAlive())
            {
                return new GameOverState();
            }
        }
        
        if (key == ConsoleKey.E && gameManager.CurrentPlayer != null)
        {
            gameManager.CurrentPlayer.GainExperience(50);
            _hud?.Render();
        }
        
        if (key == ConsoleKey.Spacebar && gameManager.ActiveEnemies.Count > 0 && gameManager.CurrentPlayer != null && gameManager.CurrentPlayer.IsAlive())
        {
            var target = gameManager.ActiveEnemies[0];
            target.TakeDamage(15);
            
            if (!target.IsAlive())
            {
                gameManager.ActiveEnemies.RemoveAt(0);
                gameManager.CurrentPlayer.GainExperience(target.ExperienceReward);
                _hud?.Render();
                
                if (gameManager.ActiveEnemies.Count == 0)
                {
                    Console.WriteLine("\n✨ ПОБЕДА! Все враги повержены! ✨\n");
                }
            }
        }
        
        return null;
    }
    
    public IGameState? Update(GameManager gameManager)
    {
        return null;
    }
    
    public string GetStateName()
    {
        return "Игровой процесс";
    }
    
    private void ShowControls()
    {
        Console.WriteLine("─────────────────────────────────────────");
        Console.WriteLine("⌨️  Управление:");
        Console.WriteLine("  D - получить урон");
        Console.WriteLine("  E - получить опыт");
        Console.WriteLine("  SPACE - атаковать врага");
        Console.WriteLine("  ESC - пауза");
        Console.WriteLine("─────────────────────────────────────────");
    }
}