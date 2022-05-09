using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public delegate void OnWaveChanged(int waveNumber);
    public event OnWaveChanged onWaveChanged;


    public static Score singleton { get; private set; }

    [SerializeField] private BestScore _bestScoreSave;

    [SerializeField] private Text _scoreText;
    [SerializeField] private Text _bestScoreText;
    [SerializeField] private Text _waveText;


    [SerializeField] private List<int> _goNextWaveScore;
    [SerializeField] private List<string> _waveNames;
    [SerializeField] private List<Color> _waveColors;


    private int _currentWave;
    public int currentWave
    {
        get => _currentWave; 
        set 
        { 
            _currentWave = value;
            _waveText.text = _waveNames[_currentWave]; 
            _waveText.color = _waveColors[_currentWave];
            onWaveChanged?.Invoke(_currentWave);
        }
    }

    private int _score;
    public int score
    {
        get => _score;
        set
        {
            _scoreText.text = value.ToString();
            _score = value;
            if (_score >= _goNextWaveScore[currentWave]) currentWave++;
            if (_score > bestScore) { bestScore = _score; _bestScoreSave.bestScore = bestScore; }
        }
    }

    private int _bestScore;
    public int bestScore
    {
        get => _bestScore;
        set { _bestScore = value; _bestScoreText.text = value.ToString(); }
    }



    private void Awake()
    {
        singleton = this;
    }

    private void Start()
    {
        currentWave = 1;
        score = 0;
        bestScore = _bestScoreSave.bestScore;


    }

}
