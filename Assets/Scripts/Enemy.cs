using UnityEngine;
using static EnemyBase;
using static Hero;

public class Enemy : Character
{
    protected override void Attack()
    {
        hero.TakeDamage(_damage);
    }

    protected override void Death()
    {
        eBase.RemoveEnemy(this);
    }

    protected override void Tic()
    {
        if (!_isMove && !_isAttack)
        {
            Move(hero.transform);
            if (Vector2.Distance(hero.transform.position, transform.position) <= _attackDistance)
            {
                _isAttack = true;
                _isMove = false;
            }
        }
    }
}
