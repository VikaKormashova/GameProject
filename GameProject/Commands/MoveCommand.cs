using GameProject.Entities;

namespace GameProject.Commands;

public class MoveCommand : ICommand
{
    private Player _player;
    private int _deltaX;
    private int _deltaY;
    private int _oldX;
    private int _oldY;
    
    public MoveCommand(Player player, int deltaX, int deltaY)
    {
        _player = player;
        _deltaX = deltaX;
        _deltaY = deltaY;
    }
    
    public void Execute()
    {
        _oldX = _player.PositionX;
        _oldY = _player.PositionY;
        _player.Move(_deltaX, _deltaY);
        Console.WriteLine($"Персонаж переместился на ({_deltaX}, {_deltaY})");
    }
    
    public void Undo()
    {
        _player.Move(_oldX - _player.PositionX, _oldY - _player.PositionY);
        Console.WriteLine($"Отмена перемещения. Возврат на ({_oldX}, {_oldY})");
    }
    
    public string GetDescription() => $"Перемещение на ({_deltaX}, {_deltaY})";
}