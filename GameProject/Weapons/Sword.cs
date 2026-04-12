namespace GameProject.Weapons;

public class Sword : IWeapon
{
    private int _baseDamage;
    
    public Sword(int baseDamage = 10)
    {
        _baseDamage = baseDamage;
    }
    
    public int GetDamage()
    {
        return _baseDamage;
    }
    
    public string GetDescription()
    {
        return "Простой меч";
    }
}