using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;

[CustomEditor(typeof(GrayScale))]
public class GrayScaleEditor : Editor
{
    private bool isGrayScale;
    public override void OnInspectorGUI()
    {
        var myTarget = (GrayScale)target;

        isGrayScale = EditorGUILayout.Toggle("Is GrayScale: ", isGrayScale);
        if (myTarget.SetGrayScale(isGrayScale))
        {
            EditorUtility.SetDirty(target);
        }
    }
}
#endif

public class GrayScale : MonoBehaviour
{
    public bool IsGrayScale;

    private Material grayScaleMaterial;
    private Image image;

    public void EnableGrayScale(bool isGrayScale)
    {
        SetGrayScale(isGrayScale);
    }

    public bool SetGrayScale(bool isGrayScale)
    {
        if (IsGrayScale != isGrayScale)
        {
            IsGrayScale = isGrayScale;

            if (image == null) image = GetComponent<Image>();
            if (image != null)
            {
                if (!isGrayScale) image.material = null;
                else
                {
                    if (grayScaleMaterial == null)
                        grayScaleMaterial = Resources.Load<Material>("Shaders/GrayScale/Grayscale2");
                    image.material = grayScaleMaterial;
                }
            }
            return true;
        }
        else return false;
    }
}
