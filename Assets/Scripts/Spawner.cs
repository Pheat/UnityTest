using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float radius = 12f;
    public float spawnIntervalStart = 1.2f;
    public float spawnIntervalMin = 0.25f;
    public float difficultyRamp = 0.02f;

    float _t;
    float _interval;
    Transform _player;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player")?.transform;
        _interval = spawnIntervalStart;
    }

    void Update()
    {
        _t += Time.deltaTime;
        if (_t >= _interval)
        {
            _t = 0f;
            SpawnOne();
            _interval = Mathf.Max(spawnIntervalMin, _interval - difficultyRamp);
        }
    }

    void SpawnOne()
    {
        if (!_player || !enemyPrefab) return;
        Vector2 dir = Random.insideUnitCircle.normalized;
        Vector2 pos = (Vector2)_player.position + dir * radius;
        Instantiate(enemyPrefab, pos, Quaternion.identity);
    }
}
