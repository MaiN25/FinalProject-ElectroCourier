using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NarrativeTrigger : MonoBehaviour
{
    public GameObject TextBox;

    public TMP_Text[] textLines;
    public TMP_Text line1;
    public TMP_Text line2;
    public TMP_Text line3;

    void Start()
    {
        line1 = textLines[0];
        line2 = textLines[1];
        line3 = textLines[2];
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
