using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class UIManager : MonoBehaviour
{
    [Header("TEXTMESHPRO")]
    [SerializeField] private TextMeshProUGUI inscore_TMP;
    [SerializeField] private TextMeshProUGUI currentscore_TMP;
    [SerializeField] private TextMeshProUGUI clickeddots_TMP;
    [SerializeField] private TextMeshProUGUI bestscore_TMP;
    [Header("CORE")]
    [SerializeField] private ScoreManager scoreManager;

    [Header("CANVAS")]
    [SerializeField] private GameObject sessiontransitCanvas;
    [SerializeField]   private GameObject scoreTableCanvas;
    [SerializeField]   private GameObject sessionexpiredCanvas;
    public void UpdateScore()
    {
        inscore_TMP.text = scoreManager.currentScore.ToString();
        currentscore_TMP.text = scoreManager.currentScore.ToString();
        clickeddots_TMP.text = scoreManager.clickedDots.ToString();
    }

    public void SessionTransition()
    {
        if(GameManager.instance.currentPhase != GameManager.Phase.sessionExpired )
        StartCoroutine(TransitSession());
    }

    private IEnumerator TransitSession()
    {
     
        yield return new WaitForSeconds(GameManager.instance.currentDot.maxspawnDelay);
        UpdateCanvas(true);
        yield return new WaitForSeconds(GameManager.instance.sessiontransitDelay);
        UpdateCanvas(false);
    }

    private void UpdateCanvas(bool isTransit)
       {
               sessiontransitCanvas.SetActive(isTransit);
               scoreTableCanvas.SetActive(isTransit);
        }

    public void UpdateSession()
    {
        StartCoroutine(TransitExpiredSession());
    }
    
    private IEnumerator TransitExpiredSession()
    {
        yield return new WaitForSeconds(GameManager.instance.currentDot.maxspawnDelay);
        sessionexpiredCanvas.SetActive(true);
        scoreTableCanvas.SetActive(true); 
    }

    public void Reset()
    {
        sessionexpiredCanvas.SetActive(false);
        UpdateCanvas(false);
    }
}
