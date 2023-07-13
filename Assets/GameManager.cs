using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; 
    public  GameObject currentLine; 
    [SerializeField] private Dot regularDot;
    [SerializeField] private Dot randomDot;
    [SerializeField]  private Dot currentDot;
    [SerializeField] private List<Line> Lines;
    [SerializeField] private int currentlineID;
    [SerializeField] private LineController linecontroller;
    private float dotspawnDelay;
    public static Transform currentspawnPoint;
    private int spawnCount;
    [SerializeField] private int spawnFrequency; // TODO Get this value from lines 
     private int sequenceCount;
    private int lineSequence;
    private int linepointsCount;
    private bool isReversed;
    [Header("EVENTS ")]
    [SerializeField] private GameEvent sequencecompletedEvent;
    [SerializeField] private GameEvent sessioncompletedEvent;
    
    public enum Session{Regular, Random}

    public Session currentSession;
    
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        Application.targetFrameRate = 80;
        InitializeGame();
    }

    private void InitializeGame()
    {
            SetDot();
           currentLine = Lines[currentlineID].Initialize();
            SetSpawnDelay();
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

        if (currentDot == regularDot)
        {
            RegularDotSequence();
        }
        else
        {
            RandomDotSequence();
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
                    sequencecompletedEvent?.Raise();
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
                    sequencecompletedEvent?.Raise();
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
                    // RANDOM SEQUENCE COMPLETED
                    // TODO Destroy current line & Spawn another line
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
        sequenceCount = 0;
        Destroy(currentLine);
        currentlineID++;
        Lines[currentlineID].Initialize();
        SetSpawnDelay();
        
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
}
