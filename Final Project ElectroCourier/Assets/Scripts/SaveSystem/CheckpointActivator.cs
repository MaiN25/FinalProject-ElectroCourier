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
        sr = this.gameObject.GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        sld.CheckpointSave();
        StartCoroutine("SaveEffect");
    }

    private IEnumerator SaveEffect()
    {
        sr.color = Color.cyan;
        yield return new WaitForSeconds(0.25f);
        sr.color = Color.white;
        StopCoroutine("SaveEffect");
    }
}
