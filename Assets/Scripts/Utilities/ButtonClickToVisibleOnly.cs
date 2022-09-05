using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClickToVisibleOnly : MonoBehaviour
{
    public float alphaTreshold = 0.1f;

    /*
     * IMAGE MUST HAVE IN SETTINGS
     *          TEXTURE TYPE - SPRITE (2D AND UI)
     *          READ/WRITE ENABLED
     */


    void Start()
    {
        this.GetComponent<Image>().alphaHitTestMinimumThreshold = alphaTreshold;
    }

}