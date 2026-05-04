using GameProject.Entities;
using GameProject.Factories;
using GameProject.Weapons;
using GameProject.Combat;
using GameProject.Strategies;
using GameProject.UI;

namespace GameProject.Core;

public class GameManager
{
    private const int DefaultMapWidth = 20;
    private const int DefaultMapHeight = 15;
    private const int DemoDamageAmount = 40;
    private const int GameLoopDelayMilliseconds = 100;
    private const int DefaultSwordDamage = 10;
    private const int FireBonus = 5;
    private const int IceBonus = 3;
    private const double CriticalMultiplier = 2.0;
    
    private static GameManager? _instance;
    private static readonly object _lockObject = new object();
    private bool _isRunning;
    
    public int MapWidth { get; set; }
    public int MapHeight { get; set; }
    public Difficulty GameDifficulty { get; set; }
    
    public Player? CurrentPlayer { get; private set; }
    public List<Enemy> ActiveEnemies { get; private set; }
    public bool IsInCombat { get; set; }
    
    private CombatFacade _combat;
    private ConsoleHUD? _hud;
    
    private GameManager()
    {
        ActiveEnemies = new List<Enemy>();
        MapWidth = DefaultMapWidth;
        MapHeight = DefaultMapHeight;
        GameDifficulty = Difficulty.Normal;
        _combat = new CombatFacade();
    }
    
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_lockObject)
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
        ShowTitle();
        StartNewGame("Hero");
        
        _hud = new ConsoleHUD(CurrentPlayer!);
        
        DemonstrateFactoryMethod();
        DemonstratePrototype();
        DemonstrateFacade();
        DemonstrateStrategyPattern();
        
        Console.WriteLine("\n=== 5. НАБЛЮДАТЕЛЬ (OBSERVER) ===\n");
        Console.WriteLine("UI автоматически обновляется через события!\n");
        Console.WriteLine("Нажмите D - получить урон (UI обновится автоматически)");
        Console.WriteLine("Нажмите E - получить опыт (UI обновится автоматически)");
        Console.WriteLine("Нажмите ESC - выход\n");
        
        StartGameLoop();
    }
    
    private void ShowTitle()
    {
        Console.Clear();
        Console.WriteLine("╔══════════════════════════════════════╗");
        Console.WriteLine("║           TURN-BASED RPG             ║");
        Console.WriteLine("╚══════════════════════════════════════╝\n");
    }
    
    private void DemonstrateFactoryMethod()
    {
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
    }
    
    private void DemonstratePrototype()
    {
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
        clonedDragon1.TakeDamage(DemoDamageAmount);
        Console.WriteLine($"Клон 1 после урона: {clonedDragon1.Health} HP");
        Console.WriteLine($"Оригинал (прототип): {dragonPrototype.Health} HP (не изменился!)\n");
    }
    
    private void DemonstrateFacade()
    {
        Console.WriteLine("=== 3. ДЕКОРАТОР (УЛУЧШЕНИЕ ОРУЖИЯ) ===\n");
        
        IWeapon sword = new Sword(DefaultSwordDamage);
        Console.WriteLine($"Базовое оружие: {sword.GetDescription()} => {sword.GetDamage()} урона");
        
        IWeapon fireSword = new FireDamageDecorator(sword, FireBonus);
        Console.WriteLine($"Огненный меч: {fireSword.GetDescription()} => {fireSword.GetDamage()} урона");
        
        IWeapon iceFireSword = new IceDamageDecorator(fireSword, IceBonus);
        Console.WriteLine($"Ледяной огненный меч: {iceFireSword.GetDescription()} => {iceFireSword.GetDamage()} урона");
        
        IWeapon critIceFireSword = new CriticalStrikeDecorator(iceFireSword, CriticalMultiplier);
        Console.WriteLine($"Критический ледяной огненный меч: {critIceFireSword.GetDescription()} => {critIceFireSword.GetDamage()} урона");
        
        Console.WriteLine("\n=== ДОКАЗАТЕЛЬСТВО ПОРЯДКА ===");
        IWeapon sword2 = new Sword(DefaultSwordDamage);
        IWeapon critFirst = new CriticalStrikeDecorator(sword2, CriticalMultiplier);
        IWeapon fireThenCrit = new FireDamageDecorator(critFirst, FireBonus);
        Console.WriteLine($"Сначала крит, потом огонь: {fireThenCrit.GetDescription()} => {fireThenCrit.GetDamage()} урона\n");
        
        Console.WriteLine($"Всего врагов: {ActiveEnemies.Count}\n");
    }
    
    private void DemonstrateStrategyPattern()
    {
        Console.WriteLine("=== 4. СТРАТЕГИЯ (ПОВЕДЕНИЕ ВРАГОВ) ===\n");
        
        var dragon = new Dragon();
        Console.WriteLine($"Создан враг для демонстрации стратегий: {dragon.Name}\n");
        
        Console.WriteLine("--- Агрессивная стратегия ---");
        dragon.SetBehavior(new AggressiveBehavior());
        dragon.Act(CurrentPlayer!);
        
        Console.WriteLine("\n--- Дальнобойная стратегия ---");
        dragon.SetBehavior(new RangedBehavior());
        dragon.Act(CurrentPlayer!);
        
        Console.WriteLine("\n--- Трусливая стратегия (при низком здоровье) ---");
        dragon.TakeDamage(70);
        Console.WriteLine($"Здоровье дракона: {dragon.Health}/{dragon.MaxHealth}");
        dragon.SetBehavior(new FleeBehavior());
        dragon.Act(CurrentPlayer!);
        
        Console.WriteLine("\n--- Защитная стратегия ---");
        dragon.SetBehavior(new DefensiveBehavior());
        dragon.Act(CurrentPlayer!);
        
        Console.WriteLine("\n=== СТРАТЕГИЯ ЗАВЕРШЕНА ===\n");
    }
    
    private void StartGameLoop()
    {
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
                else if (key.Key == ConsoleKey.D && CurrentPlayer != null)
                {
                    CurrentPlayer.TakeDamage(10);  // UI обновится автоматически через событие!
                    
                    if (!CurrentPlayer.IsAlive())
                    {
                        _isRunning = false;
                    }
                }
                else if (key.Key == ConsoleKey.E && CurrentPlayer != null)
                {
                    CurrentPlayer.GainExperience(50);  // UI обновится автоматически через событие!
                }
            }
            Thread.Sleep(GameLoopDelayMilliseconds);
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