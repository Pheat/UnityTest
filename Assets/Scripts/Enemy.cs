using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    public int maxHp = 3;
    public float speed = 3f;
    public int contactDamage = 1;

    int _hp;
    Rigidbody2D _rb;
    Transform _player;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _hp = maxHp;
        _player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void FixedUpdate()
    {
        if (!_player) return;
        Vector2 dir = (_player.position - transform.position).normalized;
        _rb.linearVelocity = dir * speed;
    }

    public void TakeDamage(int dmg)
    {
        Debug.Log("Bullet hit" + dmg);
        
        _hp -= dmg;
        if (_hp <= 0) Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Player"))
        {
            if (col.collider.TryGetComponent<PlayerHealth>(out var ph))
                ph.TakeDamage(contactDamage);
        }
    }
}
