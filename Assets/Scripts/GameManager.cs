using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public PlayerControlls playerControlls;
    [HideInInspector] public PlayerStats stats;

    private bool[] availablePegs;
    public Transform[] pegSlots;
    public List<Transform> currentPegSlots;
    public List<GameObject> currentPegs;

    public List<GameObject> ballsLeft;
    public GameObject ballSpawnPoint;

    public float points;
    public TextMeshProUGUI pointsTxt;
    public TextMeshProUGUI ballsLeftTxt;

    public float quota;
    public TextMeshProUGUI quotaTxt;

    public TextMeshProUGUI LUtxt;
    public TextMeshProUGUI RUtxt;

    public GameObject wrldSpcCnvs;
    public GameObject floatingText;

    [Header("scene difficulty stuff")]
    public float reward;

    private void Start()
    {
        stats = GameObject.Find("PlayerStats").GetComponent<PlayerStats>();
        playerControlls = GameObject.Find("Controlls").GetComponent<PlayerControlls>();
        pegSlots = GameObject.Find("Available Pegs").GetComponentsInChildren<Transform>();
        availablePegs = new bool[pegSlots.Length];
        for (int i = 0; i < pegSlots.Length; i++)
        {
            currentPegSlots.Add(pegSlots[i]);
        }

        quota *= (stats.floor + 1) * stats.quotaMult;
        quotaTxt.text = "Quota: " + quota;

        pointsTxt.text = "Points: " + points;

        reward = (stats.floor + 1) * 5 * stats.quotaMult;

        loadBalls();
        loadBoard();
        insertball();
        //Time.timeScale = .25f;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.G) && Input.GetKey(KeyCode.F) && Input.GetKey(KeyCode.P) && Input.GetKey(KeyCode.N))
        {
            points++;
            pointsTxt.text = "Points: " + points;
        }

        if (Input.GetKey(KeyCode.E) && Input.GetKey(KeyCode.R) && Input.GetKey(KeyCode.N) && Input.GetKey(KeyCode.P))
        {
            ballsLeft.Clear();
            insertball();
        }
            
    }

    public void loadBoard()
    { //shuffles peg list
        List<GameObject> temp = new List<GameObject>();
        int length = stats.ownedPegs.Count;

        for (int i = 0; i < stats.ownedPegs.Count; i++)
            temp.Add(stats.ownedPegs[i]);

        for (int i = 0; i < length; i++)
        {
            int num = Random.Range(0, temp.Count);
            currentPegs.Add(temp[num]);
            temp.RemoveAt(num);
        }

        //load the board with pegs
        for (int i = 0; i < stats.ownedPegs.Count; i++)
        {
            if (i >= availablePegs.Length)
                break;

            int randomNum = Random.Range(0, currentPegSlots.Count);
            GameObject p = Instantiate(currentPegs[i]);
            p.transform.position = currentPegSlots[randomNum].position;
            p.transform.position += new Vector3(Random.Range(-.25f, .25f), Random.Range(-.25f, .25f), 0);
            //currentPegSlots.Remove(currentPegSlots[randomNum]);
            currentPegSlots.RemoveAt(randomNum);
        }
    }

    public void loadBalls()
    {
        if (ballsLeft.Count > 0)
            ballsLeft.Clear();

        for (int i = 0; i < stats.ownedBalls.Count; i++)
        {
            ballsLeft.Add(stats.ownedBalls[i]);
        }
        shuffleBalls();
    }

    public void shuffleBalls()
    { // use temp list to put array into list to then select a random entry to put as first
        List<GameObject> temp = new List<GameObject>();
        int length = ballsLeft.Count;

        for (int i = 0; i < ballsLeft.Count; i++)
            temp.Add(ballsLeft[i]);
            
        ballsLeft.Clear();

        for (int i = 0; i < length; i++)
        {
            int num = Random.Range(0, temp.Count);
            ballsLeft.Add(temp[num]);
            temp.RemoveAt(num);
        }
    }

    public void insertball()
    {
        if (ballsLeft.Count > 0)
        {
            GameObject b = Instantiate(ballsLeft[0]);
            b.transform.position = ballSpawnPoint.transform.position;
            playerControlls.ball = b;
            playerControlls.ballInPlay = false;
            ballsLeft.Remove(ballsLeft[0]);
            ballsLeftTxt.text = "Balls: " + ballsLeft.Count;
            playerControlls.leftUses = playerControlls.maxLU;
            playerControlls.rightUses = playerControlls.maxRU;
            LUtxt.text = "Left Uses Left: " + playerControlls.leftUses;
            RUtxt.text = "Right Uses Left: " + playerControlls.rightUses;
        }
        else if (points >= quota)
        {
            stats.tickets += (stats.tickets % 5 == 0) ? (int)(stats.tickets / 5) : 0;
            stats.tickets += reward + (points - quota > 0 ? Mathf.RoundToInt((points - quota) / (5 * (stats.floor + 1))) : 0);
            playerControlls.controls.Disable();
            //stats.round++;
            print("SHOP TIME!!!!!!!!!");
            SceneManager.LoadScene("Shop");
        }
        else
        {
            if (GameObject.Find("Map Holder(Clone)") != null)
                Destroy(GameObject.Find("Map Holder(Clone)"));
            playerControlls.controls.Disable();
            print("you lost...");
            SceneManager.LoadScene("Lose Screen");
        }
    }

    public void updatePoints(float pointsToAdd)
    {
        float fakePoints = points;
        points += pointsToAdd;
        StartCoroutine(rollPoints(fakePoints, pointsToAdd));
    }

    IEnumerator rollPoints(float basePoints, float pointsToAdd)
    {
        float timeBetween = .5f / pointsToAdd;
        for (int i = 0; i < pointsToAdd; i++)
        {
            basePoints++;
            pointsTxt.text = "Points: " + basePoints;
            yield return new WaitForSeconds(timeBetween);
        }
    }

    public void ballHitPeg(Vector2 pos, float points)
    {
        GameObject txt = Instantiate(floatingText, pos, Quaternion.identity, wrldSpcCnvs.transform);
        txt.GetComponent<TextMeshProUGUI>().text = points.ToString();
        txt.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(Random.Range(-1f, 1f), Random.Range(.5f, 2f));
        txt.GetComponent<Rigidbody2D>().rotation = Random.Range(-30f, 30f);
        Destroy(txt, 2);
    }
}
