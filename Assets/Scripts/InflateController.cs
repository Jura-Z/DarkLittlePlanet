using UnityEngine;
using System.Collections;

public class InflateController : MonoBehaviour {

    public GameObject normalState;
    public GameObject inflateState;

	// Use this for initialization
	void Start () {
        normalState.SetActive(true);
        inflateState.SetActive(false);
    }
	
	// Update is called once per frame


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Jelly")
        {
            normalState.SetActive(false);
            inflateState.SetActive(true);

            Invoke("GoBackToNormal", 4);
        }

    }

    private void GoBackToNormal()
    {
        normalState.SetActive(true);
        inflateState.SetActive(false);
    }
}
