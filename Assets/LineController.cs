using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineController : MonoBehaviour
{
    [SerializeField] private Line line;
    public List<Transform> points;
    [HideInInspector] public int sequence;
    private void Awake()
    {
        sequence = line.sequence;
        GetPoints();

    }
    private void Start()
    {
        GameManager.instance.SetLineController(this);
    }
    private void GetPoints()
    {
        foreach (Transform child in transform)
        {
            points.Add(child);
        }
    }
    public void Reverse()
    {
         
    }

}
