using NaughtyAttributes;
using UnityEngine;
using static Enums;

[CreateAssetMenu(fileName = "NewDrop", menuName ="MyGame/Drop")]
public class DropItem : ScriptableObject
{
    [field: SerializeField] public int Index { get; private set;}
    [field: SerializeField] public DropType Type  { get; private set;}
    [field: SerializeField] public int Power { get; private set;}
    [field: SerializeField] public int Strenght { get; private set; }
    [field: SerializeField, ShowAssetPreview] public Sprite Icon { get; private set;}
    [field: SerializeField, ShowAssetPreview] public Sprite Shine { get; private set;}
    [field: SerializeField, ShowAssetPreview] public Sprite Back { get; private set;}
    [field: SerializeField] public string Name { get; private set;}
    [field: SerializeField] public string Description { get; private set;}
    [field: SerializeField] public DropItem NextItem { get; private set;}

    [SerializeField] private ItemEffect _effect;
    public ItemEffect effect =>SetEffect();

    private ItemEffect SetEffect() 
    {        
        _effect.Item = this;
        return _effect;
    }
}
