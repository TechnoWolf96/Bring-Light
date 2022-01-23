using UnityEngine;
using FMODUnity;

public class AreaChangeMusic : MonoBehaviour
{
    public MainMusic mainMusic;
    public EventReference newMusic;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            mainMusic.ChangeMusic(newMusic);
            Destroy(gameObject);
        }
        
    }
}
