using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroBehaviour : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeOut());
    }
    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(8.0f);
        gameObject.SetActive(false);

    }

}
