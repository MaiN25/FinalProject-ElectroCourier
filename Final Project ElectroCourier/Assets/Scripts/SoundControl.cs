using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundControl : MonoBehaviour
{
    // The global instance for other scripts to reference
    public static SoundControl instance = null;

    // singletonAudio follows the SaveLoadData gameobject, so it continues through each level of  the game
    // The main menu AudioSource and the Win/Lose AudioSources are currently separate objects
    AudioSource singletonAudio;

    public AudioClip pickupSFX;
    public AudioClip healSFX;
    public AudioClip playerHurtSFX;
    public AudioClip enemyDeathSFX;
    public AudioClip checkpointSFX;

    float sfxVolume = 2f;

    private void Start()
    {
        singletonAudio = this.gameObject.GetComponent<AudioSource>();
    }

    // Return the background music AudioSource for other scripts to reference
    public AudioSource ReturnAudioSource()
    {
        return singletonAudio;
    }

    // Game background music
    public void SilenceBackSFX()
    {
        singletonAudio.Stop();
    }
    public void PlayBackSFX()
    {
        singletonAudio.Play();
    }


    // Game element sound effects
    public void PickupSFX()
    {
        singletonAudio.PlayOneShot(pickupSFX, 1f);
    }
    public void HealSFX()
    {
        singletonAudio.PlayOneShot(healSFX, sfxVolume);
    }
    public void PlayerHurtSFX()
    {
        singletonAudio.PlayOneShot(playerHurtSFX, sfxVolume);
    }
    public void EnemyDeathSFX()
    {
        singletonAudio.PlayOneShot(enemyDeathSFX, sfxVolume);
    }
    public void CheckpointSFX()
    {
        singletonAudio.PlayOneShot(checkpointSFX, sfxVolume);
    }
}
