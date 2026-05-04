using GameProject.Entities;
using GameProject.States;
using GameProject.Strategies;

namespace GameProject.Core;

public class GameManager
{
    private const int GameLoopDelayMilliseconds = 50;
    private const int NumberOfTestGhosts = 5;
    
    private static GameManager? _instance;
    private static readonly object _lockObject = new object();
    
    private IGameState? _currentState;
    
    public Player? CurrentPlayer { get; private set; }
    public List<Enemy> ActiveEnemies { get; private set; }
    public bool IsInCombat { get; set; }
    
    private GameManager()
    {
        ActiveEnemies = new List<Enemy>();
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
    
    public void Run()
    {
        ChangeState(new MenuState());
        
        while (true)
        {
            if (Console.KeyAvailable)
            {
                var keyInfo = Console.ReadKey(true);
                var newState = _currentState?.HandleInput(keyInfo.Key);
                if (newState != null)
                {
                    ChangeState(newState);
                }
            }
            
            var updatedState = _currentState?.Update(this);
            if (updatedState != null)
            {
                ChangeState(updatedState);
            }
            
            Thread.Sleep(GameLoopDelayMilliseconds);
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