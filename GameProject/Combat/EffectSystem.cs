namespace GameProject.Combat;

public class EffectSystem
{
    public void PlayHitEffect()
    {
        Console.WriteLine("💥 *Звук удара* 💥");
    }
    
    public void PlayCriticalEffect()
    {
        Console.WriteLine("⚡✨ КРИТИЧЕСКИЙ УДАР! ✨⚡");
    }
    
    public void ShowDamageNumber(int damage)
    {
        Console.WriteLine($"  Нанесено урона: {damage}!");
    }
}