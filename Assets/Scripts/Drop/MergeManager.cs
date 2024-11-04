using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MergeManager : MonoBehaviour
{
    public List<MergeItems> Items { get; private set; }
    [SerializeField] private MergeItems _itemPrefab;
    [SerializeField] private LayoutGroup _group;
    [SerializeField] private GameObject _workshopPanel;
    [SerializeField] private RectTransform _panelArea;
    private Bounds _dropBounds;
    private void Awake()
    {
        Items = new List<MergeItems>();
        Debug.Log("size " + _panelArea.sizeDelta);
        _dropBounds = new Bounds(_panelArea.position, _panelArea.sizeDelta);
    }

    public void PutItem(DropItem item)
    {
        Items.Add(Instantiate(_itemPrefab, _group.transform));
        Items[Items.Count-1].SetItem(item);
        Items[Items.Count-1].Manager = this;
    }
    public bool DropItem(MergeItems item, Vector2 pos)
    {
        if (!_dropBounds.Contains(pos))
        {
            DropManager.Droper.DropItem(item.Item);
            RemoveItem(item);
            return false;
        }
        return true;
    }
    public void RemoveItem(MergeItems item)
    {
        Items.Remove(item);
        Destroy(item);
        Destroy(item.gameObject);
    }
    public void OnDrag(bool isDrag, int index)
    {
        for (int i = 0; i < Items.Count; i++)
        {
            if(Items[i].Index == index)
            {
                Items[i].Select(isDrag);
            }
        }
    }
    public void OpenPanel() => _workshopPanel.SetActive(true);

}
