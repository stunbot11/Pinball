using UnityEngine;

[CreateAssetMenu(fileName = "New Mod Effect", menuName = "Mod Effect")]
public class MachineMods : ScriptableObject
{
    public modEffect effect;
    public enum modEffect
    {
        PPBAdition,
        scoreMult,
        flipperUses,
        flipperStrength
    }

    public float effectAmount;
}

/*
     * master script with sub scripts
     * bought similarly to other items
     * gives a wide varaity of upgrades
     * -more flipper uses
     * still gives a small effect after getting rid of mod
     */