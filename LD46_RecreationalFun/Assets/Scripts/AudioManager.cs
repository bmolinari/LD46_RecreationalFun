using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance = null;
    public AudioSource audioSource;

    public AudioClip shopkeeper1;
    public AudioClip shopkeeper2;
    public AudioClip shopkeeper3;
    public AudioClip shortPour;
    public AudioClip longPour;
    public AudioClip openCan;
    public AudioClip gunShot;
    public AudioClip burp1;
    public AudioClip burp2;
    public AudioClip hit;
    public AudioClip death;
    public AudioClip coinExchange;
    public AudioClip buttonClip;

    [Header("Pitch")]
    public float minPitchValue;
    public float maxPitchValue;

    [Header("Volume")]
    public float minVolumeValue;
    public float maxVolumeValue;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(this);
    }

    public void PlayClip(AudioClip clip)
    {
        audioSource.pitch = 1;
        audioSource.volume = 1;
        audioSource.PlayOneShot(clip);
    }

    public void PlayRandomizedClip(AudioClip clip)
    {
        RandomizeSound();
        audioSource.PlayOneShot(clip);
    }

    private void RandomizeSound()
    {
        audioSource.pitch = Random.Range(minPitchValue, maxPitchValue);
        audioSource.volume = Random.Range(minVolumeValue, maxVolumeValue);
    }

    public void PlayShortPour()
    {
        PlayClip(shortPour);
    }

    public void PlayLongPour()
    {
        PlayClip(longPour);
    }

    public void PlayOpenCan()
    {
        PlayClip(openCan);
    }

    public void PlayGunshot()
    {
        PlayRandomizedClip(gunShot);
    }

    public void PlayBurp1()
    {
        PlayClip(burp1);
    }
    public void PlayBurp2()
    {
        PlayClip(burp2);
    }

    public void PlayHit()
    {
        PlayRandomizedClip(hit);
    }
    public void PlayDeath()
    {
        PlayClip(death);
    }

    public void PlayCoinExchange()
    {
        PlayClip(coinExchange);
    }
    public void PlayButtonClip()
    {
        PlayRandomizedClip(buttonClip);
    }

    public void PlayShopKeeper1Random()
    {
        PlayRandomizedClip(shopkeeper1);
    }
    public void PlayShopKeeper2Random()
    {
        PlayRandomizedClip(shopkeeper2);
    }
    public void PlayShopKeeper3Random()
    {
        PlayRandomizedClip(shopkeeper3);
    }

    public void PlayRandomDrinkSound()
    {
        int sound = Random.Range(0, 4);
        switch(sound)
        {
            case 0:
                PlayShortPour();
                break;
            case 1:
                PlayOpenCan();
                break;
            case 2:
                PlayBurp1();
                break;
            case 3:
                PlayBurp2();
                break;
            default:
                PlayShortPour();
                break;
        }
    }
}
