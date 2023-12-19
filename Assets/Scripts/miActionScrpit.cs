using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class miActionScrpit : MonoBehaviour
{
    private InputAction myAction;
    [Space][SerializeField] private InputActionAsset myActionsAsset;
    void Start()
    {
        //myAction = myActionsAsset.FindAction("XRI LeftHand/MyAction");
    }
    void Update()
    {
        //if (myAction.WasPerformedThisFrame())
        //{
        //    Debug.Log("PULSADO");
        //}

    }
}
