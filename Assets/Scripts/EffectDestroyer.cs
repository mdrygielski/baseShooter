using UnityEngine;
using System.Collections;

[RequireComponent(typeof(ParticleSystem))]

public class EffectDestroyer : MonoBehaviour {

    float timer;
    ParticleSystem ps;
	// Use this for initialization
	void Start () {
        
       

        ps = GetComponent<ParticleSystem>();
        timer = ps.duration;
        //AutoDestruct();
	}
	
	// Update is called once per frame
	void Update () {
        timer -= 0.01f;
        if (timer < 0)
        {
            Destroy(gameObject);
        }

	}

    /*
    IEnumerator AutoDestruct()
     {
         Destroy(gameObject);
         yield return new WaitForSeconds(ps.duration);
         
     }
     * */
}
