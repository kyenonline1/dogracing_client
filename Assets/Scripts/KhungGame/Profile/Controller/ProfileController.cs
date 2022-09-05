using AppConfig;
using Base.Utils;
using CoreBase.Controller;
using GameProtocol.ACC;
using GameProtocol.OTP;
using GameProtocol.TOP;
using Interface;
using Listener;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilitis.Command;

namespace Controller.Home.Profile
{
    public class ProfileController : UIController
    {

        public override void StartController()
        {
            base.StartController();
        }

        public override void StopController()
        {
            base.StopController();
        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_VIEW_HANDLER)]
        private void RequestACC0GetInfo(object[] param)
        {
            DataListener dataListener = new DataListener(HandlerResponseACC0GetInfo);
            ACC0_Request request = new ACC0_Request()
            {
                UserId = (long)param[0],

            };
            Network.Network.SendOperation(request, dataListener);
        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_VIEW_HANDLER)]
        private void ChangeAvatar(object[] param)
        {
            int index = (int)param[0];
            if (index < 0 && index > AvatarConfig.Length) return;
            //Debug.Log("AvatarConfig[index]: " + AvatarConfig[index]);
            avatar = index.ToString();// AvatarConfig[index].ToString();
            ACC1_Request request = new ACC1_Request()
            {
                Avatar = avatar
            };
            DataListener dataListener = new DataListener(HandlerResponseACC1ChangeInfo);
            Network.Network.SendOperation(request, dataListener);
        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_VIEW_HANDLER)]
        private void UpdateNickname(object[] param)
        {
            nickname = (string)param[0];
            ACC1_Request request = new ACC1_Request()
            {
                Nickname = nickname
            };
            DataListener dataListener = new DataListener(HandlerResponseACC1ChangeInfo);
            Network.Network.SendOperation(request, dataListener);
        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_VIEW_HANDLER)]
        private void SetGoldToSafe(object[] data)
        {
            ACC3_Request request = new ACC3_Request();
            request.Chip = (long)data[0];
            DataListener dataListener = new DataListener(HandlerResponseACC3SendGoldToSafe);
            Network.Network.SendOperation(request, dataListener);
        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_VIEW_HANDLER)]
        private void GetGoldToSafe(object[] data)
        {
            ACC4_Request request = new ACC4_Request();
            request.Gold = (long)data[0];
            request.OTP = (int)data[1];
            DataListener dataListener = new DataListener(HandlerResponseACC1ChangeInfo);
            Network.Network.SendOperation(request, dataListener);
        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_VIEW_HANDLER)]
        private void ChangeInfoClicked(object[] param)
        {
            nickname = (string)param[0];
            email = (string)param[1];
            password = (string)param[2];
            ACC1_Request request = new ACC1_Request()
            {
                Nickname = (string)param[0],
                Email = (string)param[1],
                Password = (string)param[2],
            };
            DataListener dataListener = new DataListener(HandlerResponseACC1ChangeInfo);
            Network.Network.SendOperation(request, dataListener);
        }

        [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_VIEW_HANDLER)]
        private void RequestOTP(object[] param)
        {
            OTP0_Request request = new OTP0_Request()
            {
                NumberPhone = (string)param[0],
            };
            DataListener dataListener = new DataListener(HandlerResponseGetOTP);
            Network.Network.SendOperation(request, dataListener);
        }

         [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_VIEW_HANDLER)]
        private void UpdateNumberPhone(object[] param)
        {
            numberphone = (string)param[0];
            ACC1_Request request = new ACC1_Request()
            {
                NumberPhone = numberphone,
                //OTP = (int)param[1]
            };

            DataListener dataListener = new DataListener(HandlerResponseACC1ChangeInfo);
            Network.Network.SendOperation(request, dataListener);
        }
        
         [HandleUIEvent(EventType = CoreBase.HandlerType.EVN_VIEW_HANDLER)]
        private void RequestChangePassword(object[] param)
        {
            password = (string)param[0];
            ACC1_Request request = new ACC1_Request()
            {
                Password = password,
                //OTP = (int)param[1]
            };
            DataListener dataListener = new DataListener(HandlerResponseACC1ChangeInfo);
            Network.Network.SendOperation(request, dataListener);
        }


        private int[] AvatarConfig;
        private IEnumerator HandlerResponseACC0GetInfo(string coderun, Dictionary<byte, object> data)
        {
            
            ACC0_Response response = new ACC0_Response(data);
            if (response.ErrorCode != 0)
            {
                View.OnUpdateView("ShowError", response.ErrorMsg);
                yield break;
            }
            //Debug.Log("ID : " + response.UserId);
            //if (response.UserId == ClientConfig.UserInfo.ID)
            {
                ClientConfig.UserInfo.GOLD = response.Gold;
                ClientConfig.UserInfo.GOLD_SAFE = response.GoldSafe;
                ClientConfig.UserInfo.SILVER = response.Silver;
                ClientConfig.UserInfo.PHONE = response.PhoneNumber;
                ClientConfig.UserInfo.EMAIL = response.Email;
                //Debug.Log("PHONE: " + (response.AvatarConfigs == null));
                EventManager.Instance.RaiseEventInTopic(EventManager.CHANGE_BALANCE);
                AvatarConfig = response.AvatarConfigs;
                //View.OnUpdateView("UpdateAvatarDefault", new object[] { AvatarConfig });
            }
            View.OnUpdateView("UpdateDataProfile", response.Email, response.CurrentVip, response.MaxVip, response.Exp);
            yield return null;
        }

        string avatar = string.Empty;
        string nickname = string.Empty;
        string numberphone = string.Empty;
        string password = string.Empty;
        string email = string.Empty;

        public ProfileController(IView view) : base(view)
        {
        }

        private IEnumerator HandlerResponseACC1ChangeInfo(string coderun, Dictionary<byte, object> data)
        {
            ACC1_Response response = new ACC1_Response(data);
            //Debug.Log("UPDATE IN FO ACC1 HandlerResponseACC1ChangeInfo: " + response.ErrorCode);
            if (!string.IsNullOrEmpty(response.ErrorMsg)) View.OnUpdateView("ShowError", response.ErrorMsg);
            if (response.ErrorCode != 0)
            {

                if (string.IsNullOrEmpty(ClientConfig.UserInfo.NICKNAME)) View.OnUpdateView("ShowPopupUpdateNickName");
                yield break;
            }
            View.OnUpdateView("CloseLoading");
            if (!string.IsNullOrEmpty(avatar))
            {
                ClientConfig.UserInfo.AVATAR = avatar;
                EventManager.Instance.RaiseEventInTopic(EventManager.CHANGE_AVATAR_TOPIC);
            }
           // else
            if (!string.IsNullOrEmpty(nickname))
            {
                ClientConfig.UserInfo.NICKNAME = nickname;
                EventManager.Instance.RaiseEventInTopic(EventManager.CHANGE_NICKNAME_TOPIC);
            }
            //else
            if (!string.IsNullOrEmpty(numberphone))
            {
                ClientConfig.UserInfo.PHONE = numberphone;
                EventManager.Instance.RaiseEventInTopic(EventManager.CHANGE_NUMBERPHONE_TOPIC);
            }
            //else
            if (!string.IsNullOrEmpty(password))
            {
                ClientConfig.UserInfo.PASSWORD = password;
                password = string.Empty;
            }
            if (!string.IsNullOrEmpty(email))
            {
                ClientConfig.UserInfo.EMAIL = email;
                email = string.Empty;
            }
            View.OnUpdateView("UpdateDataProfile", ClientConfig.UserInfo.EMAIL);
            yield return null;
        }

        private IEnumerator HandlerResponseACC3SendGoldToSafe(string coderun, Dictionary<byte, object> data)
        {
            ACC3_Response response = new ACC3_Response(data);
            View.OnUpdateView("ShowError", response.ErrorMsg);
            if (response.ErrorCode != 0)
            {
                yield break;
            }
            ClientConfig.UserInfo.GOLD = response.GoldChip;
            ClientConfig.UserInfo.GOLD_SAFE = response.GoldSafe;
            EventManager.Instance.RaiseEventInTopic(EventManager.CHANGE_BALANCE);
            View.OnUpdateView("UpdateGoldAndSafe");
            yield return null;
        }


        private IEnumerator HandlerResponseACC4GetGoldToSafe(string coderun, Dictionary<byte, object> data)
        {
            ACC4_Response response = new ACC4_Response(data);
            View.OnUpdateView("ShowError", response.ErrorMsg);
            if (response.ErrorCode != 0)
            {
                yield break;
            }
            ClientConfig.UserInfo.GOLD = response.GoldChip;
            ClientConfig.UserInfo.GOLD_SAFE = response.GoldSafe;
            EventManager.Instance.RaiseEventInTopic(EventManager.CHANGE_BALANCE);
            View.OnUpdateView("UpdateGoldAndSafe");
            yield return null;
        }


        private IEnumerator HandlerResponseGetOTP(string coderun, Dictionary<byte, object> data)
        {
            OTP0_Response response = new OTP0_Response(data);
            View.OnUpdateView("ShowError", response.ErrorMsg);
            if (response.ErrorCode != 0)
            {
                yield break;
            }

            ClientConfig.UserInfo.GOLD = response.CurGold;
            ClientConfig.UserInfo.GOLD_SAFE = response.CurGoldSafe;
            EventManager.Instance.RaiseEventInTopic(EventManager.CHANGE_BALANCE);
            View.OnUpdateView("CloseLoading");
        }
        
    }
}
