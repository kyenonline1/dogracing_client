// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 4.0.30319.1
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------
using System;
using System.Xml;
using UnityEngine;
using System.Collections.Generic;

namespace Languages
{

    public enum Languages
    {
        vn,
        en
    }

    public class Language
    {
        private static XmlDocument xmlDocument;
        private static Dictionary<string, string> dict;

        public static Languages LANG = Languages.vn;

        private static List<LangLocalize> ListLableLanguage = new List<LangLocalize>();

        private static List<LangLocalizeImage> ListImageLanguage = new List<LangLocalizeImage>();

        //hàm load file languages
        public static void LoadLanguageFile(Languages languageType)
        {
            string txt = "Languages/" + languageType.ToString();
            //Debug.Log("Load Language File: " + txt + " - " + (Resources.Load("DefaultAvatar/avatar01") == null));
            TextAsset textAsset = (TextAsset)Resources.Load(txt, typeof(TextAsset));
            //Debug.Log("Check File 111: " + (textAsset == null));
            dict = ReadXML(textAsset.text);
        }

        #region API

        public static Dictionary<string, string> ReadXML(string xml)
        {
            xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xml);
            var result = new Dictionary<string, string>();
            if (xmlDocument.ChildNodes.Count > 0)
            {
                var list = xmlDocument.ChildNodes.Item(1);
                if (list.ChildNodes.Count > 0)
                {
                    for (int i = 0; i < list.ChildNodes.Count; i++)
                    {
                        var t = list.ChildNodes.Item(i);
                        if (t.Name == "item")
                        {
                            string key = t.Attributes["key"].Value;
                            string val = t.InnerText;
                            if (val.EndsWith("\n"))
                                val = val.Substring(0, val.Length - 2);
                            if (!result.ContainsKey(key))
                                result.Add(key, val);
                            else
                                result[key] = val;
                        }
                    }
                }
            }

            return result;
        }

        //Hàm được gọi lúc chuyển đổi ngôn ngữ
        public static void ChangeLanguage(Languages lang)
        {
            LoadLanguageFile(lang);
            for (int i = 0; i < ListLableLanguage.Count; i++)
            {
                ListLableLanguage[i].SetText();
            }
            for (int i = 0; i < ListImageLanguage.Count; i++)
            {
                ListImageLanguage[i].SetImage(lang);
            }
        }

        public static string GetKey(string key)
        {
            if (dict != null && dict.ContainsKey(key))
            {
                return dict[key].ToString();
            }
            else
                return key;
        }

        //Sau khi gán text thì add vào list để lúc chuyển scene remove hết đi
        public static void AddLableLanguage(LangLocalize lang)
        {
            if (ListLableLanguage.Contains(lang))
                return;
            ListLableLanguage.Add(lang);
        }


        public static void RemoveLaleLanguage(LangLocalize lang)
        {
            if (!ListLableLanguage.Contains(lang))
                return;
            ListLableLanguage.Remove(lang);
        }


        //Sau khi gán text thì add vào list để lúc chuyển scene remove hết đi
        public static void AddImageLanguage(LangLocalizeImage lang)
        {
            if (ListImageLanguage.Contains(lang))
                return;
            ListImageLanguage.Add(lang);
        }


        public static void RemoveImageLanguage(LangLocalizeImage lang)
        {
            if (!ListImageLanguage.Contains(lang))
                return;
            ListImageLanguage.Remove(lang);
        }

        public static Dictionary<string, string> GetLanguageDictionary()
        {
            return dict;
        }

        #endregion
    }
}