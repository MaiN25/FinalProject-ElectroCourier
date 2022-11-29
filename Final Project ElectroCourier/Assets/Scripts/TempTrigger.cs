using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempTrigger : MonoBehaviour
{
    public GameObject textBoxPrefab;
    public GameObject textPrefab;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            textBoxPrefab.SetActive(true);
           // textPrefab.SetActive(true);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            textBoxPrefab.SetActive(false);
           // textPrefab.SetActive(false);
        }
    }
}
