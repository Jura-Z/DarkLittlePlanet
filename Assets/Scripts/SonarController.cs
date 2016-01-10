using UnityEngine;
using System.Collections;

public class SonarController : MonoBehaviour {

    public AudioSource sonarSoundFX;
    public AnimatedProjector animatedProjector;
    public float maxSonarRadius = 20;
    public float sonarTime = 5;
    public Transform sonar;

    private Vector3 sonarScale = Vector3.one;

    private bool isEmittingSonar = false;

    private float sonarStartTime = 0f;

	// Use this for initialization
	void Start () {
        animatedProjector.sonarTime = 0f;
        sonar.localScale = Vector3.one;
    }
	
	// Update is called once per frame
	void Update ()
    {
	    if(Input.GetKeyDown(KeyCode.Space) && sonarSoundFX.isPlaying == false)
        {
            sonarSoundFX.Play();
            sonarStartTime = Time.time;
            isEmittingSonar = true;
        }

        if(isEmittingSonar == true)
        {
            float sonarTimeNormalized = (Time.time - sonarStartTime) / sonarTime;
            float sonarRadius = Mathf.Lerp(0, maxSonarRadius, sonarTimeNormalized);
            animatedProjector.sonarTime = sonarRadius;
            sonarScale.x = sonarRadius * 2;
            sonarScale.y = sonarRadius * 2;
            sonarScale.z = sonarRadius * 2;

            sonar.localScale = sonarScale;
           //     Debug.Log(sonar.localScale);

            if (sonarTimeNormalized > 1)
            {
                animatedProjector.sonarTime = 0f;
                isEmittingSonar = false;
                sonar.localScale = Vector3.one;
            }
        }
	}

    private IEnumerator EmitSonar()
    {

        yield return null;
    }
}
