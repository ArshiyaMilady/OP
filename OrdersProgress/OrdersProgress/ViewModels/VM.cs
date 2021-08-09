using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OrdersProgress
{
    public class Relation_by_Level
    {
        public long Index { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int Level { get; set; }
        public string Top_Code { get; set; }
        public long Top_Index { get; set; }
        public bool IsModule { get; set; }
        public double Quantity { get; set; }

    }

    // دسترسی های یک سفارش به قسمتهای مختلف برنامه که مربوط به سفارش است
    public class Order_Access
    {
    }

    // دسترسی های یک کاربر به قسمتهای مختلف برنامه
    public class User_Access
    {
        public class Order_Level
        {

        }

        #region L1000_Orders در فرم
        public const long L1000_Order_Removed = 0b0000_0000_0000_0000_0000_0000_0000_0001; //  سفارش حذف شده
        public const long L1000_Order_Canceled = 0b0000_0000_0000_0000_0000_0000_0000_0010; //  سفارش کنسل شده
        public const long L1000_Order_Ordering = 0b0000_0000_0000_0000_0000_0000_0000_0100; //  سفارش در حال ثبت
        public const long L1000_Order_Ordered = 0b0000_0000_0000_0000_0000_0000_0000_1000; //  سفارش ثبت شده
        public const long L1000_Order_SentToCompany = 0b0000_0000_0000_0000_0000_0001_0000_0000; //  سفارش ارسال شده به شرکت
        public const long L1000_Order_SaleConfirmed = 0b0000_0000_0000_0000_0000_0000_0010_0000; //  سفارش تأیید شده توسط فروش
        public const long L1000_Order_FinancialConfirmed1 = 0b0000_0000_0000_0000_0000_0000_0100_0000; //  سفارش تأیید شده توسط مالی-مرحله 1
        public const long L1000_Order_Wait_to_Product = 0b0000_0000_0000_0000_0000_0000_1000_0000; //  سفارش در انتظار تولید - مثلا در واحد برنامه ریزی است
        public const long L1000_Order_Producting = 0b0000_0000_0000_0000_0000_0001_0000_0000; //  سفارش در حال تولید
        public const long L1000_Order_Sent_to_Warehouse = 0b0000_0000_0000_0000_0000_0010_0000_0000; //  سفارش تولید شده و به انبار ارسال شده
        public const long L1000_Order_FinancialConfirmed_Final = 0b0000_0000_0000_0000_0000_0100_0000_0000; //  سفارش تأیید نهایی مالی شده
        public const long L1000_Order_SentOut = 0b0000_0000_0000_0000_0000_1000_0000_0000; //  سفارش از انبار ارسال شده
        public const long L1000_Order_Installed = 0b0000_0000_0000_0000_0001_0000_0000_0000; //  سفارش تکمیل نصب
        public const long L1000_Order_DefiniteDelivery = 0b0000_0000_0000_0000_00010_0000_0000_0000; //  سفارش تحویل قطعی
        
        // دقت کنید که یک سفارش در حالی که مثلا می تواند منتظر تأیید فروش باشد، می تواند از مالی برگشت خورده باشد
        public const long L1000_Order_Returned = 0b0000_0000_0000_0000_0100_0000_0000_0000; //  سفارشهای برگشت شده

        //public const long L1000_Orders_Notremoved = 0b0000_0000_0000_0000_0000_0000_0000_0000; //  سفارشهای حذف نشده
        //public const long L1000_Orders_CurrentUser = 0b0000_0000_0000_0000_0000_0000_0000_0000; //  تمام سفارشها کاربر وارد شده
        //public const long L1000_Orders_All = 0b0000_0000_0000_0000_0000_0000_0000_0000; //  تمام سفارشها - مخصوص ادمین
        //public const long L1000_Orders_All = 0b0000_0000_0000_0000_0000_0000_0000_0000; //  تمام سفارشها - مخصوص ادمین

        //   سفارشهای یک کاربر - برای نماینده ها به کار می آید
        public const long L1000_Orders_OneUser = 0b0000_0000_0001_0000_0000_0000_0000_0000; 
        //   سفارشهای دیگر کاربران به جز ادمین اما با شرایط خاص - برای واحد ثبت سفارش ، واحد فروش ، مالی و غیره
        public const long L1000_Orders_Others = 0b0000_0000_0010_0000_0000_0000_0000_0000;
        public const long L1000_Orders_ExceptAdmin = 0b0100_0000_0000_0000_0000_0000_0000_0000; //  تمام سفارشها - به جز ادمین
        public const long L1000_Orders_All = 0b1000_0000_0000_0000_0000_0000_0000_0000; //  تمام سفارشها - مخصوص ادمین

        // هر سطح کاربر چه سفارشهایی را در فرم سفارشها ببیند
        public long VisibleWhichOrders(int user_level)
        {
            long result = 0b0000_0000_0000_0000_0000_0000_0000_0000;
            if (user_level == Stack.UserLevel_Admin) result = L1000_Orders_All;
            else if (user_level == Stack.UserLevel_Supervisor2) result = L1000_Orders_ExceptAdmin;

            else if (user_level == Stack.UserLevel_RegOrderUnit) result 
                    = L1000_Orders_OneUser | L1000_Orders_Others;
            else if (user_level == Stack.UserLevel_SaleManager) result 
                    = L1000_Orders_OneUser | L1000_Orders_Others | L1000_Order_SentToCompany | L1000_Order_Canceled;
            else if (user_level == Stack.UserLevel_SaleUnit) result 
                    = L1000_Orders_OneUser | L1000_Orders_Others | L1000_Order_SentToCompany;
            else if (user_level == Stack.UserLevel_FinancialUnit) result
                    = L1000_Order_SaleConfirmed | L1000_Order_Sent_to_Warehouse;
            else if (user_level == Stack.UserLevel_PlanningUnit) result = L1000_Order_FinancialConfirmed1;
            else if (user_level == Stack.UserLevel_Agent) result = L1000_Orders_OneUser;

            return result;
        }

        #endregion

    }
}
