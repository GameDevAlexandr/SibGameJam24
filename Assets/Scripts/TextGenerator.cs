using System.Collections.Generic;
using UnityEngine;

public class TextGenerator : MonoBehaviour
{
    [SerializeField] private FlyText _flyText;
    [SerializeField] private int _count;

    private List<FlyText> _radyText = new List<FlyText>(); 
    private void Start()
    {
        for (int i = 0; i < _count; i++)
        {
            _flyText.generator = this;
           Instantiate(_flyText.gameObject, transform.position, Quaternion.identity, transform);
        }
    }
    public void StartFly(string message, bool itBonus, Vector2 position)
    {
        
        if (_radyText.Count > 0)
        {
            try
            {
                _radyText[0].StartFly(position, message, itBonus);
                _radyText.RemoveAt(0);
            }
            catch
            {
                return;
            }
        }
    }
    public void StartFly(Sprite icon,string message, bool itBonus, Vector2 position)
    {
        if (_radyText.Count > 0)
        {
            try
            {
                _radyText[0].StartFly(icon, position, message, itBonus);
                _radyText.RemoveAt(0);
            }
            catch
            {
                return;
            }
        }
    }

    public void StartFly(string message, bool itBonus)
    {
        try
        {
            StartFly(message, itBonus, transform.position);
        }
        catch
        {
            return;
        }
    }

    public void RemoveText(FlyText text)
    {
        _radyText.Add(text);
    }
    private void OnDestroy()
    {
        for (int i = 0; i < _radyText.Count; i++)
        {
            try
            {
                Destroy(_radyText[i].gameObject);
            }
            catch
            {
                break;
            }
        }
    }
}
