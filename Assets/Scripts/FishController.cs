using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

public class FishController : MonoBehaviour
{
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
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
        


	}
}
