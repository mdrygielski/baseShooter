/*
 * Manage part of game actions
 * 
 * to do:
 * choose place to put key&door objects, when room decoration will be implemented  
 * 
 * 
 * */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
    public static NetworkManager networkManager;
    public static GameObject gameManager;

    public static List<GameObject> players;


	// Use this for initialization
	void Start () {
        networkManager =  gameObject.AddComponent<NetworkManager>();
        players = new List<GameObject>();

        //this is instance of GameManager
        gameManager = gameObject;


	}
    public static GameObject getPlayer(int index){
        return players[index];
    }
    
    public static int createMap(int seed){
        Debug.Log("Creating Map");
        Random.seed = seed;

        removeMap();
        Debug.Log("Removing old Map");
        gameManager.gameObject.AddComponent<MazeGenerator>();
        
        Debug.Log("Map has been created");
        return seed;
    }

    static void removeMap(){
        
        Destroy(gameManager.GetComponent<MazeGenerator>());
        
        GameObject gm = GameObject.Find("RoomManager");
        Destroy(gm);

    }
    

}
