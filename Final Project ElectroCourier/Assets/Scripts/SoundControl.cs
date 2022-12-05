using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SoundControl : MonoBehaviour
{
    // The global instance for other scripts to reference
    public static SoundControl instance = null;

    // singletonAudio follows the SaveLoadData gameobject, so it continues through each level of  the game
    // The main menu AudioSource and the Win/Lose AudioSources are currently separate objects
    public AudioSource singletonAudio;

    public AudioClip pickupSFX;
    public AudioClip healSFX;
    public AudioClip playerHurtSFX;
    public AudioClip enemyDeathSFX;
    public AudioClip checkpointSFX;

    string[] captionList = new string[4] { "", "", "", "" };
    public static int captionEnabled = 0;
    public GameObject captionBox;
    public TextMeshProUGUI captionText;


    float sfxVolume = 2f;

    private void Start()
    {
        PlayerPrefs.SetFloat("VolumeValue", 0.5f);
        singletonAudio = this.gameObject.GetComponent<AudioSource>();
    }

    // Game background music
    public void SilenceBackSFX()
    {
        singletonAudio.Stop();
    }
    public void PlayBackSFX()
    {
        Debug.Log("Audio Play");
        singletonAudio.Play();
    }


    // Game element sound effects
    public void PickupSFX()
    {
        singletonAudio.PlayOneShot(pickupSFX, sfxVolume - 1f);
        AddToCaptionList("Got Package");
    }
    public void HealSFX()
    {
        singletonAudio.PlayOneShot(healSFX, sfxVolume);
        AddToCaptionList("Got Health");
    }
    public void PlayerHurtSFX()
    {
        singletonAudio.PlayOneShot(playerHurtSFX, sfxVolume);
        AddToCaptionList("Player Hurt");
    }
    public void EnemyDeathSFX()
    {
        singletonAudio.PlayOneShot(enemyDeathSFX, sfxVolume + 1f);
        AddToCaptionList("Enemy Death");
    }
    public void CheckpointSFX()
    {
        singletonAudio.PlayOneShot(checkpointSFX, sfxVolume);
        AddToCaptionList("Checkpoint!");
    }

    public void CaptionToText()
    {
        captionText.text = captionList[0] + "\n" + captionList[1] + "\n" + captionList[2] + "\n" + captionList[3];
    }

    // When a new sound input is created, replace the last available slot or replace the last one and move the list upward
    void AddToCaptionList(string toAdd)
    {
        for (int i = 0; i < 4; i++)
        {
            if (captionList[i].Equals(""))
            {
                captionList[i] = toAdd;
                CaptionToText();
                return;
            }
        }
        if (!captionList[0].Equals(""))
        {
            MoveList(toAdd);
        }
    }

    // If the oldest list item is still there, replace it
    public IEnumerator ReduceCaptionList()
    {
        for (int i = 0; i < 4; i++)
        {
            if (!captionList[i].Equals(""))
            {
                captionList[i] = "";
                MoveList("");
                break;
            }
        }
        yield return new WaitForSeconds(2.0f);
    }

    void MoveList(string toAdd)
    {
        captionList[0] = captionList[1];
        captionList[1] = captionList[2];
        captionList[2] = captionList[3];
        captionList[3] = toAdd;
        CaptionToText();
    }
}
