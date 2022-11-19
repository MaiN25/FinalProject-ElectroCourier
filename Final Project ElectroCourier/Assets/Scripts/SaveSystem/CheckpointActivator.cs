using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointActivator : MonoBehaviour
{
    SaveLoadData sld;
    SpriteRenderer sr;

    void Start()
    {
        sld = GameObject.FindObjectOfType<SaveLoadData>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        sld.CheckpointSave();
        StartCoroutine("SaveEffect");
    }

    private IEnumerable SaveEffect()
    {
        sr.color = Color.blue;
        yield return new WaitForSeconds(0.25f);
        sr.color = Color.white;
        StopCoroutine("SaveEffect");
    }
}
