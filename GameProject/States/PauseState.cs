using GameProject.Core;

namespace GameProject.States;

public class PauseState : IGameState
{
    public void Enter()
    {
        Console.Clear();
        Console.WriteLine("╔══════════════════════════════════════╗");
        Console.WriteLine("║                ПАУЗА                 ║");
        Console.WriteLine("╚══════════════════════════════════════╝\n");
        Console.WriteLine("Игра приостановлена.\n");
        Console.WriteLine("Нажмите ESC - продолжить");
        Console.WriteLine("Нажмите M - выйти в главное меню\n");
    }
    
    public void Exit()
    {
        Console.WriteLine("Возврат в игру...");
    }
    
    public IGameState? HandleInput(ConsoleKey key)
    {
        if (key == ConsoleKey.Escape)
        {
            return new GameState();
        }
        if (key == ConsoleKey.M)
        {
            return new MenuState();
        }
        return null;
    }
    
    public IGameState? Update(GameManager gameManager)
    {
        return null;
    }
    
    public string GetStateName()
    {
        return "Пауза";
    }
}