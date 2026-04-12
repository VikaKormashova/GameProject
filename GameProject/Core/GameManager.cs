using GameProject.Entities;
using GameProject.Factories;

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
    
    private GameManager()
    {
        ActiveEnemies = new List<Enemy>();
        MapWidth = 20;
        MapHeight = 15;
        GameDifficulty = Difficulty.Normal;
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
        
        Console.WriteLine("=== 1. ФАБРИЧНЫЙ МЕТОД ===\n");
        
        List<EnemyFactory> factories = new List<EnemyFactory>
        {
            new GhostFactory(),
            new VampireFactory(),
            new DragonFactory()
        };
        
        foreach (var factory in factories)
        {
            Enemy enemy = factory.SpawnEnemy();
            ActiveEnemies.Add(enemy);
        }
        
        Console.WriteLine($"\nСоздано врагов через фабрики: {ActiveEnemies.Count}\n");
        
        Console.WriteLine("=== 2. ПРОТОТИП (КЛОНИРОВАНИЕ) ===\n");
        
        Dragon dragonPrototype = new Dragon();
        Console.WriteLine($"Создан прототип: {dragonPrototype.Name} (HP: {dragonPrototype.Health})");
        
        Console.WriteLine("Клонируем дракона 2 раза:");
        Enemy clonedDragon1 = dragonPrototype.Clone();
        Enemy clonedDragon2 = dragonPrototype.Clone();
        ActiveEnemies.Add(clonedDragon1);
        ActiveEnemies.Add(clonedDragon2);
        
        Console.WriteLine($"  Клон 1: HP {clonedDragon1.Health}");
        Console.WriteLine($"  Клон 2: HP {clonedDragon2.Health}");
        
        Console.WriteLine("\n=== ДОКАЗАТЕЛЬСТВО ГЛУБОКОГО КОПИРОВАНИЯ ===");
        clonedDragon1.TakeDamage(40);
        Console.WriteLine($"Клон 1 после урона: {clonedDragon1.Health} HP");
        Console.WriteLine($"Оригинал (прототип): {dragonPrototype.Health} HP (не изменился!)");
        
        Console.WriteLine($"\nВсего врагов: {ActiveEnemies.Count}\n");
        Console.WriteLine("=== БОЙ НАЧАЛСЯ! ===\n");
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