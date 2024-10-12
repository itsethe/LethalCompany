using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class slimeController : MonoBehaviour
{
    NavMeshAgent navMesh;
    MeshRenderer meshRen;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        navMesh = GetComponent<NavMeshAgent>();
        meshRen = GetComponent<MeshRenderer>();
        
        navMesh.SetDestination(player.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        navMesh.SetDestination(player.transform.position);
    }
}
