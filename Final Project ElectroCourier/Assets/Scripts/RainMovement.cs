using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainMovement : MonoBehaviour
{
    bool rainActivated = false;
    float speed = 0.05f;

    private void Update()
    {
        if (rainActivated)
        {
            Move();
        }
    }

    private void Move()
    {
        this.transform.Translate(Vector2.right * speed);
    }

    public void FlipSwitch()
    {
        rainActivated = !rainActivated;
        this.transform.GetChild(0).gameObject.SetActive(rainActivated);
    }
}
