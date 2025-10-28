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
    public List<GameObject> invItems;

    public void loadInventory(bool canDelete = false)
    {
        if (stats == null)
            stats = GameObject.Find("PlayerStats").GetComponent<PlayerStats>();
        if (invItems.Count > 0)
        {
            ballNames.Clear();
            pegNames.Clear();
            clearInventory();
        }
        
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
                invItems.Add(newItem);
                num++;
            }
        }

        int num2 = 0;
        for (int i = 0; i < stats.ownedPegs.Count; i++) //loads the amount of pegs you have into the inventory
        {
            if (!pegNames.Contains(stats.ownedPegs[i].name))
            {
                pegNames.Add(stats.ownedPegs[i].name);
                GameObject newItem = Instantiate(newInvItem, pegInv.transform);
                newItem.transform.localPosition = new Vector3(-300 + (150 * (num2 < 5 ? num2 : num2 - 5)), (num2 < 5 ? 100 : -150), 0);
                newItem.GetComponentInChildren<BoxCollider2D>().GetComponent<TextMeshProUGUI>().text = stats.ownedPegs[i].name;
                newItem.GetComponentInChildren<CircleCollider2D>().GetComponent<TextMeshProUGUI>().text = nameCount(stats.ownedPegs[i].name, stats.ownedPegs) + "x";
                newItem.GetComponentInChildren<Image>().sprite = stats.ownedPegs[i].GetComponent<SpriteRenderer>().sprite;
                invItems.Add(newItem);
                num2++;
            }
        }
    }

    public void clearInventory()
    {
        for (int i = invItems.Count - 1; i >= 0; i--)
            Destroy(invItems[i]);
        invItems.Clear();
    }
    private int nameCount(string nameNeeded, List<GameObject> objects)
    {
        return (objects.Count(name => name.name == nameNeeded));
    }
}
