using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "New Shop Item", menuName = "Item")]
public class BuyableObject : ScriptableObject
{
    public GameObject ball;
    public Sprite Icon;
    public float baseCost;

    public type shopType;
    public enum type
    {
        Ball,
        Peg
    }

    public rareness rarity;
    public enum rareness
    {
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary,
        Mythic
    }
}
