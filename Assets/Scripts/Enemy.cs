using UnityEngine;
using static EnemyBase;
using static Hero;

public class Enemy : Character
{
    public bool isDestroyed;
    private void Awake()
    {
        Move(hero.transform);
    }
    protected override void Attack()
    {
        hero.TakeDamage(_damage);
    }

    protected override void Death()
    {
        isDestroyed = true;
        eBase.RemoveEnemy(this);
    }

    protected override void Tic()
    {
        if (_isMove && !_isAttack)
        {
            Move(hero.transform);
            if ( Mathf.Abs(Vector2.Distance(hero.transform.position, transform.position)) <= _attackDistance)
            {
                StartAttack();
            }
        }
    }
}
