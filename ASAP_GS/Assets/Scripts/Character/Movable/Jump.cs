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

    public Jump(float jumpForce, Transform groundCollider, LayerMask layerMask, Rigidbody2D rb)
    {
        _jumpForce = jumpForce;
        _groundCollider = groundCollider;
        _layerMask = layerMask;
        _rb = rb;
    }

    private bool OnGround()
    {
        // Не работает с _colliderRadius
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
            _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            //_isJumpCoolDown = true;
            //StartCoroutine(JumpCoolDown());
        }
    }
}
