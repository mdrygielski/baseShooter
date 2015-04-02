/*
 * This is about how payer movement works
 * 
 * 
 * To do:
 * 
 * 
 * */

using UnityEngine;
using System.Collections;



public class PlayerController : MonoBehaviour {

    //player movement speed
    public float sensivity;

    //components
    CharacterController controller;
    NetworkView networkView;
    Camera playerCamera;




    //helpers
    Vector3 pointToLookAt;
    Vector3 startPoint;
    float shootDistance;
    Quaternion playerCameraRotation;



    void Awake()
    {
        //get components and carry about camera to not possess only by owner
        networkView = GetComponentInParent<NetworkView>();
        playerCamera = GetComponentInChildren<Camera>();
        playerCameraRotation = playerCamera.transform.rotation;
        if (networkView.isMine)
        {

            playerCamera.enabled = true;
        }
        else
        {

            playerCamera.enabled = false;
        }
    }


    void LateUpdate()
    {
        //keep camera rotation in initialized rotation
        if (networkView.isMine)
        {

            playerCamera.transform.rotation = playerCameraRotation;
        }
       
    }

	void Start () {

        //get rest of components
        controller = GetComponent<CharacterController>();
                
	}
	

	void Update () {

        //Do all actions only if I'am the owner
        if (!networkView.isMine)
        {
            return;
        }

       
        //get mouse position
        pointToLookAt = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.y-gameObject.transform.position.y));
        
        //rotate player along with mouse cursor
        gameObject.transform.rotation = Quaternion.LookRotation(pointToLookAt - transform.position, Vector3.up);

        //set shootDistance, after tests that distance will be constant or will depend on kind of weapon
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
                    //destroying that effect is implemented in script attached to this prefab
            }
        }
        

       
        //simple movement of player object

        if (Input.GetKey(KeyCode.W))
        {

            controller.SimpleMove(Vector3.forward * sensivity);
            
        }
        if (Input.GetKey(KeyCode.S))
        {

            controller.SimpleMove(Vector3.back * sensivity);
        }
        if (Input.GetKey(KeyCode.A))
        {

            controller.SimpleMove(Vector3.left * sensivity);
        }
        if (Input.GetKey(KeyCode.D))
        {

            controller.SimpleMove(Vector3.right * sensivity);
        }

	}


    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Vector3 direction = transform.TransformDirection(Vector3.forward * shootDistance);
        Gizmos.DrawLine(transform.position, direction);

    }
}
