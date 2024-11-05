using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyState
{

    private Transform _playerTransform;

    private float _timer;
    private float _timeBetweenShots = 2f;

    private float _exitTimer;
    private float _timeTillExit = 3f;
    private float _distanceToCountExit = 3f;

    private float _bulletSpeed = 10f;

    public EnemyAttackState(Enemy enemy, EnemyStateMachine enemyStateMachine) : base(enemy, enemyStateMachine)
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public override void AnimationTriggerEvent(Enemy.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        enemy.MoveEnemy(Vector2.zero);

        if (_timer > _timeBetweenShots)
        {
            _timer = 0f;

            Vector2 dir = (_playerTransform.position - enemy.transform.position).normalized;

            Rigidbody2D bullet = GameObject.Instantiate(enemy.BulletPrefab, enemy.transform.position, Quaternion.identity);
            bullet.velocity = dir * _bulletSpeed;

            enemy.StateMachine.ChangeState(enemy.ChaseState);
        }

        _timer += Time.deltaTime;
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

}
