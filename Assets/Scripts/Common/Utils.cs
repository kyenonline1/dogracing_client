using DG.Tweening;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public static class Utils  {

    public static void Disable(this UIMyButton btn)
    {
        btn.interactable = false;
        for (int i = 0; i < btn.transform.childCount; i++)
        {
            var img = btn.transform.GetChild(i).GetComponent<Image>();
            var txt = btn.transform.GetChild(i).GetComponent<Text>();
            if (img != null) img.SetAlpha(.4f);
            if (img != null) img.raycastTarget = false;
            if (txt != null) txt.SetAlpha(.4f);
        }
        
    }

    public static void Enable(this UIMyButton btn)
    {
        btn.interactable = true;
        for (int i = 0; i < btn.transform.childCount; i++)
        {
            var img = btn.transform.GetChild(i).GetComponent<Image>();
            var txt = btn.transform.GetChild(i).GetComponent<Text>();
            if (img != null) img.SetAlpha(1f);
            if (img != null) img.raycastTarget = true;
            if (txt != null) txt.SetAlpha(1f);
        }
    }

    public static void Enable(this UIMyButton btn, bool isEnable)
    {
        if (isEnable) btn.Enable();
        else btn.Disable();
    }


    public static void DisableWithSprite(this UIMyButton btn)
    {
        btn.interactable = false;
        btn.image.SetAlpha(.4f);
        for (int i = 0; i < btn.transform.childCount; i++)
        {
            var img = btn.transform.GetChild(i).GetComponent<Image>();
            var txt = btn.transform.GetChild(i).GetComponent<Text>();
            if (img != null) img.SetAlpha(.4f);
            if (txt != null) txt.SetAlpha(.4f);
        }
    }

    public static void EnableWithSprite(this UIMyButton btn)
    {
        btn.interactable = true;
        btn.image.SetAlpha(1);
        for (int i = 0; i < btn.transform.childCount; i++)
        {
            var img = btn.transform.GetChild(i).GetComponent<Image>();
            var txt = btn.transform.GetChild(i).GetComponent<Text>();
            if (img != null) img.SetAlpha(1f);
            if (txt != null) txt.SetAlpha(1f);
        }
    }

    public static void EnableWithSprite(this UIMyButton btn, bool isEnable)
    {
        if (isEnable) btn.EnableWithSprite();
        else btn.DisableWithSprite();
    }

    public static void SetAlpha(this Image img, float alpha, float duration = 0f)
    {
        //DOTween.ToAlpha(() => img.color, a => img.color = a, alpha, duration).SetAutoKill(true);
        img.color = new Color(img.color.r, img.color.g, img.color.b, alpha);
    }

    public static void SetAlpha(this Text txt, float alpha, float duration = 0f)
    {
        //DOTween.ToAlpha(() => txt.color, a => txt.color = a, alpha, duration).SetAutoKill(true);
        txt.color = new Color(txt.color.r, txt.color.g, txt.color.b, alpha);
    }

    public static string NumberGroup(long strNumber)
    {
        return (strNumber <= 0) ? "0" : string.Format("{0:##,##}", strNumber).Replace(",", ".");
    }

    public static IEnumerator FadeOut(this Image image, float interval, float to = 0f)
    {
        image.SetAlpha(1f);
        yield return image.DOFade(to, interval).WaitForCompletion();
    }

    public static IEnumerator FadeOut(this Text text, float interval, float to = 0f)
    {
        text.SetAlpha(1f);
        yield return text.DOFade(to, interval).WaitForCompletion();
    }

    public static IEnumerator FadeIn(this Image image, float interval, float to = 1f)
    {
        image.SetAlpha(0f);
        yield return image.DOFade(to, interval).WaitForCompletion();
    }

    public static IEnumerator FadeSound(this AudioSource audioSource, float dur, float to)
    {
        yield return audioSource.DOFade(to, dur).WaitForCompletion();
    }

    public static IEnumerator FadeIn(this Text txt, float interval, float to = 1f)
    {
        txt.SetAlpha(0f);
        yield return txt.DOFade(to, interval).WaitForCompletion();
    }

    public static IEnumerator MoveNHide(this Transform rect, Vector3 target, float duration)
    {
        var pos = rect.localPosition;
        yield return rect.DOLocalMove(target, duration).WaitForCompletion();
        rect.localPosition = pos;
        rect.gameObject.SetActive(false);
    }

    public static IEnumerator DelayAction(UnityAction action, float timer)
    {
        yield return new WaitForSeconds(timer);
        if (action != null) action();
    }


    public static string ConvertToUnSign3(string s)
    {
        Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
        string temp = s.Normalize(NormalizationForm.FormD);
        return regex.Replace(temp, string.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
    }

    public static string GetCardRankName(int rank)
    {
        return "";
        //switch ((RankName)rank)
        //{
        //    case RankName.HighCard:
        //        return  Languages.Language.GetKey("POKER_HANDCARD_HIGHCARD");

        //    case RankName.OnePair:
        //        return Languages.Language.GetKey("POKER_HANDCARD_PAIR");

        //    case RankName.TwoPair:
        //        return Languages.Language.GetKey("POKER_HANDCARD_TWOPAIR");

        //    case RankName.ThreeOfAKind:
        //        return Languages.Language.GetKey("POKER_HANDCARD_THREEOFKIND");
        //    case RankName.Straight:
        //        return Languages.Language.GetKey("POKER_HANDCARD_STRAID");
        //    case RankName.Flush:
        //        return Languages.Language.GetKey("POKER_HANDCARD_FLUSH");
        //    case RankName.FullHouse:
        //        return Languages.Language.GetKey("POKER_HANDCARD_FULLHOUSE");
        //    case RankName.FourOfAKind:
        //        return Languages.Language.GetKey("POKER_HANDCARD_FOUROFAKIND");
        //    case RankName.StraightFlush:
        //        return Languages.Language.GetKey("POKER_HANDCARD_STRAIDFLUSH");
        //    case RankName.RoyalFlush:
        //        return Languages.Language.GetKey("POKER_HANDCARD_ROYERFLUSH");
        //    default:
        //        return "";
        //}

    }
}
