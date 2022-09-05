using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Languages
{
    public class LangLocalize : MonoBehaviour
    {

        public string KEY;

        private Text current_label;

        void Awake()
        {
            current_label = this.gameObject.GetComponent<Text>();
            Language.AddLableLanguage(this);
        }

        void Start()
        {
            SetText();
        }

        //Hàm gán lại text cho label
        public void SetText()
        {
            if (current_label == null)
            {
                Debug.LogError("Null " + this.gameObject.name);
            }
            else
                current_label.text = Language.GetKey(KEY);
        }
        //Lúc chuyển scene sẽ remove hết list
        void OnDestroy()
        {
            Language.RemoveLaleLanguage(this);
        }

    }
}
