using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckParameters : MonoBehaviour
{
    public Player currentPlayer;

    private Slider healthSlider;
    private Text healthText;
    private Text magicProtectText;
    private Text physicalProtectText;

    private void Start()
    {
        healthSlider = GameObject.Find("Canvas/Characteristics/Health").GetComponent<Slider>();
        healthText = GameObject.Find("Canvas/Characteristics/Health/Text").GetComponent<Text>();
        magicProtectText = GameObject.Find("Canvas/Characteristics/MagicProtect/Text").GetComponent<Text>();
        physicalProtectText = GameObject.Find("Canvas/Characteristics/PhysicalProtect/Text").GetComponent<Text>();
        UpdateParameters();
    }

    public void UpdateParameters()
    {
        healthText.text = currentPlayer.health.ToString() + '/' + currentPlayer.maxHealth.ToString();
        healthSlider.value = (float)currentPlayer.health / currentPlayer.maxHealth;
        magicProtectText.text = currentPlayer.magicProtect.ToString();
        physicalProtectText.text = currentPlayer.physicalProtect.ToString();

    }

}
