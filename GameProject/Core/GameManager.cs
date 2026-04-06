using GameProject.Entities;

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
        Console.WriteLine($"=== ИГРА ЗАПУЩЕНА ===");
        Console.WriteLine($"Сложность: {GameDifficulty}");
        Console.WriteLine($"Размер карты: {MapWidth}x{MapHeight}\n");
        Console.WriteLine("Нажмите ESC для выхода...");
        
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
        Console.WriteLine($"\n=== БОЙ НАЧАЛСЯ! ===\nВрагов: {enemies.Count}");
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