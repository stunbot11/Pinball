using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Preset", menuName = "Preset")]
public class StartingPreset : ScriptableObject
{
    public string presetName;
    public List<GameObject> ownedPegs;
    public List<GameObject> ownedBalls;

    public int lFlips;
    public int rFlips;

    public int ballShopSlots;
    public int pegShopSlots;

    public int maxBalls;
}
