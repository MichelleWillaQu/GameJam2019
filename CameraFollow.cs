
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform target;

    public float smoothspeed = 0.25f;
    public Vector3 offset;


     void LateUpdate()
    {

        transform.position = target.position + offset ; 
    }





}
