using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private Dot dot;
    [SerializeField] private Line line;
    [SerializeField] private LineController linecontroller;
    private float dotspawnDelay;
    public static Transform currentspawnPoint;
    private int spawnCount;
    private int sequenceCount;
    private int lineSequence;
    private int linepointsCount;
    private bool isReversed;
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
        line.Initialize();
        SetSpawnDelay();
    }

    public void SetLineController(LineController _linecontroller)
    {
        linecontroller = _linecontroller;
        linepointsCount = _linecontroller.points.Count;
        lineSequence = _linecontroller.sequence;
    }

    public void LateUpdate()
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
                    SequenceCompleted();
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
                    SequenceCompleted();
                }
            }
        }
    }

    private void SequenceCompleted()
    {
        sequenceCount++;
        SwitchReverse();
        UpdateSpawnCounts();
    }
    private void SwitchReverse()
    {
        if (isReversed)
            isReversed = false;
        else isReversed = true;
    }

    private void SpawnDot()
    {
        dot.Spawn(GetSpawnPoint());
        SetSpawnDelay();
        UpdateSpawnCounts();
    }
    private void UpdateSpawnCounts()
    {
        if (isReversed)
            spawnCount--;
        else spawnCount++;
    }

    private void SetSpawnDelay()
    {
        dotspawnDelay = dot.GenerateSpawnDelay();
    }

    public Transform GetSpawnPoint()
    {
       return linecontroller.points[spawnCount];
    }

    public Transform GetRandomSpawnPoint()
    {
        return null;
    }
}
