using UnityEngine;

public class PowerEffect : ItemEffect
{
    private StatusIcon _sIcon;
    public override void Use(StatusIcon sIcon)
    {
        _sIcon = sIcon;
        Hero.hero.damageMult = 1 + (float)Item.Power / 100;
        sIcon.SetNewItem(Item);
    }
    public override void OverEffect() => Hero.hero.damageMult = 1;
    public override void Tic()
    {
        _sIcon?.UpdateValue(Time.deltaTime);
    }
}
