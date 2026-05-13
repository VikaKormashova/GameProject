using GameProject.Entities;

namespace GameProject.Commands;

public class AttackCommand : ICommand
{
    private const int DefaultDamage = 15;
    
    private Player _player;
    private Enemy? _target;
    private int _damageDealt;
    private bool _executed;
    
    public AttackCommand(Player player, Enemy target)
    {
        _player = player;
        _target = target;
    }
    
    public void Execute()
    {
        if (_target == null || !_target.IsAlive())
        {
            Console.WriteLine("Цель уже мертва!");
            return;
        }
        
        _damageDealt = DefaultDamage;
        _target.TakeDamage(_damageDealt);
        _executed = true;
        
        Console.WriteLine($"{_player.Name} атакует {_target.Name} и наносит {_damageDealt} урона!");
        
        if (!_target.IsAlive())
        {
            Console.WriteLine($"{_target.Name} повержен!");
        }
    }
    
    public void Undo()
    {
        if (!_executed) return;
        
        if (_target != null && _target.Health > 0)
        {
            _target.Heal(_damageDealt);
            Console.WriteLine($"Отмена атаки. {_target.Name} восстановил {_damageDealt} здоровья!");
        }
    }
    
    public string GetDescription() => $"Атака на {_target?.Name ?? "цель"}";
}