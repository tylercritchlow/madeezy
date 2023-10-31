using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ButtonScript : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        // Debug.Log("Touching player");
        BroadcastMessage("HandleButtonPress", gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        BroadcastMessage("HandleButtonLeave", gameObject);
    }

    void HandleButtonPress(GameObject gameObject)
    {
        Debug.Log(gameObject.transform.position);
        
        Transform parentObject = gameObject.transform.parent;
        Vector3 tweenPositionStart = gameObject.transform.parent.position;

        DOTween.Init();
        parentObject.transform.DOMoveY(tweenPositionStart.y - 0.2f, 3);
    }

    void HandleButtonLeave(GameObject gameObject)
    {
        Transform parentObject = gameObject.transform.parent;
        Vector3 tweenPositionStart = gameObject.transform.parent.position;

        DOTween.Init();
        parentObject.transform.DOMoveY(tweenPositionStart.y + 0.2f, 3);
    }

    // Above code gets called when any Button is pressed, handle accordingly.


}



