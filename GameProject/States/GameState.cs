using GameProject.Core;
using GameProject.Entities;
using GameProject.UI;
using GameProject.Commands;

namespace GameProject.States;

public class GameState : IGameState
{
    private const int DamageAmount = 10;
    private const int ExperienceAmount = 50;
    private const int AttackDamage = 15;
    
    private ConsoleHUD? _hud;
    private bool _initialized;
    
    public void Enter()
    {
        if (!_initialized)
        {
            var gameManager = GameManager.Instance;
            gameManager.StartNewGame("Hero");
            gameManager.CreateTestEnemies();
            gameManager.SetupDefaultBindings();
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
        
        // Обработка через InputManager (Command pattern)
        if (key == ConsoleKey.D || key == ConsoleKey.E)
        {
            gameManager.InputManager.HandleInput(key);
            _hud?.Render();
            
            if (gameManager.CurrentPlayer != null && !gameManager.CurrentPlayer.IsAlive())
            {
                return new GameOverState();
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
        Console.WriteLine("  WASD - перемещение");
        Console.WriteLine("  SPACE - атака");
        Console.WriteLine("  H - использовать зелье");
        Console.WriteLine("  Z - отменить последнее действие");
        Console.WriteLine("  ESC - пауза");
        Console.WriteLine("─────────────────────────────────────────");
    }
}