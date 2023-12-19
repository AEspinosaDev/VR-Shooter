using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dazzle_behavior : MonoBehaviour
{
    Light l;

    private void Awake()
    {
        l = GetComponent<Light>();
    }
    public void DisableFlare()
    {
        l.enabled = true;

        StartCoroutine(Wait());
    }
    IEnumerator Wait()
    {

        yield return new WaitForSeconds(.03f);
        l.enabled = false;

    }

}
