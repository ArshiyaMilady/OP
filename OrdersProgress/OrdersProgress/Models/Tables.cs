using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OrdersProgress.Models
{


    public class Company
    {
        [PrimaryKey, AutoIncrement]
        public long Id { get; set; }

        public bool Active { get; set; }    // آیا شرکت اجازه فعالیت دارد؟
        public long Index { get; set; } // منحصربفرد
        //public string IndexName { get; set; }    // منحصر بفرد
        public string Real_Name { get; set; }    // نام واقعی شرکت
        public string OwnerName { get; set; }  // نام رابط شرکت
        public string Domain { get; set; }   // دامین شرکت
        public string Mobile { get; set; }  // شماره همراه رابط
        public string Phone { get; set; }       // تلفن ثابت رابط شرکت
        public string EMail { get; set; }       // ایمیل شرکت
        public DateTime DateTime_mi { get; set; }   // زمان ثبت به میلادی
        public string DateTime_sh { get; set; }   // زمان ثبت به شمسی

        // از این تاریخ به بعد کاربر غیر فعال می شود
        public DateTime End_DateTime_mi { get; set; }   // زمان پایان فعالیت به میلادی
        public string End_DateTime_sh { get; set; }   // زمان پایان فعالیت به شمسی


        // 0 : رزرو کالاها از انبار به صورت دستی
        // 1 : رزرو کالاها از انبار به صورت اتوماتیک
        public byte Warehouse_Booking { get; set; }

        public string C_S1 { get; set; }
        public string C_S2 { get; set; }
        public string C_S3 { get; set; }
        public long C_L1 { get; set; }
        public long C_L2 { get; set; }
        public int C_I1 { get; set; }
        public int C_I2 { get; set; }
        public bool C_B1 { get; set; }
        public bool C_B2 { get; set; }
        public bool C_B3 { get; set; }
    }

    public class User
    {
        [PrimaryKey, AutoIncrement]
        public long Id { get; set; }
        public long Company_Index { get; set; }

        public bool Active { get; set; }    // آیا کاربر اجازه فعالیت دارد؟

        // 101 : شناسه کسی که نام خریدار را ثبت می کند. در ابتدا این شناسه مربوط می شود به کارمند شرکت
        public long Index { get; set; } // منحصربفرد
        public string Name { get; set; }    // منحصر بفرد
        public string Real_Name { get; set; }
        public string Password { get; set; }  // معمولا 1111 است
        public string Mobile { get; set; }
        public string Phone { get; set; }
        public string EMail { get; set; }
        public string Address { get; set; }
        public long UserLevel_Index { get; set; }     // شناسۀ سطح دسترسی کاربر
        public string UserLevel_Description { get; set; }     // شرح سطح دسترسی کاربر
        public bool IsDefault { get; set; }    // آیا این کاربر پیش فرض است؟
        public DateTime DateTime_mi { get; set; }   // زمان ثبت به میلادی
        public string DateTime_sh { get; set; }   // زمان ثبت به شمسی

        // از این تاریخ به بعد کاربر غیر فعال می شود
        public DateTime End_DateTime_mi { get; set; }   // زمان پایان فعالیت به میلادی
        public string End_DateTime_sh { get; set; }   // زمان پایان فعالیت به شمسی
        public string User_Domain { get; set; }   // like  :  SGPCO\mehdi.rahimi
        public string Description { get; set; }

        public string C_S1 { get; set; }
        public string C_S2 { get; set; }
        public string C_S3 { get; set; }
        public long C_L1 { get; set; }
        public long C_L2 { get; set; }
        public int C_I1 { get; set; }
        public int C_I2 { get; set; }
        public bool C_B1 { get; set; }
        public bool C_B2 { get; set; }
        public bool C_B3 { get; set; }
    }

    // هر سطح کاربری چه کاربران دیگری را در فرم سطوح کاربران ببیند؟
    public class LoginHistory
    {
        [PrimaryKey, AutoIncrement]
        public long Id { get; set; }
        public long Index { get; set; }
        public long Company_Index { get; set; }

        public long User_Index { get; set; }
        public string User_RealName { get; set; }
        public DateTime DateTime_mi { get; set; }
        public string Date_sh { get; set; } // تاریخ ورود
        public string Time { get; set; }    // ساعت ورود
    }

    // User Level سطوح کاربری
    public class User_Level
    {
        [PrimaryKey, AutoIncrement]
        public long Id { get; set; }
        public long Company_Index { get; set; }
        public bool C_B1 { get; set; }  // فیلد کمکی
        public long Index { get; set; }
        public string Description { get; set; }
        public bool Enabled { get; set; }

        // 1 : real admin
        // 2 : admin(s)
        // 3 : Senior_Auto
        // 0 : others
        public int Type { get; set; }
    }

    // هر سطح کاربری چه کاربران دیگری را در فرم سطوح کاربران ببیند؟
    public class UL_See_UL
    {
        [PrimaryKey, AutoIncrement]
        public long Id { get; set; }
        public long Company_Index { get; set; }
        public bool C_B1 { get; set; }  // فیلد کمکی
        public long Index { get; set; }
        public long MainUL_Index { get; set; }  // سطح کاربر مذکور
        public long UL_Index { get; set; }  // مابقی سطوح کاربری
    }


    // برای تأیید درخواست ها به انبار چه سطح کاربری ، سرپرست سرپرست سطح کاربری دیگر به حساب می آید
    public class UL_Confirm_UL_Request
    {
        [PrimaryKey, AutoIncrement]
        public long Id { get; set; }
        public long Company_Index { get; set; }
        public bool C_B1 { get; set; }  // فیلد کمکی
        public long Index { get; set; }
        public long UL_Index { get; set; }  // سطح کاربر مذکور
        public long Supervisor_UL_Index { get; set; }  //  سطح کاربری سرپرست 
    }

    // UL = User_Level هر کاربر می تواند دارای یک یا چند سطح کاربری باشد
    public class User_UL
    {
        [PrimaryKey, AutoIncrement]
        public long Id { get; set; }
        public long Company_Index { get; set; }
        public long Index { get; set; }
        public long User_Index { get; set; }
        public long UL_Index { get; set; }
        public string UL_Description { get; set; }
    }

    // امکانات کاربری به صورت کامل در این جدول ثبت می شوند
    public class UL_Feature
    {
        [PrimaryKey, AutoIncrement]
        public long Id { get; set; }
        public long Company_Index { get; set; }
        public bool C_B1 { get; set; }  // فیلد کمکی
        public long Index { get; set; }
        public string Description { get; set; }
        public string Unique_Phrase { get; set; }   // عبارت منحصر بفرد برای راحت تر پیدا کردن یک قابلیت
        public bool Enabled { get; set; }
    }

    // تعریف امکانات هر سطح کاربری = ارتباط هر سطح با امکانات آن
    public class User_Level_UL_Feature
    {
        [PrimaryKey, AutoIncrement]
        public long Id { get; set; }
        public long Company_Index { get; set; }
        public long User_Level_Index { get; set; }  // شناسه سطح کاربری
        public long UL_Feature_Index { get; set; }    // شناسه امکانات
        public string UL_Feature_Unique_Phrase { get; set; }
        public bool UL_Feature_Enabled { get; set; }
    }

    // هر سطح کاربری درخواست کدام دسته از کالاها را می تواند از انبار داشته باشد
    public class UL_Request_Category
    {
        [PrimaryKey, AutoIncrement]
        public long Id { get; set; }
        public long Index { get; set; }
        public long Company_Index { get; set; }
        public long User_Level_Index { get; set; }  // شناسه سطح کاربری
        public long Category_Index { get; set; }    // شناسه دسته
        public bool Need_Supervisor_Confirmation { get; set; }    // آیا نیاز به تأیید سرپرست می باشد
        public bool Need_Manager_Confirmation { get; set; }    // آیا نیاز به تأیید مدیر می باشد

        public bool C_B1 { get; set; }    // کمکی
    }

    public class User_File
    {
        [PrimaryKey, AutoIncrement]
        public long Id { get; set; }
        public long Company_Index { get; set; }
        public long Index { get; set; }

        // نوع فایل
        // 101 : تصویر امضا
        public int Type { get; set; }
        public long User_Index { get; set; }
        public string File_Index { get; set; }
        public bool Enable { get; set; }
    }

    public class Order
    {
        [PrimaryKey, AutoIncrement]
        public long Id { get; set; }
        public long Company_Index { get; set; }
        public long Index_in_Company { get; set; } // این عدد، شناسه ای است که سفارش با آن در کارخانه مشخص می شود

        // عنوان سفارش - عبارتی که سفارش را با آن می شناسند
        public string Title { get; set; }    // منحصربفرد

        // شناسه کسی که نام خریدار را ثبت می کند. در ابتدا این شناسه مربوط می شود به کارمند شرکت
        public long User_Index { get; set; }  // شناسۀ شخصی که سفارش را ثبت کرده است

        // کد  سفارش که به صورت منحصر بفرد تولید می شود
        // User_Index + DateTime  در شروع این کد عبارت  است از
        public string Index { get; set; }  // منحصربفرد

        public string Customer_Index { get; set; }   // نام خریدار
        public string Customer_Name { get; set; }   // نام خریدار
        public DateTime DateTime_mi { get; set; }   // زمان ثبت به میلادی
        public string Date_sh { get; set; }         // تاریخ ثبت به شمسی
        public string Time { get; set; }            // زمان ثبت به شمسی

        // 0 : عمومی
        public int type { get; set; }   // نوع سفارش که در جدول مراحل سفارش هم به آن اشاره شده است

        #region OL از جدول
        public long PreviousLevel_Index { get; set; }   // آخرین مرحله ای که سفارش گذرانده است
        public long CurrentLevel_Index { get; set; }    // سفارش الان در چه مرحله ای باید قرار بگیرد
        //public long CurrentLevel_Sequence { get; set; } // مهم
        public long NextLevel_Index { get; set; }       // مرحله بعدی سفارش
        // توضیحاتی که به سفارش دهنده جهت اطلاع از وضعیت سفارش نشان داده می شود
        public string Level_Description { get; set; }
        #endregion

        //public string Returning_Description { get; set; }   // در صورت بازگشت ، توضیحات آخرین بازگشت را در خود نگه می دارد
        //public string Canceling_Description { get; set; }   // در صورت کنسل کردن ، توضیحات آنرا نگه می دارد
        public string Order_Description { get; set; }

        public string C_S1 { get; set; }
        public string C_S2 { get; set; }
        public string C_S3 { get; set; }
        public long C_L1 { get; set; }
        public long C_L2 { get; set; }
        public int C_I1 { get; set; }
        public int C_I2 { get; set; }
        public bool C_B1 { get; set; }      // 1 استفاده کمکی
        public bool C_B2 { get; set; }      // 2 استفاده کمکی
        public bool C_B3 { get; set; }
    }

    // مراحلی که از ابتدا تا انتها برای یک سفارش تعریف می شود
    public class Order_Level
    {
        [PrimaryKey, AutoIncrement]
        public long Id { get; set; }
        public long Company_Index { get; set; }
        public bool C_B1 { get; set; }  //  کمکی

        public long Index { get; set; }
        // عددی که فقط ترتیب نمایش مراحل را در جدول مشخص میکند. بعد از ثبت نباید تغییر نماید
        public long Sequence { get; set; }
        public bool Enabled { get; set; }
        public string Description { get; set; } // توضیح در باره مرحله ای که واقعا انجام شده است

        public bool OrderCanChange { get; set; }    // آیا در این مرحله هنوز امکان تغییر (اطلاعات و کالاهای) سفارش می باشد
        public bool ReturningLevel { get; set; }    // آیا این مرحله مربوط به برگشت سفارش است  
        public bool CancelingLevel { get; set; }    // آیا این مرحله مربوط به کنسل شدن سفارش است  
        public bool RemovingLevel { get; set; }     // آیا این مرحله مربوط به حذف شدن سفارش است  

        // 0 : برای سفارشهای عمومی
        public int Type { get; set; }
        public string Type_Description { get; set; }
        public bool FirstLevel { get; set; }    // آیا این مرحله ، مرحله آغازین است؟
        public bool LastLevel { get; set; }      // آیا این مرحله ، مرحله پایانی است؟

        // متن پیامی که باید به کاربر نمایش داده شود تا اقدام لازم بر روی سفارش انجام گیرد
        public string MessageText { get; set; }
        public string Description2 { get; set; }    // توضیحی که در جدول سفارشها نمایش داده می شود
    }

    // در صورت بازگشت سفارش در هر مرحله، سفارش به کدام مرحله می تواند برود؟
    public class Order_Level_on_Returning
    {
        [PrimaryKey, AutoIncrement]
        public long Id { get; set; }
        public long Company_Index { get; set; }
        public long Index { get; set; }

        public long OrderLevel_Index { get; set; }
        public long OL_Retruned_Index { get; set; }
    }


    // سفارش چه مراحلی گذرانده است
    // تفاوت با تاریخچه: در صورت یک یا چند برگشت سفارش ، مراحل را می توان از این جدول حذف نمود
    public class Order_OL
    {
        [PrimaryKey, AutoIncrement]
        public long Id { get; set; }
        public long Company_Index { get; set; }
        public long Index { get; set; }

        public string Order_Index { get; set; }
        public long OrderLevel_Index { get; set; }
    }

    // پیش نیازهای یک مرحله از سفارش را بر اساس مراحل دیگر تعریف میکند
    public class OL_Prerequisite
    {
        [PrimaryKey, AutoIncrement]
        public long Id { get; set; }
        public long Company_Index { get; set; }
        public long Index { get; set; }

        public long OL_Index { get; set; }   // مرحله اصلی
        public long Prerequisite_Index { get; set; }  // مرحله (های) پیش نیازِ مرحله اصلی
    }

    // چه سطح کاربرانی می توانند چه مراحلی را تأیید نمایید
    public class OL_UL
    {
        [PrimaryKey, AutoIncrement]
        public long Id { get; set; }
        public long Company_Index { get; set; }
        public long Index { get; set; }

        public long OL_Index { get; set; }   // شناسه مرحله 
        public long UL_Index { get; set; }   // شناسه سطح کاربر
    }

    // هر سطح کاربری امکان مشاهده چه مراحلی از سفارش را دارد
    public class UL_See_OL
    {
        [PrimaryKey, AutoIncrement]
        public long Id { get; set; }
        public long Company_Index { get; set; }
        public long Index { get; set; }

        public long UL_Index { get; set; }   // شناسه سطح کاربر
        public long OL_Index { get; set; }   // شناسه مرحله 
    }

    // --- در صورتیکه از مجموعه سفارشها در خط تولید استفاده شود این کلاس بدون کاربرد خواهد بود
    // تاریخچه سفارشها = به عبارت دیگر رابطه بین سفارش و مراحل عبور کرده
    // با توجه به جدول زیر سفارش می تواند وارد مرحله ی جدید شود
    public class Order_History
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public long Company_Index { get; set; }
        public long Index { get; set; }
        //public long PassedLevel_binary { get; set; }    // مراحل گذشته تا این تاریخ به صورت باینری

        public long User_Index { get; set; }    // شناسۀ شخصی که مرحله را انجام داده است
        public string User_Name { get; set; }     // نام شخصی که مرحله را انجام داده است
        public long User_Level_Index { get; set; }    // شناسۀ شخصی که مرحله را انجام داده است

        public string Order_Index { get; set; }
        public long OrderLevel_Index { get; set; }   //آخرین مرحله ای که سفارش گذرانده است
        public string OrderLevel_Description { get; set; }  // شرح مرحله ای که سفارش گذرانده است

        public DateTime DateTime_mi { get; set; }   // زمان انجام مرحله به میلادی
        public string DateTime_sh { get; set; }     // زمان انجام مرحله به شمسی

        // اگر نیاز به ذکر توضیحی باشد مانند علت بازگشتی و ... ، این توضیح در فیلد ذخیره می شود
        public string Description { get; set; }

        public string C_S1 { get; set; }
        public string C_S2 { get; set; }
        public string C_S3 { get; set; }
        public long C_L1 { get; set; }
        public long C_L2 { get; set; }
        public int C_I1 { get; set; }
        public int C_I2 { get; set; }
        public bool C_B1 { get; set; }
        public bool C_B2 { get; set; }
        public bool C_B3 { get; set; }
    }

    // ارتباط سفارش با کالاهای آن اولویتهای سفارش ها سفارشها با چه ترتیبی تولید یا با چه ترتیبی از انبار خارج شوند
    public class OrderPriority
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public long Company_Index { get; set; }
        public long Index { get; set; }
        public string Order_Index { get; set; }
        public string Order_Title { get; set; }
        public int Priority { get; set; }
        public bool IsCompleted { get; set; }   // آیا سفارش با برداشت از انبار ، کامل می شود؟
        public double TotalQuantity { get; set; }      // تعداد اقلام سفارش
        public double CanTakeQuantity { get; set; }      // تعداد اقلامی که می توان از انبار برداشت کرد
        public double RemainedQuantity { get; set; }    // جمع تعداد اقلام باقیمانده (کسری) از سفارش
        public int ProgressPercent { get; set; }    // درصد پیشرفت سفارش

        public string C_S1 { get; set; }
        public string C_S2 { get; set; }
        public string C_S3 { get; set; }
        public long C_L1 { get; set; }
        public long C_L2 { get; set; }
        public int C_I1 { get; set; }
        public int C_I2 { get; set; }
        public bool C_B1 { get; set; }
        public bool C_B2 { get; set; }
        public bool C_B3 { get; set; }
    }

    // کالاهایی که نیاز است از انبار برداشت شوند
    // فرق این جدول با جدول فوق در این است که مثلا اگر در جدول فوق کالایی به صورت
    // ماژول درخواست شده است، در این جدول کالا به صورت آیتمهای موجود در انبار تبدیل می شود
    // تا بتوان کسری آنها را به صورت دقیق مشخص نمود
    public class Order_StockItem
    {
        [PrimaryKey, AutoIncrement]
        public long Id { get; set; }
        public long Company_Index { get; set; }

        public int C_I1 { get; set; }   // می توان برای هر کاری (حتی ذخیره دیتا) استفاده نمود
        public bool C_B1 { get; set; }  // برای کارهای کمکی

        public long Index { get; set; }
        public string Order_Index { get; set; }

        public string Top_Name { get; set; }
        // اگر این کالا زیرمجموعه ای از یک ماژول است، کد ماژول را در این فیلد میریزیم
        public string Top_Code { get; set; }

        public long Item_Index { get; set; }
        public string Item_SmallCode { get; set; }   // کد کوچک کالا
        public string Item_Name_Samll { get; set; }  // نام اصلی

        //public bool Item_Module { get; set; }    // آیا این کالا یک ماژول (ترکیبی از چند کالای دیگر) است؟

        // تعداد کالا از یک نوع در سفارش. مقدار این فیلد نباید تغییر کند
        public double Quantity { get; set; }    // در اصل می توان این فیلد را «مقدار باقیماندۀ اولیه» نامید

        public long WarehouseIndex { get; set; }     // شناسه انباری که می توان تعدادی از کالای مورد نیاز را برداشت
        public double Quantity_CanTake { get; set; }    // تعداد کالاهایی که می توانند از انبار ارسال شوند
        public double Quantity_Remained { get; set; }    // تعداد کالاهایی که (پس از ارسال از انبار) باقی می ماند
        public string Comment { get; set; } // توضیح

        public string C_S1 { get; set; }
        public string C_S2 { get; set; }
        public string C_S3 { get; set; }
        public long C_L1 { get; set; }
        public long C_L2 { get; set; }
        //public int C_I1 { get; set; }     // در بالای کلاس
        public int C_I2 { get; set; }
        //public bool C_B1 { get; set; }  // برای کارهای کمکی
        public bool C_B2 { get; set; }
        public bool C_B3 { get; set; }
    }

    public class Order_Item
    {
        [PrimaryKey, AutoIncrement]
        public long Id { get; set; }
        public long Company_Index { get; set; }
        public long Index { get; set; }
        public string Order_Index { get; set; }

        #region Item
        public long Item_Index { get; set; }
        public string Item_Name_Samll { get; set; }  // نام اصلی
        public string Item_SmallCode { get; set; }   // کد کوچک کالا

        // --- اگر این کالا زیرمجموعه ای از یک ماژول است، کد ماژول را در این فیلد میریزیم
        public string Module_SmallCode { get; set; }
        public bool Item_Module { get; set; }    // آیا این کالا یک ماژول (ترکیبی از چند کالای دیگر) است؟

        public double Quantity { get; set; }    // تعداد کالا از یک نوع

        // قیمتهای زیر، قیمتهای مربوط به زمان سفارش می باشند
        // قیمت تمام شده یک عدد کالا - برای مواد اولیه قیمت خرید در نظر گرفته می شود
        public long FixedPrice { get; set; }
        public long SalesPrice { get; set; }  // قیمت فروش (بدون تخفیف) یک عدد کالا
        #endregion

        //public double Quantity_CanTake { get; set; }    // تعداد کالاهایی که می توانند از انبار ارسال شوند
        //public double Quantity_Remained { get; set; }    // تعداد کالاهایی که (پس از ارسال از انبار) باقی می ماند

        public string C_S1 { get; set; }
        public string C_S2 { get; set; }
        public string C_S3 { get; set; }
        public long C_L1 { get; set; }
        public long C_L2 { get; set; }
        public int C_I1 { get; set; }
        public int C_I2 { get; set; }
        public bool C_B1 { get; set; }
        public bool C_B2 { get; set; }
        public bool C_B3 { get; set; }
    }

    // ارتباط سفارش با کالاها و مشخصات هر کالا
    public class Order_Item_Property
    {
        [PrimaryKey, AutoIncrement]
        public long Id { get; set; }
        public long Company_Index { get; set; }
        public long Index { get; set; }
        public long OI_Index { get; set; }  // 'Index' from 'Order_Item' table
        public string Order_Index { get; set; }

        public long Item_Index { get; set; }
        public string Item_SmallCode { get; set; }   // کد کوچک کالا
        public int ItemBatch_Counter { get; set; }  // شمارنده کالاها در یک دسته از سفارش
        public int ItemOrder_Counter { get; set; }  // شمارنده کالا در کل سفارش

        #region مشخصات کالاهای سفارش با توجه به اطلاعات زمان سفارش
        public long Property_Index { get; set; }   // کد مشخصه
        public string Property_Name { get; set; }    // unique
        public string Property_Description { get; set; }
        // در بعضی از مشخصه ها، مقدار آن استاندارد نمی باشد ( مانند ارتفاع یا عرض یا ...) و
        //لازم است مقدار آن در سفارش معلوم گردد
        public string Property_Value { get; set; }
        public bool Property_ChangingValue { get; set; }     // آیا مقدار این مشخصه بعدا (توسط سفارش دهنده) قابل تغییر است؟
        #endregion

        // معمولا برابر یک است
        // در صورتیکه تمام کالاهای یک دسته دارای مشخصه های کاملا یکسان باشد، این
        // فیلد می تواند برابر تعداد کالاهای یک دسته باشد
        //public double Quantity { get; set; }

        public string C_S1 { get; set; }
        public string C_S2 { get; set; }
        public string C_S3 { get; set; }
        public long C_L1 { get; set; }
        public long C_L2 { get; set; }
        public int C_I1 { get; set; }
        public int C_I2 { get; set; }
        public bool C_B1 { get; set; }
        public bool C_B2 { get; set; }
        public bool C_B3 { get; set; }
    }

    // رابطه بین تجمیع سفارشها و سفارشهای درون آنرا نگه می دارد
    public class Collection
    {
        [PrimaryKey, AutoIncrement]
        public long Id { get; set; }
        public long Company_Index { get; set; }

        public long Index { get; set; } // شناسه منحصربفرد که باید در شرکت به خود بگیرد
        public bool IsCompleted { get; set; }   // آیا کامل شده است؟
        public DateTime DateTime_mi { get; set; }   // زمان تجمیع سفارشها
        public string DateTime_sh { get; set; }   // زمان به شمسی

        public long PreviousLevel_Index { get; set; }   // آخرین مرحله ای که سفارش گذرانده است
        public long CurrentLevel_Index { get; set; }    // سفارش الان در چه مرحله ای باید قرار بگیرد
        public int Priority { get; set; }   // اولویت مجموعه

        public string C_S1 { get; set; }
        public string C_S2 { get; set; }
        public string C_S3 { get; set; }
        public long C_L1 { get; set; }
        public long C_L2 { get; set; }
        public int C_I1 { get; set; }
        public int C_I2 { get; set; }
        public bool C_B1 { get; set; }
        public bool C_B2 { get; set; }
        public bool C_B3 { get; set; }
    }

    // تجمیع چند سفارش
    // مجموعه ای از یک یا چند سفارش را درخود نگه می دارد
    // اینکار برای تجمیع فعالیتهای یکسان در سفارشها و ارسال دسته ای آنها به خط تولید
    // بسیار مفید است
    public class OrdersCollection
    {
        [PrimaryKey, AutoIncrement]
        public long Id { get; set; }
        public long Company_Index { get; set; }
        public long Index { get; set; }

        public long Collection_Index { get; set; } // شناسه منحصربفرد که باید در شرکت به خود بگیرد
        public string Order_Index { get; set; }

        public string C_S1 { get; set; }
        public string C_S2 { get; set; }
        public string C_S3 { get; set; }
        public long C_L1 { get; set; }
        public long C_L2 { get; set; }
        public int C_I1 { get; set; }
        public int C_I2 { get; set; }
        public bool C_B1 { get; set; }
        public bool C_B2 { get; set; }
        public bool C_B3 { get; set; }
    }

    // کالاهایی که در مجموعه سفارش ، جهت تولید ثبت شده اند
    // مثلا اگر در سفارش 1 از کالای آ ، 4 عدد و در سفارش دیگری از همان کالا، 3 عدد درخواست
    // شده باشند، در جدول زیر ثبت می شود که از کالای آ ، 7 عدد درخواست شده است
    public class Collection_Item
    {
        [PrimaryKey, AutoIncrement]
        public long Id { get; set; }
        public long Company_Index { get; set; }
        public long Index { get; set; }

        public long Collection_Index { get; set; } // شناسه منحصربفرد
        public long Item_Index { get; set; }
        public string Item_SmallCode { get; set; }
        public string Item_SmallName { get; set; }
        public double Quantity { get; set; }

        public string C_S1 { get; set; }
        public string C_S2 { get; set; }
        public string C_S3 { get; set; }
        public long C_L1 { get; set; }
        public long C_L2 { get; set; }
        public int C_I1 { get; set; }
        public int C_I2 { get; set; }
        public bool C_B1 { get; set; }
        public bool C_B2 { get; set; }
        public bool C_B3 { get; set; }
    }

    // در یک مجموعه سفارش ، چه فعالیتهایی وجود دارد و از هر فعالیت چند درصد پیشرفت کرده است
    public class Collection_Action
    {
        [PrimaryKey, AutoIncrement]
        public long Id { get; set; }
        public long Company_Index { get; set; }
        public long Index { get; set; }
        public long Collection_Index { get; set; }

        #region مشخصات فعالیت در زمان تبدیل سفارش به فعالیت
        public long Action_Index { get; set; }
        public string Action_Name { get; set; }
        public double Action_Time { get; set; }    // مدت زمان انجام این فعالیت برای یک نفر نیرو
        public double Action_Workers { get; set; }     // تعداد نیروی لازم برای انجام این فعالیت
        public long Action_TotalCost { get; set; }     // هزینه (به ریال) کامل انجام این فعالیت در زمان سفارش
        // OPC پیش نیازهای این فعالیت در
        public string Action_Prerequisites { get; set; }

        // اولویت فعالیت در روند ساخت 
        // در صورتیکه عدد اولویت یک فعالیت، کمتر باشد، آن فعالیت باید ابتدا اجرا شود
        // و در صورتیکه دو فعالیت دارای اولویت یکسانی باشند، می توانند همزمان با هم اجرا شوند
        public int OPC_Action_Priority { get; set; }
        #endregion

        public int ProgressPercent_Real { get; set; }   // درصد پیشرفت واقعی یک فعالیت از سفارش
        public int ProgressPercent_Planning { get; set; }   // درصد پیشرفت برنامه ای یک فعالیت از سفارش

        public long CurrentContractor_Index { get; set; }   // پیمانکار جاری این فعالیت

        public bool Confirm_Contractor { get; set; }    // تأیید پیمانکار
        public bool Confirm_LineManager { get; set; }   // تأیید سرپرست تولید
        public bool Confirm_QC { get; set; }    // تأیید کنترل کیفی

        public string C_S1 { get; set; }
        public string C_S2 { get; set; }
        public string C_S3 { get; set; }
        public long C_L1 { get; set; }
        public long C_L2 { get; set; }
        public int C_I1 { get; set; }
        public int C_I2 { get; set; }
        public bool C_B1 { get; set; }
        public bool C_B2 { get; set; }
        public bool C_B3 { get; set; }
    }

    // تاریخچۀ پیشرفت فعالیتهای یک مجموعه سفارش 
    public class Collection_Action_History
    {
        [PrimaryKey, AutoIncrement]
        public long Id { get; set; }
        public long Company_Index { get; set; }
        public long Index { get; set; }
        public long Collection_Index { get; set; }
        public long Action_Index { get; set; }
        public int ProgressPercent_Real { get; set; }   // درصد پیشرفت واقعی یک فعالیت از سفارش
        public long CurrentContractor_Index { get; set; }   // پیمانکار جاری این فعالیت در این زمان

        public string Description { get; set; }   // شرح تایخچه
        public bool Confirm_Contractor { get; set; }    // تأیید پیمانکار
        public bool Confirm_LineManager { get; set; }   // تأیید سرپرست تولید
        public bool Confirm_QC { get; set; }    // تأیید کنترل کیفی

        public DateTime DateTime_mi { get; set; }   // زمان ثبت به میلادی
        public string DateTime_sh { get; set; }   // زمان ثبت به شمسی

        public string C_S1 { get; set; }
        public string C_S2 { get; set; }
        public string C_S3 { get; set; }
        public long C_L1 { get; set; }
        public long C_L2 { get; set; }
        public int C_I1 { get; set; }
        public int C_I2 { get; set; }
        public bool C_B1 { get; set; }
        public bool C_B2 { get; set; }
        public bool C_B3 { get; set; }
    }

    public class Order_Attachment
    {
        [PrimaryKey, AutoIncrement]
        public long Id { get; set; }
        public long Company_Index { get; set; }
        public long Index { get; set; }
        // 102 : پیوست سفارش
        public int Type { get; set; }
        public string Order_Index { get; set; }
        public long File_Index { get; set; }
        public bool Enable { get; set; }
    }

    public class File
    {
        [PrimaryKey, AutoIncrement]
        public long Id { get; set; }
        public long Company_Index { get; set; }

        public long Index { get; set; }
        public byte[] Content { get; set; }     // محتوای فایل 
        public string OriginalFileName { get; set; }    // نام ابتدایی فایل
        public string Description { get; set; }
        public DateTime DateTime_mi { get; set; }   // زمان وارد کردن فایل به میلادی
        public string DateTime_sh { get; set; }   // زمان وارد کردن فایل به شمسی
        public bool Enable { get; set; }    // آیا فایل قابل استفاده است
    }

    public class Customer
    {
        [PrimaryKey, AutoIncrement]
        public long Id { get; set; }
        public long Company_Index { get; set; }

        // کد خریدار که به صورت منحصر بفرد تولید می شود
        // UserIndex + DateTime  در شروع این کد عبارت  است از
        public string Index { get; set; }  // منحصربفرد
        // 101 : شناسه کسی که نام خریدار را ثبت می کند. در ابتدا این شناسه مربوط می شود به کارمند شرکت
        public long User_Index { get; set; }

        // نام خریدار 
        public string Name { get; set; }    // منحصربفرد
        public string Mobile { get; set; }  // منحصربفرد
        public string Phone { get; set; }  // منحصربفرد
        public string Address { get; set; }
        public string Description { get; set; }  // شرحی در باب خریدار

        public string C_S1 { get; set; }
        public string C_S2 { get; set; }
        public string C_S3 { get; set; }
        public long C_L1 { get; set; }
        public long C_L2 { get; set; }
        public int C_I1 { get; set; }
        public int C_I2 { get; set; }
        public bool C_B1 { get; set; }
        public bool C_B2 { get; set; }
        public bool C_B3 { get; set; }
    }

    // ارتباط خریدار با سفارش هایش
    public class Order_Customer
    {
        [PrimaryKey, AutoIncrement]
        public long Id { get; set; }
        public long Company_Index { get; set; }
        public long Index { get; set; }
        public string Customer_Index { get; set; }
        public string Order_Index { get; set; }
    }


    // پیش فاکتور
    public class Proforma
    {
        [PrimaryKey, AutoIncrement]
        public long Id { get; set; }
        public long Company_Index { get; set; }

        // توسط این فیلد ، پیش فاکتور با ردیف هایش ارتباط برقرار می کند
        // Order_Index + "-" + DateTime
        public string Index { get; set; }

        // در صورتیکه برای سفارش پیش فاکتور جدیدی صادر شود باید ، پیش فاکتورهای قبلی ، غیرفعال شوند
        public bool Enable { get; set; }

        public long User_Index { get; set; }  // شناسه کاربر سفارش دهنده
        public string Order_Index { get; set; }
        public long Order_Id { get; set; }   // شناسه سفارش که در دیتابیس مرکزی مشخص می شود
        public string Order_ProformaNo { get; set; }   // شماره پیش فاکتور که توسط واحد مالی (در دیتابیس مرکزی) مشخص می شود
        public DateTime DateTime_mi { get; set; }   // زمان ثبت به میلادی
        public string DateTime_sh { get; set; }   // زمان ثبت به شمسی
        //public int Agent_Id { get; set; }   // شناسه نماینده / عاملیت / غیره
        //public string Agent_Name { get; set; }   // نام نماینده / عاملیت / غیره
        //public int Customer_Id { get; set; }   // شناسه خریدار
        public string Customer_Index { get; set; }   // شناسۀ خریدار
        public string Customer_Name { get; set; }   // نام خریدار

        public int TotalPrice_withoutDiscount { get; set; } // جمع کل قیمتهای بدون تخفیف
        public double Discount1 { get; set; }    // تخفیف اول
        public double Discount2 { get; set; }    // تخفیف دوم
        public double Discount3 { get; set; }    // صد تخفیف موردی
        public double Discount4 { get; set; }

        // اگر مخالف صفر باشد یعنی خالص پرداختی با احتساب مالیات می باشد
        public double? Tax { get; set; }
        public int TotalPrice_NetPayable { get; set; } // جمع کل - خالص پرداختی : بعد از اعمال تخفیفها

        public string PaymentMethod_Description { get; set; }   // شرح نوع پرداخت
        // ضریب تغییر قیمت بر اساس نحوه پرداخت
        // مثلا برای 40-60 یکماهه برابر 1.018 می باشد
        public double DiscountPayment_Factor { get; set; }

        // User_Index : در جدول فایل، شناسۀ امضای کاربر ثبت کنندۀ سفارش را بر میگرداند
        public string RegSign_Index { get; set; }    // آیا امضای نماینده را دارد

        // User_Index : در جدول فایل، شناسۀ امضای کارمند واحد فروش (تأیید کنندۀ سفارش) را بر میگرداند
        public string Confirmor10Sign_Index { get; set; }

        // User_Index : در جدول فایل، شناسۀ امضای سرپرست واحد فروش (تأیید کنندۀ سفارش) را بر میگرداند
        public string Confirmor20Sign_Index { get; set; }

        // User_Index : در جدول فایل، شناسۀ امضای واحد مالی (تأیید کنندۀ سفارش) را بر میگرداند
        public string Confirmor30Sign_Index { get; set; }

        // User_Index : در جدول فایل، شناسۀ امضای مدیریت (تأیید کنندۀ سفارش) را بر میگرداند
        public string Confirmor90Sign_Index { get; set; }

        public string C_S1 { get; set; }
        public string C_S2 { get; set; }
        public string C_S3 { get; set; }
        public long C_L1 { get; set; }
        public long C_L2 { get; set; }
        public int C_I1 { get; set; }
        public int C_I2 { get; set; }
        public bool C_B1 { get; set; }
        public bool C_B2 { get; set; }
        public bool C_B3 { get; set; }
    }

    // در این جدول هر ردیف از پیش فاکتور یک رکورد است
    public class Proforma_Row
    {
        [PrimaryKey, AutoIncrement]
        public long Id { get; set; }
        public long Company_Index { get; set; }
        public long Index { get; set; }
        public string Proforma_Index { get; set; }
        public long User_Index { get; set; }
        public string Order_Index { get; set; }
        public long Order_Id { get; set; }   // شناسه سفارش که در دیتابیس مرکزی مشخص می شود
        public long Item_Index { get; set; }
        public string Item_SmallCode { get; set; }
        public string Item_SmallName { get; set; }
        public int? Depo_type { get; set; }  // نوع دپو : (دپو نیست = 0) ، (کلاس بی = 2) ، (سهند = 3) و (لوکس داخلی = 4) و غیره

        // در صورت درست بودن ، این ردیف در خروجی (مانند پرینت) نمایش داده نخواهد شد
        public bool DontShow_in_Output { get; set; }
        public long PriceUnit_withoutDiscount { get; set; }  // قیمت واحد بدون تخفیف 
        public double Discount_Quantity { get; set; }   // تخفیف تعدادی 
        public int Quantity { get; set; }  // تعداد 

        public int PriceRow_withoutDiscount { get; set; }  // قیمت ردیف بدون تخفیف 
        public int PriceRow_withDiscount { get; set; }  // قیمت ردیف با تخفیف 


        public string C_S1 { get; set; }
        public string C_S2 { get; set; }
        public string C_S3 { get; set; }
        public long C_L1 { get; set; }
        public long C_L2 { get; set; }
        public int C_I1 { get; set; }
        public int C_I2 { get; set; }
        public bool C_B1 { get; set; }
        public bool C_B2 { get; set; }
        public bool C_B3 { get; set; }
    }

    //  مشخصه
    // این فعالیت بسیار مهم و دارای تنوع است زیاد است
    // به عنوان مثال می توان برای ارتفاع و عرض و ضخامت و ... مشخصه 
    // تعریف نمود و در هر سفارش به صورت مختص آن برای کالاهای آن به مشخصه ها مقدار داد
    public class Property
    {
        [PrimaryKey, AutoIncrement]
        public long Id { get; set; }
        public long Company_Index { get; set; }
        public bool C_B1 { get; set; }  // از این فیلد استفاده نشود. برای کارهای مختلف استفاده می گردد

        public long Index { get; set; }     // unique
        public string Name { get; set; }    // unique
        public string Description { get; set; }
        public bool Enable { get; set; }

        // فیلد زیر در هنگام برقراری رابطع کالا با مشخصات کاربرد دارند
        public string DefaultValue { get; set; }    // مقدار پیش فرض مشخصه

        // در فیلد زیر مقداری ذخیره نمی شود و این فیلد صرفا برای کمک به فیلد مشابه در جدول 
        // می باشد Item_Property
        public bool ChangingValue { get; set; }     // آیا مقدار این مشخصه بعدا (توسط سفارش دهنده) قابل تغییر است؟

        //public int Position { get; set; }   // مکان مشخصه در کد با شروع از عدد یک
        public string C_S1 { get; set; }
        public string C_S2 { get; set; }
        public string C_S3 { get; set; }
        public long C_L1 { get; set; }
        public long C_L2 { get; set; }
        public int C_I1 { get; set; }
        public int C_I2 { get; set; }

        // در اول کلاس آمده است
        //public bool C_B1 { get; set; }  // از این فیلد استفاده نشود. برای کارهای مختلف استفاده می گردد
        public bool C_B2 { get; set; }
        public bool C_B3 { get; set; }
    }

    // دسته هر محصول را مشخص می کند. مانند : درب، چارچوب، یراق،کمد و غیره
    public class Category
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public long Company_Index { get; set; }
        public long Index { get; set; } // شناسه 
        public bool C_B1 { get; set; }    // کمکی
        public string Name { get; set; }    // نام دسته - باید منحصر بفرد باشد
        public string Description { get; set; }    // شرح دسته
        public bool Need_Supervisor_Confirmation { get; set; }    // واقعا کمکی
        public bool Need_Manager_Confirmation { get; set; }    // واقعا کمکی
    }

    // کالا : رجوع به فایل اکسل کدها و موجودی انبار
    public class Item
    {
        [PrimaryKey, AutoIncrement]
        public long Id { get; set; }
        public long Company_Index { get; set; }
        public long Category_Index { get; set; }    // شناسه دسته
        public bool C_B1 { get; set; }  // کمکی

        // این محصول قابل استفاده است. مثلا در صورت قابل استفاده نبودن، امکان سفارش دهی نخواهد داشت
        public bool Enable { get; set; }

        // مانند شناسۀ پشت کار عمل میکند
        // هرگاه کد توسط کاربر تغییر نماید این شناسه رابطه ها را حفظ میکند
        // هرگاه نیاز به خروجی اکسل و بارگزاری اطلاعات به پایگاه داده دیگری باشد این شناسه بسیار مفید خواهد بود
        public long Index { get; set; }

        public string Name_Samll { get; set; }  // نام اصلی
        public string Code_Small { get; set; }  // کد اصلی و منحصربفرد

        // 0 : کالا یا ماژول
        // 1 : مواد اولیه
        public int Type { get; set; }
        // آیا این کالا یک ماژول (ترکیبی از چند کالای دیگر) است؟
        public bool Module { get; set; }
        // دو فیلد زیر در هنگام تعریف ماژول ها کاربرد دارند
        public double Quantity { get; set; }    // تعداد کالا در ماژول

        public int Significance_Factor { get; set; }    // ضریب اهمیت
        public int Depo_type { get; set; }    // نوع دپو : (دپو نیست = 0) و غیره

        public bool Salable { get; set; }   // قابل فروش = قابل سفارش دهی
        //public bool Changeable { get; set; }   // این کالا قابل تغییر است. سندی از آن صادر نشده و غیره

        // قیمتهای زیر، قیمتهای بروز می باشند
        public long FixedPrice { get; set; }  // قیمت تمام شده یک عدد کالا - برای مواد اولیه قیمت خرید در نظر گرفته می شود
        public long SalesPrice { get; set; }  // قیمت فروش (بدون تخفیف) یک عدد کالا

        public long Warehouse_Index { get; set; }   // شناسه انبار
        // موجودی کالا در انبار
        public double Wh_OrderPoint { get; set; }   // نقطه سفارش
        public double Wh_Quantity_Real { get; set; }   // موجودی واقعی
        // برای نگهداری تغییراتی که هنوز تأیید نشده است  = رزرو شده ها
        public double Wh_Quantity_Booking { get; set; }
        public double Wh_Quantity_x { get; set; }
        //public string Wh_Location { get; set; }     // مکان کالا در انبار
        public string Unit { get; set; }

        public double Weight { get; set; }  // وزن هرواحد بر حسب کیلوگرم
        public string Name_Full { get; set; }
        public string Code_Full { get; set; }
        public bool Need_QC_Confirmation { get; set; } // آیا برای تأیید این ردیف از سند نیاز به تأیید کنترل کیفی می باشد؟
        public bool Need_Manager_Confirmation { get; set; } // آیا برای تأیید این ردیف از سند نیاز به تأیید مدیریت می باشد؟
        public bool Bookable { get; set; }  // آیا این کالا بنا به درخواست یا سفارش قابل رزرو کردن می باشد؟
        public string C_S1 { get; set; }
        public string C_S2 { get; set; }
        public string C_S3 { get; set; }
        public long C_L1 { get; set; }
        public long C_L2 { get; set; }
        public int C_I1 { get; set; }
        public int C_I2 { get; set; }

        // در اول کلاس آمده است
        //public bool C_B1 { get; set; }  // کمکی
        public bool C_B2 { get; set; }
        public bool C_B3 { get; set; }
    }

    // تمام فایلهای مرتبط با کالایی را می توان در این جدول مشخص نمود
    public class Item_File
    {
        [PrimaryKey, AutoIncrement]
        public long Id { get; set; }
        public long Company_Index { get; set; }
        public long Index { get; set; }
        // Type=1 : تصویر کالا
        public int Type { get; set; }
        public string Item_Code_Small { get; set; }
        public long File_Index { get; set; }    // شناسه فایل
        public bool Enable { get; set; }
    }

    // ارتباط کالا با مشخصه ها
    public class Item_Property
    {
        [PrimaryKey, AutoIncrement]
        public long Id { get; set; }
        public long Company_Index { get; set; }
        public long Index { get; set; }
        public long Item_Index { get; set; }
        public string Item_Code_Small { get; set; }  // کالا
        //public bool Item_Enable { get; set; }    // این محصول قابل استفاده است

        public long Property_Index { get; set; }  // شناسۀ مشخصه
        //public bool Property_Enable { get; set; }    // این محصول قابل استفاده است
        public string DefaultValue { get; set; }    // مقدار پیش فرض مشخصه
        public bool ChangingValue { get; set; }     // آیا مقدار این مشخصه بعدا (توسط سفارش دهنده) قابل تغییر است؟
    }

    // OPC ارتباط بین کالاها و 
    public class Item_OPC
    {
        [PrimaryKey, AutoIncrement]
        public long Id { get; set; }
        public long Company_Index { get; set; }
        public long Index { get; set; }
        public long OPC_Index { get; set; }
        public long Item_Index { get; set; }
        public string Item_SmallCode { get; set; }
    }

    // ماژول ها : کالایی که ساخته شده از چند کالای دیگر است
    public class Module
    {
        [PrimaryKey, AutoIncrement]
        public long Id { get; set; }
        public long Company_Index { get; set; }
        public long Index { get; set; }
        public bool Enable { get; set; }
        public string Module_Code_Small { get; set; }  // کد کالای اصلی یا همان ماژول
        public long Item_Index { get; set; }
        public string Item_Code_Small { get; set; }  // کد کالای زیرمجموعه ی ماژول
        public double Quantity { get; set; }    // تعداد کالا در ماژول
        public string Unit { get; set; }
    }

    // فعالیتها
    public class Action
    {
        [PrimaryKey, AutoIncrement]
        public long Id { get; set; }
        public long Company_Index { get; set; }

        public long Index { get; set; }
        public string Name { get; set; }    // منحصربفرد
        public double Time { get; set; }    // مدت زمان انجام این فعالیت برای یک نفر نیرو
        public long Cost_per_Hour { get; set; }      // هزینه (به ریال) انجام این فعالیت به ازای یک نفر نیرو بر ساعت
        public double Workers { get; set; }     // تعداد نیروی لازم برای انجام این فعالیت
        public bool Enable { get; set; }

        public string C_S1 { get; set; }
        public string C_S2 { get; set; }
        public string C_S3 { get; set; }
        public long C_L1 { get; set; }
        public long C_L2 { get; set; }
        public int C_I1 { get; set; }
        public int C_I2 { get; set; }
        public bool C_B1 { get; set; }
        public bool C_B2 { get; set; }
        public bool C_B3 { get; set; }
    }

    // OPC :  ترکیبی از فعالیتها
    public class OPC
    {
        [PrimaryKey, AutoIncrement]
        public long Id { get; set; }
        public long Company_Index { get; set; }

        public long Index { get; set; }    // همان کد شش رقمی که در جدول مشخصه ها برای آن در نظر گرفته شده است
        public string Name { get; set; }
        public bool Enable { get; set; }
        public double Time { get; set; }    // مدت زمان انجام این مورد برای یک نفر نیرو

        public string C_S1 { get; set; }
        public string C_S2 { get; set; }
        public string C_S3 { get; set; }
        public long C_L1 { get; set; }
        public long C_L2 { get; set; }
        public int C_I1 { get; set; }
        public int C_I2 { get; set; }
        public bool C_B1 { get; set; }
        public bool C_B2 { get; set; }
        public bool C_B3 { get; set; }

    }

    // OPC ارتباط بین فعالیتها و 
    public class OPC_Acions
    {
        [PrimaryKey, AutoIncrement]
        public long Id { get; set; }
        public long Company_Index { get; set; }
        public long Index { get; set; }
        public long OPC_Index { get; set; }
        public long Action_Index { get; set; }
        // اولویت فعالیت - در صورتیکه عدد اولویت یک فعالیت، کمتر باشد، آن فعالیت باید ابتدا اجرا شود
        // و در صورتیکه دو فعالیت دارای اولویت یکسانی باشند، می توانند همزمان با هم اجرا شوند
        public int Priority { get; set; }

        // OPC پیش نیازهای این فعالیت در
        // Action1_Index, Action2_Index, Action3_Index, ...
        public string Action_Prerequisites { get; set; }
    }

    // واحد ها و پیمانکاران
    public class Unit
    {
        [PrimaryKey, AutoIncrement]
        public long Id { get; set; }
        public long Company_Index { get; set; }

        public long Index { get; set; }
        public string Name { get; set; }
        public bool Enable { get; set; }
        public double Time { get; set; }    // مدت زمانی (به دقیقه) که این پیمانکار در اختیار دارد

        public string C_S1 { get; set; }
        public string C_S2 { get; set; }
        public string C_S3 { get; set; }
        public long C_L1 { get; set; }
        public long C_L2 { get; set; }
        public int C_I1 { get; set; }
        public int C_I2 { get; set; }
        public bool C_B1 { get; set; }
        public bool C_B2 { get; set; }
        public bool C_B3 { get; set; }
    }

    // ارتباط بین فعالیتها و پیمانکار
    // هر پیمانکار چه فعالیتهایی را می تواند انجام دهد؟
    public class Unit_Acion
    {
        [PrimaryKey, AutoIncrement]
        public long Id { get; set; }
        public long Company_Index { get; set; }
        public long Index { get; set; }
        public long Unit_Index { get; set; }
        public long Action_Index { get; set; }
    }

    //  انبارها : در صورتیکه بیش از چند انبار وجود داشته باشد
    public class Warehouse
    {
        [PrimaryKey, AutoIncrement]
        public long Id { get; set; }
        public long Company_Index { get; set; }

        public long Index { get; set; } // شناسه انبار
        public bool Active { get; set; }    // آیا انبار فعال است
        public string Name { get; set; }    // نام انبار - باید منحصربفرد باشد
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }

        public string C_S1 { get; set; }
        public string C_S2 { get; set; }
        public string C_S3 { get; set; }
        public long C_L1 { get; set; }
        public long C_L2 { get; set; }
        public int C_I1 { get; set; }
        public int C_I2 { get; set; }
        public bool C_B1 { get; set; }
        public bool C_B2 { get; set; }
        public bool C_B3 { get; set; }
    }

    // درخواستهای کالا از انبار
    public class Warehouse_Request
    {
        [PrimaryKey, AutoIncrement]
        public long Id { get; set; }
        public long Company_Index { get; set; }
        public long Index { get; set; }
        public long Index_in_Company { get; set; }  // شماره درخواست در شرکت
        public long Unit_Index { get; set; }    // شناسه واحد درخواست کننده
        public string Unit_Name { get; set; }
        public long User_Index { get; set; }    // شناسه کاربر درخواست کننده
        public string User_Name { get; set; }
        public DateTime DateTime_mi { get; set; }   // زمان ثبت به میلادی
        public string DateTime_sh { get; set; }   // زمان ثبت به شمسی
        public long CostCenter_Index { get; set; }
        public long Item_Index { get; set; }
        public string Item_SmallCode { get; set; }
        public string Item_SmallName { get; set; }
        public double Quantity { get; set; }    // مقدار درخواستی
        public string Description { get; set; }
        public string Status_Description { get; set; } // آخرین شرحی که برای درخواست داده می شود. مانند علت عدم تأیید یا غیره

        #region تأییدیه سرپرست و مدیر در صورت نیاز به آنها
        public bool Need_Supervisor_Confirmation { get; set; }  // نیاز به تأیید سرپرست دارد؟
        public long Supervisor_Confirmer_Index { get; set; }    // شناسه سرپرست تأیید کننده
        public string Supervisor_Confirmer_Name { get; set; }    // نام سرپرست تأیید کننده
        public bool Need_Manager_Confirmation { get; set; }     // نیاز به تأیید مدیر دارد؟
        public long Manager_Confirmer_Index { get; set; }    // شناسه مدیر تأیید کننده
        public string Manager_Confirmer_Name { get; set; }    // نام مدیر تأیید کننده
        #endregion
    }

    // ردیف های موجود در حواله یا رسید انبار
    public class Warehouse_Request_Row
    {
        [PrimaryKey, AutoIncrement]
        public long Id { get; set; }
        public long Company_Index { get; set; }
        public long Index { get; set; }
        public long Warehouse_Request_Index { get; set; }
        public string Item_Index { get; set; }
        public string Item_SmallCode { get; set; }    // کد کوچک کالا
        public string Item_Name { get; set; }
        public double Quantity { get; set; }   // تعداد کالا در ردیف
        public string Item_Unit { get; set; }   // واحد شمارش کالا
        public string Description { get; set; }

        #region آیا تأیید سرپرست یا مدیر نیاز می باشد
        public bool Need_Supervisor_Confirmation { get; set; }  // نیاز به تأیید سرپرست دارد؟
        public bool Need_Manager_Confirmation { get; set; }     // نیاز به تأیید مدیر دارد؟
        #endregion

        //public double Quantity_Confirmed { get; set; }   // تعداد تأیید شده نهایی

        public bool C_B1 { get; set; }
        public bool C_B2 { get; set; }
        public bool C_B3 { get; set; }
    }

    public class Warehouse_Request_History
    {
        [PrimaryKey, AutoIncrement]
        public long Id { get; set; }
        public long Company_Index { get; set; }
        public long Index { get; set; }
        public long Warehouse_Request_Index { get; set; }
        public long User_Index { get; set; }    // شناسۀ شخصی که عملی را انجام داده است
        public string User_Name { get; set; }     // نام شخصی که عملی را انجام داده است
        public long User_Level_Index { get; set; }    // شناسۀ شخصی که عملی را انجام داده است

        public DateTime DateTime_mi { get; set; }   // زمان انجام به میلادی
        public string DateTime_sh { get; set; }     // زمان انجام به شمسی

        // اگر نیاز به ذکر توضیحی باشد مانند علت عدم تأیید و ... ، این توضیح در فیلد ذخیره می شود
        public string Description { get; set; }
    }

    // سند رسید یا حواله انبار
    public class Warehouse_Remittance
    {
        [PrimaryKey, AutoIncrement]
        public long Id { get; set; }
        public long Company_Index { get; set; }

        public long Index { get; set; }     // شناسه مخصوص این حواله
        public int Warehouse_Index { get; set; }   // شناسه انبار
        public string User_Index { get; set; }  // شناسه ثبت کننده حواله
        public string User_Name { get; set; }  // نام ثبت کننده حواله
        public DateTime DateTime_mi { get; set; }   // زمان ثبت به میلادی
        public string DateTime_sh { get; set; }   // زمان ثبت به شمسی

        // 100 : رسید ورود
        // 200 : حواله خروج
        public int Type { get; set; }  // نوع سند

        public long cost_center_Index { get; set; } // شناسه مرکز هزینه
        public string cost_center_Descripiton { get; set; } // شرح مرکز هزینه


        #region اگر سند، رسید ورود باشد
        //public long 
        #endregion

        // از دو فیلد، یکی می تواند مقدار بگیرد
        public long Contractor_Index { get; set; }  // پیمانکار مرتبط با حواله
        public string Contractor_Name { get; set; }
        public string Customer_Index { get; set; }  // مشتری مرتبط با حواله
        public string Customer_Name { get; set; }

        public string Description { get; set; }  // شرحی بر حواله در صورت نیاز

        public string C_S1 { get; set; }
        public string C_S2 { get; set; }
        public string C_S3 { get; set; }
        public long C_L1 { get; set; }
        public long C_L2 { get; set; }
        public int C_I1 { get; set; }
        public int C_I2 { get; set; }
        public bool C_B1 { get; set; }
        public bool C_B2 { get; set; }
        public bool C_B3 { get; set; }
    }

    // تاریخچه سند - در صورت نیاز به تأیید های مختلف در این جا مراحل ثبت می شوند
    public class Warehouse_Remittance_History
    {
        [PrimaryKey, AutoIncrement]
        public long Id { get; set; }
        public long Company_Index { get; set; }
        public long Index { get; set; }
        public long Warehouse_Remittance_Index { get; set; }     // شناسه مخصوص سند
        public long User_Index { get; set; }    // شناسۀ شخصی که عملی را انجام داده است
        public string User_Name { get; set; }     // نام شخصی که عملی را انجام داده است
        public long User_Level_Index { get; set; }    // شناسۀ شخصی که عملی را انجام داده است
        public string Description { get; set; }
        public DateTime DateTime_mi { get; set; }   // زمان ثبت به میلادی
        public string DateTime_sh { get; set; }   // زمان ثبت به شمسی
    }

    // ردیف های موجود در حواله یا رسید انبار
    public class Warehouse_Remittance_Row
    {
        [PrimaryKey, AutoIncrement]
        public long Id { get; set; }
        public long Company_Index { get; set; }
        public long Index { get; set; }
        public long Warehouse_Remittance_Index { get; set; }
        public string Item_Index { get; set; }
        public string Item_SmallCode { get; set; }    // کد کوچک کالا
        public string Item_Name { get; set; }
        public double Quantity { get; set; }   // تعداد کالا در ردیف
        public string Item_Unit { get; set; }   // واحد شمارش کالا
        public string Description { get; set; }  // شرحی بر حواله در صورت نیاز

        #region تأیید کنترل کیفی و مدیریت
        public bool Need_QC_Confirmation { get; set; } // آیا برای تأیید این ردیف از سند نیاز به تأیید کنترل کیفی می باشد؟
        public byte QC_Confirmation_Type { get; set; }  // منفی یک : عدم تأیید - یک : تأیید کامل - دو : تأیید تعدادی از کالاها
        public double Quantity_QC_Confirmed { get; set; }   // تعداد کالاهایی که توسط کنترل کیفی تأیید شده اند
        public DateTime QC_DateTime_mi { get; set; }    // تاریخ تأیید (یا عدم تأیید) کنترل کیفی
        public string QC_DateTime_sh { get; set; }

        public bool Need_Manager_Confirmation { get; set; } // آیا برای تأیید این ردیف از سند نیاز به تأیید مدیر می باشد؟
        public byte Manager_Confirmation_Type { get; set; }  // منفی یک : عدم تأیید - یک : تأیید کامل - دو : تأیید تعدادی از کالاها
        public DateTime Manager_DateTime_mi { get; set; }   // تاریخ تأیید (یا عدم تأیید) مدیریت
        public string Manager_DateTime_sh { get; set; }
        #endregion

        public double Quantity_Confirmed { get; set; }   // تعداد تأیید شده نهایی

        public bool C_B1 { get; set; }
        public bool C_B2 { get; set; }
        public bool C_B3 { get; set; }
    }

    // مراکز هزینه
    public class CostCenter
    {
        [PrimaryKey, AutoIncrement]
        public long Id { get; set; }
        public long Company_Index { get; set; }
        public long Index { get; set; }
        public string Description { get; set; }
    }

    // درخواست خرید
    public class PurchaseRequest
    {
        [PrimaryKey, AutoIncrement]
        public long Id { get; set; }
        public long Company_Index { get; set; }
        public long Index { get; set; }
        public string Description { get; set; }
    }





    //
}
