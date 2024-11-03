using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    public static Player player;
    public bool IsFinded { get; private set; }
    public bool IsFull { get; private set; }
    [SerializeField] private float _speed;
    [SerializeField] private Transform _dropPosition;
    [SerializeField] private MergeManager _workShop;

    private DropObject _drop;
    private Rigidbody2D _rb;
    private bool _inBase;
    private void Awake()
    {
        Controll.control.direction.AddListener(PlayerMove);
        Controll.control.interact.AddListener(PicObject);
        player = this;
        _rb = GetComponent<Rigidbody2D>();
    }
    private void PlayerMove(Vector2 direction)
    {
        _rb.velocity = direction*_speed;
    }

    public void FindDrop(bool isFind, DropObject drop)
    {
        IsFinded = isFind;
        _drop = drop;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Drop" && !IsFull && !IsFinded)
        {
            var o = collision.GetComponent<DropObject>();
            FindDrop(true, o);
            o.Find(true);
        }
        if(collision.tag == "Base" )
        {
            if (IsFull)
            {
                _workShop.PutItem(_drop.Item);
                Empty();
            }
            _inBase = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Drop")
        {
            FindDrop(false, _drop);
            _drop.Find(false);
        }
        if(collision.tag == "Base")
        {
            _inBase = false;
        }
    }
    private void PicObject()
    {
        if (IsFinded)
        {
            IsFull = true;
            _drop.transform.position = _dropPosition.position;
            _drop.transform.parent = transform;
            _drop.Find(false);
        }
        if (_inBase)
        {
            _workShop.OpenPanel();
        }
    }
    private void Empty()
    {
        IsFull = false;
        Destroy(_drop.gameObject);
    }
}
