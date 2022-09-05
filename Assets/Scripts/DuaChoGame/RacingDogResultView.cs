using AppConfig;
using GameProtocol.DOG;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using View.GamePlay.DuaCho;

public class RacingDogResultView : MonoBehaviour {
    
    //[SerializeField]
    //private Transform[] DogsResult;
    [SerializeField]
    private Transform[] DogTop;
    

    //private Vector2[] vector2s;
    private Vector2[] vector2sTop;

    private void Awake()
    {
        //vector2s = new Vector2[]
        //{
        //    new Vector2(-320, 45),
        //    new Vector2(-320, -45),
        //    new Vector2(-320, -125),
        //    new Vector2(-320, -210),
        //    new Vector2(-320, -295),
        //    new Vector2(-320, -380),
        //};
        vector2sTop = new Vector2[]
        {
           new Vector2(0, -65),
            new Vector2(410, -145),
            new Vector2(-460, -205),
        };
    }

    private void OnEnable()
    {
        RacingDogView.Instance.Index = 0;
    }

    public void ShowResult(DogRacing[] dogRacing)
    {
        try
        {
            int length = dogRacing.Length;
            //Debug.Log(" SHOW RESULT -----------: " + length + " --: " + dogRacing[0].Segments.Length);
            if (length == 0) return;
            if (dogRacing[0].Segments.Length == 0)
            {
                for (int i = 0; i < length; i++)
                {
                    byte id = dogRacing[i].DogId;
                    //DogsResult[id].localPosition = vector2s[i];
                    //Debug.Log("Result: " + id  + " , " + dogRacing[i].Order);
                    if (i < 3)
                    {
                        DogTop[id - 1].gameObject.SetActive(true);
                        DogTop[id - 1].localPosition = vector2sTop[i];
                    }
                    else
                    {
                        DogTop[id - 1].gameObject.SetActive(false);
                    }
                }
            }
            else
            {
                var dogs = dogRacing.OrderBy(dog => dog.Segments[4].Position);
                for (int i = 0; i < length; i++)
                {
                    byte id = dogs.ElementAt<DogRacing>(i).DogId;
                    //DogsResult[id].localPosition = vector2s[5 - i];
                    if (i > 2)
                    {
                        DogTop[id - 1].gameObject.SetActive(true);
                        DogTop[id - 1].localPosition = vector2sTop[5 - i];
                    }
                    else
                    {
                        DogTop[id - 1].gameObject.SetActive(false);
                    }
                }
            }
            //tranFireWork.gameObject.SetActive(true);
            //tranFireWork.Play("AnimFireWork", 0, 0);
            //ClientConfig.Sound.PlaySound(ClientConfig.Sound.SoundId.DuaCho_background_poper_party);
        }
        catch(Exception e)
        {
            Debug.LogError("ShowResult Exception: " + e.StackTrace);
        }
    }

}
