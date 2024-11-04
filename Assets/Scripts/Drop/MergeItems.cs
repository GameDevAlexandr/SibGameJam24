using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image),typeof(CanvasGroup))]
public class MergeItems : MonoBehaviour, IDragHandler, IDropHandler, IBeginDragHandler, IEndDragHandler
{
    public int Index => Item.Index;
    public MergeManager Manager { set; get; }
    public DropItem Item { get; set; }
    [SerializeField] private Image _icon;
    [SerializeField] private Image _back;
    [SerializeField] private Image _selectImg;

    private CanvasGroup _group;
    private void Awake()
    {
        _group = GetComponent<CanvasGroup>();
    }

    public void SetItem(DropItem item)
    {
        Item = item;
        _icon.sprite = item.Icon;
        _back.sprite = item.Back;
    }
    public void Select(bool isSelect) => _selectImg.enabled = isSelect;
    public void OnBeginDrag(PointerEventData eventData)
    {
        _back.transform.parent = transform.parent.parent;
        Manager.OnDrag(true, Index);
        _group.alpha = 0.7f;
        _group.blocksRaycasts = false;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if (Manager.DropItem(this, eventData.pointerCurrentRaycast.worldPosition))
        {
            _back.transform.position = transform.position;
            _back.transform.parent = transform;
            Manager.OnDrag(false, Index);
            _group.blocksRaycasts = true;
            _group.alpha = 1f;
        }
    }
    public void OnDrag(PointerEventData eventData)
    {
        _back.transform.position = eventData.position;        
    }

    public void OnDrop(PointerEventData eventData)
    {
        MergeItems mItem;
        if (mItem = eventData.pointerDrag.GetComponent<MergeItems>() )
        {
            Debug.Log("Start m");
            if (Index == mItem.Index && mItem.Item.NextItem != null)
            {
                Debug.Log("Stop m");
                Manager.OnDrag(false, Index);
                mItem.SetItem(Item.NextItem);
                FMSoundManager.Sound.Play(Enums.SoundName.armourMerge);
                Manager.RemoveItem(this);

                return;
            }
        }
        _back.transform.position = transform.position;
        _back.transform.parent = transform;
        _group.alpha = 1f;
        _group.blocksRaycasts = true;
        Manager.OnDrag(false, Index);
    }

    private void OnDestroy()
    {
        Destroy(_back.gameObject);
    }
}
