using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Unity.AI.Navigation;

public class mapGenerationScript : MonoBehaviour
{
    public GameObject[] rooms;
    public NavMeshSurface nav;
    public GameObject[] items;
    public GameObject[] enemies;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
        GameObject room = Instantiate(rooms[0]);
        recursiveSpawnRoom(room);

        nav.BuildNavMesh();
    }

    void recursiveSpawnRoom(GameObject room){
        roomScript currentRoom = room.GetComponent<roomScript>();
        for(int i = 1; i<currentRoom.entrances.Length; i++){
            int random = Random.Range(0, 10);

            GameObject newRoom;
            if(random == 0){
                newRoom = Instantiate(rooms[0]);
            }else if(random > 5){
                newRoom = Instantiate(rooms[2]);
            }else{
                newRoom = Instantiate(rooms[1]);
            }
            
            roomScript nextRoom = newRoom.GetComponent<roomScript>();
            nextRoom.entrances[0].changePosition(currentRoom.entrances[i].gameObject);
            room = newRoom;
            //currentRoom = nextRoom;

            int rand = Random.Range(0,2);

            if(rand == 1){
                Instantiate(items[0], newRoom.transform.position, transform.rotation);
            
            }else if(rand == 0){
                 GameObject enemy = Instantiate(enemies[Random.Range(0, enemies.Length)], newRoom.transform.position, transform.rotation);
                 MannequinController mc = enemy.GetComponent<MannequinController>();
                 if(mc != null){
                    mc.player = player;
                }
            }
            recursiveSpawnRoom(newRoom);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        //after spawning two entrances, spawn two more rooms after connecting two entrances
        //same for hallways
        //small rooms ends it
    }
}
