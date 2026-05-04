using GameProject.Core;

namespace GameProject.States;

public interface IGameState
{
    void Enter();
    void Exit();
    IGameState? HandleInput(ConsoleKey key);
    IGameState? Update(GameManager gameManager);
    string GetStateName();
}