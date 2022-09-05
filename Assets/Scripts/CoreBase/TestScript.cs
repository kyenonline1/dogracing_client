using UnityEngine;
using System.Collections;
using Interface;
using CoreBase.Controller;
using System.Collections.Generic;
using Utilities;
using System;
using System.Threading;
using System.Linq;

namespace CoreBase
{
    public class TestScript : MonoBehaviour
    {

        private void Awake()
        {
           // Thread thread = new Thread(testRandom);
        }

        private IEnumerator Start()
        {
            yield return null;
            Thread thread = new Thread(testRandom);
            thread.Start();
        }
        //protected override void InitGameScriptInAwake()
        //{
        //    base.InitGameScriptInAwake();

        //    //StartCoroutine(Test());
        //    //IEnumerator ienum = Test();
        //    //while (ienum.MoveNext())
        //    //    LogMng.Log("Test", "abc");
        //    //long money = 1092932096;
        //    //Dictionary<long, long> dictionary = ConvertToSmallerCoin(money);

        //    Thread thread = new Thread(() => testRandom()) { };
        //}

        //IEnumerator Test()
        //{
        //    int i = 0;
        //    while (i++ < 10)
        //    {
        //        yield return new WaitForSeconds(0.5f);
        //    }

        //    Dictionary<byte, object> abc = new Dictionary<byte, object>();
        //    abc.Add(1, "hiwhre3");


        //    IEnumerator ac = ((ExamleController) Controller).HandlePush("LGI", abc);
        //    while (ac.MoveNext())
        //        yield return ac.Current;
        //}

        //protected override IController CreateController()
        //{
        //    return new ExamleController(this);
        //}

        // Update is called once per frame
        void Update()
        {

        }


        int RandomInt(int min, int max)
        {
            if (min > max) return 0;
            List<int> arr = Enumerable.Range(min, max + 1 - min).ToList();
            arr = arr.OrderBy(n => Guid.NewGuid()).ToList();
            return arr.First();
        }


        private bool isExplode1(int circleNum)
        {
            int temp = circleNum;
            while (temp > 0)
            {
                int num = 10;
                if (temp < 10) num = temp;
                int ret = RandomInt(1, num);
                Debug.Log("isExplode1: " + ret + " -- circleNum: " +  circleNum);
                if (ret != 1) return false;
                temp = temp / 10;
            }
            return true;
        }

        private void testRandom()
        {
            int num = 500000;
            DateTime startTime = DateTime.Now;
            Debug.Log("------------------- START: ------------------ : " + startTime.ToString());
            //Console.WriteLine(startTime.ToString());
            int jcount = 0;
            Debug.Log(" 10_10_10_10_5");
            //Console.WriteLine("10_10_10_10_5");
            string str = string.Empty;
            for (var count = 1; count <= 100000000; count++)
            {
                bool isJack = isExplode1(num);
                //Debug.Log("Is Jack: " + isJack);
                if (isJack)
                {
                    jcount++;
                    str = str +  " -- " + string.Format("{0} . {1}", jcount, count);
                    //Console.WriteLine("{0} . {1}", jcount, count);
                }
            }
            Debug.Log("RESULT:  " + str);
            DateTime endTime = DateTime.Now;
            Debug.Log("------------------- END: ------------------ : " + endTime.ToString() + " --- time runing: " + (endTime - startTime));
            //Console.WriteLine(endTime.ToString());
            //Console.WriteLine(endTime - startTime);
            //Console.ReadLine();
        }


        [HandleUpdateEvent(EventType = HandlerType.EVN_UPDATEUI_HANDLER)]
        void TestUpdateView(params object[] parameters)
        {
            LogMng.Log("TestScript", "TestUpdateView");
        }



        private long[] arrMoney = new long[] { 100, 500, 1000, 5000, 10000, 50000, 100000, 500000, 1000000, 10000000, 50000000, 1000000000 };
        long CountChip = 0;
        public Dictionary<long, long> ConvertToSmallerCoin(long money)
        {
            Dictionary<long, long> result = new Dictionary<long, long>();
            //Dictionary<long, long> resultReturn = new Dictionary<long, long>();
            long temp = money;
            CountChip = 0;
            for (int i = arrMoney.Length - 1; i >= 0; i--)
            {
                if (temp / arrMoney[i] > 0)
                {
                    result.Add(arrMoney[i], temp / arrMoney[i]);
                    CountChip += temp / arrMoney[i];
                    Debug.Log(arrMoney[i] + " - " + temp / arrMoney[i]);
                    temp = money % arrMoney[i];
                }
            }
            //for (int i = 0; i < arrMoney.Length; i++)
            //{
            //    if (result.ContainsKey(arrMoney[i]))
            //    {
            //        resultReturn.Add(arrMoney[i], result[arrMoney[i]]);
            //    }
            //}
            return result;
        }
    }
}
