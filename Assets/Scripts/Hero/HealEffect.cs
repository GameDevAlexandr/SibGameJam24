

public class HealEffect : ItemEffect
{
    public override void Use(StatusIcon sIcon)
    {
        Hero.hero.Heal(Item.Power);
    }
}
