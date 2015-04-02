using UnityEngine;
using System.Collections;

public class Guard : MonoBehaviour {

    public Transform target;
    public Transform target2;
    NavMeshAgent agent;

    public float dist;
    float minDistance;

    Vector3 startPosition;
    // Use this for initialization
    void Awake()
    {

    }

	void Start () {
        minDistance = 8f;
        transform.position = new Vector3(transform.position.x, 1f, transform.position.z);
        startPosition = transform.position;
        agent = GetComponent<NavMeshAgent>();
	    
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(transform.position.x, 1f, transform.position.z);
        float p1Distance = Vector3.Distance(transform.position, target.position);
        if (p1Distance < minDistance)
        {
            if (p1Distance > dist)
            {
                agent.SetDestination(target.position);
            }
            else
            {
                agent.SetDestination(transform.position);
            }
            
        }
        else
        {
            agent.SetDestination(startPosition);
        }

        /*
        float p2Distance = Vector3.Distance(transform.position, target2.position);

        if(p1Distance < minDistance || p2Distance < minDistance){

            if(p1Distance<p2Distance){
                agent.SetDestination(target.position);
            }else{
                agent.SetDestination(target2.position);
            }


        }else{
            agent.SetDestination(startPosition);
        }
         */
       

        
	}
}
