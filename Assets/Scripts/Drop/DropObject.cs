using UnityEngine;
using static FMSoundManager;
public class DropObject : MonoBehaviour
{
    public DropItem Item { get; private set; }
    [SerializeField] private SpriteRenderer _icon;
    [SerializeField] private GameObject _pick;
    [SerializeField] private SpriteRenderer _shine;

    public void SetData(DropItem item)
    {
        Item = item;
        _icon.sprite = item.Icon;
        _shine.sprite = item.Shine;
        Sound.Play(Enums.SoundName.drop);
    }
    public void Find(bool isFind)
    {
        try
        {
            _pick.SetActive(isFind);
            _shine.gameObject.SetActive(isFind);
        }
        catch
        {
            return;
        }
    }
}
