using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainMovement : MonoBehaviour
{
    public Transform startPos;
    public Transform endPos;
    public Transform player;
    bool rainActivated = false;
    float speed = 0.01f;
    float tolerance = 2f;
    int passedSeconds = 0;
    int maxTimer = 5;
    int iSwitch = 0;

    enum RainState
    {
        noRain,
        stopped,
        start,
        on,
        end
    }
    RainState rainState = RainState.noRain;

    private void Start()
    {
        FlipSwitch();
        Debug.Log(this.gameObject.name + " Position: " + this.transform.position.x);
        Debug.Log(player.name + " Position: " + player.position.x);
        Debug.Log(startPos.name + " Position: " + startPos.position.x);
        Debug.Log(endPos.name + " Position: " + endPos.position.x);
        StartCoroutine("Test");
    }
    private void Update()
    {
        switch (rainState)
        {
            case RainState.stopped:
                Debug.Log("Rain stopped");
                FlipSwitch();
                rainState = RainState.noRain;
                break;
            case RainState.start:
                //Debug.Log("Starting rain");
                if (iSwitch < 1){
                    iSwitch += 1;
                    Debug.Log("Start Timer");
                    StartCoroutine("Timer");
                }
                Move();
                break;
            case RainState.on:
                //Debug.Log("Rain staying with player");
                Debug.Log("Timer count: " + passedSeconds);
                if (passedSeconds == maxTimer)
                {
                    rainState = RainState.end;
                }
                break;
            case RainState.end:
                Debug.Log("Rain leaving area");
                if (iSwitch < 1)
                {
                    iSwitch += 1;
                    StopCoroutine("Timer");
                }
                passedSeconds = 0;
                Move();
                break;
            default:
                break;
        }
        if (this.transform.position.x <= startPos.position.x)
        {
            rainState = RainState.start;
        } else if (this.transform.position.x >= endPos.position.x)
        {
            rainState = RainState.stopped;
        } else if (this.transform.position.x >= player.position.x - tolerance && this.transform.position.x <= player.position.x + tolerance)
        {
            rainState = RainState.on;
        }
    }

    private void Move()
    {
        this.transform.Translate(Vector2.right * speed);
    }

    public IEnumerable Timer()
    {
        Debug.Log("test");
        while (true)
        {
            passedSeconds += 1;
            Debug.Log("Timer count: " + passedSeconds);
            yield return new WaitForSeconds(1);
            Debug.Log("Ahhhhhhh");
        }
    }

    IEnumerable Test()
    {
        while (true)
        {
            Debug.Log("Well, this one works.");
            yield return new WaitForSeconds(1);
        }
    }

    public void FlipSwitch()
    {
        rainActivated = !rainActivated;
        this.transform.GetChild(0).gameObject.SetActive(rainActivated);
    }
}
