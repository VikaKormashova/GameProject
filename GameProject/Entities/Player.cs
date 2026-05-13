using GameProject.Entities;

namespace GameProject.Entities;

public class Player : Entity
{
    private const int BaseExperienceForLevelUp = 100;
    private const int ExperienceIncreasePerLevel = 25;
    private const int HealthIncreasePerLevel = 10;
    private const int StartingHealth = 100;
    private const int StartingLevel = 1;
    private const int StartingExperience = 0;
    private const int StartingPositionX = 0;
    private const int StartingPositionY = 0;
    
    private int _health;
    private int _experience;
    
    public int PositionX { get; private set; }
    public int PositionY { get; private set; }
    
    public int Experience
    {
        get => _experience;
        private set
        {
            if (_experience == value) return;
            _experience = value;
            OnExperienceChanged?.Invoke(_experience, ExperienceToNextLevel);
        }
    }
    
    public int Level { get; private set; }
    public int ExperienceToNextLevel { get; private set; }
    
    public event Action<int, int>? OnHealthChanged;
    public event Action<int, int>? OnExperienceChanged;
    public event Action<int>? OnLevelUp;
    public event Action? OnPlayerDeath;
    
    public Player(string name) : base(name, StartingHealth)
    {
        _experience = StartingExperience;
        Level = StartingLevel;
        ExperienceToNextLevel = BaseExperienceForLevelUp;
        PositionX = StartingPositionX;
        PositionY = StartingPositionY;
    }
    
    public override int Health
    {
        get => _health;
        set
        {
            if (_health == value) return;
            _health = value;
            if (_health < 0) _health = 0;
            if (_health > MaxHealth) _health = MaxHealth;
            OnHealthChanged?.Invoke(_health, MaxHealth);
            
            if (_health <= 0)
            {
                OnPlayerDeath?.Invoke();
            }
        }
    }
    
    public void Move(int deltaX, int deltaY)
    {
        PositionX += deltaX;
        PositionY += deltaY;
        Console.WriteLine($"Позиция: ({PositionX}, {PositionY})");
    }
    
    public void GainExperience(int amount)
    {
        Experience += amount;
        while (Experience >= ExperienceToNextLevel)
        {
            LevelUp();
        }
    }
    
    private void LevelUp()
    {
        Experience -= ExperienceToNextLevel;
        Level++;
        MaxHealth += HealthIncreasePerLevel;
        Health = MaxHealth;
        ExperienceToNextLevel = BaseExperienceForLevelUp + (Level - 1) * ExperienceIncreasePerLevel;
        OnLevelUp?.Invoke(Level);
    }
    
    public void Heal(int amount)
    {
        if (!IsAlive()) return;
        Health += amount;
    }
}