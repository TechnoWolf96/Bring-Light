using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedOneAttack : Stalker
{
    /* ������������� ����:
     * �������� ��������������� �� ��������� ����������, ��������� � ���������
     * ����� minStopDistance � maxStopDistance, �������� ������� �����.
     * ���� ���� ������ �� �������� �� ����������, ������� maxStopDistance,
     * �� �������� �������� ����� ��������� � ������� ����, ����� ������ ��������� �������.
     * 
     * P.S. �������� �� ������ �������� ����� ����� � ����������� ����.
     */
    [Header("Ranged Attack:")]
    public float maxStopDistance;           // ������������ ���������� ��������� �� ����
    public float minStopDistance;           // ����������� ���������� ��������� �� ����
    public OneBullet_Parameters bulletParameters;   // ��������� ����



}
