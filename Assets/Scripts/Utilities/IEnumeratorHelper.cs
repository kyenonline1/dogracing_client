using UnityEngine;

public class IEnumeratorHelper : MonoBehaviour
{
    public static IEnumeratorHelper Instance;

    void Awake()
    {
        Instance = this;
    }
}
