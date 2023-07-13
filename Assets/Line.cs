using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu( menuName = "DotVR/Line",fileName ="Line")]
public class Line : ScriptableObject
{

    public GameObject prefab;
    [SerializeField] private Vector3 spawnPoint;
    [SerializeField] private Vector3 spawnRotation;
     public int sequence;

    public GameObject Initialize()
    {
       return Instantiate(prefab, spawnPoint, Quaternion.Euler(spawnRotation));
    }
}
