using GameProject.Entities;

namespace GameProject.Entities;

public class Player : Entity
{
    public int Experience { get; private set; }
    public int Level { get; private set; }
    public int ExperienceToNextLevel { get; private set; }
    
    public Player(string name) : base(name, 100)
    {
        Experience = 0;
        Level = 1;
        ExperienceToNextLevel = 100;
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
        MaxHealth += 10;
        Health = MaxHealth;
        ExperienceToNextLevel = 100 + (Level - 1) * 25;
        Console.WriteLine($"{Name} достиг {Level} уровня!");
    }
    
    public void Heal(int amount)
    {
        if (!IsAlive()) return;
        Health += amount;
        if (Health > MaxHealth) Health = MaxHealth;
    }
}