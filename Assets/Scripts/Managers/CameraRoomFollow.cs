using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraRoomFollow : MonoBehaviour
{
    public GameObject virtualCam;
    public CinemachineVirtualCamera cmCam;

    private void OnEnable()
    {
        virtualCam =  this.transform.GetChild(0).gameObject;
        cmCam = virtualCam.GetComponent<CinemachineVirtualCamera>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !other.isTrigger)
        {
            virtualCam.SetActive(true);
            cmCam.Follow = other.gameObject.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !other.isTrigger)
        {
            virtualCam.SetActive(false);
            cmCam.Follow = null;
        }
    }
}
