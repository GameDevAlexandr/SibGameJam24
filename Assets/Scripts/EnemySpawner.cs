using System.Collections;
using UnityEngine;
using static EnemyBase;
public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private float _spawnDelay;
    [SerializeField] private Transform _maxPosition;
    [SerializeField] private Transform _minPosition;
    private Bounds _area;
    void Start()
    {
        _area.max = (Vector2)_maxPosition.position;
        _area.min = (Vector2)_minPosition.position;
        StartCoroutine(SpawnCoroutine());
    }

    private IEnumerator SpawnCoroutine()
    {
        while (true)
        {
           yield return new WaitForSeconds(_spawnDelay);
            Spawn();
        }

    }
    public void Spawn()
    {
        Vector2 rndPos = new Vector2(Random.Range(_minPosition.position.x, _maxPosition.position.x), 
            Random.Range(_minPosition.position.y, _maxPosition.position.y));
        eBase.AddEnemy(Instantiate(_enemy, rndPos, Quaternion.identity, transform));        
    }
}
