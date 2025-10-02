using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private PlayerStats stats;

    public GameObject newInvItem;
    public GameObject inventory;

    /*
            pegShopItem[i].GetComponentInChildren<BoxCollider2D>().GetComponent<TextMeshProUGUI>().text = pegs[rNum].name;
           pegShopItem[i].name = pegs[rNum].name;
           pegShopItem[i].GetComponentInChildren<CapsuleCollider2D>().GetComponent<Image>().sprite = pegs[rNum].Icon;
           pegShopItem[i].GetComponentInChildren<CircleCollider2D>().GetComponent<TextMeshProUGUI>().text = pegs[rNum].baseCost.ToString();
            */

    public void loadInventory()
    {
        for (int i = 0; i < stats.ownedBalls.Count; i++)
        {
            GameObject newItem = Instantiate(newInvItem, inventory.transform);
            newItem.transform.position += new Vector3(-300 + (50 * (i > 300 ? i - 12 : i)), 50, 0);
            newItem.GetComponentInChildren<BoxCollider2D>().GetComponent<TextMeshProUGUI>().text = stats.ownedBalls[i].name;

        }
    }
}
