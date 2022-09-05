using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class VersionGameView : MonoBehaviour
{
    private Text txtVersion;

    private void Awake()
    {
        txtVersion = GetComponent<Text>();
    }

    private void Start()
    {
        if (txtVersion)
        {
#if UNITY_ANDROID || UNITY_IOS
            txtVersion.text = string.Format("Version {0}", Application.version);
#endif
        }
    }
}
