using UnityEngine;
using System.Collections;

public class GodRays : MonoBehaviour
{
    public float maxlifetime = 5.0f;
    public float maxcoord = 500.0f;

    float startlifetime = 2.0f;
    float currentlifetime = 2.0f;

    public float movespeed = 2.0f;
    public float shrinkspeed = 2.0f;

    public float maxAlpha = 0.005f;

    public Color color = new Color(0.8f, 0.9f, 1.0f, 1.0f);

    Renderer _renderer;

    void Randomize()
    {
        this.transform.position = new Vector3(Random.Range(0.0f, maxcoord), 0, Random.Range(0.0f, maxcoord));
        this.transform.localScale = new Vector3(Random.Range(6.0f, 10.0f), this.transform.localScale.y, this.transform.localScale.z);
        startlifetime = Random.Range(0.2f, maxlifetime);
        currentlifetime = startlifetime;
    }

	// Use this for initialization
	void Start ()
    {
        _renderer = GetComponent<Renderer>();
        Randomize();
	}
	
	// Update is called once per frame
	void Update ()
    {
        currentlifetime -= Time.deltaTime;

        this.transform.position += new Vector3(Time.deltaTime * movespeed, 0, 0);
        this.transform.localScale -= new Vector3(Time.deltaTime * shrinkspeed, 0, 0);

        float a01 = Mathf.Clamp01(currentlifetime / startlifetime);
        float a0pi = a01 * Mathf.PI;
        float a = Mathf.Sin(a0pi) * maxAlpha;
        _renderer.material.SetColor("_TintColor", new Color(color.r, color.g, color.b, a));

        if (currentlifetime < 0.0f)
            Randomize();
	}
}
