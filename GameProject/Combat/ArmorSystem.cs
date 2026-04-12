namespace GameProject.Combat;

public class ArmorSystem
{
    public int ApplyArmor(int damage, int armorValue)
    {
        if (armorValue <= 0) return damage;
        
        int reducedDamage = damage - (armorValue * 2);
        return Math.Max(1, reducedDamage);
    }
}