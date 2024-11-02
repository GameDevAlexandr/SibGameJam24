using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public static EnemyBase eBase { get; private set; }
    private List<Enemy> _enemies = new List<Enemy>();

    private void Awake()
    {
        eBase = this;
    }
    public void AddEnemy(Enemy enemy) => _enemies.Add(enemy);
    public void RemoveEnemy(Enemy enemy)
    {
        _enemies.Remove(enemy);
        Destroy(enemy.gameObject);
    }
    public Enemy NearEnemy(Vector2 target)
    {
        if (_enemies.Count == 0)
        {
            return null;
        }
        var result = _enemies[0];
        float dist = Mathf.Abs(Vector2.Distance(target, _enemies[0].transform.position));
        for (int i = 0; i < _enemies.Count; i++)
        {
            float newDist = Mathf.Abs(Vector2.Distance(target, _enemies[i].transform.position));
            if (newDist < dist)
            {
                dist = newDist;
                result = _enemies[i];
            }
        }
        return result;
    }
}
