using UnityEngine;

[CreateAssetMenu(fileName = "New Peg Effect", menuName = "Peg Effect")]
public class PegEffects : ScriptableObject
{
    public pegEffect effect;
    public enum pegEffect
    {
        ballSplitter,
        ballLoader
    }

    [TextArea]
    public string description; 

    public float effectAmount;
    public float maxAmount;

    [Header("Happen Time")]
    public bool onHit;
    public bool onCrit;
    public bool onChance;

    public float chance01;
}
