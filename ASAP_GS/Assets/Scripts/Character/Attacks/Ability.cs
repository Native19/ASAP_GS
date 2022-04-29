using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Ability : MonoBehaviour
{
    protected Vector2 _attackPoint;
    protected float _overlapRadius = 2;
    protected int _damage = 100;
    protected bool _isMoovingRight = true;
    protected Animator _animator;

    public Ability (Vector2 attackPoint, float overlapRadius, int damage, Animator animator)
    {
        _attackPoint = attackPoint;
        _overlapRadius = overlapRadius;
        _damage = damage;
        _animator = animator;
    }
    public virtual void Use ()
    {
        Action();
    }

    protected virtual void Action ()
    {
        if (_animator.GetBool("isAttack"))
        {
            Debug.Log("&&&&&&&&&&&&&&&");
            return;
        }

        _animator.SetBool("isAttack", true);
        List<GameObject> collidObjects = Physics2D.OverlapCircleAll(_attackPoint, _overlapRadius)
            .ToList()
            .ConvertAll(collider => collider.gameObject);

        foreach (GameObject collision in collidObjects)
        {
            try
            {
                SimpleEnemy enemy = collision.transform.GetComponent<SimpleEnemy>();
                enemy.GetDamage(_damage);
            }
            catch
            {

            }
        }
        
    }

    public void UpdateAbility(Vector3 attackPoint, bool isMoovingRight)
    {
        _attackPoint = attackPoint;
        _isMoovingRight = isMoovingRight;
    }

    public void OverAttack()
    {
        _animator.SetBool("isAttack", false);
    }
}
