using UnityEngine;

public class PulseEmission : MonoBehaviour
{
    public Color BaseColor = Color.red;

    private Material _material;

    public void Awake()
    {
        _material = GetComponent<Renderer>().material;
    }

    void Update()
    {
        _material.SetColor("_EmissionColor", BaseColor * Mathf.LinearToGammaSpace(Mathf.PingPong(Time.time, 0.5f)));
    }
}
