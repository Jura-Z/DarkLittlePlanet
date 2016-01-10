using UnityEngine;
using System.Collections;

public class JellyController : MonoBehaviour {

    public AudioSource sonarFeedbackSoundFX;
    public AudioSource pickupSound;
    public ParticleSystem particleFX;


    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("Player eats me");
            particleFX.transform.parent = null;
            particleFX.Play();
            this.gameObject.SetActive(false);
            pickupSound.transform.parent = null;
            pickupSound.Play();
           // Invoke(KillMe, )
        }

        if (other.tag == "Sonar")
        {
            sonarFeedbackSoundFX.Stop();
            sonarFeedbackSoundFX.Play();
            Debug.Log("Sonar detected me");
        }
    }

    private void KillMe()
    {

    }
}
