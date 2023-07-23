using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(menuName = "DotVR/Dot", fileName = "Dot")]
public class Dot : ScriptableObject
{
     public float visibleDuration;
     public float maxspawnDelay;
     public GameObject prefab;

   public float GenerateSpawnDelay()
    {
        return Random.Range(visibleDuration, maxspawnDelay);
    }

    public GameObject Spawn(Transform spawnPoint)
    {
       return Instantiate(prefab, spawnPoint.position, Quaternion.identity);
    }
   
}
