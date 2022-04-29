using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MeleeEnemy : SimpleEnemy
{
    [SerializeField] protected Transform _aggressivePoint;
    [SerializeField] protected float _jumpAttackCooldown = 10f;
    [SerializeField] protected float _jumpAttackDuration = 3f;
    [SerializeField] protected int _jumpForce = 40;
    [SerializeField] protected Transform _meleeAttackPoint;
    private bool _isJumpAttackActive = false;
    private float _jumpAttackCooldownTimer = 0;
    private float _jumpAttackDurationTimer = 0;


    public override void Update()
    {
        EnemyFollow(_target);
        UpdateAttackCooldown();
        Attack();
    }

    public override void EnemyFollow(Transform target)
    {
        if (Mathf.Abs(target.position.x - transform.position.x) < 0.5)
        {
            _rb.velocity = Vector2.zero;
            return;
        }

        transform.localScale = new Vector3(
            -Mathf.Sign(target.position.x - transform.position.x) * Mathf.Abs(transform.localScale.x),
            transform.localScale.y,
            transform.localScale.z);

        if (Mathf.Abs(target.position.x - transform.position.x) < 0.5f)
            return;

        _rb.velocity = new Vector2(Mathf.Sign(target.position.x - transform.position.x) * _speed, _rb.velocity.y);
    }

    protected override void Attack()
    {
        if (_attackCooldownTimer > 0)
            return;

        _attackCooldownTimer = _attackCooldown;
        Collider2D aggressiveCollider = _aggressivePoint.GetComponent<Collider2D>();
        List<Collider2D> collisions = new List<Collider2D>();
        aggressiveCollider.OverlapCollider(new ContactFilter2D().NoFilter(), collisions);
        List<GameObject> gameObjectsInCollider = collisions.ConvertAll(collision => collision.gameObject);

        Collider2D meleeCollider = _meleeAttackPoint.GetComponent<Collider2D>();
        List<Collider2D> meleeCollisions = new List<Collider2D>();
        meleeCollider.OverlapCollider(new ContactFilter2D().NoFilter(), collisions);
        List<GameObject> gameObjectsInMeleeCollider = collisions.ConvertAll(collision => collision.gameObject);

        Collider2D enemyCollider = GetComponent<Collider2D>();
        List<Collider2D> collisionsWithEnemy = new List<Collider2D>();
        enemyCollider.OverlapCollider(new ContactFilter2D().NoFilter(), collisions);
        List<GameObject> gameObjects = collisions.ConvertAll(collision => collision.gameObject);

        if (gameObjects.Contains(_target.gameObject))
        {
            _attackCooldownTimer = _attackCooldown;

            if (_isJumpAttackActive)
                _target.transform.GetComponent<PlayerController>().GetDamage(_damage);
            else
            {
                _animator.SetBool("isAttack", true);
            }
            return;
        }

        if (gameObjectsInMeleeCollider.Contains(_target.gameObject))
        {
            return;
        }

        if (gameObjectsInCollider.Contains(_target.gameObject) && _jumpAttackCooldownTimer <= 0)
        {
            Vector2 vectorToTarget = new Vector2 (_target.position.x - transform.position.x, _target.position.y - transform.position.y);
            JumpAttack(vectorToTarget);
        }
    }

    private void MeleeAttack ()
    {
        _attackCooldownTimer = _attackCooldown;
        List<GameObject> collidObjects = Physics2D.OverlapCircleAll(_meleeAttackPoint.position, 1.5f)
            .ToList()
            .ConvertAll(collider => collider.gameObject);

        if (collidObjects.Contains(_target.gameObject))
            _target.GetComponent<PlayerController>().GetDamage(_damage);
    }

    private void JumpAttack (Vector2 vectorToTarget)
    {
        _isJumpAttackActive = true;
        _jumpAttackCooldownTimer = _jumpAttackCooldown;
        _jumpAttackDurationTimer = _jumpAttackDuration;
        _attackCooldownTimer = _attackCooldown;

        if (Mathf.Abs(transform.position.y) -1.5 < Mathf.Abs(_target.position.y) && Mathf.Abs(_target.position.y)  < Mathf.Abs(transform.position.y) + 1.5)
            return;

        _rb.AddForce(vectorToTarget.normalized * _jumpForce , ForceMode2D.Impulse);
    }

    protected override void UpdateAttackCooldown()
    {
        if (_attackCooldownTimer > 0)
            _attackCooldownTimer -= Time.deltaTime;

        if (_jumpAttackCooldownTimer > 0)
            _jumpAttackCooldownTimer -= Time.deltaTime;

        if (_jumpAttackDurationTimer > 0)
        {
            _jumpAttackDurationTimer -= Time.deltaTime;
            JumpAttackIsOver();
        }
    }

    //public override void OnCollisionEnter2D(Collision2D collision)
    //{
    //    Debug.Log("----------------------------------");
    //    if (collision.transform == _target)
    //    {
    //        Debug.Log("$$$$$$$$$$$$$$$$$$$$$");
    //        if (_isJumpAttackActive)
    //            collision.transform.GetComponent<PlayerController>().GetDamage(_damage);
    //        else
    //        {
    //            Debug.Log("!!!!!!!!!!!!!!!");
    //            _animator.SetBool("isAttack", true);
    //            MeleeAttack();
    //        }
    //    }
    //}

    public void JumpAttackIsOver()
    {
        if (_jumpAttackDurationTimer <= 0)
            _isJumpAttackActive = false;
    }

    public void AttackIsEnd()
    {
        MeleeAttack();
        _animator.SetBool("isAttack", false);
    }
}
