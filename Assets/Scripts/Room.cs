using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Room : MonoBehaviour {

    public enum Direction
    {
        NONE,
        N,
        E,
        S,
        W
        
    };

    //public Direction entrance;
    //public Direction[] entrances = new Direction[4];
    //GameObject wallN, wallE, wallS, wallW, collumn0, collumn1, collumn2, collumn3;
    //bool roomsWasCreated;

    int roomsToCreate;
    Direction[] directionsToCreate;

    int roomsCreated;
    int roomToCreate;

    float roomStep;

    bool isWaiting;



	// Use this for initialization
	void Start () {

        

        isWaiting = false;

        roomStep = 15f;
        roomsCreated = 0;

        //roomsWasCreated = false;
        roomsToCreate = Random.Range(1, 4);

        directionsToCreate = new Direction[roomsToCreate];
        //Debug.Log("Rooms to create:" + roomsToCreate + "len"+directionsToCreate.Length);
        initiateRoom();

        setDirections();






        //Debug.Log(directionsToCreate[roomsCreated]);
       
	}
	
	// Update is called once per frame
	void Update () {
        if (!RoomManager.limitReached())
        {
            if (!myJobIsDone())
            {
                //Debug.Log(RoomManager.rooms.Count);
                createRooms();
            }
            else
            {
                
            }
        }
        
	
	}

    void createRooms()
    {
        isWaiting = true;

            if (directionsToCreate[roomsCreated] == Direction.N)
            {
                
                Vector3 positionToCheck = new Vector3(Mathf.RoundToInt(transform.position.x), 0, Mathf.RoundToInt(transform.position.z + roomStep));
                int roomIndex = RoomManager.roomsIsExists(positionToCheck);
                if (roomIndex==-1)
                {
                    roomsCreated++;
                    GameObject roomObj = (GameObject)Instantiate(Resources.Load("Prefabs/Room"));
                    
                    roomObj.transform.position = positionToCheck;
                    Room room = roomObj.GetComponent<Room>();

                    removeWall(Direction.N, true);
                    room.removeWall(Direction.S);


                    isWaiting = false;

                    RoomManager.addRoom(roomObj);

                }
                


            }else if (directionsToCreate[roomsCreated] == Direction.E)
            {
                
                Vector3 positionToCheck = new Vector3(Mathf.RoundToInt(transform.position.x + roomStep), 0, Mathf.RoundToInt(transform.position.z));
                int roomIndex = RoomManager.roomsIsExists(positionToCheck);
                if (roomIndex == -1)
                {
                    roomsCreated++;
                    GameObject roomObj = (GameObject)Instantiate(Resources.Load("Prefabs/Room"));
                    roomObj.transform.position = positionToCheck;
                    Room room = roomObj.GetComponent<Room>();

                    removeWall(Direction.E, true);
                    room.removeWall(Direction.W);


                    isWaiting = false;
                    RoomManager.addRoom(roomObj);
                    
                }
            }else if (directionsToCreate[roomsCreated] == Direction.S)
            {
                
                Vector3 positionToCheck = new Vector3(Mathf.RoundToInt(transform.position.x), 0, Mathf.RoundToInt(transform.position.z - roomStep));
                int roomIndex = RoomManager.roomsIsExists(positionToCheck);
                if (roomIndex == -1)
                {
                    roomsCreated++;
                    GameObject roomObj = (GameObject)Instantiate(Resources.Load("Prefabs/Room"));
                    roomObj.transform.position = positionToCheck;
                    Room room = roomObj.GetComponent<Room>();
                    removeWall(Direction.S, true);
                    room.removeWall(Direction.N);


                    isWaiting = false;
                    RoomManager.addRoom(roomObj);
                    
                }
            }else if (directionsToCreate[roomsCreated] == Direction.W)
            {
                
                Vector3 positionToCheck = new Vector3(Mathf.RoundToInt(transform.position.x - roomStep), 0, Mathf.RoundToInt(transform.position.z));
                int roomIndex = RoomManager.roomsIsExists(positionToCheck);
                if (roomIndex == -1)
                {
                    roomsCreated++;
                    GameObject roomObj = (GameObject)Instantiate(Resources.Load("Prefabs/Room"));
                    roomObj.transform.position = positionToCheck;
                    Room room = roomObj.GetComponent<Room>();
                    removeWall(Direction.W, true);
                    room.removeWall(Direction.E);


                    isWaiting = false;
                    RoomManager.addRoom(roomObj);
                    
                }
            }
       
        if(isWaiting){
            setDirections();
        }

    }

    void setDirections()
    {
        directionsToCreate = new Direction[roomsToCreate];
        int idx = 0;
        Direction tmpDirection;
        while (idx < roomsToCreate)
        {
            tmpDirection = getDirectionByInt(Random.Range(0, 4));
            if (!directionIsInArray(directionsToCreate, tmpDirection))
            {
                directionsToCreate[idx] = tmpDirection;
                //Debug.Log("Rooms Directions:" + tmpDirection);
                idx++;
            }
        }
    }

    Direction getDirectionByInt(int num)
    {
        if (num > -1 && num < 4)
        {
            if (num == 0)
            {
                return Direction.N;
            }
            if (num == 1)
            {
                return Direction.E;
            }
            if (num == 2)
            {
                return Direction.S;
            }

            return Direction.W;
        }
        else
        {
            return Direction.NONE;
        }
    }

    public void initiateRoom()
    {


    }

    public void removeWall(Direction _direction, bool keepCollums = false)
    {
        if (_direction.Equals(Direction.N))
        {

            List<GameObject> childrens = new List<GameObject>();
            foreach (Transform child in transform)
            {
                childrens.Add(child.gameObject);
            }
            //Destroy(childrens[0]);
            childrens[0].SetActive(false);
            if (!keepCollums)
            {
                //Destroy(childrens[4]);
                //Destroy(childrens[5]);
                childrens[4].SetActive(false);
                childrens[5].SetActive(false);
            }

        }
        else if (_direction.Equals(Direction.E))
        {

            List<GameObject> childrens = new List<GameObject>();
            foreach (Transform child in transform)
            {
                childrens.Add(child.gameObject);
            }
            //Destroy(childrens[1]);
            childrens[1].SetActive(false);
            if (!keepCollums)
            {
                //Destroy(childrens[5]);
                //Destroy(childrens[6]);
                childrens[5].SetActive(false);
                childrens[6].SetActive(false);
            }

        }else if (_direction.Equals(Direction.S))
        {

            List<GameObject> childrens = new List<GameObject>();
            foreach (Transform child in transform)
            {
                childrens.Add(child.gameObject);
            }
            //Destroy(childrens[2]);
            childrens[2].SetActive(false);
            if (!keepCollums)
            {
                //Destroy(childrens[6]);
                //Destroy(childrens[7]);
                childrens[6].SetActive(false);
                childrens[7].SetActive(false);
            }

        }else if (_direction.Equals(Direction.W))
        {

            List<GameObject> childrens = new List<GameObject>();
            foreach (Transform child in transform)
            {
                childrens.Add(child.gameObject);
            }
            //Destroy(childrens[3]);
            childrens[3].SetActive(false);
            if (!keepCollums)
            {
                //Destroy(childrens[4]);
                //Destroy(childrens[7]);
                childrens[4].SetActive(false);
                childrens[7].SetActive(false);
            }

        }
       
    }
    bool directionIsInArray(Direction[] array, Direction d)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] == d)
            {
                return true;
            }
        }
        return false;
    }


    bool myJobIsDone()
    {
        if (roomsCreated < roomsToCreate)
        {
            return false;
        }
        else
        {
            return true;
            
        }
    }
}
