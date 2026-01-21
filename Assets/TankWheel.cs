using UnityEngine;

public class TankWheel : MonoBehaviour
{
    public float rotationSpeed = 100f;
    public Joystick moveJoystick;
    void Update()
    {

        float movementKeyboard = Input.GetAxis("Vertical"); 
        float movementJoystick = moveJoystick.Vertical;
        if (movementKeyboard > 0 || movementJoystick > 0 ) 
        {
            RotateWheel(-rotationSpeed);
        }
        else if (movementKeyboard < 0 ||  movementJoystick < 0 ) 
        {
            RotateWheel(rotationSpeed);
        }
    }

    void RotateWheel(float rotationAmount)
    {
        transform.Rotate(rotationAmount, 0f * Time.deltaTime, 0f);
    }
  
}