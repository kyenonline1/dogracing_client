using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableGameobjectByEndAnim : MonoBehaviour
{
    public void DisableGameObject()
    {
        this.gameObject.SetActive(false);
    }
}
