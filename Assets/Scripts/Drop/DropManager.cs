using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropManager : MonoBehaviour
{
    public static DropManager Droper { get; private set; }
    [SerializeField] private DropObject _prefab;
    [SerializeField] private float _dropDelay;
    [SerializeField] private Transform _minDropPos;
    [SerializeField] private Transform _maxDropPos;
    [SerializeField] private DropItem[] _enemyDrops;
    [SerializeField] private DropItem[] _groundDrops;

    private List<DropItem> _drops = new List<DropItem>();
    private List<DropItem> _plants = new List<DropItem>();
    private float _delay;

    private void Awake()
    {
        Droper = this;
        _delay = _dropDelay;
    }
    private void FillColection(bool atEnemy)
    {
        var l = atEnemy ? _drops : _plants;
        var a = atEnemy ? _enemyDrops : _groundDrops;
        for (int i = 0; i < a.Length; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                l.Add(a[i]);
            }
        }
    }

    public void DropItem(DropItem item, Vector2 position) 
    {
        var o = Instantiate(_prefab, position, Quaternion.identity);
        o.SetData(item);
    }
    public void DropItem(bool atEnemy, Vector2 position) => DropItem(GetDropItem(atEnemy), position);
    public void DropItem(DropItem item, Vector2 min, Vector2 max)
    {
        var pos = new Vector2(Random.Range(min.x, max.x), Random.Range(min.y, max.y));
        DropItem(item, pos);
    }
    private DropItem GetDropItem(bool atEnemy)
    {
        var l = atEnemy ? _drops : _plants;
        
        if (l.Count == 0)
        {
            FillColection(atEnemy);
        }
        var res = l[Random.Range(0, l.Count)];
        l.Remove(res);
        return res;
    }
    private void Update()
    {
        _delay -= Time.deltaTime;
        if (_delay <= 0)
        {
            _delay = _dropDelay;
            DropItem(GetDropItem(false), _minDropPos.position, _maxDropPos.position);
        }
    }
}
