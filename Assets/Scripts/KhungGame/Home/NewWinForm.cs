using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class NewWinForm : MonoBehaviour
{
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL
    public List<InputField> lstInput;
    public UnityEvent onKeyDownEnter;
    //public Text txtTab;
    //public Text txtEnter;

    public ConditionCallback CheckingValidator;
    //private int tab = 0;
    //private int enter = 0;
    private bool tabAvailable = true;
    //private bool pasteAvailable = true;
    private bool enterAvailable = true;

    private void OnEnable()
    {
        //tab = 0;
        //enter = 0;
        tabAvailable = true;
        enterAvailable = true;
    }

    void OnGUI()
    {
        //if (pasteAvailable)
        //{
        //    if ((Input.GetKeyDown(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.V)) || (Input.GetKeyDown(KeyCode.RightControl) && Input.GetKeyDown(KeyCode.V)))
        //    {
        //        for (int i = 0; i < lstInput.Count; i++)
        //        {
        //            if (lstInput[i].isFocused)
        //            {
        //                lstInput[index].text += ;
        //                return;
        //            }
        //        }
        //    }
        //} 

        if (tabAvailable && Input.GetKeyDown(KeyCode.Tab))
        {
            tabAvailable = false;
            //tab++;
            for (int i = 0; i < lstInput.Count; i++)
            {
                if (lstInput[i].isFocused)
                {
                    var index = (i + 1) % lstInput.Count;
                    lstInput[index].ActivateInputField();
                    return;
                }
            }
        }
        else if (Input.GetKeyUp(KeyCode.Tab))
        {
            tabAvailable = true;
        }

        else if (enterAvailable && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)))
        {
            enterAvailable = false;
            //enter++;
            var isFocus = false;
            for (int i = 0; i < lstInput.Count; i++)
            {
                if (lstInput[i].isFocused)
                {
                    isFocus = true;
                    break;
                }
            }
            if (isFocus && CheckingValidator.Invoke() && onKeyDownEnter != null)
                onKeyDownEnter.Invoke();
        }
        else if (Input.GetKeyUp(KeyCode.Return) || Input.GetKeyUp(KeyCode.KeypadEnter))
        {
            enterAvailable = true;
        }
        //txtTab.text = "Tab: " + tab;
        //txtEnter.text = "Enter: " + enter;
    }
#endif
}
