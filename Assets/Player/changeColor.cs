using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeColor : MonoBehaviour
{
    public void SetRed()
    {
        GetComponent<MeshRenderer>().material.color = Color.red;
    }
    public void SetBlue()
    {
        GetComponent<MeshRenderer>().material.color = Color.blue;
    }

}
