using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public StartingPreset preset;
    public List<StartingPreset> unlockedPresets;
    public List<GameObject> mods;
    public List<GameObject> ownedBalls;
    public List<GameObject> ownedPegs;

    public GameObject presetPrefab;

    [Header("Play stuff")]
    public float quotaMult = 1;
    public float tickets;
    public int lFlips;
    public int rFlips;
    public int floor;

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
