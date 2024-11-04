using UnityEngine;
using static EnemyBase;
using static Hero;

public class Enemy : Character
{
    public bool isDestroyed;
    private void Awake()
    {
        Move(hero.transform);
        _animation.AnimationState.SetAnimation(0, "start", false);
        _animation.AnimationState.Complete += AnimationState_Complete;
        bool flip = hero.transform.position.x > transform.position.x;
        _animation.skeleton.ScaleX = flip ? 1 : -1;
    }

    private void AnimationState_Complete(Spine.TrackEntry trackEntry)
    {
        _animation.AnimationState.SetAnimation(0, "idle", false);
    }

    protected override void Attack()
    {
        hero.TakeDamage(_damage);
        _animation.AnimationState.SetAnimation(0, "attack", false);
    }

    protected override void Death()
    {
        isDestroyed = true;
        DropManager.Droper.DropItem(true, transform.position);
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
