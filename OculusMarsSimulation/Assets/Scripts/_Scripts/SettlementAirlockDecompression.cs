using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettlementAirlockDecompression : MonoBehaviour {

    public float openDoorStartTime;
    public float smokeStartTime;
    public float soundStartTime;
    public float redLightStartTime;
    public float greenLightStartTime;
    
    public GameObject stateLight;
    public GameObject smoke;
    public GameObject doorOut;
    public GameObject doorIn;
    public GameObject environment;

    private AudioSource audioSource;
    private AirlockState state;

    void Start ()
    {
        audioSource = GetComponent<AudioSource>();
        state = AirlockState.Ambient;
	}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && state == AirlockState.Ambient)
        {
            //doorIn.GetComponent<SettlementAirlockDoor>().CloseDoor();
            //doorIn.GetComponent<SettlementAirlockDoor>().LockDoor();
            //StartCoroutine( DecompressAirlock() );
        }
    }

    private IEnumerator DecompressAirlock ()
    {
        float start;
        if (!doorIn.GetComponent<SettlementDoor>().isOpened)
            start = 0;
        else
            start = 2.5f;
        yield return new WaitForSeconds(start);
        state = AirlockState.Decompressing;
        StartCoroutine(PlaySound(soundStartTime));
        StartCoroutine(LightDecompressing(greenLightStartTime));
        StartCoroutine(ActivateSmoke(smokeStartTime));
        StartCoroutine(LightDecompressed(redLightStartTime));
        StartCoroutine(OpenDoorOut(openDoorStartTime));
    }

    private IEnumerator PlaySound(float start)
    {
        yield return new WaitForSeconds(start);
        audioSource.Play();
    }

    private IEnumerator LightDecompressing(float start)
    {
        yield return new WaitForSeconds(start);
        Light light = stateLight.GetComponent<Light>();
        light.GetComponent<AudioSource>().Play();
        light.color = Color.green;
        light.intensity = 5;
    }

    private IEnumerator LightDecompressed(float start)
    {
        yield return new WaitForSeconds(start);
        Light light = stateLight.GetComponent<Light>();
        light.GetComponent<AudioSource>().Play();
        light.color = Color.red;
        light.intensity = 4;
    }

    private IEnumerator ActivateSmoke(float start)
    {
        yield return new WaitForSeconds(start);
        smoke.GetComponent<SettlementAirlockSmokes>().BlastSmokes();
    }

    private IEnumerator OpenDoorOut(float start)
    {
        yield return new WaitForSeconds(start);
        state = AirlockState.Decompressed;
        doorOut.GetComponent<SettlementDoor>().Animate("open");
        environment.GetComponent<EnvironmentSound>().PlayAmbientSound();
    }
}

public enum AirlockState { Ambient, Decompressing, Decompressed };
