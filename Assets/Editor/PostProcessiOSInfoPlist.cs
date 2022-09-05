#if UNITY_IOS
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;

public class PostProcessiOSInfoPlist : MonoBehaviour
{

    [PostProcessBuildAttribute()]
    public static void OnPostprocessBuild(BuildTarget target, string pathToBuiltProject)
    {
        if (target != BuildTarget.iOS)
            return;

        string plistPath = pathToBuiltProject + "/Info.plist";

        PlistDocument plist = new PlistDocument();

        plist.ReadFromString(System.IO.File.ReadAllText(plistPath));

        //remove capability keys to avoid Unity adding MORE capabilities (which is illegal on the App Store)
        List<PlistElement> reqs = plist.root.values["UIRequiredDeviceCapabilities"].AsArray().values;

        for (int i = reqs.Count - 1; i >= 0; i--)
        {
            if (reqs[i].AsString() != "armv7")
            {
                reqs.RemoveAt(i);
            }
        }

        System.IO.File.WriteAllText(plistPath, plist.WriteToString());
    }
}
#endif