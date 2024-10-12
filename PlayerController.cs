using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    public GameObject pivot;
    Animator anim;
    public float speed = 2;
    float pivotRotation = 0;
    GameObject item;
    public GameObject hand;
    public GameObject eyeball;
    bool isHolding = false;
    int hp = 10;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float ad = Input.GetAxis("Horizontal");
        float ws = Input.GetAxis("Vertical");
        bool isWalking = !Mathf.Approximately(ad,0) || !Mathf.Approximately(ws, 0);
        anim.SetBool("isWalking", isWalking);
        anim.SetFloat("ad", ad);
        anim.SetFloat("ws", ws);
        if(Input.GetKey(KeyCode.LeftShift)){
            anim.SetBool("isRunning", true);
            speed = 4;
        }else{
            anim.SetBool("isRunning", false);
            speed = 2;
        }

        Vector3 movement = (transform.forward*ws + transform.right * ad)*Time.deltaTime*speed;
        rb.MovePosition(transform.position + movement);
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        Vector3 bodyRotation = new Vector3(0, mouseX, 0);
        Quaternion rotateBody = Quaternion.Euler(bodyRotation);
        rb.MoveRotation(transform.rotation*rotateBody);
        pivotRotation += mouseY;
        pivotRotation = Mathf.Clamp(pivotRotation, -90, 90);
        Quaternion pivotRotate = Quaternion.Euler(-pivotRotation, 0, 0);
        pivot.transform.localRotation = pivotRotate;
        for(int i = -5; i<5; i++){
            for(int j = -5; j < 5; j++){
                Vector3 two = (pivot.transform.right*i/5)+(pivot.transform.up*-j/5);
                performRaycast(two);
            }
        }
        performRaycast(Vector3.zero);

        if(Input.GetKeyDown(KeyCode.E) && item != null){
            if(isHolding == false){
                anim.SetBool("isHolding", true);
                item.transform.SetParent(hand.transform);
                item.transform.position = hand.transform.position;
                itemScript iS = item.GetComponent<itemScript>();
                iS.turnOffPhyiscs();
                isHolding = true;
            }else{
                anim.SetBool("isHolding", false);
                item.transform.SetParent(null);
                
                itemScript iS = item.GetComponent<itemScript>();
                iS.turnOnPhyiscs();
                isHolding = false;
            }
            
        }

        if(hp <=0){
            SceneManager.LoadScene(1);
        }
    }

    public void performRaycast(Vector3 offset){
        RaycastHit hit;
        //Debug.DrawRay(pivot.transform.position, (pivot.transform.forward+offset)*1000, Color.red);
        if(Physics.Raycast(pivot.transform.position, pivot.transform.forward+offset, out hit, Mathf.Infinity)){
            //Debug.DrawRay(pivot.transform.position, (pivot.transform.forward+offset)*1000, Color.red);
            if(hit.collider.CompareTag("Man")){
                //Instantiate(eyeball, hit.point, transform.rotation);
                MannequinController mc = hit.collider.GetComponent<MannequinController>();
                mc.isSeen();
            }
        }
    }
    public void OnTriggerEnter(Collider c){
        if(c.CompareTag("item")){
            item = c.gameObject;
        }
    }
    public void OnTriggerExit(Collider c){
        if(c.CompareTag("item")){
            item = null;
        }
    }

    void OnCollisionEnter(Collision c){
        if(c.collider.CompareTag("enemy") || c.collider.CompareTag("Man")){
            hp -= 2;
        }

        
    }
}
