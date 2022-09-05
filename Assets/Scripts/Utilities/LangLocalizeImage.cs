using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Languages
{
    public class LangLocalizeImage : MonoBehaviour
    {
        [SerializeField]
        private Sprite spr_vn;
        [SerializeField]
        private Sprite spr_en;

        private Image current_image;

        void Awake()
        {
            current_image = this.gameObject.GetComponent<Image>();
            Language.AddImageLanguage(this);
        }

        void Start()
        {
            SetImage(Language.LANG);
        }

        //Hàm gán lại text cho label
        public void SetImage(Languages lang)
        {
            if (current_image == null)
            {
                Debug.LogError("Null " + this.gameObject.name);
            }
            else
            {
                switch (lang)
                {
                    case Languages.vn:
                        current_image.sprite = spr_vn;
                        break;
                    case Languages.en:
                        current_image.sprite = spr_en;
                        break;
                    default:
                        current_image.sprite = spr_en;
                        break;
                }
                current_image.SetNativeSize();
            }
        }

        //Lúc chuyển scene sẽ remove hết list
        void OnDestroy()
        {
            Language.RemoveImageLanguage(this);
        }
    }
}
