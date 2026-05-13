using GameProject.Entities;
using GameProject.States;
using GameProject.Strategies;
using GameProject.Commands;

namespace GameProject.Core;

public class GameManager
{
    private const int GameLoopDelayMilliseconds = 50;
    private const int NumberOfTestGhosts = 5;
    
    private static GameManager? _instance;
    private static readonly object _lockObject = new object();
    
    private IGameState? _currentState;
    private InputManager _inputManager;
    
    public Player? CurrentPlayer { get; private set; }
    public List<Enemy> ActiveEnemies { get; private set; }
    public bool IsInCombat { get; set; }
    public InputManager InputManager => _inputManager;
    
    private GameManager()
    {
        ActiveEnemies = new List<Enemy>();
        _inputManager = new InputManager();
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
    
    public void ChangeState(IGameState newState)
    {
        _currentState?.Exit();
        _currentState = newState;
        _currentState.Enter();
    }
    
    public void SetupDefaultBindings()
    {
        if (CurrentPlayer == null) return;
        
        _inputManager.BindKey(ConsoleKey.W, new MoveCommand(CurrentPlayer, 0, -1));
        _inputManager.BindKey(ConsoleKey.S, new MoveCommand(CurrentPlayer, 0, 1));
        _inputManager.BindKey(ConsoleKey.A, new MoveCommand(CurrentPlayer, -1, 0));
        _inputManager.BindKey(ConsoleKey.D, new MoveCommand(CurrentPlayer, 1, 0));
        
        if (ActiveEnemies.Count > 0)
        {
            _inputManager.BindKey(ConsoleKey.Spacebar, new AttackCommand(CurrentPlayer, ActiveEnemies[0]));
        }
        
        _inputManager.BindKey(ConsoleKey.H, new UsePotionCommand(CurrentPlayer, 30));
        _inputManager.BindKey(ConsoleKey.Z, new NullCommand());
    }
    
    public void RemapKey(ConsoleKey oldKey, ConsoleKey newKey)
    {
        var bindings = _inputManager.GetType()
            .GetField("_keyBindings", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            ?.GetValue(_inputManager) as Dictionary<ConsoleKey, ICommand>;
        
        if (bindings != null && bindings.TryGetValue(oldKey, out ICommand? command))
        {
            _inputManager.UnbindKey(oldKey);
            _inputManager.BindKey(newKey, command);
            Console.WriteLine($"\n🔧 Переназначено: {oldKey} → {newKey}\n");
        }
    }
    
    public void Run()
    {
        ChangeState(new MenuState());
        
        while (true)
        {
            try
            {
                if (Console.KeyAvailable)
                {
                    var keyInfo = Console.ReadKey(true);
                    
                    if (keyInfo.Key == ConsoleKey.Escape)
                    {
                        var newState = _currentState?.HandleInput(keyInfo.Key);
                        if (newState != null) ChangeState(newState);
                    }
                    else if (_currentState is GameState)
                    {
                        _inputManager.HandleInput(keyInfo.Key);
                    }
                    else
                    {
                        var newState = _currentState?.HandleInput(keyInfo.Key);
                        if (newState != null) ChangeState(newState);
                    }
                }
                
                var updatedState = _currentState?.Update(this);
                if (updatedState != null)
                {
                    ChangeState(updatedState);
                }
                
                Thread.Sleep(GameLoopDelayMilliseconds);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n⚠️ Ошибка: {ex.Message}");
                Console.WriteLine("Нажмите любую клавишу для продолжения...");
                Console.ReadKey(true);
            }
        }
    }
    
    public void StartNewGame(string playerName)
    {
        CurrentPlayer = new Player(playerName);
        ActiveEnemies.Clear();
        Console.WriteLine($"Добро пожаловать, {playerName}!");
    }
    
    public void CreateTestEnemies()
    {
        var dragon = new Dragon();
        dragon.SetBehavior(new AggressiveBehavior());
        ActiveEnemies.Add(dragon);
        
        for (int i = 0; i < NumberOfTestGhosts; i++)
        {
            var ghost = new Ghost();
            ghost.SetBehavior(new AggressiveBehavior());
            ActiveEnemies.Add(ghost);
        }
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