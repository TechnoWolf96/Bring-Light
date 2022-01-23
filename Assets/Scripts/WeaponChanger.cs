using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponChanger : MonoBehaviour
{
    public Player player;
    public GameObject sword;
    public GameObject bow;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            player.ChangeWeapon(sword);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            player.ChangeWeapon(bow);
        }
    }
}
