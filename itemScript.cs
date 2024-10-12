using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemScript : MonoBehaviour
{

    Rigidbody rb;
    public Collider c;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        //c = this.GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void turnOffPhyiscs(){
        rb.isKinematic = true;
        rb.useGravity = false;
        c.isTrigger = true;
    }

    public void turnOnPhyiscs(){
        rb.isKinematic = false;
        rb.useGravity = true;
        c.isTrigger = false;
    }
}
