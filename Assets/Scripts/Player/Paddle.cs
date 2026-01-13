using UnityEngine;

public class Paddle : MonoBehaviour
{
    public void Move(bool isActive)
    {
        GetComponent<HingeJoint2D>().useMotor = !isActive;
    }
}
