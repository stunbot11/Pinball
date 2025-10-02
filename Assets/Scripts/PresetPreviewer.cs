using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;

public class PresetPreviewer : MonoBehaviour
{
    public PlayerStats playerStats;
    public TextMeshProUGUI pName;
    public TextMeshProUGUI balls;
    public TextMeshProUGUI pegs;
    public TextMeshProUGUI flips;
    public TextMeshProUGUI ballShopSlots;
    public TextMeshProUGUI pegShopSlots;
    public TextMeshProUGUI maxBalls;

    [HideInInspector] public List<string> ballNames;
    [HideInInspector] public List<string> pegNames;

    public void changePreset(StartingPreset preset)
    {
        playerStats.preset = preset;
        pName.text = preset.name;

        balls.text = "Balls: ";
        for (int i = 0; i < preset.ownedBalls.Count; i++) //gets the amount of each type of ball
        {
            if (!ballNames.Contains(preset.ownedBalls[i].name))
            {
                ballNames.Add(preset.ownedBalls[i].name);
                balls.text += nameCount(preset.ownedBalls[i].name, preset.ownedBalls);
            }
        }
        pegs.text = "Pegs: ";

        for (int i = 0; i < preset.ownedPegs.Count; i++)
        {
            if (!pegNames.Contains(preset.ownedPegs[i].name))
            {
                pegNames.Add(preset.ownedPegs[i].name);
                pegs.text += nameCount(preset.ownedPegs[i].name, preset.ownedPegs);
            }
        }

        flips.text = "R/L Flips: " + preset.lFlips + "/" + preset.rFlips;
        ballShopSlots.text = "Ball Shop Slots: " + preset.ballShopSlots;
        pegShopSlots.text = "Peg Shop Slots: " + preset.pegShopSlots;
        maxBalls.text = "Max Balls: " + preset.maxBalls;

        ballNames.Clear();
        pegNames.Clear();
    }

    private string nameCount(string nameNeeded, List<GameObject> objects)
    {
        return (objects.Count(name => name.name == nameNeeded) + "x " + nameNeeded + ", ");
    }
}
