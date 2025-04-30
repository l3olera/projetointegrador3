using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventoy Items/Create New")]
public class Items : ScriptableObject
{
    public int itemID; //ID do item
    
    public LocalizedString itemName;

    public Sprite itemSprite; //Sprite do item
}
