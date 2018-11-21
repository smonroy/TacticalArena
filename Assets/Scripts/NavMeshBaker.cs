using UnityEngine;
using UnityEngine.AI;

public class NavMeshBaker : MonoBehaviour
{
    public NavMeshSurface[] surfaces;

    // Use this for initialization
    void Start()
    {
        //bakeTime = Time.time + bakeDelay;
        for (int i = 0; i < surfaces.Length; i++)
        {
            surfaces[i].BuildNavMesh();
        }
    }
}