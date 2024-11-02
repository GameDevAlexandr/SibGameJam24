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
        if (!_isMove && !_isAttack && enm!= null)
        {
            Move(enm.transform);
            if (Vector2.Distance(_enemy.transform.position, transform.position) <= _attackDistance)
            {
                _isAttack = true;
                _isMove = false;
            }
        }
    }

    protected override void Attack()
    {
        _enemy?.TakeDamage(_damage);
    }

    protected override void Death()
    {
    }
}
