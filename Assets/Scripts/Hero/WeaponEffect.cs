using UnityEngine;

public class WeaponEffect : ItemEffect
{ 
    public override void Use(StatusIcon sIcon)
    {
        Hero.hero.damageBoost += Item.Power;
        Hero.hero.SetWeapon(Item.Icon);
        sIcon.SetNewItem(Item);
        Debug.Log("Status " + Item.Type);

    }
    public override void OverEffect()
    {
        Hero.hero.damageBoost -= Item.Power;
        Hero.hero.SetWeapon(null);
    }
}
