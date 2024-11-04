public class ArmourEffect : ItemEffect
{ 
    public override void Use(StatusIcon sIcon)
    {
        Hero.hero.Armour = Item.Power;
        sIcon.SetNewItem(Item);
    }
    public override void OverEffect() => Hero.hero.Armour = 0;
}
