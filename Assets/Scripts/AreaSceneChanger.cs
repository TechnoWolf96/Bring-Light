using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaSceneChanger : MonoBehaviour
{
    public Animator fadeAnim;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            fadeAnim.SetTrigger("Fade");
        }
    }
}
