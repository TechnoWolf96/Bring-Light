using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    public int speedBonus;
    public int healthBonus;
    public ProtectParameters protection;
    protected CheckParameters checkParameters;

    protected virtual void Start()
    {
        checkParameters = GameObject.FindWithTag("Script").GetComponent<CheckParameters>();
        checkParameters.currentPlayer.protect += protection;
        checkParameters.currentPlayer.maxHealth += healthBonus;
        checkParameters.currentPlayer.health += healthBonus;
        checkParameters.currentPlayer.speed += speedBonus;
        checkParameters.UpdateParameters();
    }

    protected virtual void OnDestroy()
    {
        checkParameters.currentPlayer.protect -= protection;
        checkParameters.currentPlayer.maxHealth -= healthBonus;
        checkParameters.currentPlayer.health -= healthBonus;
        if (checkParameters.currentPlayer.health <= 0)          // Игрок не умирает от снятия предмета
            checkParameters.currentPlayer.health = 1;
        checkParameters.currentPlayer.speed -= speedBonus;
        checkParameters.UpdateParameters();
    }
}
