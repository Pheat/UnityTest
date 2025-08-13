using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    public int maxHp = 5;
    public UnityEvent onDeath;

    int _hp;

    void Awake() => _hp = maxHp;

    public void TakeDamage(int dmg)
    {
        _hp -= dmg;
        if (_hp <= 0)
        {
            onDeath?.Invoke();
            Time.timeScale = 0f; // simple game over
        }
    }

    public float Hp01() => Mathf.Clamp01(_hp / (float)maxHp);
}
