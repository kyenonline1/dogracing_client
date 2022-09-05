using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{

    [SerializeField] private Image imgFillAmount;
    [SerializeField] private Transform tranRotate;


    long[] rewards;
    // Start is called before the first frame update
    void Start()
    {
        //List<long> lstValue = new List<long>();
        //for(int i = 0; i < 10; i++)
        //{
        //    var rw = 1000000 + (10 - i) * 1000000;
        //    lstValue.Add(rw);
        //}

        //for (int i = 0; i < 10; i++)
        //{
        //    var rw = 800000;
        //    lstValue.Add(rw);
        //}

        //for (int i = 0; i < 20; i++)
        //{
        //    var rw = 500000;
        //    lstValue.Add(rw);
        //}

        //for (int i = 0; i < 20; i++)
        //{
        //    var rw = 100000;
        //    lstValue.Add(rw);
        //}

        //rewards = lstValue.ToArray();


        //var groupReward = rewards.GroupBy(x => x).ToArray();
        ////Debug.Log("groupReward: " + groupReward.Length);

        //foreach(var vl in groupReward)
        //{
        //    //Debug.Log("Key: " + vl.Key );
        //    var values = vl.ToList();
        //    //Debug.Log("Count : " + values.Count);
        //    foreach (var p in values)
        //    {
        //        //Debug.Log("values: " + p);
        //    }
        //}

        //DateTime today = DateTime.Today;

        //Debug.Log("today: " + today.ToString());

        //var dates = Enumerable.Range(0, 60).Select(days => today.AddDays(days)).ToList();

        //foreach(var date in dates)
        //{
        //    Debug.Log("Date: " + date.ToString());
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (imgFillAmount)
        {
            float rate = imgFillAmount.fillAmount;
            float rotateZ = rate * 360f;
            tranRotate.eulerAngles = new Vector3(0, 0, rotateZ);
        }
    }

    private void ConvertRateToRotate()
    {
        //rate = 1 -> z = 0, rate = 0 -> z = -360 

    }
}
