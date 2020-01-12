using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTrial : MonoBehaviour
{

    [SerializeField]
    private Transform target;

    [SerializeField]
    private Vector3 offsetPosition;

    [SerializeField]
    private Space offsetPositionSpace = Space.Self;

    [SerializeField]
    private bool lookAt = true;

    private Vector3 restrictedOffset;
    public void Start()
    {
        restrictedOffset = transform.position - target.position;
    }
    private void Update()
    {
        Refresh();
    }

    public void Refresh()
    {
        if (GameScript.GamePhase == 2)
        {
            if (target == null)
            {
                Debug.LogWarning("Missing target ref !", this);

                return;
            }

            // compute position
            if (offsetPositionSpace == Space.Self)
            {
                transform.position = target.TransformPoint(offsetPosition);
            }
            else
            {
                transform.position = target.position + offsetPosition;
            }

            // compute rotation
            if (lookAt)
            {
                transform.LookAt(target);
            }
            else
            {
                transform.rotation = target.rotation;
            }
        }
        else
            RestrictedMovement();
    }

    private void RestrictedMovement()
    {
        transform.position = target.position + restrictedOffset;
    }

    //public GameObject player;        //Public variable to store a reference to the player game object


    //private Vector3 offset;            //Private variable to store the offset distance between the player and camera

    //// Use this for initialization
    //void Start()
    //{
    //    player = GameObject.Find("RedRidingCap");
    //    //Calculate and store the offset value by getting the distance between the player's position and camera's position.
    //    offset = transform.position - player.transform.position;
    //}

    //// LateUpdate is called after Update each frame
    //void LateUpdate()
    //{
    //    // Set the position of the camera's transform to be the same as the player's, but offset by the calculated offset distance.
    //    transform.position = player.transform.position + offset;
    //    //if (GameScript.GamePhase == 2)
    //    //{
    //    //    // Rotation
    //    //    float horizontalAxis = Input.GetAxis("Horizontal");
    //    //    transform.Rotate(0, horizontalAxis, 0);
    //    //    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.FromToRotation(transform.up, GetHitNormal()) * transform.rotation, 0.1f * Time.deltaTime);
    //    //}
    //}


    //private Vector3 GetHitNormal()
    //{
    //    RaycastHit hit;
    //    if (Physics.Raycast(transform.position, Vector3.down, out hit))
    //        return hit.normal;
    //    else
    //        return Vector3.zero;
    //}
}
