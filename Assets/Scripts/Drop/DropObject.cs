using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropObject : MonoBehaviour
{
    public DropItem Item { get; private set; }
    [SerializeField] private SpriteRenderer _icon;
    [SerializeField] private GameObject _pick;

    public void SetData(DropItem item)
    {
        Item = item;
        _icon.sprite = item.Icon;
    }
    public void Find(bool isFind) => _pick?.SetActive(isFind);
}
