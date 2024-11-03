using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRecipe", menuName ="MyGame/Recipe")]
public class RecipeItem : MonoBehaviour
{
    [field: SerializeField] public int Index { get; private set; }
    [field: SerializeField] public DropItem[] Components { get; private set; }
    [field: SerializeField] public DropItem Result { get; private set; }
}
