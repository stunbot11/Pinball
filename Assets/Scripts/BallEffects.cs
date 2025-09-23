using UnityEngine;

[CreateAssetMenu(fileName = "New Ball Effect", menuName = "Ball Effect")]
public class BallEffects : ScriptableObject
{
    public ballEffect effect;
    public enum ballEffect
    {
        increment,
        phibonnachee,
        growth
    }
}
