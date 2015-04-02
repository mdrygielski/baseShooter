/*
 * This is about how payer movement works
 * 
 * 
 * To do:
 * keep parent moving with childer
 * 
 * */

using UnityEngine;
using System.Collections;



public class PlayerController : MonoBehaviour {

    //player movement speed
    public float sensivity;

    CharacterController controller;
    NetworkView networkView;
    //public Camera playerCamera;




    //helpers
    Vector3 pointToLookAt;
    Vector3 startPoint;
    float shootDistance;


    //effects
    //public ParticleSystem hitEffect;

	void Start () {
        controller = GetComponent<CharacterController>();
        //networkView = GetComponent<NetworkView>();
        networkView = GetComponentInParent<NetworkView>();

        
       
        
	}
	

	void Update () {

        //Do all actions only if I'am the owner
        if (!networkView.isMine)
        {
            return;
        }

        //keep camera along to player
        Camera.main.transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
        Camera.main.transform.position = new Vector3(gameObject.transform.position.x, 18, gameObject.transform.position.z);

        

        
        //get mouse position
        pointToLookAt = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.y-gameObject.transform.position.y));
        
        //rotate player along with mouse cursor
        gameObject.transform.rotation = Quaternion.LookRotation(pointToLookAt - transform.position, Vector3.up);

        //set shootDistance, after tests that distance will be constant
        //shootDistance = Vector3.Distance(transform.position, pointToLookAt);
        shootDistance = 30;
        Vector3 direction = transform.TransformDirection(Vector3.forward * shootDistance);

        RaycastHit hit;
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Shot Pressed");

            if (Physics.Raycast(transform.position, direction, out hit))
            {
                Debug.Log("Shot Fired");
             
                    ParticleSystem hitEffect = (ParticleSystem)Network.Instantiate(Resources.Load("Effects/hitEffect"), hit.point, Quaternion.identity,0);
                    //Destroy(hitEffect, hitEffect.duration);
               




                //Debug.Log(hit.transform.gameObject);
            }
        }
        

       
        //simple movement of player object


        if (Input.GetKey(KeyCode.W))
        {

            //Vector3 forward = transform.TransformDirection(Vector3.forward);
            //controller.SimpleMove(forward * sensivity);
           
            controller.SimpleMove(Vector3.forward * sensivity);
            
        }
        if (Input.GetKey(KeyCode.S))
        {
            //Vector3 backward = transform.TransformDirection(Vector3.back);
            //controller.SimpleMove(backward * sensivity);

            controller.SimpleMove(Vector3.back * sensivity);
        }
        if (Input.GetKey(KeyCode.A))
        {
            //Vector3 left = transform.TransformDirection(Vector3.left);
            //controller.SimpleMove(left * sensivity);

            controller.SimpleMove(Vector3.left * sensivity);
        }
        if (Input.GetKey(KeyCode.D))
        {
            //Vector3 right = transform.TransformDirection(Vector3.right);
            //controller.SimpleMove(right * sensivity);

            controller.SimpleMove(Vector3.right * sensivity);
        }

	}


    void OnDrawGizmos()
    {
        Vector3 startPoint = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        Gizmos.color = Color.red;
       // Vector3 pos = new Vector3(transform.position.x, transform.position.y, transform.position.z + 2);
        Vector3 direction = transform.TransformDirection(Vector3.forward* shootDistance);
        //Vector3 direction = pointToLookAt- transform.position;
        Collider col = GetComponent<Collider>();
        //Vector3 direction = (Vector3.forward * shootDistance) - pos;
        Vector3 pos = startPoint+(Vector3.forward*2);
        //Vector3 direction = (pos - transform.position).normalized * shootDistance;
        //Gizmos.DrawRay(transform.localPosition, direction);
        Gizmos.DrawLine(transform.position, direction);
        //Debug.Log(pos);
    }
}
