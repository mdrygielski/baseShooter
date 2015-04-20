using UnityEngine;
using System.Collections;

public class Guard : MonoBehaviour {

    public GameObject target;
    //public Transform target2;
    NavMeshAgent agent;

    public float dist;
    float minDistance;

    Vector3 startPosition;

    public bool isActive = false;
    // Use this for initialization
    void Awake()
    {

    }

	void Start () {
        target = GameManager.getPlayer(0);

        minDistance = 7f;
        //transform.position = new Vector3(transform.position.x, 1f, transform.position.z);
        startPosition = transform.position;
        agent = GetComponent<NavMeshAgent>();

        StartCoroutine("wakeMeUp"); 
	}

    IEnumerator wakeMeUp()
    {
        
        yield return new WaitForSeconds(1.0F);
        isActive = true;
    }


    void doAI()
    {
        float p1Distance = Vector3.Distance(transform.position, target.transform.position);
        if (p1Distance < minDistance)
        {

            agent.SetDestination(target.transform.position);

        }
        else
        {
            agent.SetDestination(startPosition);
        }

    }
	// Update is called once per frame
	void Update () {

        if (isActive)
        {
            doAI();
        }


        
	}
}
