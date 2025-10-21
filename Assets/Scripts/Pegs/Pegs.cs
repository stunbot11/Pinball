using UnityEngine;

public class Pegs : MonoBehaviour
{
    GameManager gameManager;
    Rigidbody2D rb;
    public PegEffects effect;
    public PegEnchants enchants;

    public float ppb; //Points per bounce

    public bool isMoving;
    public Vector2 startPos;
    public Vector2[] goToPositions;
    public float speed;
    public int currentSpot;

    public int effectCounter;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        rb = GetComponent<Rigidbody2D>();
        for (int i = 0; i < goToPositions.Length; i++)
        {
            goToPositions[i] += (Vector2)transform.localPosition;
        }
    }

    private void Update()
    {
        if (isMoving)
        {
            rb.linearVelocity = (goToPositions[currentSpot] - (Vector2)transform.localPosition).normalized * speed * Time.deltaTime;
            if ((Vector2)transform.localPosition == goToPositions[currentSpot])
            {
                if (currentSpot == goToPositions.Length - 1)
                    currentSpot = 0;
                else
                    currentSpot++;
            }
        }
    }

    public void effectTrigger(GameObject currentBall)
    {
        if (effectCounter >= effect.maxAmount)
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
