using UnityEngine;
using System.Collections;

public class PickupObject : MonoBehaviour {

    void Start()
    {
        FishController.Instance.AddCollecatlbe(this.gameObject);
	}
	
    void OnDestroy()
    {
        OnPickUp();
    }

    void OnPickUp()
    {
        FishController.Instance.RemoveCollecatlbe(this.gameObject);
    }
}
