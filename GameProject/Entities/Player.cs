using GameProject.Entities;

namespace GameProject.Entities;

public class Player : Entity
{
    private const int BaseExperienceForLevelUp = 100;
    private const int ExperienceIncreasePerLevel = 25;
    private const int HealthIncreasePerLevel = 10;
    
    public int Experience { get; private set; }
    public int Level { get; private set; }
    public int ExperienceToNextLevel { get; private set; }
    
    public Player(string name) : base(name, 100)
    {
        Experience = 0;
        Level = 1;
        ExperienceToNextLevel = BaseExperienceForLevelUp;
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
        Console.WriteLine($"{Name} достиг {Level} уровня!");
    }
    
    public void Heal(int amount)
    {
        if (!IsAlive()) return;
        Health += amount;
        if (Health > MaxHealth) Health = MaxHealth;
    }
}