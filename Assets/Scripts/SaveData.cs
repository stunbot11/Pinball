using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    [SerializeField] public playerInventory inventory = new playerInventory();
    [SerializeField] public unlockedThings unlocks = new unlockedThings();
    public void saveData()
    {
        string inventoryString = JsonUtility.ToJson(inventory);
        string unlocksString = JsonUtility.ToJson(unlocks);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/Inventory.json", inventoryString);
        System.IO.File.WriteAllText(Application.persistentDataPath + "/Unlocked.json", unlocksString);
    }
}

[System.Serializable]
public class playerInventory
{
    public StartingPreset preset;
    public List<GameObject> ownedBalls;
    public List<GameObject> ownedPegs;

    public GameObject presetPrefab;

    [Header("Play stuff")]
    public float points;
    public int lFlips;
    public int rFlips;
    public int round;

    public int maxBalls;

    [Header("Shop stuff")]
    public int ballShopSlots;
    public int pegShopSlots;
}

[System.Serializable]
public class unlockedThings
{
    public List<StartingPreset> unlockedPresets;
    public List<GameObject> unlockedBalls;
    public List<GameObject> unlockedPegs;
    public int highestRound;
    public int highScore;
}