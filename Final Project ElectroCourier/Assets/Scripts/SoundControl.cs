using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundControl : MonoBehaviour
{
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

    public void SilenceBackSFX()
    {
        singletonAudio.Stop();
    }
    public void PlayBackSFX()
    {
        singletonAudio.Play();
    }



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
