using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomManager : MonoBehaviour {


    public static List<Vector3> roomsPosition;
    public static List<GameObject> rooms;
    public static int RoomLimit;
    static Transform rootParent;


	// Use this for initialization
	void Start () {
        //find Root object to carry all rooms;
        //rootParent = GameObject.Find("RoomManager").transform;
        rootParent = gameObject.transform;

        //RoomLimit = 100;
        roomsPosition = new List<Vector3>();
        rooms = new List<GameObject>();

        GameObject firstRoom = (GameObject)Instantiate(Resources.Load("Prefabs/Room"));
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
	
	// Update is called once per frame
	void Update () {
	
	}
}
