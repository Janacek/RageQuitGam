using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightUpZone : MonoBehaviour {
    public GameObject BossRef;

    // Use this for initialization
    void OnEnable() {
        BossRef.GetComponent<BossMovements>().MoveIn = 2f;

    }

    // Update is called once per frame
    void Update () {
		
	}
}
