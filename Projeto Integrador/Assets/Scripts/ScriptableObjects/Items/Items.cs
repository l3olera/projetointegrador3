using UnityEngine;
using UnityEngine.Localization;

[CreateAssetMenu(fileName = "New Item", menuName = "ScriptableObjects/Inventoy Items/Create New")]
public class Items : ScriptableObject
{
    public int itemID; //ID do item

    public LocalizedString itemName;

    public Sprite itemSprite; //Sprite do item
}
