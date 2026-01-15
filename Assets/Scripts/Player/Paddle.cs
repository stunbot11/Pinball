using UnityEngine;

public class Paddle : MonoBehaviour
{
    private HingeJoint2D j2D;
    private float speed;

    private void Start()
    {
        j2D = GetComponent<HingeJoint2D>();
        speed = j2D.motor.motorSpeed;
    }
    public void Move(bool isActive)
    {
        JointMotor2D motor = j2D.motor;
        motor.motorSpeed = speed * (isActive ? -1 : 1);
        j2D.motor = motor;
    }
}
