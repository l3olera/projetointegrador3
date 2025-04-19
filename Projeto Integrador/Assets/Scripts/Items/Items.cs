using JetBrains.Annotations;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventoy Items/Create New")]
public class Items : ScriptableObject
{
    public string itemName; //Nome do item
    public Sprite itemSprite; //Sprite do item
}
