using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerControl : MonoBehaviour
{
    [SerializeField] private List<GameObject> spawners;
    private int currentIndex;

    private void Start()
    {
        Score.singleton.onWaveChanged += ChangeSpawners;
        currentIndex = 1;
    }

    private void ChangeSpawners(int index)
    {
        spawners[currentIndex].SetActive(false);
        currentIndex = index;
        spawners[currentIndex].SetActive(true);
    }



}
