using UnityEngine;
using static EnemyBase;

public class Hero : Character
{
    public static Hero hero;
    private Enemy _enemy;
    public void Awake()
    {
        hero = this;
    }

    override protected void Tic()
    {
        var enm = eBase.NearEnemy(transform.position);
        if (_enemy != enm)
        {
            _enemy = enm;
            Move(_enemy.transform);
        }
        if (_isMove && !_isAttack && enm!= null)
        {
            if (Mathf.Abs(Vector2.Distance(_enemy.transform.position, transform.position)) <= _attackDistance)
            {                
                StartAttack();
            }
        }
    }

    protected override void Attack()
    {
        if(!_enemy.isDestroyed)
        _enemy.TakeDamage(_damage);
        if (_enemy.isDestroyed)
        {
            _isAttack = false;
        }
    }

    protected override void Death()
    {
    }
}
