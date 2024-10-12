using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class bugScript : MonoBehaviour
{
    public detectionScript ds;
    enum bugactions {Patrol, Attack, Steal};
    bugactions currentaction = bugactions.Patrol;
    NavMeshAgent nav;
    public GameObject hand;
    bool isHolding = false;
    // Start is called before the first frame update
    Vector3 pos;
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        pos = new Vector3(Random.Range(-10,10), 0, Random.Range(-10,10));
        nav.SetDestination(pos);
    }

    // Update is called once per frame
    void Update()
    {
       switch(currentaction){
        case bugactions.Patrol:
        if(nav.stoppingDistance > nav.remainingDistance){
            pos = new Vector3(Random.Range(-10,10), 0, Random.Range(-10,10));
            nav.SetDestination(pos);
        }
        if(ds.item != null && isHolding == false){
            currentaction = bugactions.Steal;
            pos = ds.item.transform.position;
            nav.SetDestination(pos);
            
        }
        break;
        case bugactions.Steal:
            if(nav.stoppingDistance > nav.remainingDistance){
                ds.item.transform.SetParent(hand.transform);
                ds.item.transform.position = hand.transform.position;
                itemScript iS = ds.item.GetComponent<itemScript>();
                iS.turnOffPhyiscs();
                currentaction = bugactions.Patrol;
                isHolding = true;
            }
        break;
       } 
    }
    
}
