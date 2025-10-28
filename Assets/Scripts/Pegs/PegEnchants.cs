using UnityEngine;

public class PegEnchants : MonoBehaviour
{
    Pegs peg;
    public enchant enchants;
    public Color shopColor;
    public enum enchant
    {
        moving,
        doubleEffect
    }

    public Vector2 startPos;
    public Vector2[] goToPositions;
    public float speed;
    public int currentSpot;

    private void Start()
    {
        peg = GetComponent<Pegs>();
        peg.enchants = this;

        goToPositions = new Vector2[Random.Range(2,5)];
        goToPositions[0] = startPos;
        for (int i = 1; i < goToPositions.Length; i++)
        {
            goToPositions[i] = new Vector2(Random.Range(-1,1), Random.Range(-1, 1));
            goToPositions[i] += (Vector2)transform.localPosition;
        }
    }

    public void ranEnchant()
    {
        int enchNum = Random.Range(0, 2);
        switch (enchNum)
        {
            case 0:
                shopColor = Color.red;
                enchants = enchant.moving;
                break;
            case 1:
                shopColor = Color.green;
                enchants = enchant.doubleEffect;
                break;
            case 2:
                shopColor = Color.blue;
                break;
            case 3:
                shopColor = Color.white;
                break;
            case 4:
                shopColor = Color.purple;
                break;
            case 5:
                shopColor = Color.yellow;
                break;
        }
    }

    

    private void Update()
    {
        if (enchants.Equals(enchant.moving))
        {
            peg.rb.linearVelocity = (goToPositions[currentSpot] - (Vector2)transform.localPosition).normalized * speed * Time.deltaTime;
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
