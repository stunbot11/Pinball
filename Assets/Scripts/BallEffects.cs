using UnityEngine;

[CreateAssetMenu(fileName = "New Ball Effect", menuName = "Ball Effect")]
public class BallEffects : ScriptableObject
{
    public ballEffect effect;
    public enum ballEffect
    {
        addPPB,
        phibonnachee,
        growth,
        refreshPegs
    }

    public float effectAmount;
    public float maxAmount;

    [Header("Happen Time")]
    public bool onLoad;
    public bool onHit;
    public bool onLoss;
    public bool onCrit;
    public bool onChance;
}
