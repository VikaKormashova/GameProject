namespace GameProject.Weapons;

public class IceDamageDecorator : WeaponDecorator
{
    private int _iceBonus;
    
    public IceDamageDecorator(IWeapon weapon, int iceBonus = 3) : base(weapon)
    {
        _iceBonus = iceBonus;
    }
    
    public override int GetDamage()
    {
        return _weapon.GetDamage() + _iceBonus;
    }
    
    public override string GetDescription()
    {
        return _weapon.GetDescription() + " + Лёд (+" + _iceBonus + ")";
    }
}