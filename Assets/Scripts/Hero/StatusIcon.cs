using UnityEngine;
using UnityEngine.UI;

public class StatusIcon : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private Image _progress;
    [field: SerializeField] public Enums.DropType Type { get; private set; }
    private float _value;
    private int _maxValue =>_item.Strenght;
    private DropItem _item;
    private ItemEffect _effect;

    public void SetNewItem(DropItem item)
    {
        _effect?.OverEffect();
        gameObject.SetActive(true);
        _item = item;
        _value = _maxValue;
        _effect = item.effect;
        _icon.sprite = item.Icon;
        UpdateValue(0);
    }

    public void UpdateValue(float count)
    {
        if (!gameObject.activeSelf)
        {
            return;
        }
        _value -= count;
        if (_value <= 0)
        {
            gameObject.SetActive(false);
            _effect.OverEffect();
            return;
        }
        _progress.fillAmount = _value / _maxValue;
        return;        
    }
}
