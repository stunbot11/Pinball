using UnityEngine;

public class Pegs : MonoBehaviour
{
    Rigidbody2D rb;

    public float ppb; //Points per bounce

    public bool isMoving;
    public Vector2 startPos;
    public Vector2[] goToPositions;
    public float speed;
    public int currentSpot;

    private void Start()
    {
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
}
