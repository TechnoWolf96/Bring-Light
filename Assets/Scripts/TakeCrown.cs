using UnityEngine;
using FMODUnity;

public class TakeCrown : MonoBehaviour
{
    public GameObject barrier;
    public GameObject spawner;
    public MainMusic mainMusic;
    public EventReference newMusic;
    public GameObject image;
    bool timer = false;
    float time = 60f;
    bool taked = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !taked)
        {
            taked = true;
            spawner.SetActive(true);
            barrier.SetActive(true);
            timer = true;
            mainMusic.ChangeMusic(newMusic);
            Destroy(image);
        }
    }

    private void FixedUpdate()
    {
        if (timer)
        {
            time -= Time.deltaTime;
        }
        if (timer && time < 0)
        {
            Destroy(barrier);
            timer = false;
        }
        
    }




}
