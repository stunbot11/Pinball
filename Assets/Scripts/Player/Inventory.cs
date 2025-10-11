using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    private PlayerStats stats;

    public GameObject newInvItem;
    public GameObject ballInv;
    public GameObject pegInv;

    public List<string> ballNames;
    public List<string> pegNames;

    private void Start()
    {
        stats = GameObject.Find("PlayerStats").GetComponent<PlayerStats>();
        loadInventory();
    }
    public void loadInventory()
    {
        int num = 0;
        for (int i = 0; i < stats.ownedBalls.Count; i++) //loads the amount of balls you have into the inventory
        {
            if (!ballNames.Contains(stats.ownedBalls[i].name))
            {
                ballNames.Add(stats.ownedBalls[i].name);
                GameObject newItem = Instantiate(newInvItem, ballInv.transform);
                newItem.transform.localPosition = new Vector3(-300 + (150 * (num < 5 ? num : num - 5)), (num < 5 ? 100 : -150), 0);
                newItem.GetComponentInChildren<BoxCollider2D>().GetComponent<TextMeshProUGUI>().text = stats.ownedBalls[i].name;
                newItem.GetComponentInChildren<CircleCollider2D>().GetComponent<TextMeshProUGUI>().text = nameCount(stats.ownedBalls[i].name, stats.ownedBalls) + "x";
                newItem.GetComponentInChildren<Image>().sprite = stats.ownedBalls[i].GetComponent<SpriteRenderer>().sprite;
                num++;
            }
        }

        for (int i = 0; i < stats.ownedPegs.Count; i++) //loads the amount of pegs you have into the inventory
        {
            if (!pegNames.Contains(stats.ownedPegs[i].name))
            {
                pegNames.Add(stats.ownedPegs[i].name);
                GameObject newItem = Instantiate(newInvItem, pegInv.transform);
                newItem.transform.localPosition = new Vector3(-300 + (150 * (i < 5 ? i : i - 5)), (i < 5 ? 100 : -150), 0);
                newItem.GetComponentInChildren<BoxCollider2D>().GetComponent<TextMeshProUGUI>().text = stats.ownedPegs[i].name;
                newItem.GetComponentInChildren<CircleCollider2D>().GetComponent<TextMeshProUGUI>().text = nameCount(stats.ownedPegs[i].name, stats.ownedPegs) + "x";
                newItem.GetComponentInChildren<Image>().sprite = stats.ownedPegs[i].GetComponent<SpriteRenderer>().sprite;
            }
        }
    }
    private int nameCount(string nameNeeded, List<GameObject> objects)
    {
        return (objects.Count(name => name.name == nameNeeded));
    }
}
