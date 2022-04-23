using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;
    [SerializeField] private Vector2 _rotation;
    [SerializeField] private int _damage;
    private Rigidbody2D _rb;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    public void Initial(/*float speed,*/ Vector2 rotation, int damage)
    {
        //_speed = speed;
        _rotation = rotation;
        _damage = damage;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        // Заглушка
        //List<GameObject> collisionObjects = new List<GameObject>();
        //GameObject player = collisionObjects.Find(obj => obj.tag == "Player");
        if (collision.transform.tag == "Player")
            //player.GetComponent<PlayerController>().GetDamage(_damage);
            collision.transform.GetComponent<PlayerController>().GetDamage(_damage);

        if (collision.transform.tag == "Ground")
            Destroy(transform.gameObject);
    }

    private void Move()
    {
        _rb.velocity = _rotation * _speed;
    }
}
