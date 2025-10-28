using UnityEngine;

public class BallEnchants : MonoBehaviour
{
    Ball ball;
    public Color shopColor;
    public enchant enchants;
    public enum enchant
    {
        moving,
        doubleEffect
    }

    private void Start()
    {
        ball = GetComponent<Ball>();
        ball.enchants = this;
    }

    public void ranEnchant()
    {
        int enchNum = Random.Range(0, 6);
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
}
