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
        loadBalls();
    }

    public void loadBalls()
    { //clears the shop of any possible things then spawns new buyable objects
        for (int i = 0; i < stats.ballShopSlots; i++)
        {
            if (ballShopItem[i] != null)
                Destroy(ballShopItem[i]);

            int rNum = Random.Range(0, balls.Count);
            ballShopItem[i] = Instantiate(basicShopItem, ballShop.transform);
            ballShopItem[i].transform.localPosition = new Vector2(-200 + (200 * i), -25);
            ballShopItem[i].GetComponentInChildren<BoxCollider2D>().GetComponent<TextMeshProUGUI>().text = balls[rNum].name;
            ballShopItem[i].name = balls[rNum].name;
            ballShopItem[i].GetComponentInChildren<CapsuleCollider2D>().GetComponent<Image>().sprite = balls[rNum].Icon;
            ballShopItem[i].GetComponentInChildren<CircleCollider2D>().GetComponent<TextMeshProUGUI>().text = balls[rNum].baseCost.ToString();
            ballShopItem[i].GetComponentInChildren<Button>().onClick.AddListener(() => buy(balls[rNum].ball));
            int ballnum = i;
            ballShopItem[i].GetComponentInChildren<Button>().onClick.AddListener(() => destroy(ballShopItem[ballnum]));
        }
    }

    public void refreshBalls()
    {
        if (stats.points >= rBallCost)
        {
            stats.points -= rBallCost;
            rBallCost *= refreshMult;
            rBallTxt.text = "Refresh: " + rBallCost;
            loadBalls();
        }
    }

    public void loadPegs()
    { //clears the shop of any possible things then spawns new buyable objects
        for (int i = 0; i < stats.ballShopSlots; i++)
        {
            if (ballShopItem[i] != null)
                Destroy(ballShopItem[i]);

            int rNum = Random.Range(0, balls.Count);
            pegShopItem[i] = Instantiate(basicShopItem, pegShop.transform);
            pegShopItem[i].transform.localPosition = new Vector2(-200 + (200 * i), -25);
            pegShopItem[i].GetComponentInChildren<BoxCollider2D>().GetComponent<TextMeshProUGUI>().text = pegs[rNum].name;
            pegShopItem[i].name = balls[rNum].name;
            pegShopItem[i].GetComponentInChildren<CapsuleCollider2D>().GetComponent<Image>().sprite = pegs[rNum].Icon;
            pegShopItem[i].GetComponentInChildren<CircleCollider2D>().GetComponent<TextMeshProUGUI>().text = pegs[rNum].baseCost.ToString();
            pegShopItem[i].GetComponentInChildren<Button>().onClick.AddListener(() => buy(pegs[rNum].ball));
            int ballnum = i;
            pegShopItem[i].GetComponentInChildren<Button>().onClick.AddListener(() => destroy(pegShopItem[ballnum]));
        }
    }

    public void refreshPegs()
    {
        if (stats.points >= rBallCost)
        {
            stats.points -= rPegCost;
            rPegCost *= refreshMult;
            rPegTxt.text = "Refresh: " + rPegCost;
            loadBalls();
        }
    }

    public void buy(GameObject thing)
    {
        stats.ownedBalls.Add(thing);
    }

    public void destroy(GameObject thing)
    {
        Destroy(thing);
    }
}
