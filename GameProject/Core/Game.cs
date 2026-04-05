using GameProject.Entities;

namespace GameProject.Core;

public class Game
{
    private bool _isRunning;
    private GameManager _gameManager;
    
    public Game()
    {
        _gameManager = GameManager.Instance;
    }
    
    public void Start()
    {
        Console.WriteLine("=== GAME STARTED ===\n");
        _gameManager.StartNewGame("Hero");
        
        var enemies = new List<Enemy>
        {
            new Enemy("Гоблин", 30, 5, 20),
            new Enemy("Орк", 50, 8, 35)
        };
        _gameManager.StartCombat(enemies);
        
        _isRunning = true;
        Run();
    }
    
    public void Stop()
    {
        _isRunning = false;
        Console.WriteLine("\n=== GAME OVER ===");
    }
    
    private void Run()
    {
        Console.WriteLine("\nИгровой цикл запущен. Нажмите ESC для выхода...\n");
        
        while (_isRunning)
        {
            HandleInput();
            Update();
            Render();
            
            Thread.Sleep(100);
        }
    }
    
    private void HandleInput()
    {
        if (Console.KeyAvailable)
        {
            var key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.Escape)
            {
                Stop();
            }
            else if (key.Key == ConsoleKey.Spacebar)
            {
                Console.WriteLine("Пробел нажат!");
            }
            else if (key.Key == ConsoleKey.Enter)
            {
                Console.WriteLine("Enter нажат!");
            }
        }
    }
    
    private void Update()
    {
        if (_gameManager.IsInCombat && _gameManager.ActiveEnemies.Count > 0)
        {
        }
    }
    
    private void Render()
    {
        Console.Clear();
        
        Console.WriteLine("╔══════════════════════════════════════╗");
        Console.WriteLine("║           TURN-BASED RPG             ║");
        Console.WriteLine("╚══════════════════════════════════════╝\n");
        
        if (_gameManager.CurrentPlayer != null)
        {
            Console.WriteLine($"ИГРОК: {_gameManager.CurrentPlayer.Name}");
            Console.WriteLine($"❤️  Здоровье: {_gameManager.CurrentPlayer.Health}/{_gameManager.CurrentPlayer.MaxHealth}");
            Console.WriteLine($"✨ Опыт: {_gameManager.CurrentPlayer.Experience}/{_gameManager.CurrentPlayer.ExperienceToNextLevel}");
            Console.WriteLine($"⭐ Уровень: {_gameManager.CurrentPlayer.Level}\n");
        }
        
        if (_gameManager.IsInCombat && _gameManager.ActiveEnemies.Count > 0)
        {
            Console.WriteLine("ВРАГИ:");
            foreach (var enemy in _gameManager.ActiveEnemies)
            {
                Console.WriteLine($"  👾 {enemy.Name}: ❤️ {enemy.Health}/{enemy.MaxHealth} (⚔️ {enemy.Damage} урона)");
            }
            Console.WriteLine();
        }
        
        Console.WriteLine("─────────────────────────────────────────");
        Console.WriteLine("⌨️  Управление:");
        Console.WriteLine("  ESC - выход из игры");
        Console.WriteLine("  SPACE - тестовая команда");
        Console.WriteLine("  ENTER - тестовая команда");
        Console.WriteLine("─────────────────────────────────────────");
    }
}