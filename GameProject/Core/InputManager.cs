using GameProject.Commands;

namespace GameProject.Core;

public class InputManager
{
    private Dictionary<ConsoleKey, ICommand> _keyBindings;
    private Stack<ICommand> _commandHistory;
    
    public InputManager()
    {
        _keyBindings = new Dictionary<ConsoleKey, ICommand>();
        _commandHistory = new Stack<ICommand>();
    }
    
    public void BindKey(ConsoleKey key, ICommand command)
    {
        _keyBindings[key] = command;
        Console.WriteLine($"Клавиша {key} привязана к команде: {command.GetDescription()}");
    }
    
    public void UnbindKey(ConsoleKey key)
    {
        if (_keyBindings.ContainsKey(key))
        {
            _keyBindings.Remove(key);
            Console.WriteLine($"Клавиша {key} отвязана");
        }
    }
    
    public void HandleInput(ConsoleKey key)
    {
        if (_keyBindings.TryGetValue(key, out ICommand? command))
        {
            command.Execute();
            _commandHistory.Push(command);
        }
    }
    
    public void UndoLastCommand()
    {
        if (_commandHistory.Count > 0)
        {
            var command = _commandHistory.Pop();
            command.Undo();
            Console.WriteLine($"Отменена команда: {command.GetDescription()}");
        }
        else
        {
            Console.WriteLine("Нет действий для отмены!");
        }
    }
    
    public void ShowBindings()
    {
        Console.WriteLine("\n=== ТЕКУЩИЕ НАСТРОЙКИ УПРАВЛЕНИЯ ===");
        foreach (var binding in _keyBindings)
        {
            Console.WriteLine($"  {binding.Key} -> {binding.Value.GetDescription()}");
        }
        Console.WriteLine("=====================================\n");
    }
}