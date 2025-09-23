using UnityEngine;

public class Ball : MonoBehaviour
{
    GameManager gameManager;
    public BallEffects effects;
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
            /*
            switch (effects.n) 
            {
                case 
            }
            */
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
