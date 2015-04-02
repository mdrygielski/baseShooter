using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {

    string registeredGameName = "serverName-baseShoter";
    bool isRefreshing = false;
    float refreshLength = 3f;
    HostData[] hostData;
    public static NetworkView networkView;

    public int mapSeed;

    void Start()
    {
        networkView = gameObject.AddComponent<NetworkView>();
        networkView.stateSynchronization = NetworkStateSynchronization.Unreliable;
        Debug.Log(networkView.stateSynchronization);
    }

    private void StartServer()
    {
        mapSeed = Random.Range(0,99999);
        Network.InitializeServer(2, 25002, false);
        MasterServer.RegisterHost(registeredGameName, "baseShoter", "comment");
        GameManager.createMap(mapSeed);
    }

    public IEnumerator RefreshHostList()
    {
        Debug.Log("Refreshing...");
        MasterServer.RequestHostList(registeredGameName);

        float timeStarted = Time.time;
        float timeEnd = Time.time + refreshLength;

        while (Time.time < timeEnd)
        {

            hostData = MasterServer.PollHostList();
            yield return new WaitForEndOfFrame();
        }

        if (hostData == null || hostData.Length == 0)
        {
            Debug.Log("No servers found");
        }
        else
        {
            Debug.Log(hostData.Length+" servers has been found");
            
        }
    }

    void OnServerInitialized()
    {
        //ustawienei ziarna
        //ladowanie terenu
        //generacja mapy dla serwera
        //ustawienie kamery
        //spawn gracza
        Debug.Log("Server initialazed");
        //GameManager.createMap(mapSeed);
        SpawnPlayer();
    }

    void OnMasterServerEvent(MasterServerEvent masterServerEvent)
    {
        if (masterServerEvent == MasterServerEvent.RegistrationSucceeded)
        {
            Debug.Log("Server registered");
        }
    }


    public void OnGUI()
    {
        if (Network.isServer)
        {
            GUILayout.Label("Its a server");
        }
        if (Network.isClient)
        {
            GUILayout.Label("Its a client");
        }
        GUI.Box(new Rect(100f, 0f, 150f, 30f), "Map seed:"+mapSeed);
        

        if (GUI.Button(new Rect(25f, 25f, 150f, 30f), "Start new server"))
        {
            //start server
            StartServer();
        }

        if (GUI.Button(new Rect(25f, 65f, 150f, 30f), "Refresh list"))
        {
            //refresh list
            StartCoroutine("RefreshHostList");
        }

        if (GUI.Button(new Rect(25f, 105f, 150f, 30f), "Spawn Me"))
        {
            //refresh list
            SpawnPlayer();
        }

        if (hostData != null)
        {
            for (int i = 0; i < hostData.Length; i++)
            {
                if (GUI.Button(new Rect(Screen.width / 2, 65f + (30f * i), 300f, 30f), hostData[i].gameName))
                {
                    Network.Connect(hostData[i]);

                }
            }
        }
    }

    [RPC]
    public int getSeed()
    {
        return mapSeed;
    }

    private void SpawnPlayer()
    {
        Debug.Log("Spawning player");
        Network.Instantiate(Resources.Load("Prefabs/Player"),new Vector3(0,1,0),Quaternion.identity,0);
    }

    void OnPlayerDisconnected(NetworkPlayer player)
    {
        Debug.Log("Player disconnected from" + player.ipAddress + ":" + player.port);
        Network.RemoveRPCs(player);
        Network.DestroyPlayerObjects(player);
    }

    void OnPlayerConnected(NetworkPlayer player)
    {
        //BitStream bs = new BitStream();
        //bs.Serialize(ref mapSeed);
        if (Network.isServer)
        {
            networkView.RPC("setSeed", player, mapSeed);
            networkView.RPC("createMap", player, mapSeed);
        }

        
        //SpawnPlayer();
        //networkView.RPC("getSeed", RPCMode.Server);
        //GameManager.createMap(mapSeed);
        Debug.Log("Client Connected OnPlayerConnected");
    }


    void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
    {
        if (stream.isWriting)
        {
            
            stream.Serialize(ref mapSeed);
        }
        else
        {
            int seed = 0;
            stream.Serialize(ref seed);
            mapSeed = seed;
        }
    }

    void OnApplicationQuit()
    {
        if (Network.isServer)
        {
            Network.Disconnect(200);
            MasterServer.UnregisterHost();

        }
        if (Network.isClient)
        {
            Network.Disconnect(200);
            

        }
    }

    [RPC]
    public void setSeed(int num)
    {
        mapSeed = num;
    }

    [RPC]
    public void createMap(int seed)
    {
        GameManager.createMap(seed);
    }

    void Update()
    {


    }

}
