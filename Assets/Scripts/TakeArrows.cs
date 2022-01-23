
using UnityEngine;
using FMODUnity;

public class TakeArrows : MonoBehaviour
{
    public GameObject newWeapon;
    public WeaponChanger weaponChanger;
    public Player player;
    public GameObject spawner;
    public MainMusic mainMusic;
    public EventReference newMusic;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            weaponChanger.bow = newWeapon;
            player.ChangeWeapon(newWeapon);
            spawner.SetActive(true);
            mainMusic.ChangeMusic(newMusic);
            Destroy(gameObject);
        }
    }



}
