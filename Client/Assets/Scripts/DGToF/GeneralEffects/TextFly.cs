using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextFly : MonoBehaviour
{
    private Vector3 savedPos;
    public float swaySpeed;
    public float flyHeight;
    // Use this for initialization
    void Start ()
    {
        savedPos = this.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        this.transform.position = savedPos + new Vector3(0, (float)System.Math.Sin(Time.time* swaySpeed) * flyHeight, 0);
    }
}
