using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class FollowWP : MonoBehaviour
{
    Transform goal;
    float speed = 5.0f;
    float accuracy = 5.0f;
    float rotSpeed = 2.0f;

    public GameObject wpManager;
    GameObject[] wps;
    GameObject currentNode;
    int currentWP = 0;
    Graph g;


    // Start is called before the first frame update
    void Start()
    {
        wps = wpManager.GetComponent<WPManager>().waypoints;
        g = wpManager.GetComponent<WPManager>().graph;
        currentNode = wps[0];

        Invoke("GoToRuin", 2);
    }
    public void GoToHeli()
    {
        g.AStar(currentNode, wps[1]);
        currentWP = 1;
    }
    public void Ruin()
    {
        g.AStar(currentNode, wps[9]);
        currentWP = 9;
    }
    // Update is called once per frame
    void LateUpdate()
    {
        if (Vector3.Distance(g.pathList[currentWP].getId().transform.position,this.transform.position)< accuracy)
        {
            currentNode = g.pathList[currentWP].getId();
            currentWP++;
        }
        if(currentWP< g.pathList.Count)
        {
            goal = g.pathList[currentWP].getId().transform;
            Vector3 lookAtGoal = new Vector3(goal.position.x,this.transform.position.y,goal.position.z);

            Vector3 direction = lookAtGoal - this.transform.position;
            this.transform.rotation = Quaernion.Slerp(this.transform.rotation, Time.deltaTime * rotSpeed);


            this.transform.Translate ( 0,0,speed * Time.deltaTime );
        }
    }
}
