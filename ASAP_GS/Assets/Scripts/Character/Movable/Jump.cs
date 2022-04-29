using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour, IJump
{
    private float _jumpForce = 50f;
    private Transform _groundCollider;
    private LayerMask _layerMask;
    private float _jumpCoolDown = 0.5f;
    private Rigidbody2D _rb;
    private bool _onGround;
    private float _colliderRadius = 0.5f;
    private bool _isJumpCoolDown = false;
    private Animator _animator;

    public Jump(float jumpForce, Transform groundCollider, LayerMask layerMask, Rigidbody2D rb,Animator animator)
    {
        _jumpForce = jumpForce;
        _groundCollider = groundCollider;
        _layerMask = layerMask;
        _rb = rb;
        _animator = animator;
    }

    private bool OnGround()
    {
        return Physics2D.OverlapCircle(_groundCollider.position, 0.5f, _layerMask);
    }

    IEnumerator JumpCoolDown()
    {
        yield return new WaitForSeconds(_jumpCoolDown);
        _isJumpCoolDown = false;
    }

    void IJump.Jump()
    {
        _onGround = OnGround();
        if (_onGround && !_isJumpCoolDown)
        {
            _animator.SetBool("isJump", true);
            _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }
    }
}
