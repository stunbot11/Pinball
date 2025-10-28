using UnityEngine;

public class Pegs : MonoBehaviour
{
    GameManager gameManager;
    [HideInInspector] public Rigidbody2D rb;
    public PegEffects effect;
    public PegEnchants enchants;

    public float ppb; //Points per bounce

    public int effectCounter;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void effectTrigger(GameObject currentBall)
    {
        if (effect == null || effectCounter >= effect.maxAmount)
            return;
        effectCounter++;
        switch (effect.effect)
        {
            case PegEffects.pegEffect.ballSplitter:
                gameManager.ballsAlive++;
                currentBall.transform.localScale /= 1.2f;
                Instantiate(currentBall, currentBall.transform.position, Quaternion.identity, null);
                break;
            case PegEffects.pegEffect.ballLoader:
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            effectTrigger(collision.gameObject);
        }
    }
}
