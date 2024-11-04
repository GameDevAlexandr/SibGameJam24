using UnityEngine;

public class SpeedEffect : ItemEffect
{
    private StatusIcon _sIcon;
    public override void Use(StatusIcon sIcon)
    {
        _sIcon = sIcon;
        Hero.hero.SpeedBoost =1+ (float)Item.Power/100;
        sIcon.SetNewItem(Item);
    }
    public override void OverEffect() => Hero.hero.SpeedBoost = 1;
    public override void Tic()
    {
        _sIcon?.UpdateValue(Time.deltaTime);
    }
}
