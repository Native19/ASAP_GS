using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] private float _speed = 10f;
    private bool _isMooving = false;
    private Rigidbody2D _rb;
    private bool _isLeft = false;
    private bool _isIdle = true;

    public bool IsMooving() => _isMooving;

    void Start()
    {
        if (transform.GetComponent<Rigidbody2D>())
            _rb = transform.GetComponent<Rigidbody2D>();
        else
            _rb = transform.gameObject.AddComponent<Rigidbody2D>();
    }

    void Update()
    {
        AddVelocity();
    }

    private void AddVelocity ()
    {
        Vector2 normalizeHorizontalVelocity = new Vector2(Input.GetAxis("Horizontal"), 0);
        _rb.velocity = normalizeHorizontalVelocity * _speed;

        _isMooving = normalizeHorizontalVelocity.magnitude > 0.1;
        _isLeft = normalizeHorizontalVelocity.x < -0.1;
    }
}
