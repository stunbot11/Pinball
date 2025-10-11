using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    GameManager gameManager;
    public List<BallEffects> effects;
    public float ppb;
    public float mult;
    public float points;
    public float lastNum;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void ballEffect(int i)
    {
        switch (effects[i].effect)
        {
            case BallEffects.ballEffect.addPPB:
                ppb += effects[i].effectAmount;

                break;

            case BallEffects.ballEffect.phibonnachee:
                float tempNum = ppb;
                print("ppb" + ppb);
                print(lastNum);
                points = ppb;
                ppb += lastNum;
                lastNum = tempNum;
                
                break;

            case BallEffects.ballEffect.growth:
                if (transform.localScale.x * effects[i].effectAmount < effects[i].maxAmount)
                {
                    transform.localScale *= effects[i].effectAmount;
                }
                else
                    transform.localScale = new Vector3(effects[i].maxAmount, effects[i].maxAmount, effects[i].maxAmount);
                break;

            default:
                Debug.LogWarning(effects[i].effect + " doesn't have code/ isnt set properly");
                break;
        }
        if (ppb > effects[i].maxAmount && effects[i].maxAmount != 0)
            ppb = effects[i].maxAmount;
    }

    public void effectChecker(string thingToCheckFor)
    {
        if (effects != null)
        { 
            for (int i = 0; i < effects.Count; i++)
            {
                switch (thingToCheckFor)
                {
                    case "onLoad":
                        ballEffect(i);
                        break;

                    case "onHit":
                        ballEffect(i);
                        break;

                    case "onLoss":
                        ballEffect(i);
                        break;

                    case "onChance":
                        ballEffect(i);
                        break;

                    case "onCrit":
                        ballEffect(i);
                        break;
                }
            }
        }
    }

    public void loaded()
    {
        effectChecker("onLoad");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Peg")
        {
            collision.collider.GetComponent<Animation>().Play();
            points += (ppb + collision.collider.GetComponent<Pegs>().ppb) * mult;
            gameManager.ballHitPeg(transform.position, points);
            effectChecker("onHit");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "KillZone")
        {
            effectChecker("onLoss");
            gameManager.updatePoints(points);
            gameManager.insertball();
            Destroy(gameObject);
        }
        else if (collision.tag == "Start")
            gameManager.playerControlls.ballInPlay = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Start")
            gameManager.playerControlls.ballInPlay = true;
    }
}
