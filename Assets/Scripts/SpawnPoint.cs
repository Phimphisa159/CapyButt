using System.Collections.Generic;

using UnityEngine;


public class SpawnPoint : MonoBehaviour

{

    private static List<SpawnPoint> spawnPoints = new List<SpawnPoint>();


    private void OnEnable()

    {

        spawnPoints.Add(this);

    }

    private void OnDisable()

    {

        spawnPoints.Remove(this);

    }

    public static Vector3 GetRandomSpawnPos()

    {
        if (spawnPoints.Count == 0)
        {
            return Vector3.zero;
        }
        return spawnPoints[0].transform.position;
    }
    public static Vector3 GetInGame()

    {
        if (spawnPoints.Count == 0)
        {
            return Vector3.zero;
        }
        return spawnPoints[1].transform.position;
    }


    private void OnDrawGizmos()

    {

        Gizmos.color = Color.blue;

        Gizmos.DrawSphere(transform.position, 1);

    }

}