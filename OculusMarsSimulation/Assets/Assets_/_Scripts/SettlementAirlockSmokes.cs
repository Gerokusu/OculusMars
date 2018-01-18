using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettlementAirlockSmokes : MonoBehaviour {

    public List<GameObject> smokes;

    public void BlastSmokes ()
    {
        foreach (GameObject smoke in smokes)
        {
            smoke.GetComponent<ParticleSystem>().Play();
        }
    }
}
