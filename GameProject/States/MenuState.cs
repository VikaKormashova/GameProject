using GameProject.Core;

namespace GameProject.States;

public class MenuState : IGameState
{
    public void Enter()
    {
        Console.Clear();
        Console.WriteLine("╔══════════════════════════════════════╗");
        Console.WriteLine("║           TURN-BASED RPG             ║");
        Console.WriteLine("║         ГЛАВНОЕ МЕНЮ                 ║");
        Console.WriteLine("╚══════════════════════════════════════╝\n");
        Console.WriteLine("Нажмите ENTER - начать игру");
        Console.WriteLine("Нажмите ESC - выход\n");
    }
    
    public void Exit()
    {
        Console.WriteLine("Выход из меню...");
    }
    
    public IGameState? HandleInput(ConsoleKey key)
    {
        if (key == ConsoleKey.Enter)
        {
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
        return "Главное меню";
    }
}