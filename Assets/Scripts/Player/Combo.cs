using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Combo : MonoBehaviour
{
    private Color[] colors = {Color.red, Color.orange, Color.yellow, Color.green, Color.blue, Color.purple, Color.violet, Color.brown, Color.pink, Color.cyan, Color.white, Color.royalBlue, Color.rosyBrown, Color.skyBlue, Color.hotPink, Color.rebeccaPurple, Color.darkRed, Color.orangeRed, Color.orchid, Color.limeGreen, Color.forestGreen};
    public GameObject comboBox;
    public Image comboMeter;
    public TextMeshProUGUI hitsTXT;
    public TextMeshProUGUI multTXT;
    public Vector2 baseSize;
    public float maxTime;
    public float timer;
    public float mult;
    public float multGain;
    public float sizeReductionTime;
    public int baseHitsNeeded;
    public int hitsNeeded;
    public int hits;
    public int level;



    private void Start()
    {
        baseSize = new Vector2(transform.localScale.x, transform.localScale.y);
    }
    private void Update()
    {
        timer -= Time.deltaTime;
        comboMeter.fillAmount = timer / maxTime;
        if (timer <= 0)
            LoseCombo();
        else
            comboBox.transform.localScale = baseSize * Mathf.Lerp(2.5f, 1, maxTime - timer);
        hitsTXT.text = hits.ToString();
        multTXT.text = mult + "x";
    }

    public void Hit()
    {
        comboBox.SetActive(true);
        ChangeRotation();
        ChangeHitColor();
        timer = maxTime;
        hits++;
        if (hits >= hitsNeeded)
            LevelUp();
    }

    public void ChangeRotation()
    {
        comboBox.transform.rotation = Quaternion.Euler(0f, 0f, Random.Range(-20f, 20f));
    }

    public void ChangeHitColor()
    {
        //hitsTXT.color = new Color(Random.Range(0, 255f), Random.Range(0, 255f), Random.Range(0, 255f));

        hitsTXT.color = colors[Random.Range(0, colors.Length)];
        
    }

    private void LevelUp()
    {
        level++;
        mult += multGain;
        hitsNeeded = Mathf.RoundToInt(hitsNeeded * 1.5f);
    }

    private void LoseCombo()
    {
        comboBox.SetActive(false);
        hitsNeeded = baseHitsNeeded;
        hits = 0;
        mult = 1;
        level = 1;
    }
}
