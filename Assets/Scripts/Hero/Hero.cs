using Spine.Unity;
using Spine.Unity.Examples;
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
    [SerializeField] private SpriteAttacher _attacher;
    [SerializeField] private GameObject _finfshPanel;
    [SerializeField] private StatusIcon[] statuses;
    private Enemy _enemy;
    private float _aSpd;
    private float _mSpd;
    private bool _idle;
    private bool _isDeath;

    public void Awake()
    {
        hero = this;
        _aSpd = _atkSpeed;
        _mSpd = _moveSpeed;
        _animation.AnimationState.Complete += AnimationState_Complete;
    }

    private void AnimationState_Complete(Spine.TrackEntry trackEntry)
    {
        if(trackEntry.Animation.Name == "death")
        {
            Time.timeScale = 0;
            _finfshPanel.SetActive(true);
        }
    }

    override protected void Tic()
    {
        if (_isDeath)
        {
            return;
        } 
        Vector2 enmPos = transform.position;
        var enm = eBase.NearEnemy(transform.position);
        if (enm != null)
        {
            enmPos = enm.transform.position;
        }
        if (_enemy != enm && !_isMove)
        {
            _enemy = enm;            
            Move(_enemy.transform);
            Sound.Play(SoundName.knightFootStep);
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
            float dis = Vector2.Distance(enmPos, transform.position);
            bool flip = enmPos.x > transform.position.x;
            _animation.skeleton.ScaleX = flip ? 1 : -1;
            if (Mathf.Abs(dis) <= _attackDistance)
            {
                Sound.Stop(SoundName.knightFootStep);
                StartAttack();
            }
        }
        for (int i = 0; i < statuses.Length; i++)
        {
            if (statuses[i].gameObject.activeSelf)
            {
                statuses[i].effect.Tic();
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
        if (_isDeath)
        {
            return;
        }
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
        Debug.Log(_currHealth);
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
        _isDeath = true;
        _animation.AnimationState.SetAnimation(0, "death", false);
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
    public void SetWeapon(Sprite weapon)
    {
        _attacher.sprite = weapon;
        _attacher.Start();
        
    }
}
