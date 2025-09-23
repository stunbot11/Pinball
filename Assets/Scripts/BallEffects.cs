using UnityEngine;

[CreateAssetMenu(fileName = "New Ball Effect", menuName = "Ball Effect")]
public class BallEffects : ScriptableObject
{
    public ballEffect effect;
    public enum ballEffect
    {
        addPPB,
        phibonnachee,
        growth
    }

    public float effectAmount;
    public float maxAmount;
}
