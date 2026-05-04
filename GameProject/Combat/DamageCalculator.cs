namespace GameProject.Combat;

public class DamageCalculator
{
    private const int CriticalHitChancePercent = 20;
    private const double MinDamageVariance = 0.8;
    private const double MaxDamageVariance = 1.2;
    private const int MinDamage = 1;
    
    private Random _random = new Random();
    
    public int CalculateDamage(int baseDamage, bool isCritical)
    {
        int damage = baseDamage;
        
        if (isCritical)
        {
            damage *= 2;
        }
        
        double variance = MinDamageVariance + (_random.NextDouble() * (MaxDamageVariance - MinDamageVariance));
        damage = (int)(damage * variance);
        
        return Math.Max(MinDamage, damage);
    }
    
    public bool IsCriticalHit()
    {
        return _random.Next(100) < CriticalHitChancePercent;
    }
}