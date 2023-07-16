
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private GameData data;
    internal int currentScore;
    internal int bestScore;
    internal int clickedDots;
    [SerializeField] private int addscoreAmount;
    [SerializeField] private int losescoreAmount;
   
    private void OnEnable()
    {
        bestScore = data.bestScore;
        addscoreAmount = data.addscoreAmount;
        clickedDots = 0;
    }

    public void AddScore()
    {
        currentScore += addscoreAmount;
        clickedDots++;
    }

    public void LoseScore()
    {
        addscoreAmount += losescoreAmount;
        clickedDots--;
    }

    public void SaveScore()
    {
        if (currentScore > bestScore)
        {
            data.bestScore = currentScore;
        }
    }
    
}
