using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    
    public float missionOrderStartTime;
    private AudioSource missionOrder;

    void Start()
    {
        missionOrder = GetComponent<AudioSource>();
        StartCoroutine( GiveMission() );
    }

    public IEnumerator GiveMission ()
    {
        yield return new WaitForSeconds(missionOrderStartTime);
        missionOrder.Play();
        Debug.Log("Hello Agent 247. Yesturday you forgot your hammer next to the external generator. Today, your mission is to get your hammer back, and come back to the martian module.");
    }
}
