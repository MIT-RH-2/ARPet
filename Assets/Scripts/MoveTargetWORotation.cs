using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTargetWORotation : MonoBehaviour{
    public Transform Tracker;

    // Start is called before the first frame update
    void Start(){
        Tracker = GameObject.FindGameObjectWithTag("Target").transform;

        Vector3 targetRotation = new Vector3(0.0f, Tracker.rotation.eulerAngles.y, 0.0f);
        this.transform.localEulerAngles = targetRotation;
    }

    // Update is called once per frame
    void Update(){
        this.transform.position = Tracker.position;
    }
}
