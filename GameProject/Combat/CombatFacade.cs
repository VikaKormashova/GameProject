using GameProject.Entities;

namespace GameProject.Combat;

public class CombatFacade
{
    private DamageCalculator _damageCalculator;
    private ArmorSystem _armorSystem;
    private EffectSystem _effectSystem;
    
    public CombatFacade()
    {
        _damageCalculator = new DamageCalculator();
        _armorSystem = new ArmorSystem();
        _effectSystem = new EffectSystem();
    }
    
    public int ProcessAttack(Player attacker, Enemy target, int baseDamage)
    {
        Console.WriteLine($"\n{attacker.Name} атакует {target.Name}!");
        
        bool isCritical = _damageCalculator.IsCriticalHit();
        
        int rawDamage = _damageCalculator.CalculateDamage(baseDamage, isCritical);
        
        int finalDamage = _armorSystem.ApplyArmor(rawDamage, 0);
        
        if (isCritical)
        {
            _effectSystem.PlayCriticalEffect();
        }
        _effectSystem.PlayHitEffect();
        _effectSystem.ShowDamageNumber(finalDamage);
        
        target.TakeDamage(finalDamage);
        
        return finalDamage;
    }
    
    public int ProcessEnemyAttack(Enemy attacker, Player target)
    {
        Console.WriteLine($"\n{attacker.Name} атакует {target.Name}!");
        
        int rawDamage = attacker.Damage;
        
        Random rand = new Random();
        double variance = 0.9 + (rand.NextDouble() * 0.2);
        int finalDamage = (int)(rawDamage * variance);
        
        _effectSystem.PlayHitEffect();
        _effectSystem.ShowDamageNumber(finalDamage);
        
        target.TakeDamage(finalDamage);
        
        return finalDamage;
    }
}