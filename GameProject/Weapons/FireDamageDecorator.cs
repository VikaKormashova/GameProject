namespace GameProject.Weapons;

public class FireDamageDecorator : WeaponDecorator
{
    private int _fireBonus;
    
    public FireDamageDecorator(IWeapon weapon, int fireBonus = 5) : base(weapon)
    {
        _fireBonus = fireBonus;
    }
    
    public override int GetDamage()
    {
        return _weapon.GetDamage() + _fireBonus;
    }
    
    public override string GetDescription()
    {
        return _weapon.GetDescription() + " + Огонь (+" + _fireBonus + ")";
    }
}