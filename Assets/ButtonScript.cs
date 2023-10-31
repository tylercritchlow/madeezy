using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ButtonScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

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
        
        Transform ParentObject = gameObject.transform.parent;
        Vector3 TweenPositionStart = gameObject.transform.parent.position;

        DOTween.Init();
        ParentObject.transform.DOMoveY(TweenPositionStart.y - 0.2f, 3);
    }

    void HandleButtonLeave(GameObject gameObject)
    {
        Transform ParentObject = gameObject.transform.parent;
        Vector3 TweenPositionStart = gameObject.transform.parent.position;

        DOTween.Init();
        ParentObject.transform.DOMoveY(TweenPositionStart.y + 0.2f, 3);
    }

    // Above code gets called when any Button is pressed, handle accorodingly.


}



