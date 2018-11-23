using UnityEngine;
using UnityEngine.AI;

public class NavMeshBaker : MonoBehaviour
{
    public NavMeshSurface[] surfaces;
    public GameObject[] unitPrefabs;
    public Vector3[] spawnPoints;

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < surfaces.Length; i++)
        {
            surfaces[i].BuildNavMesh();
        }

        for (int j = 0; j < unitPrefabs.Length; j++)
        {
            if (j < 3)
            {
                Instantiate(unitPrefabs[j], spawnPoints[j], Quaternion.LookRotation(Vector3.back));
            }
            else
            {
                Instantiate(unitPrefabs[j], spawnPoints[j], Quaternion.identity);
            }
        }
    }
}