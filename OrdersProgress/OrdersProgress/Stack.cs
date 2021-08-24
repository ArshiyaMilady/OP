using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;


namespace OrdersProgress
{
    static class Stack
    {
        public static bool bx;
        public static int ix;
        public static long lx;
        public static string sx;

        // فعلا شناسه کاربر 1201 باشد
        public static long UserIndex = -1; // شناسه کاربری که با نرم افزار کار می کند
        public static string UserName = null;
        public static int UserLevel_Type = -1;   // سطح کاربری عادی  = تمام سطوح به غیر از ادمین و ادمین واقعی
        public static long UserLevel_Index = -1;
        public static long Company_Index = 1;

        // false : رزرو کالاها از انبار به صورت دستی
        // true : رزرو کالاها از انبار به صورت اتوماتیک
        public static bool bWarehouse_Booking_MaxHours = false;


        //public static string UserName = "admin";
        // سطح دسترسی کاربری که با نرم افزار کار می کند 
        //public static int UserLevel = UserLevel_Supervisor1;

        // امکانات کاربر وارد شده را در خود نگه می دارد
        public static List<string> lstUser_ULF_UniquePhrase = new List<string>();

        //public static string OrderIndex;   // شناسه سفارشی که در حال مورد استفاده قرار گرفته است

        public static string Standard_Salt = "hvfhf pgri ih";  // کلمه "ارباب حلقه ها" وقتی کیبورد انگلیسی است
        //public static string Standard_Salt = ";hchfghk;h";  // کلمه "کازابلانکا" وقتی کیبورد انگلیسی است


        public const int OrderLevel_Removed = -10001;// { get { return -10001; } }
        public const int OrderLevel_Canceled = -200;// { get { return -200; } }
        public const int OrderLevel_Returned = -100; // { return -100; } }
        public const int OrderLevel_Ordering1_ConfirmItems = 100; // { return 100; } }        // در حال ثبت سفارش - کالاهای سفارش تأیید شده اند
        //public const int OrderLevel_Ordering2_ConfirmProperties = ; // { return 100; } }   // در حال ثبت سفارش - مشخصات کالال تأیید شده اند
        public const int OrderLevel_OrderCompleted = 200; // { return 200; } }  // ثبت سفارش کامل شده است
        public const int OrderLevel_SendToCompany = 300; // { return 300; } }   // به کارخانه ارسال شده است
        public const int OrderLevel_SaleConfirmed = 400; // { return 400; } }   // تأیید شده توسط واحد فروش
        public const int OrderLevel_FinancialConfirmed = 500; // { return 500; } }   // تأیید شده توسط واحد مالی
        public const int OrderLevel_Producting = 700; // { return 700; } }   // در حال تولید
        public const int OrderLevel_Producted = 800; // { return 800; } }   // تولید تکمیل شده است

        public const int OrderLevel_SentOut = 10000; // { return 10000; } }   // سفارش ارسال شده است


        public const int UserLevel_Admin = 10; // { return 10; } }
        public const int UserLevel_Supervisor1 = 101; // { return 101; } }
        public const int UserLevel_Supervisor2 = 102; // { return 102; } }
        public const int UserLevel_Supervisor3 = 103; // { return 103; } }        
        public const int UserLevel_RegOrderUnit = 1002; // واحد ثبت سفارش
        public const int UserLevel_SaleManager = 1011; //  سرپرست فروش
        public const int UserLevel_SaleUnit = 1012; // واحد فروش
        public const int UserLevel_FinancialUnit = 1022; // واحد مالی
        public const int UserLevel_PlanningUnit = 1032; // واحد برنامه ریزی
        public const int UserLevel_Agent = 2001; // { return 2001; } }  

        //public static Models.Order order_actions { get; set; }
    }

    static class Stack_Methods
    {
        public static string Miladi_to_Shamsi_YYYYMMDD(DateTime dtMiladi, string Between = "/")
        {
            var pc = new System.Globalization.PersianCalendar();
            int iMonth = pc.GetMonth(dtMiladi);
            string sMonth = (iMonth < 10) ? ("0" + iMonth) : iMonth.ToString();
            int iDay = pc.GetDayOfMonth(dtMiladi);
            string sDay = (iDay < 10) ? ("0" + iDay) : iDay.ToString();
            return (pc.GetYear(dtMiladi).ToString() + Between + sMonth + Between + sDay);
        }

        public static string NowTime_HHMMSSFFF(string between = ":")
        {
            string sDT = DateTime.Now.TimeOfDay.ToString(@"hh\:mm\:ss") + ":" + DateTime.Now.Millisecond;
            if (!between.Equals(":")) sDT = sDT.Replace(":", between);
            return sDT;
        }

        // با توجه به دو تابع تاریخ و زمان، برای زمان امروز، رشته ای با فرمت زیر را بر میگرداند
        // YYYY/MM/DD-HH:MM:SS
        public static string DateTimeNow_Shamsi(string BetweenDate = "/", string BetweenTime = ":",bool HasMillisecond = false)
        {
            string sMiladi_to_Shamsi_YYYYMMDD = Miladi_to_Shamsi_YYYYMMDD(DateTime.Now, BetweenDate)
                + "-" + NowTime_HHMMSSFFF(BetweenTime);
            if (HasMillisecond) return sMiladi_to_Shamsi_YYYYMMDD;
            else
            {
                // تعداد کاراکتر زمان بدون میلی ثانیه
                int nTimeLength = 15 + 2 * BetweenDate.Length+ 2 * BetweenTime.Length;
                return sMiladi_to_Shamsi_YYYYMMDD.Substring(0, nTimeLength);
            }
        }

        // شناسه، سطح دسترسی و ... را برای یک کاربر با معلوم بودن نام بر میگرداند
        public static bool GetAllUserData(string user_name)
        {
            Stack.UserName = user_name;
            Models.User user = Program.dbOperations.GetUserAsync(user_name);
            Stack.UserIndex = user.Index;
            Stack.Company_Index = user.Company_Index;
            if (Program.dbOperations.GetAllUser_ULsAsync(Stack.Company_Index, Stack.UserIndex).Any())
            {
                Stack.UserLevel_Index = Program.dbOperations.GetAllUser_ULsAsync(Stack.Company_Index, Stack.UserIndex).First().UL_Index;
                Stack.UserLevel_Type = Program.dbOperations.GetUser_LevelAsync(Stack.UserLevel_Index).Type;
            }

            Stack.bWarehouse_Booking_MaxHours = Program.dbOperations.GetCompanyAsync(user.Company_Index).Warehouse_AutomaticBooking;

            return ((Stack.UserIndex > 0) && (Stack.UserLevel_Index > 0) && (Stack.UserLevel_Type >= 0));
        }

        // شناسه تمام کاربران استاندارد مانند ادمین اصلی و ادمین و کاربر ارشد را بر میگرداند
        public static List<long> GetStandardUsersIndex(long user_level_type=100)
        {
            List<long> lstUL = new List<long>();
            if(user_level_type == 10)
            {
                foreach(Models.User_Level ul in Program.dbOperations.GetAllUser_LevelsAsync(Stack.Company_Index)
                    .Where(d=>d.Type>0).ToList())
                        lstUL.Add(ul.Index);
            }
            else
            {
                foreach (Models.User_Level ul in Program.dbOperations.GetAllUser_LevelsAsync(Stack.Company_Index)
                    .Where(d => d.Type ==user_level_type).ToList())
                        lstUL.Add(ul.Index);
            }
            List<long> lstResult = new List<long>();
            foreach (long ul_index in lstUL)
                lstResult.AddRange(Program.dbOperations.GetAllUser_ULsAsync(Stack.Company_Index, 0, ul_index)
                    .Select(d=>d.User_Index).ToArray());
            return lstResult;
        }
    }

    public class CryptographyProcessor
    {
        // طول رشته خرجی مضربی از 8 می باشد
        // حتی اگر عدد ورودی تابع مضربی از 8 نباشد
        public string CreateRandomSalt(int size = 32)
        {
            //Generate a cryptographic random number.
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[size];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }

        public string GenerateHash(string input, string salt)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(input + salt);
            SHA256Managed sHA256ManagedString = new SHA256Managed();
            byte[] hash = sHA256ManagedString.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        // RealHashedValue : رشته هش شده با سالت که در دیتابیس ذخیره شده است
        // InputValue : مقدار ورودی بدون هش که باید بررسی شود
        // salt : سالتی که باید بر روی مقدار ورودی اعمال شود و با رشته هش شده مقایسه شود
        public bool AreEqual(string RealHashedValue, string InputValue, string salt)
        {
            string InputHashedPin = GenerateHash(InputValue, salt);
            InputHashedPin = GenerateHash(InputHashedPin, ";hchfghk;h");  // کلمه "کازابلانکا" وقتی کیبورد انگلیسی است
            //string RealHashedPin = GenerateHash(RealHashPassword, ";hchfghk;h");  // کلمه "کازابلانکا" وقتی کیبورد انگلیسی است
            //return InputHashedPin.Equals(RealHashedPin);
            return InputHashedPin.Equals(RealHashedValue);
        }

        public string GenerateHash_2Times(string input, string salt1, string salt2)
        {
            if (string.IsNullOrEmpty(input) || string.IsNullOrEmpty(salt1) || string.IsNullOrEmpty(salt2))
                return null;

            string s = GenerateHash(input, salt1);
            return GenerateHash(s, salt2);
        }

        public string XOR_2Strings(string text, string key)
        {
            var result = new StringBuilder();

            for (int c = 0; c < text.Length; c++)
                result.Append((char)((uint)text[c] ^ (uint)key[c % key.Length]));

            return result.ToString();
        }
    }



}
