using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherEnemy : SimpleEnemy
{
    [SerializeField] protected Transform _aggressivePoint;
    [SerializeField] protected Transform _projectile;

    public override void Update()
    {
        EnemyFollow(_target);
        UpdateAttackCooldown();
        Attack();      
    }

    public override void EnemyFollow(Transform target)
    {
        transform.localScale = new Vector3(
            -Mathf.Sign(target.position.x - transform.position.x) * Mathf.Abs(transform.localScale.x),
            transform.localScale.y,
            transform.localScale.z);
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

        if (gameObjectsInCollider.Contains(_target.gameObject))
        {       
             _animator.SetBool("isAttack", true);
        }
    }

    public void AttackIsEnd()
    {
        _animator.SetBool("isAttack", false);
    }

    public void SpawnFrog()
    {
        float angle = Mathf.Atan2(_target.position.y - transform.position.y, _target.position.x - transform.position.x) * Mathf.Rad2Deg;
        Quaternion quaternion = Quaternion.Euler(0, 0, angle);
        Transform obj = Instantiate(_projectile, transform.position, quaternion);
        obj.transform.localScale = new Vector3(obj.transform.localScale.x, Mathf.Sign(_target.position.x - transform.position.x) * obj.transform.localScale.y, obj.transform.localScale.z);
        obj.GetComponent<Projectile>().Initial((_target.position - transform.position).normalized, 1);
    }
}
