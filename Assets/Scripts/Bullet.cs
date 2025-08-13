using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float life = 2f;
    public int damage = 1;

    void Start() => Destroy(gameObject, life);

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Other:" + other.gameObject.name);
        if (other.TryGetComponent<Enemy>(out var enemy))
        {
            Debug.Log("Take damage?");
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
