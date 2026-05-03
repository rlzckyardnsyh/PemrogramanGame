using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    public Transform spawnPoint;

    public float delay = 3f;   // jeda spawn
    public int maxSpawn = 2;   // total spawn

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        for (int i = 0; i < maxSpawn; i++)
        {
            yield return new WaitForSeconds(delay);

            Vector3 pos = spawnPoint != null ? spawnPoint.position : transform.position;
            Instantiate(objectToSpawn, pos, Quaternion.identity);
        }
    }
}