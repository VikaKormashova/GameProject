using GameProject.Entities;

namespace GameProject.Commands;

public class UsePotionCommand : ICommand
{
    private const int DefaultHealAmount = 30;
    
    private Player _player;
    private int _healAmount;
    private bool _executed;
    
    public UsePotionCommand(Player player, int healAmount = DefaultHealAmount)
    {
        _player = player;
        _healAmount = healAmount;
    }
    
    public void Execute()
    {
        if (!_player.IsAlive())
        {
            Console.WriteLine("Нельзя использовать зелье - персонаж мёртв!");
            return;
        }
        
        _player.Heal(_healAmount);
        _executed = true;
        Console.WriteLine($"{_player.Name} использовал зелье и восстановил {_healAmount} здоровья!");
    }
    
    public void Undo()
    {
        if (!_executed) return;
        
        _player.TakeDamage(_healAmount);
        Console.WriteLine($"Отмена использования зелья. {_player.Name} теряет {_healAmount} здоровья!");
    }
    
    public string GetDescription() => $"Использование зелья (+{_healAmount} HP)";
}