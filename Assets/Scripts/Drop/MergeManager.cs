using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MergeManager : MonoBehaviour
{
    public List<MergeItems> Items { get; private set; }
    [SerializeField] private MergeItems _itemPrefab;
    [SerializeField] private LayoutGroup _group;
    [SerializeField] private GameObject _workshopPanel;
    private void Awake()
    {
        Items = new List<MergeItems>();
    }

    public void PutItem(DropItem item)
    {
        Items.Add(Instantiate(_itemPrefab, _group.transform));
        Items[Items.Count-1].SetItem(item);
        Items[Items.Count-1].Manager = this;
    }
    public void DropItem(DropItem item)
    {

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
