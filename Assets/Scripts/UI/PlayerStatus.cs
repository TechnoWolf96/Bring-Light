using UnityEngine.UI;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public string _name;
    public int level;
    public int currentExperience;
    public int maxExperience;


    protected Player player;

    protected Text nameText;
    protected Text levelText;
    protected Text healthText;
    protected Image healthFiller;
    protected Text experienceText;
    protected Image experienceFiller;





    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        player.onHealthChanged += UpdateParameters;
        nameText = GameObject.Find("Canvas/Status/Name/Text").GetComponent<Text>();
        levelText = GameObject.Find("Canvas/Status/Level/Text").GetComponent<Text>();
        healthText = GameObject.Find("Canvas/Status/Health/Text").GetComponent<Text>();
        healthFiller = GameObject.Find("Canvas/Status/Health/Filler").GetComponent<Image>();
        experienceText = GameObject.Find("Canvas/Status/Experience/Text").GetComponent<Text>();
        experienceFiller = GameObject.Find("Canvas/Status/Experience/Filler").GetComponent<Image>();
        UpdateParameters();
    }

    public void UpdateParameters()
    {
        nameText.text = _name;
        levelText.text = level.ToString();
        healthText.text = player.health.ToString() + "/" + player.maxHealth.ToString();
        healthFiller.fillAmount = (float)player.health/player.maxHealth;
        experienceText.text = currentExperience.ToString() + "/" + maxExperience.ToString();
        experienceFiller.fillAmount = (float)currentExperience / maxExperience;
    }

    public void LevelUp()
    {

    }

}
