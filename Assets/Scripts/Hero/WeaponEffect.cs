public class WeaponEffect : ItemEffect
{ 
    public override void Use(StatusIcon sIcon)
    {
        Hero.hero.damageBoost += Item.Power;
        sIcon.SetNewItem(Item);
    }
    public override void OverEffect() => Hero.hero.damageBoost -= Item.Power;
}
