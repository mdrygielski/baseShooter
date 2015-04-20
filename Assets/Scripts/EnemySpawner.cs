using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

    public int spawnAmount;
    public int spawnCounter = 0;

    public bool isActive;

    float spawnDelta = .2f;
    float spawnDeltaCounter = 0;
	// Use this for initialization
	void Start () {
        spawnAmount = Random.Range(4,8);

        /*
        for (int i = 0; i < spawnAmount; i++)
        {
            
            spawn();
        }
         */

	}

    void spawn()
    {
        if (spawnCounter <= spawnAmount)
        {
            GameObject enemyObj = (GameObject)Instantiate(Resources.Load("Prefabs/Enemy"));
            // enemyObj.GetComponent<Rigidbody>().gameObject.SetActive(false);
            Guard enemy = enemyObj.GetComponent<Guard>();
            enemyObj.transform.parent = transform;
            enemyObj.transform.position = transform.position;
            spawnCounter++;
            //enemyObj.GetComponent<Rigidbody>().gameObject.SetActive(true);
        }

    }
	// Update is called once per frame
	void Update () {

        if (Vector3.Distance(transform.position, GameManager.getPlayer(0).transform.position) < 13)
        {
            isActive = true;
        }
        else
        {
            isActive = false;
        }


        if (isActive)
        {
            spawnDeltaCounter += Time.deltaTime;
            if (spawnDeltaCounter > spawnDelta)
            {
                spawn();
                spawnDeltaCounter = 0;
            }
        }
	}

    void OnDrawGizmos()
    {
        if (!isActive)
        {
            Gizmos.color = Color.yellow;
            
        }
        else
        {
            Gizmos.color = Color.green;

        }
        Gizmos.DrawCube(transform.position, new Vector3(.5f, .5f, .5f));
    }
}
