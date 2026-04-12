namespace GameProject.Weapons;

public class CriticalStrikeDecorator : WeaponDecorator
{
    private double _multiplier;
    
    public CriticalStrikeDecorator(IWeapon weapon, double multiplier = 2.0) : base(weapon)
    {
        _multiplier = multiplier;
    }
    
    public override int GetDamage()
    {
        return (int)(_weapon.GetDamage() * _multiplier);
    }
    
    public override string GetDescription()
    {
        return _weapon.GetDescription() + " x" + _multiplier;
    }
}