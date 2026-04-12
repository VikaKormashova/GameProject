using GameProject.Entities;
using GameProject.Factories;
using GameProject.Weapons;
using GameProject.Combat;

namespace GameProject.Core;

public class GameManager
{
    private static GameManager? _instance;
    private static readonly object _lock = new object();
    private bool _isRunning;
    
    public int MapWidth { get; set; }
    public int MapHeight { get; set; }
    public Difficulty GameDifficulty { get; set; }
    
    public Player? CurrentPlayer { get; private set; }
    public List<Enemy> ActiveEnemies { get; private set; }
    public bool IsInCombat { get; set; }
    
    private CombatFacade _combat;
    
    private GameManager()
    {
        ActiveEnemies = new List<Enemy>();
        MapWidth = 20;
        MapHeight = 15;
        GameDifficulty = Difficulty.Normal;
        _combat = new CombatFacade();
    }
    
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new GameManager();
                    }
                }
            }
            return _instance;
        }
    }
    
    public void Run()
    {
        _isRunning = true;
        Console.Clear();
        Console.WriteLine("╔══════════════════════════════════════╗");
        Console.WriteLine("║           TURN-BASED RPG             ║");
        Console.WriteLine("╚══════════════════════════════════════╝\n");
        
        StartNewGame("Hero");
        
        Console.WriteLine("=== ДЕМОНСТРАЦИЯ ФАСАДА ===\n");
        
        var dragon = new Dragon();
        Console.WriteLine($"Создан враг: {dragon.Name} (HP: {dragon.Health})");
        
        _combat.ProcessAttack(CurrentPlayer!, dragon, 15);
        Console.WriteLine($"Здоровье дракона: {dragon.Health}\n");
        
        _combat.ProcessEnemyAttack(dragon, CurrentPlayer!);
        Console.WriteLine($"Здоровье игрока: {CurrentPlayer!.Health}\n");
        
        Console.WriteLine("=== ДЕМОНСТРАЦИЯ ПРОСТОТЫ ФАСАДА ===\n");
        Console.WriteLine("Раньше для атаки нужно было:\n" +
                         "  - Рассчитать крит\n" +
                         "  - Рассчитать урон\n" +
                         "  - Применить броню\n" +
                         "  - Воспроизвести эффекты\n" +
                         "  - Нанести урон\n");
        Console.WriteLine("Теперь всё это в одном методе:\n" +
                         "  _combat.ProcessAttack(attacker, target, damage)\n");
        
        Console.WriteLine("Нажмите ESC для выхода...\n");
        
        while (_isRunning)
        {
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true);
                if (key.Key == ConsoleKey.Escape)
                {
                    _isRunning = false;
                    Console.WriteLine("\n=== ИГРА ЗАВЕРШЕНА ===");
                }
            }
            Thread.Sleep(100);
        }
    }
    
    public void StartNewGame(string playerName)
    {
        CurrentPlayer = new Player(playerName);
        Console.WriteLine($"Добро пожаловать, {playerName}!");
    }
    
    public void StartCombat(List<Enemy> enemies)
    {
        ActiveEnemies = enemies;
        IsInCombat = true;
    }
    
    public void EndCombat()
    {
        int totalExp = ActiveEnemies.Sum(e => e.ExperienceReward);
        if (CurrentPlayer != null)
        {
            CurrentPlayer.GainExperience(totalExp);
        }
        Console.WriteLine($"\nБой окончен! Получено {totalExp} опыта!");
        ActiveEnemies.Clear();
        IsInCombat = false;
    }
}
