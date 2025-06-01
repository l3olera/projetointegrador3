using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "ScriptableObjects/Inventoy Items/Create New")]
public class Items : ScriptableObject
{
    public int itemID; //ID do item

    [Tooltip("Nome do item que está no localizationManager para identificar sua tradução")]
    public string itemName; //Nome do item que está no localizationManager para identificar sua tradução

    public Sprite itemSprite; //Sprite do item
}
