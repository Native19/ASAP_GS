using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField] private float _jumpForce = 250f;
    [SerializeField] private Transform _groundCollider;
    [SerializeField] private LayerMask _LayerMask;
    private Rigidbody2D _rb;
    private bool _onGround;
    private float _colliderRadius = 0.5f;

    void Start()
    {
        _rb = transform.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _onGround = OnGround();
        AddJumpForce(_jumpForce);
    }

    private void AddJumpForce (float jumpForce)
    {
        //float normalVerticalDirection = Input.GetAxis("Vertical");

        //if (normalVerticalDirection < 0.1)
        //    return;
        //if (_onGround)
        //    _rb.AddForce(Vector2.up * jumpForce);
        if (Input.GetKeyDown(KeyCode.Space) && _onGround)
        {
            _rb.AddForce(Vector2.up * jumpForce);
        }
    }

    private bool OnGround()
    {
        return Physics2D.OverlapCircle(_groundCollider.position, _colliderRadius, _LayerMask);
    }
}
