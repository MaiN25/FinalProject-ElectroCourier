using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointActivator : MonoBehaviour
{
    SaveLoadData sld;
    SoundControl sc;
    SpriteRenderer sr;

    void Start()
    {
        sld = GameObject.FindObjectOfType<SaveLoadData>();
        sc = GameObject.FindObjectOfType<SoundControl>();
        sr = this.gameObject.GetComponent<SpriteRenderer>();
    }

    // When a player steps on a checkpoint, the game is saved then both visual and sound effects are activated
    private void OnTriggerEnter2D(Collider2D collision)
    {
        sld.SaveGame();
        StartCoroutine("SaveEffect");
    }

    // When the checkpoint is activated, a tune plays and the checkpoint flashes cyan before returning to its original color
    private IEnumerator SaveEffect()
    {
        sc.CheckpointSFX();
        sr.color = Color.cyan;
        yield return new WaitForSeconds(0.25f);
        sr.color = Color.white;
        StopCoroutine("SaveEffect");
    }
}
