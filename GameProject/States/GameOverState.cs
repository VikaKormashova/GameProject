using GameProject.Core;

namespace GameProject.States;

public class GameOverState : IGameState
{
    public void Enter()
    {
        Console.Clear();
        Console.WriteLine("╔══════════════════════════════════════╗");
        Console.WriteLine("║              GAME OVER               ║");
        Console.WriteLine("║            ВЫ ПОВЕРЖЕНЫ!             ║");
        Console.WriteLine("╚══════════════════════════════════════╝\n");
        Console.WriteLine("Нажмите R - начать заново");
        Console.WriteLine("Нажмите ESC - выйти\n");
    }
    
    public void Exit()
    {
        Console.WriteLine("Выход из экрана Game Over...");
    }
    
    public IGameState? HandleInput(ConsoleKey key)
    {
        if (key == ConsoleKey.R)
        {
            var gameManager = GameManager.Instance;
            gameManager.ActiveEnemies.Clear();
            return new GameState();
        }
        if (key == ConsoleKey.Escape)
        {
            Environment.Exit(0);
        }
        return null;
    }
    
    public IGameState? Update(GameManager gameManager)
    {
        return null;
    }
    
    public string GetStateName()
    {
        return "Конец игры";
    }
}