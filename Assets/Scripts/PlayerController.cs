using UnityEngine;
using UnityEngine.InputSystem; // nový Input System

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 6f;
    public Rigidbody2D rb;

    [Header("Shooting")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 14f;
    public float fireCooldown = 0.15f;
    public float spawnOffset = 0.6f;

    private float _cooldown;
    private Vector2 _move;
    private Camera _cam;

    void Awake()
    {
        if (!rb) rb = GetComponent<Rigidbody2D>();
        _cam = Camera.main;
    }

    void Update()
    {
        // ---- MOVEMENT (WASD) ----
        var k = Keyboard.current;
        if (k != null)
        {
            float x = (k.dKey.isPressed ? 1 : 0) - (k.aKey.isPressed ? 1 : 0);
            float y = (k.wKey.isPressed ? 1 : 0) - (k.sKey.isPressed ? 1 : 0);
            _move = new Vector2(x, y).normalized;
        }

        // ---- AIM (mouse) ----
        Vector3 mouseWorld = GetMouseWorld();
        Vector2 dir = (mouseWorld - transform.position);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        rb.MoveRotation(angle);

        // ---- SHOOT (LMB hold) ----
        _cooldown -= Time.deltaTime;
        bool firePressed = Mouse.current != null && Mouse.current.leftButton.isPressed;
        if (firePressed && _cooldown <= 0f)
        {
            Shoot(dir.normalized);
            _cooldown = fireCooldown;
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = _move * moveSpeed;
    }

    void Shoot(Vector2 dir)
    {
        if (!bulletPrefab || !firePoint) return;

        Vector3 pos = firePoint.position + (Vector3)(dir * spawnOffset);
        var go = Instantiate(bulletPrefab, pos, Quaternion.identity);

        if (go.TryGetComponent<Rigidbody2D>(out var rb2))
            rb2.linearVelocity = dir.normalized * bulletSpeed;
    }

    Vector3 GetMouseWorld()
    {
        // U orthographic kamery stačí doplnit „z“ tak, aby nebyl záporný offset.
        // Player i kamera měj klidně na Z=0 (kamera orthographic).
        if (_cam == null || Mouse.current == null) return transform.position;
        Vector2 mp = Mouse.current.position.ReadValue();
        var screen = new Vector3(mp.x, mp.y, 0f);
        return _cam.ScreenToWorldPoint(screen);
    }
}
