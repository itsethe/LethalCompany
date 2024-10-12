using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MannequinController : MonoBehaviour
{
    NavMeshAgent navMesh;
    MeshRenderer meshRen;
    public GameObject player;
    bool seen = false;
    float timer = 0;

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
       
        if(seen == true){
            navMesh.speed = 0;
        }else{
            navMesh.speed = 1;
        }
        timer += Time.deltaTime;
        if(timer >= 1 && seen == true){
            seen = false;
            
            timer = 0;
        }

        if(seen == false){
            navMesh.SetDestination(player.transform.position);
        }
        
    }
    public void isSeen(){
        seen = true;
    }
    void OnTriggerEnter(Collider c){
        if(c.CompareTag("playerEye")){
            seen = true;
        }
    }
    void OnTriggerExit(Collider c){
        if(c.CompareTag("playerEye")){
            seen = false;
        }
    }
}
