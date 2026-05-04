using GameProject.Entities;

namespace GameProject.UI;

public class ConsoleHUD
{
    private int _currentHealth;
    private int _maxHealth;
    private int _currentExp;
    private int _expToNextLevel;
    private int _currentLevel;
    
    public ConsoleHUD(Player player)
    {
        player.OnHealthChanged += UpdateHealth;
        player.OnExperienceChanged += UpdateExperience;
        player.OnLevelUp += UpdateLevel;
        player.OnPlayerDeath += ShowGameOver;
    }
    
    private void UpdateHealth(int current, int max)
    {
        _currentHealth = current;
        _maxHealth = max;
        Render();
    }
    
    private void UpdateExperience(int current, int max)
    {
        _currentExp = current;
        _expToNextLevel = max;
        Render();
    }
    
    private void UpdateLevel(int level)
    {
        _currentLevel = level;
        Render();
    }
    
    private void ShowGameOver()
    {
        Console.WriteLine("\n╔══════════════════════════════════════╗");
        Console.WriteLine("║            ИГРА ОКОНЧЕНА            ║");
        Console.WriteLine("║            ВЫ ПОВЕРЖЕНЫ!            ║");
        Console.WriteLine("╚══════════════════════════════════════╝\n");
    }
    
    public void Render()
    {
        Console.Clear();
        Console.WriteLine("╔══════════════════════════════════════╗");
        Console.WriteLine("║           TURN-BASED RPG             ║");
        Console.WriteLine("╚══════════════════════════════════════╝\n");
        
        int healthPercent = _maxHealth > 0 ? (_currentHealth * 100) / _maxHealth : 0;
        int healthBarSize = 20;
        int healthFilled = (healthPercent * healthBarSize) / 100;
        string healthBar = new string('█', healthFilled) + new string('░', healthBarSize - healthFilled);
        Console.WriteLine($"[HP: {healthBar}] {_currentHealth}/{_maxHealth}");
        
        int expPercent = _expToNextLevel > 0 ? (_currentExp * 100) / _expToNextLevel : 0;
        int expBarSize = 20;
        int expFilled = (expPercent * expBarSize) / 100;
        string expBar = new string('█', expFilled) + new string('░', expBarSize - expFilled);
        Console.WriteLine($"[XP: {expBar}] {_currentExp}/{_expToNextLevel}");
        
        Console.WriteLine($"Уровень: {_currentLevel}\n");
        Console.WriteLine("─────────────────────────────────────────");
        Console.WriteLine("⌨️  Управление:");
        Console.WriteLine("  D - получить урон");
        Console.WriteLine("  E - получить опыт");
        Console.WriteLine("  ESC - выход из игры");
        Console.WriteLine("─────────────────────────────────────────");
    }
}