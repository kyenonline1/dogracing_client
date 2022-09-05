using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace View.Home.Top
{
    public class ItemCateTypeView : MonoBehaviour
    {
        [SerializeField] Text txtCateName;

        public void SetData(string catename)
        {
            if (txtCateName) txtCateName.text = catename;
        }
    }
}