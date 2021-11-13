using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedOneAttack : Stalker
{
    /* Преследование цели:
     * Существо останавливается на случайном расстоянии, выбранном в интервале
     * между minStopDistance и maxStopDistance, начинает дальнюю атаку.
     * Если цель отошла от существа на расстояние, большее maxStopDistance,
     * то существо начинает снова двигаться в сторону цели, снова находя случайную позицию.
     * 
     * P.S. Существо не должно стрелять через стену в направлении цели.
     */
    [Header("Ranged Attack:")]
    public float maxStopDistance;           // Максимальное расстояние остановки до цели
    public float minStopDistance;           // Минимальное расстояние остановки до цели
    public OneBullet_Parameters bulletParameters;   // Параметры пули



}
