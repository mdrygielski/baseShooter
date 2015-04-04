/*
 * Manage structures needed to generate maze
 * 
 * 
 * */


using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomManager : MonoBehaviour {


    public static List<Vector3> roomsPosition;
    public static List<GameObject> rooms;
    public static int RoomLimit;
    static Transform rootParent;
    public static List<List<GameObject>> corridors;

    //number of ways going out from init room
    public static int ways;



	void Start () {
        //find Root object to carry all rooms;
        rootParent = gameObject.transform;

        //RoomLimit = 100;
        roomsPosition = new List<Vector3>();
        rooms = new List<GameObject>();

        GameObject firstRoom = (GameObject)Instantiate(Resources.Load("Prefabs/Room"));

        //initialize initRoom parameters;

        Room initRoom = firstRoom.GetComponent<Room>();
        initRoom.setDistance(0); //distance from init room;
        initRoom.setWayId(-1); //wayId, for init room its always -1;

        firstRoom.transform.position = new Vector3(0, 0, 0);
        addRoom(firstRoom);

        //initialization of GameObject 'ground' its useless couse its done by inspector;
        /*
        GameObject ground = (GameObject.CreatePrimitive(PrimitiveType.Cube));
        ground.name = "Ground";
        ground.transform.position = firstRoom.transform.position;
        Vector3 scale = firstRoom.transform.localScale*RoomLimit/2;
        ground.transform.localScale = new Vector3(scale.x,0.1f,scale.z);
        Renderer r = ground.GetComponent<Renderer>();
        r.material = (Material)Resources.Load("Materials/Space");
        */
	}


    public static bool addRoom(GameObject room)
    {
        if (rooms.Count < RoomLimit)
        {

                room.transform.parent = rootParent;
                rooms.Add(room);
                room.name = "Room"+(rooms.Count-1);
                roomsPosition.Add(room.transform.position);
                return true;

        }

        return false;

    }


    public static int roomsIsExists(GameObject room)
    {
        for (int i = 0; i < roomsPosition.Count; i++)
        {
            if (Mathf.RoundToInt(roomsPosition[i].x) == Mathf.RoundToInt(room.transform.position.x) && Mathf.RoundToInt(roomsPosition[i].z) == Mathf.RoundToInt(room.transform.position.z))
            {
                return i;
            }
        }

        return -1;
    }
    public static int roomsIsExists(Vector3 position)
    {
        for (int i = 0; i < roomsPosition.Count; i++)
        {
            if (Mathf.RoundToInt(roomsPosition[i].x) == Mathf.RoundToInt(position.x) && Mathf.RoundToInt(roomsPosition[i].z) == Mathf.RoundToInt(position.z))
            {
                return i;
            }
        }

        return -1;
    }

    public static bool limitReached()
    {
        if (rooms.Count < RoomLimit)
        {
            
            return false;
        }
        else
        {
            
            return true;
        }
    }

    public static void setWays(int _ways)
    {
        //This method initialize array of arrays which length depends on numbers ways outgoing from init room

        ways = _ways;
        corridors = new List<List<GameObject>>();
        for (int i = 0; i < ways; i++)
        {
            corridors.Add(new List<GameObject>());
        }
        //Debug.Log("setWays call:" + _ways+" corridoes:"+corridors.Count);
    }

    
    public static void setRoomWay(GameObject parent, GameObject _room, int numberOfWay)
    {
        //This method build metric for every room, and fill corridors array of arrays, which carry informations about all parts of maze 

        Room parentRoom = parent.GetComponent<Room>();
        Room room = _room.GetComponent<Room>();

        if (parentRoom.isInitRoom)
        {
            //Debug.Log("Parent room set way call");
            room.setWayId(numberOfWay);
           
        }
        else
        {
            room.setWayId(parentRoom.getWayId());
            //Debug.Log("NOT Parent room set way call");
        }
        room.setDistance(parentRoom.distance+1);

        //Debug.Log("WayId:" + room.getWayId());
        corridors[room.getWayId()-1].Add(_room);

    }
    

    //Just for drawing areas on scene preview
    void OnDrawGizmos()
    {
        for (int i = 0; i < corridors.Count; i++)
        {
            if(i==0){
                Gizmos.color = Color.red;
            }
            if(i==1){
                Gizmos.color = Color.green;
            }
            if(i==2){
                Gizmos.color = Color.blue;
            }

            for (int j = 0; j < corridors[i].Count;j++ )
            {

                Gizmos.DrawCube(corridors[i][j].transform.position, new Vector3(15f, 0.1f, 15f));
            }


        }

            


    }
}
