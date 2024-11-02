using System.Collections;
using System.Collections.Generic;
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

    protected int _currHealth;
    private Transform _target;
    protected bool _isMove;
    protected bool _isAttack;
    private float _attackDelay;
    public virtual void TakeDamage(int damage)
    {
        _currHealth -= damage;
        if (_currHealth <= 0)
        {
            Death();
        }
    }
    protected void Move(Transform target)
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
        _isAttack = true;
        _attackDelay = 1f / _atkSpeed;
    }
    protected abstract void Tic();
    protected abstract void Death();

}
