using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject player;
    public GameObject shopTooltip;
    public GameObject salesCounter;
    public GameObject startText;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI messageText;

    public AudioClip clip1;
    public AudioClip clip2;
    public AudioClip clip3;


    public bool isSpeaking = false;
    private float maxTime = 3f;
    private float currentTime = 0f;

    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void Update()
    {
        if (isSpeaking)
        {
            if (currentTime < maxTime)
            {
                currentTime += Time.deltaTime;
            }
            else
            {
                Destroy(salesCounter.GetComponent<BoxCollider2D>());
                foreach (Transform child in salesCounter.transform)
                {
                    child.gameObject.SetActive(true);
                }
                startText.SetActive(true);
            }
        }
    }

    public void EquipRifle()
    {
        Debug.Log("Equip Rifle");
        player.transform.GetComponentInChildren<MainMenuWeapon>().fireType = FireType.Burst;
        player.transform.GetComponentInChildren<MainMenuWeapon>().burstAmount = 3;
        player.transform.GetComponentInChildren<MainMenuWeapon>().maxBurstFireCooldown = .1f;
        player.transform.GetComponentInChildren<MainMenuWeapon>().limitedAmmo = false;
        player.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = new Color(.5f, 0, .5f);
        AudioManager.instance.PlayCoinExchange();
    }

    public void ShowRifleTip()
    {
        nameText.text = "Scorpid Tail";
        descriptionText.text = "Burst Fire";
        messageText.text = "";
        shopTooltip.SetActive(true);
    }

    public void EquipMachineGun()
    {
        Debug.Log("Equip Machine");
        player.transform.GetComponentInChildren<MainMenuWeapon>().fireType = FireType.Automatic;
        player.transform.GetComponentInChildren<MainMenuWeapon>().maxAutomaticFireCooldown = .15f;
        player.transform.GetComponentInChildren<MainMenuWeapon>().limitedAmmo = false;
        player.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = new Color(1, .6f, 0);
        AudioManager.instance.PlayCoinExchange();
    }
    public void ShowMachineGunTip()
    {
        nameText.text = "Fly Swatter";
        descriptionText.text = "Automatic";
        messageText.text = "";
        shopTooltip.SetActive(true);
    }

    public void EquipPistol()
    {
        Debug.Log("Equip Pistol");
        player.transform.GetComponentInChildren<MainMenuWeapon>().fireType = FireType.SemiAutomatic;
        player.transform.GetComponentInChildren<MainMenuWeapon>().limitedAmmo = false;
        player.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 0);
        AudioManager.instance.PlayCoinExchange();
    }

    public void ShowPistolTip()
    {
        nameText.text = "Beetle Burster";
        descriptionText.text = "Semi Automatic";
        messageText.text = "";
        shopTooltip.SetActive(true);
    }

    public void ShowGreeting()
    {
        messageText.text = "Thanks for coming! These bugs have been a real problem.";
        nameText.text = "";
        descriptionText.text = "";
        shopTooltip.SetActive(true);
        isSpeaking = true;
        AudioManager.instance.PlayRandomizedClip(clip3);
    }

    public void ShowDrinkMessage()
    {
        messageText.text = "First rounds on me for your trouble.";
        nameText.text = "";
        descriptionText.text = "";
        shopTooltip.SetActive(true);
    }

    public void HideToolTip()
    {
        shopTooltip.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
}
