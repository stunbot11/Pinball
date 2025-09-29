using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public StartingPreset preset;
    public List<GameObject> ownedPegs;
    public List<GameObject> ownedBalls;

    [Header("Play stuff")]
    public float points;
    public int lFlips;
    public int rFlips;
    public int round;

    public int maxBalls;

    [Header("Shop stuff")]
    public int ballShopSlots;
    public int pegShopSlots;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("Main Menu"))
        {
            loadPreset();
        }
    }

    public void setPreset(StartingPreset inputPreset)
    {
        preset = inputPreset;
    }

    public void loadPreset()
    {
        for (int i = 0; i < preset.ownedPegs.Count; i++)
            ownedPegs.Add(preset.ownedPegs[i]);

        for (int i = 0; i < preset.ownedBalls.Count; i++)
            ownedBalls.Add(preset.ownedBalls[i]);

        lFlips = preset.lFlips;
        rFlips = preset.rFlips;

        ballShopSlots = preset.ballShopSlots;
        pegShopSlots = preset.pegShopSlots;

        maxBalls = preset.maxBalls;
    }
}
