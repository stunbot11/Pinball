using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    GameManager gameManager;
    public List<BallEffects> effects;
    public float ppb;
    public float mult;
    public float points;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Peg")
        {
            collision.collider.GetComponent<Animation>().Play();
            points += (ppb + collision.collider.GetComponent<Pegs>().ppb) * mult;
            gameManager.ballHitPeg(transform.position, points);

            if (effects != null)
            {
                for (int i = 0; i < effects.Count; i++)
                {
                    switch (effects[i].effect)
                    {
                        case BallEffects.ballEffect.addPPB:

                            if (ppb + effects[i].effectAmount < effects[i].maxAmount || effects[i].maxAmount == 0)
                                ppb += effects[i].effectAmount;
                            else if (ppb + effects[i].effectAmount > effects[i].maxAmount)
                                ppb = effects[i].maxAmount;

                                break;

                        case BallEffects.ballEffect.phibonnachee:
                            print("phibonnachee is being worked on");
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
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "KillZone")
        {
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
