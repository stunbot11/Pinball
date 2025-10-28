using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public PlayerStats stats;
    public TextMeshProUGUI points;
    public TextMeshProUGUI rBallTxt;
    public TextMeshProUGUI rPegTxt;


    public List<BuyableObject> balls;
    public List<BuyableObject> pegs;
    public List<BuyableObject> mods;

    public GameObject shopItemTemplate;
    public GameObject ballShop;
    public GameObject pegShop;
    public GameObject modShop;
    public GameObject[] ballShopItem;
    public GameObject[] pegShopItem;
    public GameObject[] modShopItem;

    public BuyableObject[] currentBallShopItemData;
    public BuyableObject[] currentPegShopItemData;
    public BuyableObject[] currentModShopItemData;

    [Header("Costs/Chances")]
    public float rBallCost = 5;
    public float ballEnchantChance;
    public float rPegCost = 5;
    public float pegEnchantChance;
    public float refreshMult = 1.5f;
    public float modEnchantChance;

    private void Start()
    {
        stats = GameObject.Find("PlayerStats").GetComponent<PlayerStats>();
        points.text = "Tickets: " + stats.tickets;
        rBallTxt.text = "Refresh: " + rBallCost;
        rPegTxt.text = "Refresh: " + rPegCost;
        ballShopItem = new GameObject[stats.ballShopSlots];
        pegShopItem = new GameObject[stats.pegShopSlots];
        currentBallShopItemData = new BuyableObject[stats.ballShopSlots];
        currentPegShopItemData = new BuyableObject[stats.pegShopSlots];
        loadBalls();
        loadPegs();
    }

    public void loadBalls()
    { //clears the shop of any possible things then spawns new buyable objects
        for (int i = 0; i < stats.ballShopSlots; i++)
        {
            if (ballShopItem[i] != null)
                Destroy(ballShopItem[i]);
            if (currentBallShopItemData[i] != null)
                Destroy(currentBallShopItemData[i]);

            int rNum = Random.Range(0, balls.Count);
            currentBallShopItemData[i] = balls[rNum];
            ballShopItem[i] = Instantiate(shopItemTemplate, ballShop.transform);
            ballShopItem[i].transform.localPosition = new Vector2(-200 + (200 * i), -25);
            ballShopItem[i].GetComponentInChildren<BoxCollider2D>().GetComponent<TextMeshProUGUI>().text = balls[rNum].name;
            ballShopItem[i].name = balls[rNum].name;
            ballShopItem[i].GetComponentInChildren<CapsuleCollider2D>().GetComponent<Image>().sprite = balls[rNum].Icon;
            ballShopItem[i].GetComponentInChildren<CircleCollider2D>().GetComponent<TextMeshProUGUI>().text = balls[rNum].baseCost.ToString();
            int ballnum = i;
            bool enchanted = Random.Range(0, 1f) <= ballEnchantChance;
            if (enchanted)
            {
                BallEnchants thisEnchant = new BallEnchants();
                thisEnchant.ranEnchant();
                ballShopItem[i].GetComponentInChildren<PolygonCollider2D>().GetComponent<Image>().color = thisEnchant.shopColor;
            }
            ballShopItem[i].GetComponentInChildren<Button>().onClick.AddListener(() => buy(currentBallShopItemData[ballnum], enchanted));
            balls[rNum].shopItem = ballShopItem[i];
        }
    }

    public void refreshBalls()
    {
        if (stats.tickets >= rBallCost)
        {
            stats.tickets -= rBallCost;
            rBallCost = System.MathF.Round(rBallCost * refreshMult, 1);
            rBallTxt.text = "Refresh: " + rBallCost;
            points.text = "Tickets: " + stats.tickets;
            loadBalls();
        }
    }

    public void loadPegs()
    { //clears the shop of any possible things then spawns new buyable objects
        for (int i = 0; i < stats.pegShopSlots; i++)
        {
            if (pegShopItem[i] != null)
                Destroy(pegShopItem[i]);
            if (currentPegShopItemData[i] != null)
                Destroy(currentPegShopItemData[i]);

            int rNum = Random.Range(0, pegs.Count);
            currentPegShopItemData[i] = pegs[rNum];
            pegShopItem[i] = Instantiate(shopItemTemplate, pegShop.transform);
            pegShopItem[i].transform.localPosition = new Vector2(-200 + (200 * i), -25);
            pegShopItem[i].GetComponentInChildren<BoxCollider2D>().GetComponent<TextMeshProUGUI>().text = pegs[rNum].name;
            pegShopItem[i].name = pegs[rNum].name;
            pegShopItem[i].GetComponentInChildren<CapsuleCollider2D>().GetComponent<Image>().sprite = pegs[rNum].Icon;
            pegShopItem[i].GetComponentInChildren<CircleCollider2D>().GetComponent<TextMeshProUGUI>().text = pegs[rNum].baseCost.ToString();
            int pegnum = i;
            bool enchanted = Random.Range(0, 1f) <= pegEnchantChance;
            if (enchanted)
            {
                PegEnchants thisEnchant = new PegEnchants();
                thisEnchant.ranEnchant();
                pegShopItem[i].GetComponentInChildren<PolygonCollider2D>().GetComponent<Image>().color = thisEnchant.shopColor;
            }
            pegShopItem[pegnum].GetComponentInChildren<Button>().onClick.AddListener(() => buy(currentPegShopItemData[pegnum], enchanted));
            pegs[rNum].shopItem = pegShopItem[i];
            
        }
    }

    public void refreshPegs()
    {
        if (stats.tickets >= rPegCost)
        {
            stats.tickets -= rPegCost;
            rPegCost = System.MathF.Round(rPegCost * refreshMult, 1);
            rPegTxt.text = "Refresh: " + rPegCost;
            points.text = "Tickets: " + stats.tickets;
            loadPegs();
        }
    }

    public void loadMods()
    { //clears the shop of any possible things then spawns new buyable objects
        for (int i = 0; i < stats.modShopSlots; i++)
        {
            if (modShopItem[i] != null)
                Destroy(modShopItem[i]);
            if (currentModShopItemData[i] != null)
                Destroy(currentModShopItemData[i]);

            int rNum = Random.Range(0, mods.Count);
            currentModShopItemData[i] = mods[rNum];
            modShopItem[i] = Instantiate(shopItemTemplate, modShop.transform);
            modShopItem[i].transform.localPosition = new Vector2(-200 + (200 * i), -25);
            modShopItem[i].GetComponentInChildren<BoxCollider2D>().GetComponent<TextMeshProUGUI>().text = mods[rNum].name;
            modShopItem[i].name = mods[rNum].name;
            modShopItem[i].GetComponentInChildren<CapsuleCollider2D>().GetComponent<Image>().sprite = mods[rNum].Icon;
            modShopItem[i].GetComponentInChildren<CircleCollider2D>().GetComponent<TextMeshProUGUI>().text = mods[rNum].baseCost.ToString();
            int modNum = i;
            modShopItem[i].GetComponentInChildren<Button>().onClick.AddListener(() => buy(currentModShopItemData[modNum]));
            mods[rNum].shopItem = modShopItem[i];
        }
    }

    public void buy(BuyableObject data, bool enchanted = false)
    {
        if (stats.tickets >= data.baseCost)
        {
            switch (data.shopType)
            {
                case BuyableObject.type.Ball:
                    if (stats.ownedBalls.Count < stats.maxBalls)
                    {
                        stats.ownedBalls.Add(data.buyable);
                        if (enchanted)
                            stats.ownedBalls[stats.ownedBalls.Count - 1].AddComponent<BallEnchants>();
                        stats.tickets -= data.baseCost;
                        Destroy(EventSystem.current.currentSelectedGameObject.transform.parent.gameObject);
                    }
                    else
                        print("empty ur pockets u loot goblin");

                        break;

                case BuyableObject.type.Peg:
                    stats.ownedPegs.Add(data.buyable);
                    if (enchanted)
                        stats.ownedPegs[stats.ownedPegs.Count - 1].AddComponent<PegEnchants>();
                    stats.tickets -= data.baseCost;
                    Destroy(EventSystem.current.currentSelectedGameObject.transform.parent.gameObject);
                    break;
            }
            points.text = "Tickets: " + stats.tickets;
            
        }
        else
            print("wow loser, you broke");
    }
}
