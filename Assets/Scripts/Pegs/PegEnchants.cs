using UnityEngine;

public class PegEnchants : MonoBehaviour
{
    Pegs peg;
    public enchant enchants;
    public enum enchant
    {
        moving,
        doubleEffect
    }

    private void Start()
    {
        peg = GetComponent<Pegs>();
        peg.enchants = this;
    }
}
