using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Utilites
{
    public class TextNumberMoney : MonoBehaviour
    {
        public Sprite[] sprPlusNumbers;
        public Sprite[] sprSubNumbers;
        public Sprite sprChamPlus,
            sprChamSub,
            sprPlus,
            sprSub;

        public Image[] imgChams;
        public Image[] imgNumbers;


        private void Start()
        {
            //long money = 3960;
            //ShowNumber(money);
        }

        public void ShowNumber(long money)
        {
            bool isPlus = money >= 0;
            if (!isPlus) money = money * -1;
            string number = money.ToString();// MoneyHelper.FormatNumberAbsolute(money);
            //Debug.Log("ShowNumber: " + money);
            if (string.IsNullOrEmpty(number)) return;
            int length = number.Length;
            //int countcham = length / 3;
            //int countMove = (length - 1) * 27;
            //int spaceCham = countcham * 12;
            //int posX = -360;
            //if (countMove >= 315) posX = 0;
            //else posX = -360 + countMove + spaceCham;
            ////Debug.LogFormat("posX: {0} , countMove: {1} , spaceCham: {2}", posX, countMove, spaceCham);
            //transform.DOLocalMoveX(posX, 0.05f);
            for (int i = 0; i < imgNumbers.Length; i++)
            {
                imgNumbers[i].gameObject.SetActive(false);
            }
            for (int i = 0; i < imgChams.Length; i++)
            {
                imgChams[i].gameObject.SetActive(false);
                imgChams[i].sprite = isPlus ? sprChamPlus : sprChamSub;
                imgChams[i].SetNativeSize();
            }
            imgNumbers[length].gameObject.SetActive(true);
            imgNumbers[length].sprite = isPlus ? sprPlus : sprSub;
            imgNumbers[length].SetNativeSize();
            imgChams[0].gameObject.SetActive(length > 3);
            imgChams[1].gameObject.SetActive(length > 6);
            imgChams[2].gameObject.SetActive(length > 9);
            number = number.Replace(",", ".");
            for(int i = 0; i < length; i++)
            {
                //Debug.LogFormat("Ký tự thứ {0} - là {1} ", i, number[i]);
                imgNumbers[length - 1 - i].sprite = sprPlusNumbers[0];
                imgNumbers[length - 1 - i].SetNativeSize();
                StartCoroutine(IEShowTextNumber(imgNumbers[length - 1 - i], int.Parse(number[i].ToString()), isPlus ? sprPlusNumbers : sprSubNumbers));
            }
        }
       

        private IEnumerator IEShowTextNumber(Image imgNumber, int value, Sprite[] sprNumbers)
        {
           // Debug.Log("IEShowTextNumber: " + imgNumber.name + " - value: " + value);
            imgNumber.gameObject.SetActive(true);
            for (int i = 0; i <= value; i++)
            {
                imgNumber.sprite = sprNumbers[i];
                imgNumber.SetNativeSize();
                yield return new WaitForSeconds(0.1f);
            }
            yield return null;
        }
    }
}
