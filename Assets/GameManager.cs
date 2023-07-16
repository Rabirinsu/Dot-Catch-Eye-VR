using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [Header("INTERACTORS ")] 
    [SerializeField] private GameObject righteyeInteractor;
    [SerializeField] private GameObject lefteyeInteractor;
    private int sesionCount = 1;
    [Header("CORE ")]
    public static GameManager instance; 
    public  GameObject currentLine; 
    [SerializeField] private Dot regularDot;
    [SerializeField] private Dot randomDot;
    [HideInInspector]public Dot currentDot;
    [SerializeField] private List<Line> Lines;
    [SerializeField] private int currentlineID;
    [SerializeField] private LineController linecontroller;
    private float dotspawnDelay;
    public float sessiontransitDelay;
    public static Transform currentspawnPoint;
    private int spawnCount;
    [SerializeField] private int spawnFrequency; // TODO Get this value from lines 
     private int sequenceCount;
    private int lineSequence;
    private int linepointsCount;
    private bool isReversed;
    [FormerlySerializedAs("sequencecompletedEvent")]
    [Header("EVENTS ")]
    [SerializeField] private GameEvent sequencecompletedEvent_regular;
    [SerializeField] private GameEvent sequencecompletedEvent_random;
    [SerializeField] private GameEvent sessioncompletedEvent;
    [SerializeField] private GameEvent sessionexpiredEvent;
    
    public enum Phase {None, OnSequence ,sequenceCompleted, sessionExpired,}

    public Phase currentPhase;
    public enum Session{Regular, Random}

    public Session currentSession;

    private void OnEnable()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
        
        Application.targetFrameRate = 120;
        InitializeGame();
    }
    public void SetRandomSession()
    {
        currentSession = Session.Random;
    }  
    public void SetRegularSession()
    {
        currentSession = Session.Regular;
    }
    
    private void InitializeGame()
    {
      
            currentLine = Lines[currentlineID].Initialize();
            SetSpawnDelay();
            currentPhase =  Phase.OnSequence;
            SetDot();
            UpdateEye();
    }

    private void SetDot()
    {
        if (currentSession == Session.Random)
            currentDot = randomDot;
        else currentDot = regularDot;
    }

    public void SetLineController(LineController _linecontroller)
    {
        linecontroller = _linecontroller;
        linepointsCount = _linecontroller.points.Count;
        lineSequence = _linecontroller.sequence;
    }

    public void LateUpdate()
    {
        if (currentPhase == Phase.OnSequence)
        {
            
            if (currentDot == regularDot)
            {
                RegularDotSequence();
            }
            else
            {
                RandomDotSequence();
            } 
        }
    }

    public void UpdateEye()
    {
        switch (sesionCount)
        {
            case 1:
                righteyeInteractor.SetActive(true);
                lefteyeInteractor.SetActive(false);
                break; 
            case 2:
                righteyeInteractor.SetActive(false);
                lefteyeInteractor.SetActive(true);
                break; 
            case 3:
                righteyeInteractor.SetActive(true);
                lefteyeInteractor.SetActive(true);
                break;
        }
    }
    private void RegularDotSequence()
    {
        if(sequenceCount < lineSequence)
        {

            if (!isReversed)
            {
                if (spawnCount < linepointsCount && linecontroller)
                {
                    dotspawnDelay -= Time.deltaTime;
                    if (dotspawnDelay < 0)
                    {
                        SpawnDot();
                    }
                }
                else if (spawnCount >= linepointsCount)
                {
                    sequencecompletedEvent_regular?.Raise();
                }
            }
            else
            {
                if (spawnCount >= 0 && linecontroller)
                {
                    dotspawnDelay -= Time.deltaTime;
                    if (dotspawnDelay < 0)
                    {
                        SpawnDot();
                    }
                }
                else if (spawnCount < 0)
                {
                    sequencecompletedEvent_regular?.Raise();
                }
            }
        }
         // TODO DESTROY CURRENT LINE SPAWN NEXT LINE RESET SPAWNCOUNT 
        else sessioncompletedEvent?.Raise();
    }
    private void RandomDotSequence()
    {
                if (spawnCount < spawnFrequency && linecontroller)
                {
                    dotspawnDelay -= Time.deltaTime;
                    if (dotspawnDelay < 0)
                    {
                        SpawnDot();
                    }
                }
                else if (spawnCount >= spawnFrequency)
                {
                    sessioncompletedEvent?.Raise();
                }
    }
    
    public void SwitchReverse()
    {
        sequenceCount++;
        if (isReversed)
            isReversed = false;
        else isReversed = true;
    }

    public void SetNextLine()
    {
        currentPhase =  Phase.sequenceCompleted;
        StartCoroutine(NextLineSpawn());
    }

    private IEnumerator NextLineSpawn()
    {
    
        yield return new WaitForSeconds(currentDot.maxspawnDelay);
        sequenceCount = 0;
        spawnCount = 0;
        currentlineID++;
        Destroy(currentLine);
        if (currentlineID < Lines.Count)
        {
            yield return new WaitForSeconds(sessiontransitDelay);
            currentLine = Lines[currentlineID].Initialize();
            SetSpawnDelay();
            currentPhase =  Phase.OnSequence;
        }
        else
        {
            this.enabled = false;
            sessionexpiredEvent?.Raise();
            currentPhase =  Phase.sessionExpired;
            yield return null;
        }
     
    }
    
    private void SpawnDot()
    {
        if (currentDot == regularDot)
        {
            currentDot.Spawn(GetSpawnPoint());
        }
        else
        {
            currentDot.Spawn(GetRandomSpawnPoint());
        }
        SetSpawnDelay();
        UpdateSpawnCounts();
    }
    
    public void UpdateSpawnCounts()
    {
        if (isReversed)
            spawnCount--;
        else spawnCount++;
    }

    private void SetSpawnDelay()
    {
        dotspawnDelay = regularDot.GenerateSpawnDelay();
    }

    public Transform GetSpawnPoint()
    {
       return linecontroller.points[spawnCount];
    } 

    public Transform GetRandomSpawnPoint()
    {
        return linecontroller.points[Random.Range(0,linecontroller.points.Count)];
    }

    public void Reset()
    {
        currentlineID = 0;
        currentPhase =  Phase.None;
        spawnCount = 0;
        sequenceCount = 0;
        isReversed = false;
        this.enabled = true;
    }

    public void NextSession()
    {
        Reset();
        sesionCount++;
        UpdateEye();
    }
}
