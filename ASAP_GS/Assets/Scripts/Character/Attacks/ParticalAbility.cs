using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ParticalAbility : Ability
{
    private Transform _particalAttack;
    public ParticalAbility(Vector2 attackPoint, float overlapRadius, int damage, Transform partical, Animator animator) : base ( attackPoint, overlapRadius, damage, animator)
    {
        _attackPoint = attackPoint;
        _overlapRadius = overlapRadius;
        _damage = damage;
        _particalAttack = partical;
        _animator = animator;
    }
    public override void Use ()
    {
        Action();
        //Transform partical = Instantiate(_particalAttack, _attackPoint.position, Quaternion.identity);
        //Destroy(partical.gameObject, 5);
    }

    protected virtual void Action()
    {
        float sideFloat = _isMoovingRight ? 4 : -4; ;
        List<GameObject> collidObjects = Physics2D.OverlapAreaAll(new Vector2(_attackPoint.x, _attackPoint.y +1f), new Vector2 (_attackPoint.x + sideFloat, _attackPoint.y - 1f))
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
        Transform partical;
        if (_isMoovingRight)
        {
            partical = Instantiate(_particalAttack, _attackPoint, Quaternion.identity);
        }
        else
        {
            partical = Instantiate(_particalAttack, _attackPoint, Quaternion.Euler(0, 0, 180));
        }
        Destroy(partical.gameObject, 1);
    }
}
