using System;

namespace utils
{
	public class UserInfo{
		public enum AccountType
		{
			GUEST = 0, 
			LOGIN_USER,
		}
		public enum UserType
		{
			NORMAL = 0, 
			AGENCY_LEVEL_1 =1,
			AGENCY_LEVEL_2 =2, 
			SERVER = 3 //Sender = Server
		}

		public static string UserName;
		public static string UserFullName;
		public static string UserId;
		public static long Balance;
		public static AccountType Type;
		public static string AvatarUrl;
        public static int CurrenExp;
        public static int MaxExp;
        public static int Level;
        public static int NumMailUnread;
		public static int Vip;
		public static string VipNameShow;
		public static long VipPoint;
		public static UserType userType;
    }


	//{"accessToken":"636199885139070000.c16e11c5c28d4be10842ec3023adc51f","accountType":0,"expireDate":"1515902553","refreshToken":"","userId":"1501609933","userName":"payment.test01"}
}