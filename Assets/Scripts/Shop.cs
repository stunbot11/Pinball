using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public PlayerStats stats;
    public TextMeshProUGUI points;
    public TextMeshProUGUI rBallTxt;
    public TextMeshProUGUI rPegTxt;

    public List<BuyableObject> balls;
    public List<BuyableObject> pegs;

    public GameObject basicShopItem;
    public GameObject ballShop;
    public GameObject pegShop;
    public GameObject[] ballShopItem;
    public GameObject[] pegShopItem;

    public BuyableObject[] currentBallShopItemData;
    public BuyableObject[] currentPegShopItemData;

    [Header("Costs")]
    public float rBallCost = 5;
    public float rPegCost = 5;
    public float refreshMult = 1.5f;

    private void Start()
    {
        stats = GameObject.Find("PlayerStats").GetComponent<PlayerStats>();
        points.text = "Points" + stats.points;
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
            ballShopItem[i] = Instantiate(basicShopItem, ballShop.transform);
            ballShopItem[i].transform.localPosition = new Vector2(-200 + (200 * i), -25);
            ballShopItem[i].GetComponentInChildren<BoxCollider2D>().GetComponent<TextMeshProUGUI>().text = balls[rNum].name;
            ballShopItem[i].name = balls[rNum].name;
            ballShopItem[i].GetComponentInChildren<CapsuleCollider2D>().GetComponent<Image>().sprite = balls[rNum].Icon;
            ballShopItem[i].GetComponentInChildren<CircleCollider2D>().GetComponent<TextMeshProUGUI>().text = balls[rNum].baseCost.ToString();
            int ballnum = i;
            ballShopItem[i].GetComponentInChildren<Button>().onClick.AddListener(() => buy(currentBallShopItemData[ballnum]));
            balls[rNum].shopItem = ballShopItem[i];
        }
    }

    public void refreshBalls()
    {
        if (stats.points >= rBallCost)
        {
            stats.points -= rBallCost;
            rBallCost *= refreshMult;
            rBallTxt.text = "Refresh: " + rBallCost;
            points.text = "Points" + stats.points;
            loadBalls();
        }
    }

    public void loadPegs()
    { //clears the shop of any possible things then spawns new buyable objects
        for (int i = 0; i < stats.ballShopSlots; i++)
        {
            if (pegShopItem[i] != null)
                Destroy(pegShopItem[i]);
            if (currentPegShopItemData[i] != null)
                Destroy(currentPegShopItemData[i]);

            int rNum = Random.Range(0, balls.Count);
            currentPegShopItemData[i] = pegs[rNum];
            pegShopItem[i] = Instantiate(basicShopItem, pegShop.transform);
            pegShopItem[i].transform.localPosition = new Vector2(-200 + (200 * i), -25);
            pegShopItem[i].GetComponentInChildren<BoxCollider2D>().GetComponent<TextMeshProUGUI>().text = pegs[rNum].name;
            pegShopItem[i].name = pegs[rNum].name;
            pegShopItem[i].GetComponentInChildren<CapsuleCollider2D>().GetComponent<Image>().sprite = pegs[rNum].Icon;
            pegShopItem[i].GetComponentInChildren<CircleCollider2D>().GetComponent<TextMeshProUGUI>().text = pegs[rNum].baseCost.ToString();
            int pegnum = i;
            pegShopItem[pegnum].GetComponentInChildren<Button>().onClick.AddListener(() => buy(currentPegShopItemData[pegnum]));
            pegs[rNum].shopItem = pegShopItem[i];
        }
    }

    public void refreshPegs()
    {
        if (stats.points >= rBallCost)
        {
            stats.points -= rPegCost;
            rPegCost *= refreshMult;
            rPegTxt.text = "Refresh: " + rPegCost;
            points.text = "Points" + stats.points;
            loadPegs();
        }
    }

    public void buy(BuyableObject data)
    {
        if (stats.points >= data.baseCost)
        {
            stats.points -= data.baseCost;
            points.text = "Points" + stats.points;
            switch (data.shopType)
            {
                case BuyableObject.type.Ball:
                    stats.ownedBalls.Add(data.buyable);
                    Destroy(data.shopItem);
                    break;

                case BuyableObject.type.Peg:
                    stats.ownedPegs.Add(data.buyable);
                    Destroy(data.shopItem);
                    break;
            }
        }
        else
            print("wow loser, you broke");

    }
}
