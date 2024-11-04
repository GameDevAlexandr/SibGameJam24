using UnityEngine;

public abstract class ItemEffect : MonoBehaviour
{
    public virtual DropItem Item { get; set; }
    public abstract void Use(StatusIcon sIcon);
    public virtual void OverEffect() { }
    public virtual void Tic() { }
}
