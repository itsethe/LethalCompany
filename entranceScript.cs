using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class entranceScript : MonoBehaviour
{
    public GameObject room;
    Vector3 offset;
    
    // Start is called before the first frame update
    void Start()
    {
        //offset = room.transform.position-transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
       
        
    }
    public void changePosition(GameObject a){
        room.transform.rotation = a.transform.rotation;
        offset = room.transform.position-transform.position;
        transform.position = a.transform.position;
        room.transform.position = transform.position + offset;
    }
}
