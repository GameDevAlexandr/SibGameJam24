using Spine.Unity;
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
    [SerializeField] private SkeletonAnimation _animation;

    private DropObject _drop;
    private Rigidbody2D _rb;
    private bool _inBase;
    private bool _isRuned;
    private bool _inHero { get => Hero.hero.pic.activeSelf; set => Hero.hero.pic.SetActive(value); }
    private void Awake()
    {
        Controll.control.direction.AddListener(PlayerMove);
        Controll.control.interact.AddListener(PicObject);
        player = this;
        _rb = GetComponent<Rigidbody2D>();
    }
    private void PlayerMove(Vector2 direction)
    {
        if(direction != Vector2.zero) _animation.skeleton.ScaleX = direction.x > 0 ? 1 : -1;

        if(_isRuned && direction == Vector2.zero)
        {
            _animation.AnimationState.SetAnimation(0, "idle", true);
            _isRuned = false;
        }
        if(!_isRuned && direction != Vector2.zero)
        {
            _animation.AnimationState.SetAnimation(0, "run", true);
            _isRuned = true;
        }
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
            _workShop.Near(true);
        }
        if(collision.tag == "Hero" && IsFull)
        {
            _inHero = true;
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
            _workShop.Near(false);
        }
        if (collision.tag == "Hero")
        {
            _inHero = false;
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
        if (_inHero)
        {
            Hero.hero.SetItem(_drop.Item);
            Empty();
        }
    }
    private void Empty()
    {
        IsFull = false;
        Destroy(_drop.gameObject);
    }
}
