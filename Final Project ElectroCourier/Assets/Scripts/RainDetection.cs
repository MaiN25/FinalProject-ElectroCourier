using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainDetection : MonoBehaviour
{
    bool inSafe = false;
    bool inRain = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Safezone"))
        {
            inSafe = true;
        }
        if (collision.CompareTag("Rain"))
        {
            inRain = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Safezone"))
        {
            inSafe = false;
        }
        if (collision.CompareTag("Rain"))
        {
            inRain = false;
        }
    }

    private void Update()
    {
        if (inSafe)
        {
            Debug.Log("Safe!");
        } else if (inRain && !inSafe)
        {
            Debug.Log("Rain...");
        } else if (!inRain && !inSafe)
        {
            Debug.Log("Outside of Rain");
        }
    }
}
