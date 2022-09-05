using System.Collections;
using UnityEngine;
using Utilities.Custom;

public class WWWCacheOrDownload
{
    private WWW www;

    public WWWCacheOrDownload(string url)
    {
        string filePath = Application.persistentDataPath;
        filePath += "/" + MD5.GetMd5String(url);
        string loadFilepath = filePath;
        bool web = false;
        bool useCached = false;
        useCached = System.IO.File.Exists(filePath);
        if (useCached)
        {
            //check how old
            System.DateTime written = System.IO.File.GetLastWriteTimeUtc(filePath);
            System.DateTime now = System.DateTime.UtcNow;
            double totalHours = now.Subtract(written).TotalHours;
            if (totalHours > 300)
                useCached = false;
        }
        if (System.IO.File.Exists(filePath))
        {
            string pathforwww = "file:///" + loadFilepath;
            //Debug.Log("TRYING FROM CACHE " + url + "  file " + pathforwww);
            www = new WWW(pathforwww);
        }
        else
        {
            web = true;
            www = new WWW(url);
        }
        MonoInstance.Instance.StartCoroutine(doLoad(www, filePath, web));
    }

    public WWW WWW
    {
        get { return www; }
    }

    //public static WWW GetCachedWWW(string url)
    //{
    //    string filePath = Application.persistentDataPath;
    //    var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(url);
    //    filePath += "/" + System.Convert.ToBase64String(plainTextBytes);
    //    string loadFilepath = filePath;
    //    bool web = false;
    //    WWW www;
    //    bool useCached = false;
    //    useCached = System.IO.File.Exists(filePath);
    //    if (useCached)
    //    {
    //        //check how old
    //        System.DateTime written = System.IO.File.GetLastWriteTimeUtc(filePath);
    //        System.DateTime now = System.DateTime.UtcNow;
    //        double totalHours = now.Subtract(written).TotalHours;
    //        if (totalHours > 300)
    //            useCached = false;
    //    }
    //    if (System.IO.File.Exists(filePath))
    //    {
    //        string pathforwww = "file:///" + loadFilepath;
    //        Debug.Log("TRYING FROM CACHE " + url + "  file " + pathforwww);
    //        www = new WWW(pathforwww);
    //    }
    //    else
    //    {
    //        web = true;
    //        www = new WWW(url);
    //    }
    //    IEnumeratorHelper.Instance.StartCoroutine(doLoad(www, filePath, web));
    //    return www;
    //}

    IEnumerator doLoad(WWW www, string filePath, bool web)
    {
        yield return www;

        if (www.error == null)
        {
            if (web)
            {
                //System.IO.Directory.GetFiles
                //Debug.Log("SAVING DOWNLOAD  " + www.url + " to " + filePath);
                // string fullPath = filePath;
                System.IO.File.WriteAllBytes(filePath, www.bytes);
                //Debug.Log("SAVING DONE  " + www.url + " to " + filePath);
                //Debug.Log("FILE ATTRIBUTES  " + File.GetAttributes(filePath));
                //if (File.Exists(fullPath))
                // {
                //    Debug.Log("File.Exists " + fullPath);
                // }
            }
            else
            {
                //Debug.Log("SUCCESS CACHE LOAD OF " + www.url);
            }
        }
        else
        {
            if (!web)
            {
                System.IO.File.Delete(filePath);
            }
            //Debug.Log("WWW ERROR " + www.error);
        }
    }
}
