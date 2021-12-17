using UnityEngine;
using UnityEngine.UI;

public class CheckParameters_NotRelease : MonoBehaviour
{
    public Player_NotRelease currentPlayer;

    private Slider healthSlider;
    private Text healthText;
    private Text fireProtectText;
    private Text physicalProtectText;
    private Text coldProtectText;
    private Text poisonProtectText;
    private Text darkProtectText;

    private void Start()
    {
        healthSlider = GameObject.Find("Canvas/Characteristics/Health").GetComponent<Slider>();
        healthText = GameObject.Find("Canvas/Characteristics/Health/Text").GetComponent<Text>();
        fireProtectText = GameObject.Find("Canvas/Characteristics/FireProtect/Text").GetComponent<Text>();
        physicalProtectText = GameObject.Find("Canvas/Characteristics/PhysicalProtect/Text").GetComponent<Text>();
        coldProtectText = GameObject.Find("Canvas/Characteristics/ColdProtect/Text").GetComponent<Text>();
        poisonProtectText = GameObject.Find("Canvas/Characteristics/PoisonProtect/Text").GetComponent<Text>();
        darkProtectText = GameObject.Find("Canvas/Characteristics/DarkProtect/Text").GetComponent<Text>();
        UpdateParameters();
    }

    public void UpdateParameters()
    {
        healthText.text = currentPlayer.health.ToString() + '/' + currentPlayer.maxHealth.ToString();
        healthSlider.value = (float)currentPlayer.health / currentPlayer.maxHealth;
        fireProtectText.text = currentPlayer.protect.fiery.ToString();
        physicalProtectText.text = currentPlayer.protect.physical.ToString();
        coldProtectText.text = currentPlayer.protect.cold.ToString();
        poisonProtectText.text = currentPlayer.protect.poison.ToString();
        darkProtectText.text = currentPlayer.protect.dark.ToString();

    }

}
