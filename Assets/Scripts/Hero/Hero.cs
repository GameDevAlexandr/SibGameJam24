using Spine.Unity;
using UnityEngine;
using UnityEngine.Events;
using static EnemyBase;
using static Enums;
using static FMSoundManager;

public class Hero : Character
{
    public UnityEvent ticEvent = new UnityEvent();
    public static Hero hero;
    public GameObject pic;
    public int Armour { set => _armour = value; }
    public int damageBoost;
    public float SpeedBoost { set { _atkSpeed = _aSpd * value; _moveSpeed = _mSpd * value; } }
    public float damageMult = 1;    
    [SerializeField] private StatusIcon[] statuses;
    private Enemy _enemy;
    private float _aSpd;
    private float _mSpd;
    private bool _idle;

    public void Awake()
    {
        hero = this;
        _aSpd = _atkSpeed;
        _mSpd = _moveSpeed;
    }
    override protected void Tic()
    {
        var enm = eBase.NearEnemy(transform.position);
        if (_enemy != enm && !_isMove)
        {
            _enemy = enm;
            Move(_enemy.transform);
            _animation.AnimationState.SetAnimation(0, "walk", true);
            _idle = false;
        }
        else if (!_idle &&!_isAttack&&!_isMove)
        {
            _animation.AnimationState.SetAnimation(0, "idle", true);
            _idle = true;
        }
        if (_isMove && !_isAttack && !enm.isDestroyed)
        {
            float dis = Vector2.Distance(_enemy.transform.position, transform.position);
            bool flip = _enemy.transform.position.x > transform.position.x;
            _animation.skeleton.ScaleX = flip ? 1 : -1;
            if (Mathf.Abs(dis) <= _attackDistance)
            {                
                StartAttack();
            }
        }
        
        ticEvent?.Invoke();
    }
    public override void TakeDamage(int damage)
    {
        GetStatus(DropType.armour).UpdateValue(damage);
        Sound.Play(_armour > 0 ? SoundName.armourHit : SoundName.heroHit, transform.position);
        base.TakeDamage(damage);
    }
    protected override void Attack()
    {
        if (!_enemy.isDestroyed)
        {
            _enemy.TakeDamage((int)((_damage+damageBoost)*damageMult));
            _wStrengrt = Mathf.Max(0, _wStrengrt - 10);
            GetStatus(DropType.weapon).UpdateValue(10);
            _animation.AnimationState.SetAnimation(0, "attack", false);
            Sound.Play(SoundName.knightAttack, transform.position);
        }
        if (_enemy.isDestroyed)
        {
            _isAttack = false;
        }
    }

    public void Heal(int value)
    {
        _currHealth = Mathf.Min(_health, _currHealth + value);
        TakeDamage(0);
    }
    public void SetItem(DropItem item)
    {
        for (int i = 0; i < statuses.Length; i++)
        {
            if(statuses[i].Type == item.Type)
            {
                item.effect.Use(statuses[i]);
                return;
            }
        }
        item.effect.Use(null);
    }
    protected override void Death()
    {
    }

    private StatusIcon GetStatus(Enums.DropType type)
    {
        for (int i = 0; i < statuses.Length; i++)
        {
            if(statuses[i].Type == type)
            {
                return statuses[i];
            }
        }
        return null;
    }
}
