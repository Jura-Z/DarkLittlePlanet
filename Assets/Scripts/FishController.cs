using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

[RequireComponent(typeof(Rigidbody))]
public class FishController : MonoBehaviour
{
    public float speed = 10f;
    public float maxVelocityChange = 10.0f;
    private Rigidbody characterRigidBody;
     
    static FishController instance = null;
    public static FishController Instance {
        get { Assert.IsNotNull(instance); return instance; }
    }


    public List<GameObject> collectables = new List<GameObject>();
    public Transform player;

    // ----------------------------------------------------
    public void AddCollecatlbe(GameObject collectable)
    {
        collectables.Add(collectable);
    }

    // ----------------------------------------------------
    public void RemoveCollecatlbe(GameObject collectable)
    {
        collectables.Remove(collectable);
    }

    // ----------------------------------------------------
    public GameObject FindClosest(out float distance)
    {
        int indx = GetIndxOfClosest(out distance);
        return (indx == -1) ? null : collectables[indx];
    }

    // ----------------------------------------------------
    // ----------------------------------------------------

    void Awake()
    {
        Assert.IsNotNull(player, "player == null");

        Assert.IsNull(instance);
        instance = this;
    }

    int GetIndxOfClosest(out float distance)
    {
        int min = -1;
        distance = float.MaxValue;

        for (int i = 0; i < collectables.Count; ++i)
        {
            GameObject c = collectables[i];
            Vector3 vec = player.transform.position - c.transform.position;

            if (distance > vec.sqrMagnitude)
            {
                distance = vec.sqrMagnitude;
                min = i;
            }
        }

        if (min != -1)
            distance = Mathf.Sqrt(distance);
        return min;
    }

	// Use this for initialization
	void Start ()
    {
        characterRigidBody = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update ()
    {

        // Calculate how fast we should be moving
        Vector3 targetVelocity = new Vector3(0f, 0f, Input.GetAxis("Vertical"));
        Vector3 turnVelocity = new Vector3(0f, Input.GetAxis("Horizontal"), 0f); ;
        targetVelocity = transform.TransformDirection(targetVelocity);
        targetVelocity *= speed;

        // Apply a force that attempts to reach our target velocity
        Vector3 velocity = characterRigidBody.velocity;
        Vector3 velocityChange = (targetVelocity - velocity);
        velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
        velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
        velocityChange.y = 0;

        Quaternion deltaRotation = Quaternion.Euler(turnVelocity * Time.deltaTime * 100);
        characterRigidBody.MoveRotation(characterRigidBody.rotation * deltaRotation);
        characterRigidBody.AddForce(velocityChange, ForceMode.VelocityChange);
    }
}
