using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Handle the text dialogue   
public class TempTrigger : MonoBehaviour
{
    // The global instance for other scripts to reference
    public static TempTrigger instance = null;

    public GameObject textBoxPrefab;

    private void Awake()
    {
        float size = PlayerPrefs.GetFloat("Textsize"); ;
        Vector3 scale = new Vector3(size + 0.5f, size + 0.5f, size + 0.5f);
        textBoxPrefab.transform.localScale = scale;
    }
    public void SetTextBoxSize(Vector3 size)
    {
        textBoxPrefab.transform.localScale = size;
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            textBoxPrefab.SetActive(true);
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            textBoxPrefab.SetActive(false);
        }
    }
}
