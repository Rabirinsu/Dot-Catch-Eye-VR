using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("TEXTMESHPRO")]
    [SerializeField] private TextMeshProUGUI inscore_TMP;
    [SerializeField] private TextMeshProUGUI currentscore_TMP;
    [SerializeField] private TextMeshProUGUI clickeddots_TMP;
    [SerializeField] private TextMeshProUGUI bestscore_TMP;
    [Header("CORE")]
    [SerializeField] private ScoreManager scoreManager;
    [Header("DROPDOWNS")]
  
    [SerializeField] private TMP_Dropdown dotcolorDropdown;
    [SerializeField] private TMP_Dropdown dotsizeDropdown;
    [SerializeField] private TMP_Dropdown linecolorDropdown;
    [SerializeField] private TMP_Dropdown sequenceDropdown;
    [SerializeField] private Image dotdropdowniconImage;
    [SerializeField] private Image linedropdowniconImage;
    [SerializeField] private GameObject dotPrefab;
    [SerializeField] private Material dotMaterial;
    [SerializeField] private Material lineMaterial;
    [Header("CANVAS")]
    [SerializeField] private GameObject sessiontransitCanvas;
    [SerializeField]   private GameObject scoreTableCanvas;
    [SerializeField]   private GameObject sessionexpiredCanvas;
    [SerializeField] private GameManager gameManager;
    private void Start()
    {
        dotcolorDropdown.onValueChanged.AddListener(OnDotColorDropdownValueChanged);
        linecolorDropdown.onValueChanged.AddListener(OnLineColorDropdownValueChanged);
        dotsizeDropdown.onValueChanged.AddListener(OnSizeDropdownValueChanged);
        sequenceDropdown.onValueChanged.AddListener(OnSequenceDropdownValueChanged);
    }

    public void UpdateScore()
    {
        inscore_TMP.text = scoreManager.currentScore.ToString();
        currentscore_TMP.text = scoreManager.currentScore.ToString();
        clickeddots_TMP.text = scoreManager.clickedDots.ToString();
    }
    
    public void OnDotColorDropdownValueChanged(int index)
    {
      
        switch (index)
        {
            case 0:
                dotdropdowniconImage.color = Color.green;
                dotMaterial.color = Color.green;
                break;
            case 1:
                dotdropdowniconImage.color = Color.magenta;
                dotMaterial.color = Color.magenta;
                break;
            case 2:
                dotdropdowniconImage.color = Color.blue;
                dotMaterial.color = Color.blue;
                break;
        }
    }
    
     public void OnSizeDropdownValueChanged(int index)
    {
      
        switch (index)
        {
            case 0:
                  dotPrefab.transform.localScale = new Vector3(.1f, .1f,.1f);
                break;
            case 1:
                dotPrefab.transform.localScale = new Vector3(.15f, .15f,.15f);
                break;
            case 2:
                dotPrefab.transform.localScale = new Vector3(.25f, .25f,.25f);
                break; 
            case 3:
                dotPrefab.transform.localScale = new Vector3(.35f, .35f,.35f);
                break;
        }
    }
     
     public void OnLineColorDropdownValueChanged(int index)
    {
      
        switch (index)
        {
            case 0:
                linedropdowniconImage.color = Color.red;
                lineMaterial.color = Color.red;
                break;
            case 1:
                linedropdowniconImage.color = Color.white;
                lineMaterial.color = Color.white;
                break;
            case 2:
                linedropdowniconImage.color = Color.cyan;
                lineMaterial.color = Color.cyan;
                break;
        }
    } 
     public void OnSequenceDropdownValueChanged(int index)
     {
        switch (index)
        {
            case 0:
                gameManager.spawnFrequency = 2;
                break;
            case 1:
                gameManager.spawnFrequency = 4;
                break;
            case 3:
                gameManager.spawnFrequency = 6;
                break;     
            case 4:
                gameManager.spawnFrequency = 8;
                break;
            case 5:
                gameManager.spawnFrequency = 10;
                break;
        }
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
