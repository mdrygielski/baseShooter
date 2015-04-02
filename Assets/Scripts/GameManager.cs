using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {
    public static NetworkManager networkManager;
    public static GameObject gameManager;


	// Use this for initialization
	void Start () {
        networkManager =  gameObject.AddComponent<NetworkManager>();
        

        //this is instance of GameManager
        gameManager = gameObject;

        

        //MazeGenerator mazeGenerator = gameObject.AddComponent<MazeGenerator>();
        //MazeGenerator mazeGenerator = gameObject.GetComponent<MazeGenerator>();
       
	
	}
    
    public static int createMap(int seed){
        Debug.Log("Creating Map");
        //int seedTemporary = Random.seed;
        Random.seed = seed;

        removeMap();
        Debug.Log("Removing old Map");
        gameManager.gameObject.AddComponent<MazeGenerator>();
        
        //Random.seed = seedTemporary;
        Debug.Log("Map has been created");
        return seed;
    }

    static void removeMap(){
        
        Destroy(gameManager.GetComponent<MazeGenerator>());
        
        GameObject gm = GameObject.Find("RoomManager");
        Destroy(gm);
        //RoomManager gm = GameObject.FindObjectOfType<RoomManager>();
        //if (gm != null)
        //{
            //Destroy(gm[gm.Length-1]);
            //Destroy(gm.GetComponent<RoomManager>());
        //    Debug.Log(gm);
        //    Destroy(gm);
            
            //
        //}
        
        //Destroy(GameObject.Find("RoomManager").GetComponent<RoomManager>());
    }
    
	
	// Update is called once per frame
	void Update () {
	
	}
}
