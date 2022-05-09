using UnityEngine;

public class ScoreForKill : MonoBehaviour
{
    [SerializeField] private int _score;


    void Start()
    {
        GetComponent<Creature>().onDeath += OnScore;
    }

    private void OnScore() => Score.singleton.score += _score;


}
