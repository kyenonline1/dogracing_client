using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameProtocol.Protocol;
using System.Threading;
using System.Reflection;
using System;
using System.IO;
using ExitGames.Client.Photon;
using Client.Photon;
using Utilities;
using Network;
using AppConfig;
using Listener;
using GameProtocol.Base;

namespace Game.Developer.Johnroid.Model
{
    public class GamePlayback
    {
        public enum Status
        {
            Error = -1, Finish
        }
        private const string TAG = "GamePlayback";

        public delegate void HandlePush(string coderun, Dictionary<byte, object> data);
		public delegate void PlaybackStatus(Status status);

        //public event HandleResponse SBI2_HandleResponse, SBI5_HandleResponse;
        public event HandlePush PushHandler;
		public event PlaybackStatus OnPlaybackStatus;
        //private string id_match = string.Empty;
        //private string  url_playback = string.Empty;
        private bool running = false, pause = false;
        
        private List<Dictionary<byte, object>> GameEvents = new List<Dictionary<byte, object>>();
        private List<int> TimeWait = new List<int>();

        private List<Dictionary<byte, object>> GameEventsReplay = new List<Dictionary<byte, object>>();
        private List<int> TimeWaitReplay = new List<int>();

        public GamePlayback(string match_id, string url_playback, string username)
        {
            //DataListener listener = new DataListener(SBI20_HandleResponse);
            //SBI20_Request request = new SBI20_Request()
            //{
            //    MatchId = match_id,
            //    Username = username,
            //    SenderId = listener.GetHashCode()
            //};

            //NetworkConnector.SendOperation(request, listener;)
            //this.id_match = match_id;
            //this.url_playback = url_playback;
        }

        private void SBI20_HandleResponse(string coderun, Dictionary<byte, object> data)
        {
            //SBI20_Response response = new SBI20_Response(data);
            //if (response.ErrorCode != (short)ReturnCode.OK)
            //{
            //    return;
            //}

            //url_playback = response.Url;

            LoadAndPlayMatch();
        }

        public void LoadAndPlayMatch()
        {
            LogMng.Log(TAG, "Read match data...");

            //MonoBehaviourHelper.Instance.ExecuteIEnumerator(ReadData("http://203.162.234.90/data.json"));
            //MonoBehaviourHelper.Instance.ExecuteIEnumerator(ReadData("http://dl.icasino88.net:6100/image/poker_fake.json"));
            //MonoBehaviourHelper.Instance.ExecuteIEnumerator(ReadData("http://203.162.234.90/chiaki_new_15_50_0b6efb37bf6f45596cb4f80705b52c93.json"));
            //MonoBehaviourHelper.Instance.ExecuteIEnumerator(ReadData(url_playback));


            //TextAsset data = Resources.Load("poker_fake") as TextAsset;
            //try
            //{
            //    FileStream file = File.Open(Application.persistentDataPath + "/" + "poker_fake.json", FileMode.Open);
            //    BinaryReader reader = new BinaryReader(file);
            //    int count = 0, length = (int)file.Length;
            //    byte[] buffer = new byte[length];
            //    while (count < length)
            //    {
            //        int read = reader.Read(buffer, count, length - count);
            //        count += read;
            //    }

            //    file.Close();

            //    HandleMatchData(buffer);
            //}
            //catch (Exception e)
            //{
            //    UnityEngine.Debug.LogException(e);
            //}

            
        }

        private IEnumerator ReadData(string link)
        {
            //Debug.LogError("LINK :  " + link);
            WWW www = new WWW(link);
            yield return www;
           // Debug.LogError("WWW:  " + www.error);
            if (www.error != null)
            {
                OnPlaybackStatus(Status.Error);
                yield break;
            }
            byte[] data = www.bytes;
            HandleMatchData(data);
        }

        public void HandleMatchData(byte[] data_as_byte)
        {
            using (MemoryStream stream = new MemoryStream(data_as_byte))
            {
                using (BinaryReader reader = new BinaryReader(stream))
                {
                    try
                    {
                        while (reader.PeekChar() != -1)
                        {
                            int len = reader.ReadInt32();
                            byte flag = reader.ReadByte();
                            int time = reader.ReadInt32();

                            Dictionary<byte, object> data = null;

                            byte[] opbytes = reader.ReadBytes(len);

                            LogMng.Log(TAG, "len: " + len + ", flag: " + flag + ", time: " + time);

                            if (flag == Packet.FLAG_ACTIVE)
                            {
                                data = FakeOperations.DeserializeToResponse(opbytes).Parameters;
                                LogMng.Log(TAG, "Response struct: " + Packet.DictToString2(data));

                            }
                            else if (flag == Packet.FLAG_PASSIVE)
                            {
                                data = FakeOperations.DeserializeToEventData(opbytes).Parameters;
                                LogMng.Log(TAG, "Push struct: " + Packet.DictToString2(data));
                            }
                            else
                                continue;

                            data[(byte)ParameterCode.Flag] = flag;

                            GameEvents.Add(data);
                            TimeWait.Add(time);

                            GameEventsReplay.Add(data);
                            TimeWaitReplay.Add(time);
                        }
                    }
                    catch (Exception e)
                    {
                        OnPlaybackStatus(Status.Error);
                        Debug.LogException(e);
                        return;
                    }
                }
            }

            LogMng.Log(TAG, "Parser done, start playback: event count = " + GameEvents.Count);
			StartPlayBack();
        }

        public void StartPlayBack()
        {
            running = true;
            new Thread(StartPlayBackImpl).Start();
        }

        public void PausePlayBack()
        {
            pause = true;
        }

        public void ResumePlayBack()
        {
            pause = false;
        }

        public void StopPlayBack()
        {
            running = false;
            //events.Clear();
            //lock (events)
            //{
            //    Monitor.Pulse(events);
            //}

            GameEvents.Clear();
            lock (GameEvents)
            {
                Monitor.Pulse(GameEvents);
            }
        }

        public void Replay()
        {
           
            GameEvents.AddRange(GameEventsReplay);
            TimeWait.AddRange(TimeWaitReplay);

            StartPlayBack();
        }

        public void StartPlayBackImpl()
        {
            while (running && GameEvents.Count > 0)
            {
                if (!pause)
                {
                    Dictionary<byte, object> mesage = GameEvents[0];
                    GameEvents.Remove(mesage);
                    int timedelay = TimeWait[0];
                    TimeWait.RemoveAt(0);
                    ProcessMesssage(mesage);

                    lock (GameEvents)
                    {
                        Monitor.Wait(GameEvents, timedelay);
                    }
                }
                else
                {
                    lock (GameEvents)
                    {
                        Monitor.Wait(GameEvents, 1000);
                    }
                }
            }

			OnPlaybackStatus (Status.Finish);
        }

        private void ProcessMesssage(Dictionary<byte, object> msgbase)
        {
            string coderun = (string)msgbase[(byte)ParameterCode.CodeRun];
            if ((byte)msgbase[(byte)ParameterCode.Flag] == Packet.FLAG_ACTIVE)
            {
                LogMng.Log(TAG, "Process active packet");
                //get listener

                //if (coderun.StartsWith(GameConfig.RequestCode.SBI))
                //{
                //    if (coderun.EndsWith("2"))
                //        SBI2_HandleResponse(coderun, msgbase);
                //    else if (coderun.EndsWith("5"))
                //        SBI5_HandleResponse(coderun, msgbase);
                //    return;
                //}
            }


            if ((byte)msgbase[(byte)ParameterCode.Flag] == Packet.FLAG_PASSIVE)
            {
                PushHandler(coderun, msgbase);
            }
        }
    }
}