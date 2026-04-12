namespace GameProject.Combat;

public class DamageCalculator
{
    private Random _random = new Random();
    
    public int CalculateDamage(int baseDamage, bool isCritical)
    {
        int damage = baseDamage;
        
        if (isCritical)
        {
            damage *= 2;
        }
        
        double variance = 0.8 + (_random.NextDouble() * 0.4);
        damage = (int)(damage * variance);
        
        return Math.Max(1, damage);
    }
    
    public bool IsCriticalHit()
    {
        return _random.Next(100) < 20;
    }
}