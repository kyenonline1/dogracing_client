using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using AssetBundles;

public class ImageLoader
{
    public static IEnumerator HTTPLoadImage(Image image, string imageUrl)
    {
       // Debug.Log("HTTPLoadImage");
        //var text = Resources.Load<Texture2D>("DefaultAvatar/AvatarLoading");
        //image.sprite = ConvertSpireToTexture(text);
        image.gameObject.SetActive(true);
        image.SetAlpha(0, 0);
        //Debug.Log("HTTPLoadImage 111");
        if (!string.IsNullOrEmpty(imageUrl) && (imageUrl.Contains("http")  || imageUrl.Contains("facebook")))
        {
            var download = new WWWCacheOrDownload(imageUrl);
            while (!download.WWW.isDone)
                yield return download.WWW;
            if (image != null)
            {
                if (string.IsNullOrEmpty(download.WWW.error))
                {
                   // Debug.LogError("Load image error11111 " + download.WWW.error + ", image url: " + imageUrl);
                    image.sprite = ConvertSpireToTexture(download.WWW.texture);
                    image.SetAlpha(1, 0);
                    image.gameObject.SetActive(true);
                }
                else
                {
                    //Debug.LogError("Load image error " + download.WWW.error + ", image url: " + imageUrl);
                    var texture = Resources.Load<Texture2D>("DefaultAvatar/avatar01");
                    image.sprite = ConvertSpireToTexture(texture);
                    image.SetAlpha(1, 0);
                    image.gameObject.SetActive(true);
                }
            }
        }
        else
        {
            //Debug.Log("Load image error2222 " + ", image url: " + imageUrl);
            int avtId = -1;
            if (int.TryParse(imageUrl, out avtId))
            {
                string strId = (avtId < 10) ? string.Format("avatar0{0}", avtId) : string.Format("avatar{0}", avtId);
                //Debug.Log("Load image error 3333333333" + ", image url: " + strId);
                //LoadAssetBundle.LoadSpriteToAtlas(TagAssetBundle.Tag_UI.TAG_UI_COMMON, TagAssetBundle.AtlasName.LOBBY_AVATAR, strId, (sprite) => {
                //    if (image != null)
                //    {
                //        //Debug.Log("Load Avatar: " + (sprite == null) + " , image: " + (image == null));
                //        if (sprite) image.sprite = sprite;
                //        image.SetAlpha(1, 0);
                //        image.gameObject.SetActive(true);
                //    }
                //});
            }
            else
            {
                if (image == null) yield break;
                var texture = Resources.Load<Texture2D>("DefaultAvatar/avatar01");
                image.sprite = ConvertSpireToTexture(texture);
                image.SetAlpha(1, 0);
                image.gameObject.SetActive(true);
            }
        }
        yield return null;
    }
    private static Sprite ConvertSpireToTexture(Texture2D text)
    {
        return Sprite.Create(text, new Rect(0, 0, text.width, text.height), new Vector2(0.5f, 0.5f));
    }
}