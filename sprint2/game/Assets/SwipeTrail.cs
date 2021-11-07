using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwipeTrail : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
            || Input.GetMouseButton(0))
        {
            this.GetComponent<RawImage>().enabled = true;
            this.transform.position = Input.mousePosition;
        }
        else
        {
            this.GetComponent<RawImage>().enabled = false;
        }
    }
}
