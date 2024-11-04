using Spine.Unity;
using UnityEngine;
using UnityEngine.UI;

public abstract class Character : MonoBehaviour
{
    [SerializeField] protected int _damage;
    [SerializeField] protected int _health;
    [SerializeField] protected float _atkSpeed;
    [SerializeField] protected float _moveSpeed;
    [SerializeField] protected float _attackDistance;
    [SerializeField] protected Image _healthBar;
    [SerializeField] protected SkeletonAnimation _animation;
    [SerializeField] private TextGenerator _tg;

    protected int _currHealth;
    private Transform _target;
    protected bool _isMove;
    protected bool _isAttack;
    private float _attackDelay;
    protected int _armour;
    protected int _wStrengrt;
    protected int _aStrenght;

    private void Start()
    {
        _currHealth = _health;
    }

    public virtual void TakeDamage(int damage)
    {
        if (_currHealth <= 0)
        {
            return;
        }
        if (damage > 0)
        {
            damage = (int)(damage * ((float)damage / (damage + _armour)));
            _currHealth -= damage;
        }
        _healthBar.fillAmount = (float)_currHealth / _health;
        _aStrenght = Mathf.Max(0, _aStrenght - damage);
        _animation.AnimationState.SetAnimation(0, "hit", false);
        _tg.StartFly(damage.ToString(), false);
        if (_currHealth <= 0)
        {
            Death();
        }        
    }
    protected virtual void Move(Transform target)
    {
        _target = target;        
        _isMove = true;
    }

    private void Update()
    {
        Tic();
        if (_isMove)
        {
            Vector2 direct = (_target.position - transform.position).normalized;
            transform.Translate(direct * Time.deltaTime * _moveSpeed);
        }
        if (_isAttack)
        {
            _attackDelay -= Time.deltaTime;
            if (_attackDelay <= 0)
            {
                _attackDelay = 1f / _atkSpeed;
                Attack();
            }
        }
    }

    protected abstract void Attack();
    protected void StartAttack()
    {
        _isMove = false;
        _isAttack = true;
        _attackDelay = 0;
    }
    protected abstract void Tic();
    protected abstract void Death();

    private void EraseArmour(int count)
    {
        if (_aStrenght > 0)
        {
            _aStrenght -= count;
        }
    }
    public void SetDrop(DropItem item)
    {

    }
}
