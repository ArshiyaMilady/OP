using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrdersProgress.Models
{
    class dbOperations
    {
        readonly SQLiteAsyncConnection _db;

        public dbOperations(string dbPath = null)
        {
            if (string.IsNullOrEmpty(dbPath))
                dbPath = Application.StartupPath + @"\System.SQLite.DB.db3";

            _db = new SQLiteAsyncConnection(dbPath);
            _db.CreateTableAsync<Company>().Wait();
            _db.CreateTableAsync<User>().Wait();
            _db.CreateTableAsync<LoginHistory>().Wait();
            _db.CreateTableAsync<User_Level>().Wait();
            _db.CreateTableAsync<UL_See_UL>().Wait();
            _db.CreateTableAsync<User_UL>().Wait();
            _db.CreateTableAsync<UL_Feature>().Wait();
            _db.CreateTableAsync<User_Level_UL_Feature>().Wait();
            _db.CreateTableAsync<UL_See_OL>().Wait();
            _db.CreateTableAsync<UL_Confirm_UL_Request>().Wait();
            _db.CreateTableAsync<UL_Request_Category>().Wait();
            _db.CreateTableAsync<User_File>().Wait();
            _db.CreateTableAsync<Order>().Wait();
            _db.CreateTableAsync<OrdersCollection>().Wait();
            _db.CreateTableAsync<Collection>().Wait();
            _db.CreateTableAsync<Collection_Item>().Wait();
            _db.CreateTableAsync<Collection_Action>().Wait();
            _db.CreateTableAsync<Collection_Action_History>().Wait();
            _db.CreateTableAsync<Order_Attachment>().Wait();
            _db.CreateTableAsync<File>().Wait();
            _db.CreateTableAsync<Customer>().Wait();
            _db.CreateTableAsync<Order_Customer>().Wait();
            _db.CreateTableAsync<Order_Item>().Wait();
            _db.CreateTableAsync<Order_StockItem>().Wait();
            _db.CreateTableAsync<Order_Item_Property>().Wait();
            _db.CreateTableAsync<Order_Level>().Wait();
            _db.CreateTableAsync<Order_OL>().Wait();
            _db.CreateTableAsync<Order_Level_on_Returning>().Wait();
            _db.CreateTableAsync<OL_Prerequisite>().Wait();
            _db.CreateTableAsync<OL_UL>().Wait();
            _db.CreateTableAsync<Order_History>().Wait();
            _db.CreateTableAsync<Proforma>().Wait();
            _db.CreateTableAsync<Proforma_Row>().Wait();
            _db.CreateTableAsync<OrderPriority>().Wait();
            _db.CreateTableAsync<Property>().Wait();
            //_db.DropTableAsync<Item>().Wait();
            _db.CreateTableAsync<Category>().Wait();
            _db.CreateTableAsync<Item>().Wait();
            _db.CreateTableAsync<Item_File>().Wait();
            _db.CreateTableAsync<Item_Property>().Wait();
            _db.CreateTableAsync<Module>().Wait();
            _db.CreateTableAsync<Action>().Wait();
            _db.CreateTableAsync<OPC>().Wait();
            _db.CreateTableAsync<Item_OPC>().Wait();
            _db.CreateTableAsync<OPC_Acions>().Wait();
            _db.CreateTableAsync<Unit>().Wait();
            _db.CreateTableAsync<Unit_Acion>().Wait();
            _db.CreateTableAsync<Warehouse>().Wait();
            _db.CreateTableAsync<Warehouse_Request>().Wait();
            _db.CreateTableAsync<Warehouse_Request_Row>().Wait();
            _db.CreateTableAsync<Warehouse_Request_History>().Wait();
            _db.CreateTableAsync<Warehouse_Remittance>().Wait();
            _db.CreateTableAsync<Warehouse_Remittance_History>().Wait();
            _db.CreateTableAsync<Warehouse_Remittance_Row>().Wait();
            _db.CreateTableAsync<CostCenter>().Wait();
            _db.CreateTableAsync<PurchaseRequest>().Wait();
        }


        // ********** Company *************
        #region Company
        // activeType = -1 : فقط کاربران غیرفعال
        // activeType = 0 : تمام کاربران
        // activeType = 1 : فقط کاربران فعال
        public List<Company> GetAllCompaniesAsync(int activeType = 1)
        {
            if (activeType == -1)
                return _db.Table<Company>().Where(d => !d.Active).ToListAsync().Result;
            else if (activeType == 1)
                return _db.Table<Company>().Where(d => d.Active).ToListAsync().Result;
            else
                return _db.Table<Company>().ToListAsync().Result;
        }

        public Company GetCompanyAsync(long CompanyIndex)
        {
            return _db.Table<Company>().FirstOrDefaultAsync(d => d.Index == CompanyIndex).Result;
        }

        //public Company GetCompanyAsync(string CompanyName)
        //{
        //    return _db.Table<Company>().FirstOrDefaultAsync(d => d.Name.ToLower().Equals(CompanyName.ToLower())).Result;
        //}

        public long AddCompanyAsync(Company company, long CompanyIndex = 0)
        {
            if (CompanyIndex == 0)
                company.Index = GetNewIndex_Company();
            else company.Index = CompanyIndex;

            _db.InsertAsync(company);
            return company.Id;
        }

        public long AddCompany(Company company) //,long CompanyIndex=0)
        {
            company.Index = GetNewIndex_Company();
            _db.InsertAsync(company).Wait();
            return company.Id;
        }

        public int DeleteCompanyAsync(Company company)
        {
            return _db.DeleteAsync(company).Result;
        }

        public void DeleteCompany(Company company)
        {
            _db.DeleteAsync(company).Wait();
        }

        public int DeleteAllCompaniesAsync()
        {
            return _db.DeleteAllAsync<Company>().Result;
        }

        public int UpdateCompanyAsync(Company company)
        {
            return _db.UpdateAsync(company).Result;
        }

        public long GetNewIndex_Company()
        {
            if (_db.Table<Company>().ToListAsync().Result.Any())
                return _db.Table<Company>().ToListAsync().Result.Max(d => d.Index) + 1;
            else return 1;
        }
        #endregion Company

        // ********** User *************
        #region User
        // activeType = -1 : فقط کاربران غیرفعال
        // activeType = 0 : تمام کاربران
        // activeType = 1 : فقط کاربران فعال
        public List<User> GetAllUsersAsync(long company_index, int activeType = 1)
        {
            if (activeType == -1)
                return _db.Table<User>().Where(b => b.Company_Index == company_index)
                    .Where(d => !d.Active).ToListAsync().Result;
            else if (activeType == 1)
                return _db.Table<User>().Where(b => b.Company_Index == company_index)
                    .Where(d => d.Active).ToListAsync().Result;
            else
                return _db.Table<User>().Where(b => b.Company_Index == company_index).ToListAsync().Result;
        }

        public User GetUserAsync(long UserIndex)
        {
            return _db.Table<User>().FirstOrDefaultAsync(d => d.Index == UserIndex).Result;
        }

        public User GetUserAsync(string UserName)
        {
            return _db.Table<User>().FirstOrDefaultAsync(d => d.Name.ToLower().Equals(UserName.ToLower())).Result;
        }

        public long AddUserAsync(User user, long UserIndex = 0)
        {
            if (UserIndex == 0)
                user.Index = GetNewIndex_User();
            else user.Index = UserIndex;

            _db.InsertAsync(user);
            return user.Id;
        }

        public long AddUser(User user) //,long UserIndex=0)
        {
            user.Index = GetNewIndex_User();
            _db.InsertAsync(user).Wait();
            return user.Id;
        }

        public int DeleteUserAsync(User user)
        {
            return _db.DeleteAsync(user).Result;
        }

        public void DeleteUser(User user)
        {
            _db.DeleteAsync(user).Wait();
        }

        public int DeleteAllUsersAsync()
        {
            return _db.DeleteAllAsync<User>().Result;
        }

        public int UpdateUserAsync(User user)
        {
            return _db.UpdateAsync(user).Result;
        }

        public long GetNewIndex_User()
        {
            if (_db.Table<User>().ToListAsync().Result.Any())
                return _db.Table<User>().ToListAsync().Result.Max(d => d.Index) + 1;
            else return 1;
        }

        #endregion User

        // ********** LoginHistory *************
        #region LoginHistory
        public List<LoginHistory> GetAllLoginHistorysAsync(long company_index, long user_index = -1)
        {
            List<LoginHistory> lstLH = _db.Table<LoginHistory>()
                .Where(b => b.Company_Index == company_index).ToListAsync().Result;

            if (user_index > 0)
                return lstLH.Where(d => d.User_Index == user_index).ToList();
            else
                return lstLH;
        }

        public LoginHistory GetLoginHistoryAsync(long login_history_index)
        {
            return _db.Table<LoginHistory>().FirstOrDefaultAsync(d => d.Index == login_history_index).Result;
        }

        public long AddLoginHistoryAsync(LoginHistory login_history)
        {
            login_history.Index = GetNewIndex_LoginHistory();
            _db.InsertAsync(login_history);
            return login_history.Index;
        }

        public long AddLoginHistory(LoginHistory login_history)
        {
            login_history.Index = GetNewIndex_LoginHistory();
            _db.InsertAsync(login_history).Wait();
            return login_history.Index;
        }

        public int DeleteLoginHistoryAsync(LoginHistory login_history)
        {
            return _db.DeleteAsync(login_history).Result;
        }

        public int DeleteAllLoginHistorysAsync()
        {
            return _db.DeleteAllAsync<LoginHistory>().Result;
        }

        public int UpdateLoginHistoryAsync(LoginHistory login_history)
        {
            return _db.UpdateAsync(login_history).Result;
        }

        public long GetNewIndex_LoginHistory()
        {
            if (_db.Table<LoginHistory>().ToListAsync().Result.Any())
                return _db.Table<LoginHistory>().ToListAsync().Result.Max(d => d.Index) + 1;
            else return 1;
        }
        #endregion LoginHistory

        // ********** User_Level *************
        #region User_Level
        public List<User_Level> GetAllUser_LevelsAsync(long company_index, int EnableType = 1)
        {
            if (EnableType == -1)
                return _db.Table<User_Level>().Where(b => b.Company_Index == company_index).Where(d => !d.Enabled).ToListAsync().Result;
            else if (EnableType == 1)
                return _db.Table<User_Level>().Where(b => b.Company_Index == company_index).Where(d => d.Enabled).ToListAsync().Result;
            else
                return _db.Table<User_Level>().Where(b => b.Company_Index == company_index).ToListAsync().Result;
        }

        public User_Level GetUser_LevelAsync(long user_level_index)
        {
            return _db.Table<User_Level>().FirstOrDefaultAsync(d => d.Index == user_level_index).Result;
        }

        public long AddUser_LevelAsync(User_Level user_Level)
        {
            user_Level.Index = GetNewIndex_User_Level();
            _db.InsertAsync(user_Level);
            return user_Level.Index;
        }

        public long AddUser_Level(User_Level user_Level)
        {
            user_Level.Index = GetNewIndex_User_Level();
            _db.InsertAsync(user_Level).Wait();
            return user_Level.Index;
        }

        public int DeleteUser_LevelAsync(User_Level user_Level)
        {
            return _db.DeleteAsync(user_Level).Result;
        }

        public int DeleteAllUser_LevelsAsync()
        {
            return _db.DeleteAllAsync<User_Level>().Result;
        }

        public int UpdateUser_LevelAsync(User_Level user_Level)
        {
            return _db.UpdateAsync(user_Level).Result;
        }

        public long GetNewIndex_User_Level()
        {
            if (_db.Table<User_Level>().ToListAsync().Result.Any())
                return _db.Table<User_Level>().ToListAsync().Result.Max(d => d.Index) + 1;
            else return 1;
        }
        #endregion User_Level

        // ********** UL_See_UL *************
        #region UL_See_UL
        public List<UL_See_UL> GetAllUL_See_ULsAsync(long company_index, long mainUL_index = 0)
        {
            if (mainUL_index == 0)
                return _db.Table<UL_See_UL>().Where(b => b.Company_Index == company_index).ToListAsync().Result;
            else  // یک کاربر می تواند دارای بیش از یک سطح کاربری باشد
                return _db.Table<UL_See_UL>().Where(b => b.Company_Index == company_index)
                    .Where(d => d.MainUL_Index == mainUL_index).ToListAsync().Result;
        }

        public UL_See_UL GetUL_See_ULAsync(long index)
        {
            return _db.Table<UL_See_UL>().FirstOrDefaultAsync(d => d.Index == index).Result;
        }

        public long AddUL_See_ULAsync(UL_See_UL ul_see_ul)
        {
            ul_see_ul.Index = GetNewIndex_UL_See_UL();
            _db.InsertAsync(ul_see_ul);
            return ul_see_ul.Index;
        }

        public long AddUL_See_UL(UL_See_UL ul_see_ul)
        {
            ul_see_ul.Index = GetNewIndex_UL_See_UL();
            _db.InsertAsync(ul_see_ul).Wait();
            return ul_see_ul.Index;
        }

        public int DeleteUL_See_ULAsync(UL_See_UL ul_see_ul)
        {
            return _db.DeleteAsync(ul_see_ul).Result;
        }

        public int DeleteAllUL_See_ULsAsync()
        {
            return _db.DeleteAllAsync<UL_See_UL>().Result;
        }

        public int UpdateUL_See_ULAsync(UL_See_UL ul_see_ul)
        {
            return _db.UpdateAsync(ul_see_ul).Result;
        }

        public long GetNewIndex_UL_See_UL()
        {
            if (_db.Table<UL_See_UL>().ToListAsync().Result.Any())
                return _db.Table<UL_See_UL>().ToListAsync().Result.Max(d => d.Index) + 1;
            else return 1;
        }
        #endregion UL_See_UL

        // ********** UL_Confirm_UL_Request *************
        #region UL_Confirm_UL_Request
        public List<UL_Confirm_UL_Request> GetAllUL_Confirm_UL_RequestsAsync(long company_index, long UL_index = 0, long Supervisor_UL_index = 0)
        {
            List<UL_Confirm_UL_Request> lstUCUR = _db.Table<UL_Confirm_UL_Request>().Where(b => b.Company_Index == company_index).ToListAsync().Result;
            if (UL_index > 0)
                return lstUCUR.Where(d=>d.UL_Index == UL_index).ToList();
            if (Supervisor_UL_index > 0)
                return lstUCUR.Where(d => d.Supervisor_UL_Index == Supervisor_UL_index).ToList();

            return lstUCUR;
        }

        public UL_Confirm_UL_Request GetUL_Confirm_UL_RequestAsync(long index)
        {
            return _db.Table<UL_Confirm_UL_Request>().FirstOrDefaultAsync(d => d.Index == index).Result;
        }

        public long AddUL_Confirm_UL_RequestAsync(UL_Confirm_UL_Request ul_Confirm_UL_Request)
        {
            ul_Confirm_UL_Request.Index = GetNewIndex_UL_Confirm_UL_Request();
            _db.InsertAsync(ul_Confirm_UL_Request);
            return ul_Confirm_UL_Request.Index;
        }

        public long AddUL_Confirm_UL_Request(UL_Confirm_UL_Request ul_Confirm_UL_Request)
        {
            ul_Confirm_UL_Request.Index = GetNewIndex_UL_Confirm_UL_Request();
            _db.InsertAsync(ul_Confirm_UL_Request).Wait();
            return ul_Confirm_UL_Request.Index;
        }

        public int DeleteUL_Confirm_UL_RequestAsync(UL_Confirm_UL_Request ul_Confirm_UL_Request)
        {
            return _db.DeleteAsync(ul_Confirm_UL_Request).Result;
        }

        public int DeleteAllUL_Confirm_UL_RequestsAsync()
        {
            return _db.DeleteAllAsync<UL_Confirm_UL_Request>().Result;
        }

        public int UpdateUL_Confirm_UL_RequestAsync(UL_Confirm_UL_Request ul_Confirm_UL_Request)
        {
            return _db.UpdateAsync(ul_Confirm_UL_Request).Result;
        }

        public long GetNewIndex_UL_Confirm_UL_Request()
        {
            if (_db.Table<UL_Confirm_UL_Request>().ToListAsync().Result.Any())
                return _db.Table<UL_Confirm_UL_Request>().ToListAsync().Result.Max(d => d.Index) + 1;
            else return 1;
        }
        #endregion UL_Confirm_UL_Request

        // ********** User_UL *************
        #region User_UL
        public List<User_UL> GetAllUser_ULsAsync(long company_index, long user_index = 0, long ul_index = 0)
        {
            List<User_UL> lstUser_UL = _db.Table<User_UL>().Where(b => b.Company_Index == company_index).ToListAsync().Result;
            if (user_index > 0)
                return _db.Table<User_UL>().Where(b => b.Company_Index == company_index).Where(d => d.User_Index == user_index).ToListAsync().Result;
            if (ul_index > 0)
                return _db.Table<User_UL>().Where(b => b.Company_Index == company_index).Where(d => d.UL_Index == ul_index).ToListAsync().Result;

            return lstUser_UL;
        }

        public User_UL GetUser_ULAsync(long index)
        {
            return _db.Table<User_UL>().FirstOrDefaultAsync(d => d.Index == index).Result;
        }

        public long AddUser_ULAsync(User_UL user_ul)
        {
            user_ul.Index = GetNewIndex_User_UL();
            _db.InsertAsync(user_ul);
            return user_ul.Index;
        }

        public long AddUser_UL(User_UL user_ul)
        {
            user_ul.Index = GetNewIndex_User_UL();
            _db.InsertAsync(user_ul).Wait();
            return user_ul.Index;
        }

        public int DeleteUser_ULAsync(User_UL user_ul)
        {
            return _db.DeleteAsync(user_ul).Result;
        }

        public int DeleteAllUser_ULsAsync()
        {
            return _db.DeleteAllAsync<User_UL>().Result;
        }

        public int UpdateUser_ULAsync(User_UL user_ul)
        {
            return _db.UpdateAsync(user_ul).Result;
        }

        public long GetNewIndex_User_UL()
        {
            if (_db.Table<User_UL>().ToListAsync().Result.Any())
                return _db.Table<User_UL>().ToListAsync().Result.Max(d => d.Index) + 1;
            else return 1;
        }
        #endregion User_UL

        // ********** UL_Feature *************
        #region UL_Feature
        public List<UL_Feature> GetAllUL_FeaturesAsync(long company_index, int EnableType = 1)
        {
            List<UL_Feature> lstULF = _db.Table<UL_Feature>()
                .Where(b => b.Company_Index == company_index).ToListAsync().Result;

            if (EnableType == 1)  // فقط امکانات فعال
                lstULF = lstULF.Where(d => d.Enabled).ToList();
            else if (EnableType == -1)  // فقط امکانات غیرفعال
                lstULF = lstULF.Where(d => !d.Enabled).ToList();
            // else // تمام امکانات

            return lstULF;
        }

        //public List<UL_Feature> GetAllUL_Features(long company_index)
        //{
        //    var res = _db.Table<UL_Feature>().Where(b => b.Company_Index == 0).ToListAsync();
        //    res.Wait();
        //    return res.Result;
        //}

        public UL_Feature GetUL_FeatureAsync(long index)
        {
            return _db.Table<UL_Feature>().FirstOrDefaultAsync(d => d.Index == index).Result;
        }

        public UL_Feature GetUL_FeatureAsync(string unique_phrase)
        {
            return _db.Table<UL_Feature>().FirstOrDefaultAsync(d => d.Unique_Phrase.ToLower().Equals(unique_phrase.ToLower())).Result;
        }

        public long AddUL_FeatureAsync(UL_Feature ul_Feature, long index = 0)
        {
            if (index == 0)
                ul_Feature.Index = GetNewIndex_UL_Feature();
            else ul_Feature.Index = index;
            _db.InsertAsync(ul_Feature);
            return ul_Feature.Index;
        }

        public long AddUL_Feature(UL_Feature ul_Feature, long index = 0)
        {
            if (index == 0)
                ul_Feature.Index = GetNewIndex_UL_Feature();
            else ul_Feature.Index = index;
            _db.InsertAsync(ul_Feature).Wait();
            return ul_Feature.Index;
        }

        public int DeleteUL_FeatureAsync(UL_Feature ul_Feature)
        {
            return _db.DeleteAsync(ul_Feature).Result;
        }

        public int DeleteAllUL_FeaturesAsync()
        {
            return _db.DeleteAllAsync<UL_Feature>().Result;
        }

        public int UpdateUL_FeatureAsync(UL_Feature ul_Feature)
        {
            return _db.UpdateAsync(ul_Feature).Result;
        }

        public long GetNewIndex_UL_Feature()
        {
            if (_db.Table<UL_Feature>().ToListAsync().Result.Any())
                return _db.Table<UL_Feature>().ToListAsync().Result.Max(d => d.Index) + 1;
            else return 1;
        }

        #endregion UL_Feature

        // ********** User_Level_UL_Feature *************
        #region User_Level_UL_Feature
        public List<User_Level_UL_Feature> GetAllUser_Level_UL_FeaturesAsync(long company_index
            , long user_level_index = 0,int EnableType=1)
        {
            List<User_Level_UL_Feature> lstUL_ULF = _db.Table<User_Level_UL_Feature>()
                .Where(b => b.Company_Index == company_index).ToListAsync().Result;

            if (EnableType == 1)  // فقط امکانات فعال
                lstUL_ULF = lstUL_ULF.Where(d => d.UL_Feature_Enabled).ToList();
            else if (EnableType == -1)  // فقط امکانات غیرفعال
                lstUL_ULF = lstUL_ULF.Where(d => !d.UL_Feature_Enabled).ToList();
            //else  // if(EnableType == 0) همه ی امکانات
            //    return lstUL_ULF = lstUL_ULF.Where(b => b.Company_Index == company_index).OrderBy(j => j.UL_Feature_Enabled).ToListAsync().Result;

            if (user_level_index > 0)
                lstUL_ULF = lstUL_ULF.Where(d => d.User_Level_Index == user_level_index).ToList();

            return lstUL_ULF;
        }

        public List<User_Level_UL_Feature> GetAllUser_Level_UL_FeaturesAsync(string user_level_unique_phrase)
        {
            return _db.Table<User_Level_UL_Feature>()
                .Where(d => d.UL_Feature_Unique_Phrase.ToLower()
                .Equals(user_level_unique_phrase.ToLower())).ToListAsync().Result;
        }

        public User_Level_UL_Feature GetUser_Level_UL_FeatureAsync(long id)
        {
            return _db.Table<User_Level_UL_Feature>().FirstOrDefaultAsync(d => d.Id == id).Result;
        }

        public User_Level_UL_Feature GetUser_Level_UL_Feature(string ul_feature_unique_phrase)
        {
            return _db.Table<User_Level_UL_Feature>().FirstOrDefaultAsync
                (d => d.UL_Feature_Unique_Phrase.ToLower().Equals(ul_feature_unique_phrase.ToLower())).Result;
        }

        public long AddUser_Level_UL_FeatureAsync(User_Level_UL_Feature user_Level_UL_Feature)
        {
            //user_Level_UL_Feature.Index = GetNewIndex_User_Level_UL_Feature();
            _db.InsertAsync(user_Level_UL_Feature);
            return user_Level_UL_Feature.Id;
        }

        public int DeleteUser_Level_UL_FeatureAsync(User_Level_UL_Feature user_Level_UL_Feature)
        {
            return _db.DeleteAsync(user_Level_UL_Feature).Result;
        }

        public void DeleteUser_Level_UL_Feature(User_Level_UL_Feature user_Level_UL_Feature)
        {
            _db.DeleteAsync(user_Level_UL_Feature).Wait();
        }

        public void DeleteAllUser_Level_UL_Features(List<User_Level_UL_Feature> lstUser_Level_ULFs)
        {
            foreach (User_Level_UL_Feature ul_ulf in lstUser_Level_ULFs)
                DeleteUser_Level_UL_FeatureAsync(ul_ulf);
        }

        public int DeleteAllUser_Level_UL_FeaturesAsync()
        {
            return _db.DeleteAllAsync<User_Level_UL_Feature>().Result;
        }

        public int UpdateUser_Level_UL_FeatureAsync(User_Level_UL_Feature user_Level_UL_Feature)
        {
            return _db.UpdateAsync(user_Level_UL_Feature).Result;
        }

        #endregion User_Level_UL_Feature

        // ********** UL_See_OL *************
        #region UL_See_OL
        public List<UL_See_OL> GetAllUL_See_OLsAsync(long company_index, long UL_index = 0)
        {
            if (UL_index == 0)
                return _db.Table<UL_See_OL>().Where(b => b.Company_Index == company_index).ToListAsync().Result;
            else  
                return _db.Table<UL_See_OL>().Where(b => b.Company_Index == company_index)
                    .Where(d => d.UL_Index == UL_index).ToListAsync().Result;
        }

        public UL_See_OL GetUL_See_OLAsync(long index)
        {
            return _db.Table<UL_See_OL>().FirstOrDefaultAsync(d => d.Index == index).Result;
        }

        public long AddUL_See_OLAsync(UL_See_OL ul_see_ol)
        {
            ul_see_ol.Index = GetNewIndex_UL_See_OL();
            _db.InsertAsync(ul_see_ol);
            return ul_see_ol.Index;
        }

        public long AddUL_See_OL(UL_See_OL ul_see_ol)
        {
            ul_see_ol.Index = GetNewIndex_UL_See_OL();
            _db.InsertAsync(ul_see_ol).Wait();
            return ul_see_ol.Index;
        }

        public int DeleteUL_See_OLAsync(UL_See_OL ul_see_ol)
        {
            return _db.DeleteAsync(ul_see_ol).Result;
        }

        public int DeleteAllUL_See_OLsAsync()
        {
            return _db.DeleteAllAsync<UL_See_OL>().Result;
        }

        public int UpdateUL_See_OLAsync(UL_See_OL ul_see_ol)
        {
            return _db.UpdateAsync(ul_see_ol).Result;
        }

        public long GetNewIndex_UL_See_OL()
        {
            if (_db.Table<UL_See_OL>().ToListAsync().Result.Any())
                return _db.Table<UL_See_OL>().ToListAsync().Result.Max(d => d.Index) + 1;
            else return 1;
        }
        #endregion UL_See_OL

        // ********** UL_Request_Category *************
        #region UL_Request_Category
        public List<UL_Request_Category> GetAllUL_Request_CategoriesAsync(long company_index, long user_level_index = 0)
        {
            if (user_level_index == 0)
                return _db.Table<UL_Request_Category>().Where(b => b.Company_Index == company_index).ToListAsync().Result;
            else  
                return _db.Table<UL_Request_Category>().Where(b => b.Company_Index == company_index)
                    .Where(d => d.User_Level_Index == user_level_index).ToListAsync().Result;
        }

        public UL_Request_Category GetUL_Request_CategoryAsync(long index)
        {
            return _db.Table<UL_Request_Category>().FirstOrDefaultAsync(d => d.Index == index).Result;
        }

        public long AddUL_Request_CategoryAsync(UL_Request_Category ul_request_category)
        {
            ul_request_category.Index = GetNewIndex_UL_Request_Category();
            _db.InsertAsync(ul_request_category);
            return ul_request_category.Index;
        }

        public long AddUL_Request_Category(UL_Request_Category ul_request_category)
        {
            ul_request_category.Index = GetNewIndex_UL_Request_Category();
            _db.InsertAsync(ul_request_category).Wait();
            return ul_request_category.Index;
        }

        public int DeleteUL_Request_CategoryAsync(UL_Request_Category ul_request_category)
        {
            return _db.DeleteAsync(ul_request_category).Result;
        }

        public int DeleteAllUL_Request_CategoriesAsync()
        {
            return _db.DeleteAllAsync<UL_Request_Category>().Result;
        }

        public int UpdateUL_Request_CategoryAsync(UL_Request_Category ul_request_category)
        {
            return _db.UpdateAsync(ul_request_category).Result;
        }

        public long GetNewIndex_UL_Request_Category()
        {
            if (_db.Table<UL_Request_Category>().ToListAsync().Result.Any())
                return _db.Table<UL_Request_Category>().ToListAsync().Result.Max(d => d.Index) + 1;
            else return 1;
        }
        #endregion UL_Request_Category


        // ********** User_File *************
        #region User_File
        public List<User_File> GetAllUser_FilesAsync(long company_index, long User_Index = 0)
        {
            var UserFiles = _db.Table<User_File>().Where(b => b.Company_Index == company_index).ToListAsync().Result;
            if (User_Index > 0) UserFiles = UserFiles.Where(d => d.User_Index == User_Index).ToList();
            return UserFiles;
        }

        public User_File GetUser_FileAsync(int id)
        {
            return _db.Table<User_File>().FirstAsync(d => d.Id == id).Result;
        }

        public User_File GetUser_FileAsync(long User_Index, int Type, bool Enable = true)
        {
            return _db.Table<User_File>().Where(d => d.User_Index == User_Index)
                .Where(d => d.Enable == Enable).FirstOrDefaultAsync(d => d.Type == Type).Result;
        }

        public long AddUser_FileAsync(User_File user_File)
        {
            _db.InsertAsync(user_File);
            return user_File.Id;
        }

        public int DeleteUser_FileAsync(User_File user_File)
        {
            return _db.DeleteAsync(user_File).Result;
        }

        public int DeleteAllUser_FilesAsync()
        {
            return _db.DeleteAllAsync<User_File>().Result;
        }

        public int UpdateUser_FileAsync(User_File user_File)
        {
            return _db.UpdateAsync(user_File).Result;
        }

        public long GetNewIndex_User_File()
        {
            if (_db.Table<User_File>().ToListAsync().Result.Any())
                return _db.Table<User_File>().ToListAsync().Result.Max(d => d.Index) + 1;
            else return 1;
        }
        #endregion User_File


        // ********** Order *************
        #region Order
        public List<Order> GetAllOrdersAsync(long company_index, long UserIndex = 0)
        {
            if (UserIndex <= 0)
                return _db.Table<Order>().Where(b => b.Company_Index == company_index).ToListAsync().Result;
            else
                return _db.Table<Order>().Where(b => b.Company_Index == company_index)
                    .Where(d => d.User_Index == UserIndex).ToListAsync().Result;
        }

        public List<Order> GetAllOrders(long company_index, long UserIndex = 0)
        {
            var res = _db.Table<Order>().Where(b => b.Company_Index == company_index).ToListAsync();
            res.Wait();
            if (UserIndex <= 0)
                return res.Result;
            else
                return res.Result.Where(d => d.User_Index == UserIndex).ToList();
        }

        public Order GetOrderAsync(long id)
        {
            return _db.Table<Order>().FirstAsync(d => d.Id == id).Result;
        }

        public Order GetOrderAsync(string OrderIndex)
        {
            return _db.Table<Order>()//.Where(d => d.User_Index == UserIndex)
                .FirstOrDefaultAsync(j => j.Index.ToLower().Equals(OrderIndex.ToLower())).Result;
        }

        public long AddOrderAsync(Order order)
        {
            order.Index_in_Company = GetNewOrderIndex_in_Company(order.Company_Index);
            _db.InsertAsync(order);
            return order.Id;
        }

        public long AddOrder(Order order)
        {
            order.Index_in_Company = GetNewOrderIndex_in_Company(order.Company_Index);
            _db.InsertAsync(order).Wait();
            return order.Id;
        }

        // حذف کامل سفارش و پاک کردن آن از دیتابیس
        public int DeleteOrderCompletely(Order order)
        {
            return _db.DeleteAsync(order).Result;
        }

        // حذف سفارش بدون پاک کردن آن از دیتابیس
        public void DeleteOrderAsync(Order order)
        {
            order.CurrentLevel_Index = -10001;
            UpdateOrderAsync(order);
        }

        public int DeleteAllOrdersAsync(long company_index = 0)
        {
            if (company_index > 0)
            {
                foreach (Order order in GetAllOrdersAsync(company_index))
                    DeleteOrderAsync(order);
                return 1;
            }
            else
                return _db.DeleteAllAsync<Order>().Result;
        }

        public int UpdateOrderAsync(Order order)
        {
            return _db.UpdateAsync(order).Result;
        }

        public void UpdateOrder(Order order)
        {
            _db.UpdateAsync(order).Wait();
        }

        public long GetNewOrderIndex_in_Company(long company_index)
        {
            if (_db.Table<Order>().ToListAsync().Result.Any())
                return _db.Table<Order>().Where(b=>b.Company_Index == company_index)
                    .ToListAsync().Result.Max(d => d.Index_in_Company) + 1;
            else return 1;
        }
        #endregion Order


        // ********** Order_Item *************
        #region Order_Item
        public List<Order_Item> GetAllOrder_ItemsAsync(long company_index, string Order_Index = null)
        {
            var Order_Items = _db.Table<Order_Item>().Where(b => b.Company_Index == company_index).ToListAsync().Result;
            if (!string.IsNullOrEmpty(Order_Index))
                Order_Items = Order_Items.Where(d => d.Order_Index.ToLower().Equals(Order_Index.ToLower())).ToList();
            return Order_Items;
        }

        public Order_Item GetOrder_ItemAsync(long id)
        {
            return _db.Table<Order_Item>().FirstAsync(d => d.Id == id).Result;
        }

        public Order_Item GetOrder_ItemAsync(string OrderIndex, long ItemIndex)
        {
            return _db.Table<Order_Item>()
            .Where(d => d.Order_Index.ToLower().Equals(OrderIndex.ToLower()))
            .FirstOrDefaultAsync(n => n.Item_Index == ItemIndex).Result;
        }

        public long AddOrder_ItemAsync(Order_Item order_Item)
        {
            order_Item.Index = GetNewIndex_Order_Item();
            _db.InsertAsync(order_Item);
            return order_Item.Id;
        }

        public void AddOrder_Item(Order_Item order_Item)
        {
            order_Item.Index = GetNewIndex_Order_Item();
            _db.InsertAsync(order_Item).Wait();
        }

        public int DeleteOrder_ItemAsync(Order_Item order_Item)
        {
            return _db.DeleteAsync(order_Item).Result;
        }

        public void DeleteOrder_Item(Order_Item order_Item)
        {
            _db.DeleteAsync(order_Item).Wait();
        }

        public int DeleteAllOrder_ItemsAsync()
        {
            return _db.DeleteAllAsync<Order_Item>().Result;
        }

        public int UpdateOrder_ItemAsync(Order_Item order_Item)
        {
            return _db.UpdateAsync(order_Item).Result;
        }

        public long GetNewIndex_Order_Item()
        {
            if (_db.Table<Order_Item>().ToListAsync().Result.Any())
                return _db.Table<Order_Item>().ToListAsync().Result.Max(d => d.Index) + 1;
            else return 1;
        }

        #endregion Order_Item


        // ********** Order_StockItem *************
        // آیتمهایی از سفارش که در انبار کد داشته باشند
        #region Order_StockItem
        public List<Order_StockItem> GetAllOrder_StockItemsAsync(long company_index, string Order_Index = null)
        {
            var Order_StockItems = _db.Table<Order_StockItem>().Where(b => b.Company_Index == company_index).ToListAsync().Result;
            if (!string.IsNullOrEmpty(Order_Index))
                Order_StockItems = Order_StockItems.Where(d => d.Order_Index.ToLower().Equals(Order_Index.ToLower())).ToList();
            return Order_StockItems;
        }

        public List<Order_StockItem> GetAllOrder_StockItems(string Order_Index = null)
        {
            var Order_StockItems = _db.Table<Order_StockItem>().ToListAsync();
            Order_StockItems.Wait();
            if (!string.IsNullOrEmpty(Order_Index))
                return Order_StockItems.Result.Where(d => d.Order_Index.ToLower().Equals(Order_Index.ToLower())).ToList();
            return Order_StockItems.Result;
        }

        public Order_StockItem GetOrder_StockItemAsync(long id)
        {
            return _db.Table<Order_StockItem>().FirstAsync(d => d.Id == id).Result;
        }

        public Order_StockItem GetOrder_StockItemAsync(string OrderIndex, long ItemIndex)
        {
            return _db.Table<Order_StockItem>()
            .Where(d => d.Order_Index.ToLower().Equals(OrderIndex.ToLower()))
            .FirstOrDefaultAsync(n => n.Item_Index == ItemIndex).Result;
        }

        public long AddOrder_StockItemAsync(Order_StockItem order_stock_item)
        {
            order_stock_item.Index = GetNewIndex_Order_StockItem();
            _db.InsertAsync(order_stock_item);
            return order_stock_item.Id;
        }

        public void AddOrder_StockItem(Order_StockItem order_stock_item)
        {
            order_stock_item.Index = GetNewIndex_Order_StockItem();
            _db.InsertAsync(order_stock_item).Wait();
        }

        public int DeleteOrder_StockItemAsync(Order_StockItem order_stock_item)
        {
            return _db.DeleteAsync(order_stock_item).Result;
        }

        public void DeleteOrder_StockItem(Order_StockItem order_stock_item)
        {
            _db.DeleteAsync(order_stock_item).Wait();
        }

        public int DeleteAllOrders_StockItemsAsync()
        {
            return _db.DeleteAllAsync<Order_StockItem>().Result;
        }

        public void DeleteOrder_StockItemsAsync(string OrderIndex)
        {
            if (GetAllOrder_StockItemsAsync(Stack.Company_Index, OrderIndex).Any())
            {
                foreach (Order_StockItem osi in GetAllOrder_StockItemsAsync(Stack.Company_Index, OrderIndex))
                    _db.DeleteAsync(osi);
            }
        }

        public void DeleteOrder_StockItems(long company_index, string OrderIndex)
        {
            if (GetAllOrder_StockItemsAsync(company_index, OrderIndex).Any())
            {
                foreach (Order_StockItem osi in GetAllOrder_StockItemsAsync(company_index, OrderIndex))
                    DeleteOrder_StockItem(osi);
            }
        }

        public int UpdateOrder_StockItemAsync(Order_StockItem order_stock_item)
        {
            return _db.UpdateAsync(order_stock_item).Result;
        }

        public void UpdateOrder_StockItem(Order_StockItem order_stock_item)
        {
            _db.UpdateAsync(order_stock_item).Wait();
        }

        public long GetNewIndex_Order_StockItem()
        {
            if (_db.Table<Order_StockItem>().ToListAsync().Result.Any())
                return _db.Table<Order_StockItem>().ToListAsync().Result.Max(d => d.Index) + 1;
            else return 1;
        }

        #endregion Order_StockItem


        // ********** Order_Item_Property *************
        #region Order_Item_Property
        public List<Order_Item_Property> GetAllOrder_Item_PropertiesAsync(long company_index, string Order_Index = null, string Item_SmallCode = null)
        {
            var OIPs = _db.Table<Order_Item_Property>().Where(b => b.Company_Index == company_index).ToListAsync().Result;
            if (!string.IsNullOrEmpty(Order_Index))
            {
                OIPs = OIPs.Where(d => d.Order_Index.ToLower().Equals(Order_Index.ToLower())).ToList();
                if (!string.IsNullOrEmpty(Item_SmallCode))
                    OIPs = OIPs.Where(d => d.Item_SmallCode.ToLower().Equals(Item_SmallCode.ToLower())).ToList();
            }
            return OIPs;
        }

        public List<Order_Item_Property> GetAllOrder_Item_PropertiesAsync(long company_index, string Order_Index, long ItemIndex, long PropertyIndex = 0)
        {
            var OIPs = _db.Table<Order_Item_Property>().ToListAsync().Result;
            if (!string.IsNullOrEmpty(Order_Index))
            {
                OIPs = OIPs.Where(d => d.Order_Index.ToLower().Equals(Order_Index.ToLower())).ToList();
                if (ItemIndex > 0)
                {
                    OIPs = OIPs.Where(d => d.Item_Index == ItemIndex).ToList();
                    if (PropertyIndex > 0)
                        OIPs = OIPs.Where(d => d.Property_Index == PropertyIndex).ToList();
                }
            }
            return OIPs;
        }

        public Order_Item_Property GetOrder_Item_PropertyAsync(long index)
        {
            return _db.Table<Order_Item_Property>().FirstAsync(d => d.Index == index).Result;
        }

        public Order_Item_Property GetOrder_Item_PropertyAsync(string OrderIndex, long ItemIndex, long PropertyIndex, int ItemBatchCounter)
        {
            return _db.Table<Order_Item_Property>().Where(d => d.Order_Index.Equals(OrderIndex))
                .Where(j => j.Item_Index == ItemIndex).Where(d => d.Property_Index == PropertyIndex)
                .FirstOrDefaultAsync(q => q.ItemBatch_Counter == ItemBatchCounter).Result;
        }

        public long AddOrder_Item_PropertyAsync(Order_Item_Property order_Item_Property)
        {
            order_Item_Property.Index = GetNewIndex_Order_Item_Property();
            _db.InsertAsync(order_Item_Property);
            return order_Item_Property.Id;
        }

        public void AddOrder_Item_Property(Order_Item_Property order_Item_Property)
        {
            order_Item_Property.Index = GetNewIndex_Order_Item_Property();
            _db.InsertAsync(order_Item_Property).Wait();
            //return order_Item_Property.Id;
        }

        public int DeleteOrder_Item_PropertyAsync(Order_Item_Property order_Item_Property)
        {
            return _db.DeleteAsync(order_Item_Property).Result;
        }

        public int DeleteAllOrder_Item_PropertiesAsync()
        {
            return _db.DeleteAllAsync<Order_Item_Property>().Result;
        }

        public int UpdateOrder_Item_PropertyAsync(Order_Item_Property order_Item_Property)
        {
            return _db.UpdateAsync(order_Item_Property).Result;
        }

        public long GetNewIndex_Order_Item_Property()
        {
            if (_db.Table<Order_Item_Property>().ToListAsync().Result.Any())
                return _db.Table<Order_Item_Property>().ToListAsync().Result.Max(d => d.Index) + 1;
            else return 1;
        }
        #endregion Order_Item_Property


        // ********** Order_Level *************
        #region Order_Level
        public List<Order_Level> GetAllOrder_LevelsAsync(long company_index, int EnableType = 1)
        {
            if (EnableType == 1)  // فقط مرحله های فعال
                return _db.Table<Order_Level>().Where(b => b.Company_Index == company_index).Where(d => d.Enabled).OrderBy(j => j.Sequence).ToListAsync().Result;
            else if (EnableType == -1)  // فقط مرحله های غیرفعال
                return _db.Table<Order_Level>().Where(b => b.Company_Index == company_index).Where(d => !d.Enabled).OrderBy(j => j.Sequence).ToListAsync().Result;
            else  // if(EnableType == 0) همه ی مرحله ها
                return _db.Table<Order_Level>().Where(b => b.Company_Index == company_index).OrderBy(j => j.Sequence).ToListAsync().Result;
        }

        //public List<Order_Level> GetAllOrder_Levels(long company_index, int EnableType = 1)
        //{
        //    var res = _db.Table<Order_Level>().Where(b => b.Company_Index == company_index).ToListAsync();
        //    res.Wait();

        //    if (EnableType == 1)  // فقط مرحله های فعال
        //        return res.Result.Where(d => d.Enabled).OrderBy(j => j.Sequence).ToList();
        //    else if (EnableType == -1)  // فقط مرحله های غیرفعال
        //        return res.Result.Where(d => !d.Enabled).OrderBy(j => j.Sequence).ToList();
        //    else  // if(EnableType == 0) همه ی مرحله ها
        //        return res.Result.OrderBy(j => j.Sequence).ToList();
        //}

        public Order_Level GetOrder_LevelAsync(long OrderLevel_Index)
        {
            return _db.Table<Order_Level>().FirstOrDefaultAsync(d => d.Index == OrderLevel_Index).Result;
        }

        public Order_Level GetOrder_Level_by_Sequence(long sequence)
        {
            return _db.Table<Order_Level>().FirstOrDefaultAsync(d => d.Sequence == sequence).Result;
        }

        public long AddOrder_LevelAsync(Order_Level order_Level)
        {
            order_Level.Index = GetNewIndex_Order_Level();
            _db.InsertAsync(order_Level);
            return order_Level.Id;
        }

        public long AddOrder_Level(Order_Level order_Level)
        {
            order_Level.Index = GetNewIndex_Order_Level();
            _db.InsertAsync(order_Level).Wait();
            return order_Level.Index;
        }

        public int DeleteOrder_LevelAsync(Order_Level order_Level)
        {
            return _db.DeleteAsync(order_Level).Result;
        }

        public int DeleteAllOrder_LevelsAsync()
        {
            return _db.DeleteAllAsync<Order_Level>().Result;
        }

        public int UpdateOrder_LevelAsync(Order_Level order_Level)
        {
            return _db.UpdateAsync(order_Level).Result;
        }

        public long GetNewIndex_Order_Level()
        {
            if (_db.Table<Order_Level>().ToListAsync().Result.Any())
                return _db.Table<Order_Level>().ToListAsync().Result.Max(d => d.Index) + 1;
            else return 1;
        }
        #endregion Order_Level

        // ********** Order_Level_on_Returning *************
        #region Order_Level_on_Returning
        public List<Order_Level_on_Returning> GetAllOrder_Level_on_ReturningsAsync(long company_index, long ol_index = 0)
        {
            if (ol_index==0)
                return _db.Table<Order_Level_on_Returning>().Where(b => b.Company_Index == company_index).OrderBy(d=>d.Index).ToListAsync().Result;
            else return _db.Table<Order_Level_on_Returning>().Where(b => b.Company_Index == company_index)
                    .Where(d => d.OrderLevel_Index == ol_index).OrderBy(d => d.Index).ToListAsync().Result;
        }

        public Order_Level_on_Returning GetOrder_Level_on_ReturningAsync(long id)
        {
            return _db.Table<Order_Level_on_Returning>().FirstAsync(d => d.Id == id).Result;
        }

        //public Order_Level_on_Returning GetOrder_Level_on_ReturningAsync(long Order_Level_on_ReturningIndex)
        //{
        //    return _db.Table<Order_Level_on_Returning>().FirstOrDefaultAsync(d => d.Index == Order_Level_on_ReturningIndex).Result;
        //}

        public long AddOrder_Level_on_ReturningAsync(Order_Level_on_Returning ol_on_returning)
        {
            ol_on_returning.Index = GetNewIndex_Order_Level_on_Returning();
            _db.InsertAsync(ol_on_returning);
            return ol_on_returning.Id;
        }

        public long AddOrder_Level_on_Returning(Order_Level_on_Returning ol_on_returning)
        {
            ol_on_returning.Index = GetNewIndex_Order_Level_on_Returning();
            _db.InsertAsync(ol_on_returning).Wait();
            return ol_on_returning.Index;
        }

        public int DeleteOrder_Level_on_ReturningAsync(Order_Level_on_Returning ol_on_returning)
        {
            return _db.DeleteAsync(ol_on_returning).Result;
        }

        public int DeleteAllOrder_Level_on_ReturningsAsync()
        {
            return _db.DeleteAllAsync<Order_Level_on_Returning>().Result;
        }

        public int UpdateOrder_Level_on_ReturningAsync(Order_Level_on_Returning ol_on_returning)
        {
            return _db.UpdateAsync(ol_on_returning).Result;
        }

        public long GetNewIndex_Order_Level_on_Returning()
        {
            if (_db.Table<Order_Level_on_Returning>().ToListAsync().Result.Any())
                return _db.Table<Order_Level_on_Returning>().ToListAsync().Result.Max(d => d.Index) + 1;
            else return 1;
        }
        #endregion Order_Level_on_Returning

        // ********** Order_OL *************
        #region Order_OL
        public List<Order_OL> GetAllOrder_OLsAsync(long company_index, string Order_Index = null)
        {
            if (string.IsNullOrEmpty(Order_Index))
                return _db.Table<Order_OL>().Where(b => b.Company_Index == company_index).OrderBy(d=>d.Index).ToListAsync().Result;
            else return _db.Table<Order_OL>().Where(b => b.Company_Index == company_index)
                    .Where(d => d.Order_Index.Equals(Order_Index)).OrderBy(d => d.Index).ToListAsync().Result;
        }

        public Order_OL GetOrder_OLAsync(long id)
        {
            return _db.Table<Order_OL>().FirstAsync(d => d.Id == id).Result;
        }

        //public Order_OL GetOrder_OLAsync(long Order_OLIndex)
        //{
        //    return _db.Table<Order_OL>().FirstOrDefaultAsync(d => d.Index == Order_OLIndex).Result;
        //}

        public long AddOrder_OLAsync(Order_OL order_ol)
        {
            order_ol.Index = GetNewIndex_Order_OL();
            _db.InsertAsync(order_ol);
            return order_ol.Id;
        }

        public long AddOrder_OL(Order_OL order_ol)
        {
            order_ol.Index = GetNewIndex_Order_OL();
            _db.InsertAsync(order_ol).Wait();
            return order_ol.Index;
        }

        public int DeleteOrder_OLAsync(Order_OL order_ol)
        {
            return _db.DeleteAsync(order_ol).Result;
        }

        public void DeleteOrder_OL(Order_OL order_ol)
        {
            _db.DeleteAsync(order_ol).Wait();
        }

        public int DeleteAllOrder_OLsAsync()
        {
            return _db.DeleteAllAsync<Order_OL>().Result;
        }

        public int UpdateOrder_OLAsync(Order_OL order_ol)
        {
            return _db.UpdateAsync(order_ol).Result;
        }

        public long GetNewIndex_Order_OL()
        {
            if (_db.Table<Order_OL>().ToListAsync().Result.Any())
                return _db.Table<Order_OL>().ToListAsync().Result.Max(d => d.Index) + 1;
            else return 1;
        }
        #endregion Order_OL


        // ********** OL_Prerequisite *************
        #region OL_Prerequisite
        public List<OL_Prerequisite> GetAllOL_PrerequisitesAsync(long company_index, long OrderLevel_Index = 0)
        {
            if (OrderLevel_Index <= 0)
                return _db.Table<OL_Prerequisite>().Where(b => b.Company_Index == company_index).ToListAsync().Result;
            else return _db.Table<OL_Prerequisite>().Where(b => b.Company_Index == company_index)
                    .Where(d => d.OL_Index == OrderLevel_Index).ToListAsync().Result;

        }

        public OL_Prerequisite GetOL_PrerequisiteAsync(long index)
        {
            return _db.Table<OL_Prerequisite>().FirstOrDefaultAsync(d => d.Index == index).Result;
        }

        public long AddOL_PrerequisiteAsync(OL_Prerequisite ol_prerequisit)
        {
            ol_prerequisit.Index = GetNewIndex_OL_Prerequisite();
            _db.InsertAsync(ol_prerequisit);
            return ol_prerequisit.Id;
        }

        public int DeleteOL_PrerequisiteAsync(OL_Prerequisite ol_prerequisit)
        {
            return _db.DeleteAsync(ol_prerequisit).Result;
        }

        public void DeleteOL_Prerequisite(OL_Prerequisite ol_prerequisit)
        {
            _db.DeleteAsync(ol_prerequisit).Wait();
        }

        public int DeleteAllOL_PrerequisitesAsync()
        {
            return _db.DeleteAllAsync<OL_Prerequisite>().Result;
        }

        public void DeleteAllOL_Prerequisites(long OrderLevel_Index = 0)
        {
            if (OrderLevel_Index == 0)
            {
                var res = _db.DeleteAllAsync<OL_Prerequisite>();
                res.Wait();
            }
            else
            {
                foreach (Models.OL_Prerequisite olp in GetAllOL_PrerequisitesAsync(OrderLevel_Index))
                    DeleteOL_Prerequisite(olp);
            }
        }

        public int UpdateOL_PrerequisiteAsync(OL_Prerequisite ol_prerequisit)
        {
            return _db.UpdateAsync(ol_prerequisit).Result;
        }

        public long GetNewIndex_OL_Prerequisite()
        {
            if (_db.Table<OL_Prerequisite>().ToListAsync().Result.Any())
                return _db.Table<OL_Prerequisite>().ToListAsync().Result.Max(d => d.Index) + 1;
            else return 1;
        }
        #endregion OL_Prerequisit


        // ********** OL_UL : order_level & user_level *************
        #region OL_UL
        public List<OL_UL> GetAllOL_ULsAsync(long company_index, long order_level_index = 0, long user_level_index = 0)
        {
            List<OL_UL> lstOL_UL = _db.Table<OL_UL>().Where(b => b.Company_Index == company_index).ToListAsync().Result;
            if (order_level_index > 0)
                lstOL_UL = lstOL_UL.Where(d => d.OL_Index == order_level_index).ToList();
            if (user_level_index > 0)
                lstOL_UL = lstOL_UL.Where(d => d.UL_Index == user_level_index).ToList();

            return lstOL_UL;
        }

        public OL_UL GetOL_ULAsync(long index)
        {
            return _db.Table<OL_UL>().FirstOrDefaultAsync(d => d.Index == index).Result;
        }

        public long AddOL_ULAsync(OL_UL ol_ul)
        {
            ol_ul.Index = GetNewIndex_OL_UL();
            _db.InsertAsync(ol_ul);
            return ol_ul.Id;
        }

        public int DeleteOL_ULAsync(OL_UL ol_ul)
        {
            return _db.DeleteAsync(ol_ul).Result;
        }

        public void DeleteOL_UL(OL_UL ol_ul)
        {
            _db.DeleteAsync(ol_ul).Wait();
        }

        public void DeleteAllOL_ULs(List<OL_UL> lstUser_Level_ULFs)
        {
            foreach (OL_UL ul_ulf in lstUser_Level_ULFs)
                DeleteOL_ULAsync(ul_ulf);
        }

        public int DeleteAllOL_ULsAsync()
        {
            return _db.DeleteAllAsync<OL_UL>().Result;
        }

        public int UpdateOL_ULAsync(OL_UL ol_ul)
        {
            return _db.UpdateAsync(ol_ul).Result;
        }

        public long GetNewIndex_OL_UL()
        {
            if (_db.Table<OL_UL>().ToListAsync().Result.Any())
                return _db.Table<OL_UL>().ToListAsync().Result.Max(d => d.Index) + 1;
            else return 1;
        }


        #endregion OL_UL


        // ********** Order_History *************
        #region Order_History
        public List<Order_History> GetAllOrder_HistorysAsync(long company_index, string Order_Index = null)
        {
            if (string.IsNullOrEmpty(Order_Index))
                return _db.Table<Order_History>().Where(b => b.Company_Index == company_index).ToListAsync().Result;
            else return _db.Table<Order_History>().Where(b => b.Company_Index == company_index)
                    .Where(d => d.Order_Index.Equals(Order_Index)).ToListAsync().Result;
        }

        public Order_History GetOrder_HistoryAsync(long id)
        {
            return _db.Table<Order_History>().FirstAsync(d => d.Id == id).Result;
        }

        //public Order_History GetOrder_HistoryAsync(long Order_HistoryIndex)
        //{
        //    return _db.Table<Order_History>().FirstOrDefaultAsync(d => d.Index == Order_HistoryIndex).Result;
        //}

        public long AddOrder_HistoryAsync(Order_History order_History)
        {
            order_History.Index = GetNewIndex_Order_History();
            _db.InsertAsync(order_History);
            return order_History.Id;
        }

        public int DeleteOrder_HistoryAsync(Order_History order_History)
        {
            return _db.DeleteAsync(order_History).Result;
        }

        public int DeleteAllOrder_HistorysAsync()
        {
            return _db.DeleteAllAsync<Order_History>().Result;
        }

        public int UpdateOrder_HistoryAsync(Order_History order_History)
        {
            return _db.UpdateAsync(order_History).Result;
        }

        public long GetNewIndex_Order_History()
        {
            if (_db.Table<Order_History>().ToListAsync().Result.Any())
                return _db.Table<Order_History>().ToListAsync().Result.Max(d => d.Index) + 1;
            else return 1;
        }
        #endregion Order_History

        // ********** Collection *************
        #region Collection
        public List<Collection> GetAllCollectionsAsync(long company_index)
        {
            return _db.Table<Collection>().Where(b => b.Company_Index == company_index).ToListAsync().Result;
        }

        public Collection GetCollectionAsync(long Index)
        {
            return _db.Table<Collection>().FirstOrDefaultAsync(d => d.Index == Index).Result;
        }

        public long AddCollectionAsync(Collection collection)
        {
            _db.InsertAsync(collection);
            return collection.Id;
        }

        public int DeleteCollectionAsync(Collection collection)
        {
            return _db.DeleteAsync(collection).Result;
        }

        public int DeleteAllCollectionsAsync()
        {
            return _db.DeleteAllAsync<Collection>().Result;
        }

        public int UpdateCollectionAsync(Collection collection)
        {
            return _db.UpdateAsync(collection).Result;
        }

        public long GetNewIndex_Collection()
        {
            if (_db.Table<Collection>().ToListAsync().Result.Any())
                return _db.Table<Collection>().ToListAsync().Result.Max(d => d.Index) + 1;
            else return 1;
        }
        #endregion Collection


        // ********** OrdersCollection *************
        #region OrdersCollection
        public List<OrdersCollection> GetAllOrdersCollectionsAsync(long company_index)
        {
            return _db.Table<OrdersCollection>().Where(b => b.Company_Index == company_index).ToListAsync().Result;
        }

        public OrdersCollection GetOrdersCollectionAsync(long Index)
        {
            return _db.Table<OrdersCollection>().FirstOrDefaultAsync(d => d.Collection_Index == Index).Result;
        }

        public long AddOrdersCollectionAsync(OrdersCollection ordersCollection)
        {
            _db.InsertAsync(ordersCollection);
            return ordersCollection.Id;
        }

        public int DeleteOrdersCollectionAsync(OrdersCollection ordersCollection)
        {
            return _db.DeleteAsync(ordersCollection).Result;
        }

        public int DeleteAllOrdersCollectionsAsync()
        {
            return _db.DeleteAllAsync<OrdersCollection>().Result;
        }

        public int UpdateOrdersCollectionAsync(OrdersCollection ordersCollection)
        {
            return _db.UpdateAsync(ordersCollection).Result;
        }

        public long GetNewIndex_OrdersCollection()
        {
            if (_db.Table<OrdersCollection>().ToListAsync().Result.Any())
                return _db.Table<OrdersCollection>().ToListAsync().Result.Max(d => d.Index) + 1;
            else return 1;
        }
        #endregion OrdersCollection


        // ********** Collection_Item *************
        #region Collection_Item
        public List<Collection_Item> GetAllCollection_ItemsAsync(long company_index)
        {
            return _db.Table<Collection_Item>().Where(b => b.Company_Index == company_index).ToListAsync().Result;
        }

        public Collection_Item GetCollection_ItemAsync(long Collection_ItemIndex)
        {
            return _db.Table<Collection_Item>().FirstOrDefaultAsync(d => d.Collection_Index == Collection_ItemIndex).Result;
        }

        public long AddCollection_ItemAsync(Collection_Item ordersCollection_Item)
        {
            _db.InsertAsync(ordersCollection_Item);
            return ordersCollection_Item.Id;
        }

        public int DeleteCollection_ItemAsync(Collection_Item ordersCollection_Item)
        {
            return _db.DeleteAsync(ordersCollection_Item).Result;
        }

        public int DeleteAllCollection_ItemsAsync()
        {
            return _db.DeleteAllAsync<Collection_Item>().Result;
        }

        public int UpdateCollection_ItemAsync(Collection_Item ordersCollection_Item)
        {
            return _db.UpdateAsync(ordersCollection_Item).Result;
        }

        public long GetNewIndex_Collection_Item()
        {
            if (_db.Table<Collection_Item>().ToListAsync().Result.Any())
                return _db.Table<Collection_Item>().ToListAsync().Result.Max(d => d.Index) + 1;
            else return 1;
        }
        #endregion Collection_Item


        // ********** Collection_Action *************
        #region Collection_Action
        public List<Collection_Action> GetAllCollection_ActionsAsync(long company_index)
        {
            return _db.Table<Collection_Action>().Where(b => b.Company_Index == company_index).ToListAsync().Result;
        }

        public Collection_Action GetCollection_ActionAsync(long Collection_ActionIndex)
        {
            return _db.Table<Collection_Action>().FirstOrDefaultAsync(d => d.Collection_Index == Collection_ActionIndex).Result;
        }

        public long AddCollection_ActionAsync(Collection_Action ordersCollection_Action)
        {
            _db.InsertAsync(ordersCollection_Action);
            return ordersCollection_Action.Id;
        }

        public int DeleteCollection_ActionAsync(Collection_Action ordersCollection_Action)
        {
            return _db.DeleteAsync(ordersCollection_Action).Result;
        }

        public int DeleteAllCollection_ActionsAsync()
        {
            return _db.DeleteAllAsync<Collection_Action>().Result;
        }

        public int UpdateCollection_ActionAsync(Collection_Action ordersCollection_Action)
        {
            return _db.UpdateAsync(ordersCollection_Action).Result;
        }

        public long GetNewIndex_Collection_Action()
        {
            if (_db.Table<Collection_Action>().ToListAsync().Result.Any())
                return _db.Table<Collection_Action>().ToListAsync().Result.Max(d => d.Index) + 1;
            else return 1;
        }
        #endregion Collection_Action


        // ********** Collection_Action_History *************
        #region Collection_Action_History
        public List<Collection_Action_History> GetAllCollection_Action_HistorysAsync(long company_index)
        {
            return _db.Table<Collection_Action_History>().Where(b => b.Company_Index == company_index).ToListAsync().Result;
        }

        public Collection_Action_History GetCollection_Action_HistoryAsync(long ActionIndex)
        {
            return _db.Table<Collection_Action_History>().FirstOrDefaultAsync(d => d.Action_Index == ActionIndex).Result;
        }

        public long AddCollection_Action_HistoryAsync(Collection_Action_History ordersCollection_Action_History)
        {
            _db.InsertAsync(ordersCollection_Action_History);
            return ordersCollection_Action_History.Id;
        }

        public int DeleteCollection_Action_HistoryAsync(Collection_Action_History ordersCollection_Action_History)
        {
            return _db.DeleteAsync(ordersCollection_Action_History).Result;
        }

        public int DeleteAllCollection_Action_HistorysAsync()
        {
            return _db.DeleteAllAsync<Collection_Action_History>().Result;
        }

        public int UpdateCollection_Action_HistoryAsync(Collection_Action_History ordersCollection_Action_History)
        {
            return _db.UpdateAsync(ordersCollection_Action_History).Result;
        }

        public long GetNewIndex_Collection_Action_History()
        {
            if (_db.Table<Collection_Action_History>().ToListAsync().Result.Any())
                return _db.Table<Collection_Action_History>().ToListAsync().Result.Max(d => d.Index) + 1;
            else return 1;
        }
        #endregion Collection_Action_History


        // ********** Order_Attachment *************
        #region Order_Attachment
        public List<Order_Attachment> GetAllOrder_AttachmentsAsync(long company_index, string Order_Index = null)
        {
            var UserFiles = _db.Table<Order_Attachment>().Where(b => b.Company_Index == company_index).ToListAsync().Result;
            if (!string.IsNullOrEmpty(Order_Index))
                UserFiles = UserFiles.Where(d => d.Order_Index.ToLower().Equals(Order_Index.ToLower())).ToList();
            return UserFiles;
        }

        public Order_Attachment GetOrder_AttachmentAsync(int id)
        {
            return _db.Table<Order_Attachment>().FirstAsync(d => d.Id == id).Result;
        }

        public Order_Attachment GetOrder_AttachmentAsync(long id, string Order_Index = null, int Type = -1, bool Enable = true)
        {
            if (id == 0)
            {
                return _db.Table<Order_Attachment>()
                .Where(d => d.Order_Index.ToLower().Equals(Order_Index.ToLower()))
                .Where(j => j.Type == Type).FirstOrDefaultAsync(n => n.Enable == Enable).Result;
            }
            else
                return _db.Table<Order_Attachment>().FirstOrDefaultAsync(d => d.Id == id).Result;
        }

        public long AddOrder_AttachmentAsync(Order_Attachment order_Attachment)
        {
            _db.InsertAsync(order_Attachment);
            return order_Attachment.Id;
        }

        public int DeleteOrder_AttachmentAsync(Order_Attachment order_Attachment)
        {
            return _db.DeleteAsync(order_Attachment).Result;
        }

        public int DeleteAllOrder_AttachmentsAsync()
        {
            return _db.DeleteAllAsync<Order_Attachment>().Result;
        }

        public int UpdateOrder_AttachmentAsync(Order_Attachment order_Attachment)
        {
            return _db.UpdateAsync(order_Attachment).Result;
        }

        public long GetNewIndex_Order_Attachment()
        {
            if (_db.Table<Order_Attachment>().ToListAsync().Result.Any())
                return _db.Table<Order_Attachment>().ToListAsync().Result.Max(d => d.Index) + 1;
            else return 1;
        }
        #endregion Order_Attachment


        // ********** File *************
        #region File
        public List<File> GetAllFilesAsync(long company_index)
        {
            return _db.Table<File>().Where(b => b.Company_Index == company_index).ToListAsync().Result;
        }

        public File GetFileAsync(long index)
        {
            return _db.Table<File>().FirstAsync(d => d.Index == index).Result;
        }

        //public File GetFileAsync(long Index)
        //{
        //    return _db.Table<File>().FirstOrDefaultAsync(j => j.Index.ToLower().Equals(Index.ToLower())).Result;
        //}

        public long AddFileAsync(File file)
        {
            file.Index = GetNewIndex_File();
            _db.InsertAsync(file);
            return file.Index;
        }

        public int DeleteFileAsync(File file)
        {
            return _db.DeleteAsync(file).Result;
        }

        public int DeleteAllFilesAsync()
        {
            return _db.DeleteAllAsync<File>().Result;
        }

        public int UpdateFileAsync(File file)
        {
            return _db.UpdateAsync(file).Result;
        }

        public long GetNewIndex_File()
        {
            if (_db.Table<File>().ToListAsync().Result.Any())
                return _db.Table<File>().ToListAsync().Result.Max(d => d.Index) + 1;
            else return 1;
        }
        #endregion File


        // ********** Customer *************
        #region Customer
        public List<Customer> GetAllCustomersAsync(long company_index, long UserIndex = -1)
        {
            if (UserIndex <= 0)
                return _db.Table<Customer>().Where(b => b.Company_Index == company_index).ToListAsync().Result;
            else return _db.Table<Customer>().Where(b => b.Company_Index == company_index).Where(d => d.User_Index == UserIndex).ToListAsync().Result;
        }

        public Customer GetCustomerAsync(long id)
        {
            return _db.Table<Customer>().FirstAsync(d => d.Id == id).Result;
        }

        public Customer GetCustomerAsync(string CustomerIndex)
        {
            return _db.Table<Customer>().FirstOrDefaultAsync(j => j.Index.ToLower().Equals(CustomerIndex.ToLower())).Result;
        }

        public long AddCustomer(Customer customer)
        {
            _db.InsertAsync(customer).Wait();
            return customer.Id;
        }

        public int DeleteCustomerAsync(Customer customer)
        {
            return _db.DeleteAsync(customer).Result;
        }

        public int DeleteAllCustomersAsync()
        {
            return _db.DeleteAllAsync<Customer>().Result;
        }

        public int UpdateCustomerAsync(Customer customer)
        {
            return _db.UpdateAsync(customer).Result;
        }
        #endregion Customer


        // ********** Order_Customer *************
        #region Order_Customer
        public List<Order_Customer> GetAllOrder_CustomersAsync(long company_index, string CustomerIndex = null)
        {
            if (string.IsNullOrEmpty(CustomerIndex))
                return _db.Table<Order_Customer>().Where(b => b.Company_Index == company_index).ToListAsync().Result;
            else
                return _db.Table<Order_Customer>().Where(b => b.Company_Index == company_index)
                    .Where(j => j.Customer_Index.ToLower().Equals(CustomerIndex.ToLower())).ToListAsync().Result;

        }

        public Order_Customer GetOrder_CustomerAsync(long id)
        {
            return _db.Table<Order_Customer>().FirstAsync(d => d.Id == id).Result;
        }

        public Order_Customer GetOrder_CustomerAsync(string OrderIndex)
        {
            return _db.Table<Order_Customer>().FirstOrDefaultAsync(j => j.Order_Index.ToLower().Equals(OrderIndex.ToLower())).Result;
        }

        public long AddOrder_CustomerAsync(Order_Customer order_Order_Customer)
        {
            _db.InsertAsync(order_Order_Customer);
            return order_Order_Customer.Id;
        }

        public int DeleteOrder_CustomerAsync(Order_Customer order_Order_Customer)
        {
            return _db.DeleteAsync(order_Order_Customer).Result;
        }

        public int DeleteAllOrder_CustomersAsync()
        {
            return _db.DeleteAllAsync<Order_Customer>().Result;
        }

        public int UpdateOrder_CustomerAsync(Order_Customer order_Order_Customer)
        {
            return _db.UpdateAsync(order_Order_Customer).Result;
        }

        public long GetNewIndex_Order_Customer()
        {
            if (_db.Table<Order_Customer>().ToListAsync().Result.Any())
                return _db.Table<Order_Customer>().ToListAsync().Result.Max(d => d.Index) + 1;
            else return 1;
        }
        #endregion Order_Customer

        // ********** Proforma *************
        #region Proforma
        public List<Proforma> GetAllProformasAsync(long company_index)
        {
            return _db.Table<Proforma>().Where(b => b.Company_Index == company_index).ToListAsync().Result;
        }

        public Proforma GetProformaAsync(long id)
        {
            return _db.Table<Proforma>().FirstAsync(d => d.Id == id).Result;
        }

        public Proforma GetProformaAsync(string ProformaIndex)
        {
            return _db.Table<Proforma>().FirstOrDefaultAsync(d => d.Index.ToLower().Equals(ProformaIndex.ToLower())).Result;
        }

        public long AddProformaAsync(Proforma proforma)
        {
            _db.InsertAsync(proforma);
            return proforma.Id;
        }

        public int DeleteProformaAsync(Proforma proforma)
        {
            return _db.DeleteAsync(proforma).Result;
        }

        public int DeleteAllProformasAsync()
        {
            return _db.DeleteAllAsync<Proforma>().Result;
        }

        public int UpdateProformaAsync(Proforma proforma)
        {
            return _db.UpdateAsync(proforma).Result;
        }
        #endregion Proforma


        // ********** Proforma_Row *************
        #region Proforma_Row
        public List<Proforma_Row> GetAllProforma_RowsAsync(long company_index, string ProformaIndex = null)
        {
            if (string.IsNullOrEmpty(ProformaIndex))
                return _db.Table<Proforma_Row>().Where(b => b.Company_Index == company_index).ToListAsync().Result;
            else return _db.Table<Proforma_Row>().Where(b => b.Company_Index == company_index)
                    .Where(d => d.Proforma_Index.ToLower().Equals(ProformaIndex.ToLower())).ToListAsync().Result;
        }

        public Proforma_Row GetProforma_RowAsync(long id)
        {
            return _db.Table<Proforma_Row>().FirstAsync(d => d.Id == id).Result;
        }

        //public Proforma_Row GetProforma_RowAsync(string Proforma_RowIndex)
        //{
        //    return _db.Table<Proforma_Row>().FirstOrDefaultAsync(d => d.Index.ToLower().Equals(Proforma_RowIndex.ToLower())).Result;
        //}

        public long AddProforma_RowAsync(Proforma_Row proforma_Row)
        {
            _db.InsertAsync(proforma_Row);
            return proforma_Row.Id;
        }

        public int DeleteProforma_RowAsync(Proforma_Row proforma_Row)
        {
            return _db.DeleteAsync(proforma_Row).Result;
        }

        public int DeleteAllProforma_RowsAsync()
        {
            return _db.DeleteAllAsync<Proforma_Row>().Result;
        }

        public int UpdateProforma_RowAsync(Proforma_Row proforma_Row)
        {
            return _db.UpdateAsync(proforma_Row).Result;
        }

        public long GetNewIndex_Proforma_Row()
        {
            if (_db.Table<Proforma_Row>().ToListAsync().Result.Any())
                return _db.Table<Proforma_Row>().ToListAsync().Result.Max(d => d.Index) + 1;
            else return 1;
        }
        #endregion Proforma_Row


        // ********** OrderPriority *************
        #region OrderPriority
        public List<OrderPriority> GetAllOrderPrioritysAsync(long company_index)
        {
            return _db.Table<OrderPriority>().Where(b => b.Company_Index == company_index).ToListAsync().Result;
        }

        public OrderPriority GetOrderPriorityAsync(long index)
        {
            return _db.Table<OrderPriority>().FirstAsync(d => d.Index == index).Result;
        }

        public OrderPriority GetOrderPriorityAsync(string OrderIndex)
        {
            return _db.Table<OrderPriority>().FirstOrDefaultAsync(d => d.Order_Index.ToLower().Equals(OrderIndex.ToLower())).Result;
        }

        public long AddOrderPriorityAsync(OrderPriority orderPriority)
        {
            orderPriority.Index = GetNewIndex_OrderPriority();
            _db.InsertAsync(orderPriority);
            return orderPriority.Id;
        }

        public int DeleteOrderPriorityAsync(OrderPriority orderPriority)
        {
            return _db.DeleteAsync(orderPriority).Result;
        }

        public int DeleteAllOrderPrioritysAsync()
        {
            return _db.DeleteAllAsync<OrderPriority>().Result;
        }

        public int UpdateOrderPriorityAsync(OrderPriority orderPriority)
        {
            return _db.UpdateAsync(orderPriority).Result;
        }

        public void UpdateOrderPriority(OrderPriority orderPriority)
        {
            _db.UpdateAsync(orderPriority).Wait();
        }

        public void UpdateOrdersPriorities(List<OrderPriority> lstOP)
        {
            _db.UpdateAllAsync(lstOP).Wait();
        }

        public long GetNewIndex_OrderPriority()
        {
            if (_db.Table<OrderPriority>().ToListAsync().Result.Any())
                return _db.Table<OrderPriority>().ToListAsync().Result.Max(d => d.Index) + 1;
            else return 1;
        }
        #endregion OrderPriority


        // ********** Property *************
        #region Property
        public List<Property> GetAllPropertiesAsync(long company_index, int EnableType = 1)
        {
            if (EnableType == 1)  // فقط مشخصه های فعال
                return _db.Table<Property>().Where(b => b.Company_Index == company_index).Where(d => d.Enable).ToListAsync().Result;
            else if (EnableType == -1)  // فقط مشخصه های غیرفعال
                return _db.Table<Property>().Where(b => b.Company_Index == company_index).Where(d => !d.Enable).ToListAsync().Result;
            else  // if(EnableType == 0) همه ی مشخصه ها
                return _db.Table<Property>().Where(b => b.Company_Index == company_index).ToListAsync().Result;
        }

        public List<Property> GetAllProperties(long company_index, int EnableType = 1)
        {
            var res = _db.Table<Property>().Where(b => b.Company_Index == company_index).ToListAsync();
            res.Wait();

            if (EnableType == 1)  // فقط مشخصه های فعال
                return res.Result.Where(d => d.Enable).ToList();
            else if (EnableType == -1)  // فقط مشخصه های غیرفعال
                return res.Result.Where(d => !d.Enable).ToList();
            else  // if(EnableType == 0) همه ی مشخصه ها
                return res.Result.ToList();
        }

        public Property GetPropertyAsync(long index)
        {
            return _db.Table<Property>().FirstOrDefaultAsync(d => d.Index == index).Result;
        }

        public Property GetPropertyAsync(string name)
        {
            return _db.Table<Property>().FirstOrDefaultAsync(d => d.Name.ToLower().Equals(name.ToLower())).Result;
        }

        public long GetNewIndex_Property()
        {
            if (_db.Table<Property>().ToListAsync().Result.Any())
                return _db.Table<Property>().ToListAsync().Result.Max(d=>d.Id) + 1;
            else return 1;
        }

        public long AddPropertyAsync(Property property)
        {
            property.Index = GetNewIndex_Property();
            _db.InsertAsync(property).Wait();
            return property.Id;
        }

        public int DeletePropertyAsync(Property property)
        {
            return _db.DeleteAsync(property).Result;
        }

        public int DeleteAllPropertiesAsync()
        {
            return _db.DeleteAllAsync<Property>().Result;
        }

        public int UpdatePropertyAsync(Property property)
        {
            return _db.UpdateAsync(property).Result;
        }

        // for all records :  C_B1 = false
        public void Properties_Reset_Values()
        {
            //foreach (Property property in GetAllPropertiesAsync(Stack.Company_Index, 0))
            //{
            //    property.C_B1 = false;
            //    property.DefaultValue = null;
            //    UpdatePropertyAsync(property);
            //}

            List<Property> lstProperties = GetAllPropertiesAsync(Stack.Company_Index, 0);
            foreach (Property property in lstProperties)
            {
                property.C_B1 = false;
                property.ChangingValue = false;
                property.DefaultValue = null;
            }
            _db.UpdateAllAsync(lstProperties).Wait();
        }

        #endregion Property


        // ********** Category *************
        #region Category
        public List<Category> GetAllCategoriesAsync(long company_index)
        {
            return _db.Table<Category>().Where(b => b.Company_Index == company_index).ToListAsync().Result;

        }

        public Category GetCategoryAsync(long index)
        {
            return _db.Table<Category>().FirstOrDefaultAsync(d => d.Index == index).Result;
        }

        public Category GetCategoryAsync(string name, long company_index)
        {
            return _db.Table<Category>().Where(b => b.Company_Index == company_index)
                .FirstOrDefaultAsync(d => d.Name.ToLower().Equals(name.ToLower())).Result;
        }

        public long AddCategoryAsync(Category category)
        {
            category.Index = GetNewIndex_Category();
            _db.InsertAsync(category);
            return category.Index;
        }

        public long AddCategory(Category category)
        {
            category.Index = GetNewIndex_Category();
            _db.InsertAsync(category).Wait();
            return category.Index;
        }

        public int DeleteCategoryAsync(Category category)
        {
            return _db.DeleteAsync(category).Result;
        }

        public int DeleteAllCategoriesAsync()
        {
            return _db.DeleteAllAsync<Category>().Result;
        }

        public int UpdateCategoryAsync(Category category)
        {
            return _db.UpdateAsync(category).Result;
        }

        public long GetNewIndex_Category()
        {
            if (_db.Table<Category>().ToListAsync().Result.Any())
                return _db.Table<Category>().ToListAsync().Result.Max(d => d.Index) + 1;
            else return 1;
        }
        #endregion Category


        // ********** Item *************
        #region Item
        // type=0  : کالاها و ماژول ها
        // type=1  : مواد اولیه
        // type=100 : همه موارد
        public List<Item> GetAllItemsAsync(long company_index, int EnableType = 1, int type = 0)
        {
            if (type == 100)
            {
                if (EnableType == 1)  // فقط مشخصه های فعال
                    return _db.Table<Item>().Where(b => b.Company_Index == company_index).Where(d => d.Enable).ToListAsync().Result;
                else if (EnableType == -1)  // فقط مشخصه های غیرفعال
                    return _db.Table<Item>().Where(b => b.Company_Index == company_index).Where(d => !d.Enable).ToListAsync().Result;
                else  // if(EnableType == 0) همه ی مشخصه ها
                    return _db.Table<Item>().Where(b => b.Company_Index == company_index).ToListAsync().Result;
            }
            else
            {
                if (EnableType == 1)  // فقط مشخصه های فعال
                    return _db.Table<Item>().Where(b => b.Company_Index == company_index).Where(b => b.Type == type).Where(d => d.Enable).ToListAsync().Result;
                else if (EnableType == -1)  // فقط مشخصه های غیرفعال
                    return _db.Table<Item>().Where(b => b.Company_Index == company_index).Where(b => b.Type == type).Where(d => !d.Enable).ToListAsync().Result;
                else  // if(EnableType == 0) همه ی مشخصه ها
                    return _db.Table<Item>().Where(b => b.Company_Index == company_index).Where(b => b.Type == type).ToListAsync().Result;
            }
        }

        // مشخصاً کالاهای یک انبار را بر میگرداند
        public List<Item> GetAllItems_in_WarehouseAsync(long company_index, long warehouse_index, int EnableType = 1)
        {
            List<Models.Item> lstItems = _db.Table<Item>().Where(b => b.Company_Index == company_index)
                .Where(d=>d.Warehouse_Index == warehouse_index).ToListAsync().Result;

                if (EnableType == 1)  // فقط مشخصه های فعال
                    lstItems = lstItems.Where(d => d.Enable).ToList();
                else if (EnableType == -1)  // فقط مشخصه های غیرفعال
                    lstItems = lstItems.Where(d => !d.Enable).ToList();
                //else  // if(EnableType == 0) همه ی مشخصه ها
                //    lstItems = lstItems.ToListAsync().Result;

            return lstItems;
        }

        public Item GetItemAsync(long index)
        {
            return _db.Table<Item>().FirstAsync(d => d.Index == index).Result;
        }

        // Enable_is_important : با توجه به اینکه برای یک کد می توان ورژن های متواتی تعریف نمود
        //                       با درست بودن این پارامتر تنها کدهایی استفاده می شوند که فعال باشند
        public Item GetItemAsync(long company_index, string SmallCode, bool Enable_is_important = false)
        {
            if (Enable_is_important)
                return _db.Table<Item>().Where(b=>b.Company_Index == company_index).Where(d => d.Enable == true)
                    .FirstOrDefaultAsync(d => d.Code_Small.ToLower().Equals(SmallCode.ToLower())).Result;
            else
                return _db.Table<Item>().Where(b => b.Company_Index == company_index)
                    .FirstOrDefaultAsync(d => d.Code_Small.ToLower().Equals(SmallCode.ToLower())).Result;
        }

        // Enable_is_important : با توجه به اینکه برای یک کد می توان ورژن های متواتی تعریف نمود
        //                       با درست بودن این پارامتر تنها کدهایی استفاده می شوند که فعال باشند
        public Item GetItem(long company_index, string SmallCode, bool Enable_is_important = false)
        {
            var res = _db.Table<Item>().Where(b => b.Company_Index == company_index)
                .Where(d => d.Code_Small.ToLower().Equals(SmallCode.ToLower())).ToListAsync();
            res.Wait();

            if (Enable_is_important)
                return res.Result.FirstOrDefault(d => d.Enable == true);
            else
                return res.Result.FirstOrDefault();
        }

        public Item GetItem(long index, bool Enable_is_important = false)
        {
            return _db.Table<Item>().FirstOrDefaultAsync(d => d.Index == index).Result;
        }

        public long AddItemAsync(Item item, long index = 0)
        {
            if (index == 0) item.Index = GetNewIndex_Item();
            else item.Index = index;
            _db.InsertAsync(item);
            return item.Index;
        }

        public long AddItem(Item item, long index = 0)
        {
            if (index <= 0) item.Index = GetNewIndex_Item();
            else item.Index = index;
            _db.InsertAsync(item).Wait();
            return item.Index;
        }

        public int DeleteItemAsync(Item item)
        {
            return _db.DeleteAsync(item).Result;
        }

        public int DeleteAllItemsAsync()
        {
            return _db.DeleteAllAsync<Item>().Result;
        }

        public int UpdateItemAsync(Item item)
        {
            return _db.UpdateAsync(item).Result;
        }

        public void UpdateItem(Item item)
        {
            _db.UpdateAsync(item).Wait();
        }

        public void UpdateItems(List<Item> lstItem)
        {
            _db.UpdateAllAsync(lstItem).Wait();
        }

        public long GetNewIndex_Item()
        {
            if (_db.Table<Item>().ToListAsync().Result.Any())
                return _db.Table<Item>().ToListAsync().Result.Max(d=>d.Id) + 1;
            else return 1;
        }

        // for all records :  C_B1 = false   and   Quantity = 0
        public void Items_Reset_Values(long company_index)
        {
            List<Item> lstItems = GetAllItemsAsync(company_index, 0, 100).ToList();
            foreach (Item item in lstItems)
            {
                item.C_B1 = false;
                item.Quantity = 0;
            }
            _db.UpdateAllAsync(lstItems).Wait();
        }
        #endregion Item


        // ********** Item_File *************
        #region Item_File
        public List<Item_File> GetAllItem_FilesAsync(long company_index, string Item_SmallCode = null, bool JustEnabled = true)
        {
            List<Item_File> lstIF = _db.Table<Item_File>().Where(b => b.Company_Index == company_index).ToListAsync().Result;

            if (!string.IsNullOrEmpty(Item_SmallCode))
                lstIF = lstIF.Where(d => d.Item_Code_Small.Equals(Item_SmallCode)).ToList();

            if (JustEnabled) lstIF = lstIF.Where(d => d.Enable).ToList();
            return lstIF;
        }

        // --- باید یکی از مشخصه های فوق غیر صفر و دیگری غیر صفر باشد
        // Item_Index != 0 : تمام مشخصه هایی که با این کالا در ارتباط هستند
        // PropertyIndex != 0 : تمام کالاهایی که با این مشخصه در ارتباط هستند
        //public List<Item_File> GetAllItem_FilesAsync(long ItemIndex = 0, long PropertyIndex=0)
        //{
        //    if (ItemIndex > 0)
        //        return _db.Table<Item_File>().Where(d => d.Item_Index == ItemIndex).ToListAsync().Result;
        //    else if (PropertyIndex > 0)
        //        return _db.Table<Item_File>().Where(d => d.Property_Index == PropertyIndex).ToListAsync().Result;
        //    else return null;
        //}

        public Item_File GetItem_FileAsync(long index)
        {
            return _db.Table<Item_File>().FirstAsync(d => d.Index == index).Result;
        }

        public Item_File GetItem_FileAsync(string Item_SmallCode, int type = 1, bool Enable = false)
        {
            if (Enable)
                return _db.Table<Item_File>().Where(b => b.Type == type).Where(d => d.Enable)
                    .FirstOrDefaultAsync(j => j.Item_Code_Small.ToLower().Equals(Item_SmallCode.ToLower())).Result;
            else
                return _db.Table<Item_File>().Where(b => b.Type == type)
                    .FirstOrDefaultAsync(j => j.Item_Code_Small.ToLower().Equals(Item_SmallCode.ToLower())).Result;
        }

        public long AddItem_FileAsync(Item_File item_File)
        {
            item_File.Index = GetNewIndex_Item_File();
            _db.InsertAsync(item_File);
            return item_File.Index;
        }

        public int DeleteItem_FileAsync(Item_File item_File)
        {
            return _db.DeleteAsync(item_File).Result;
        }

        public void DeleteItem_File(Item_File item_File)
        {
            _db.DeleteAsync(item_File).Wait();
        }

        public int DeleteAllItem_FilesAsync()
        {
            return _db.DeleteAllAsync<Item_File>().Result;
        }

        public int UpdateItem_FileAsync(Item_File item_File)
        {
            return _db.UpdateAsync(item_File).Result;
        }

        public long GetNewIndex_Item_File()
        {
            if (_db.Table<Item_File>().ToListAsync().Result.Any())
                return _db.Table<Item_File>().ToListAsync().Result.Max(d => d.Index) + 1;
            else return 1;
        }
        #endregion Item_File

        // ********** Item_Property *************
        #region Item_Property
        public List<Item_Property> GetAllItem_PropertiesAsync(long company_index, string Item_SmallCode = null)
        {
            //return _db.Table<Item_Property>().ToListAsync().Result;

            if (string.IsNullOrEmpty(Item_SmallCode))
                return _db.Table<Item_Property>().Where(b => b.Company_Index == company_index).ToListAsync().Result;
            else return _db.Table<Item_Property>().Where(b => b.Company_Index == company_index)
                    .Where(d => d.Item_Code_Small.Equals(Item_SmallCode)).ToListAsync().Result;

        }

        // باید یکی از مشخصه های فوق غیر صفر و دیگری غیر صفر باشد
        // Item_Index != 0 : تمام مشخصه هایی که با این کالا در ارتباط هستند
        // PropertyIndex != 0 : تمام کالاهایی که با این مشخصه در ارتباط هستند
        public List<Item_Property> GetAllItem_PropertiesAsync(long company_index, long ItemIndex , long PropertyIndex = 0)
        {
            if (ItemIndex > 0)
                return _db.Table<Item_Property>().Where(b => b.Company_Index == company_index).Where(d => d.Item_Index == ItemIndex).ToListAsync().Result;
            else if (PropertyIndex > 0)
                return _db.Table<Item_Property>().Where(b => b.Company_Index == company_index).Where(d => d.Property_Index == PropertyIndex).ToListAsync().Result;
            else return null;
        }



        public Item_Property GetItem_PropertyAsync(long company_index, long id)
        {
            return _db.Table<Item_Property>().Where(b => b.Company_Index == company_index).FirstAsync(d => d.Id == id).Result;
        }

        public Item_Property GetItem_PropertyAsync(long company_index, long Item_Index, long PropertyIndex)
        {
            return _db.Table<Item_Property>().Where(b => b.Company_Index == company_index)
                .Where(d => d.Item_Index == Item_Index)
                .FirstOrDefaultAsync(j => j.Property_Index == PropertyIndex).Result;
        }

        public Item_Property GetItem_PropertyAsync(long company_index, string Item_SmallCode, long PropertyIndex)
        {
            return _db.Table<Item_Property>().Where(b => b.Company_Index == company_index)
                .Where(d => d.Item_Code_Small.ToLower().Equals(Item_SmallCode.ToLower()))
                .FirstOrDefaultAsync(j => j.Property_Index == PropertyIndex).Result;
        }

        public long AddItem_PropertyAsync(Item_Property item_Property)
        {
            item_Property.Index = GetNewIndex_Item_Property();
            _db.InsertAsync(item_Property);
            return item_Property.Id;
        }

        public int DeleteItem_PropertyAsync(Item_Property item_Property)
        {
            return _db.DeleteAsync(item_Property).Result;
        }

        public void DeleteItem_Property(Item_Property item_Property)
        {
            _db.DeleteAsync(item_Property).Wait();
        }

        public int DeleteAllItem_PropertiesAsync()
        {
            return _db.DeleteAllAsync<Item_Property>().Result;
        }

        public int UpdateItem_PropertyAsync(Item_Property item_Property)
        {
            return _db.UpdateAsync(item_Property).Result;
        }

        public long GetNewIndex_Item_Property()
        {
            if (_db.Table<Item_Property>().ToListAsync().Result.Any())
                return _db.Table<Item_Property>().ToListAsync().Result.Max(d => d.Index) + 1;
            else return 1;
        }
        #endregion Item_Property


        // ********** Item_OPC *************
        #region Item_OPC
        public List<Item_OPC> GetAllItem_OPCsAsync(long company_index, string SmallCode = null)
        {
            //if(string.IsNullOrEmpty(SmallCode))
            return _db.Table<Item_OPC>().Where(b => b.Company_Index == company_index).ToListAsync().Result;
            //else return _db.Table<Item_OPC>().Where(b => b.Company_Index == company_index).Where(d=>d.Item_Code_Small.Equals(SmallCode)).ToListAsync().Result;
        }

        public Item_OPC GetItem_OPCAsync(long opc_index)
        {
            return _db.Table<Item_OPC>().FirstAsync(d => d.OPC_Index == opc_index).Result;
        }

        public Item_OPC GetItem_OPCAsync(string SmallCode)
        {
            return _db.Table<Item_OPC>().FirstOrDefaultAsync(d => d.Item_SmallCode.ToLower().Equals(SmallCode.ToLower())).Result;
        }

        public long AddItem_OPCAsync(Item_OPC item_OPC)
        {
            item_OPC.Index = GetNewIndex_Item_OPC();
            _db.InsertAsync(item_OPC);
            return item_OPC.Id;
        }

        public int DeleteItem_OPCAsync(Item_OPC item_OPC)
        {
            return _db.DeleteAsync(item_OPC).Result;
        }

        public int DeleteAllItem_OPCsAsync()
        {
            return _db.DeleteAllAsync<Item_OPC>().Result;
        }

        public int UpdateItem_OPCAsync(Item_OPC item_OPC)
        {
            return _db.UpdateAsync(item_OPC).Result;
        }

        public long GetNewIndex_Item_OPC()
        {
            if (_db.Table<Item_OPC>().ToListAsync().Result.Any())
                return _db.Table<Item_OPC>().ToListAsync().Result.Max(d => d.Index) + 1;
            else return 1;
        }
        #endregion Item_OPC


        // رابطه یک کالا با کالاهای دیگر
        // ********** Module = Item -> Items *************
        #region Module
        public List<Module> GetAllModulesAsync(long company_index, int EnableType, string Module_Code_Small = null)
        {
            List<Module> lstResult = _db.Table<Module>().Where(b => b.Company_Index == company_index).ToListAsync().Result;

            if (EnableType == 1)  // فقط ماژول های فعال
                lstResult = _db.Table<Module>().Where(d => d.Enable).ToListAsync().Result;
            else if (EnableType == -1)  // فقط ماژول های غیرفعال
                lstResult = _db.Table<Module>().Where(d => !d.Enable).ToListAsync().Result;
            //else  // if(EnableType == 0) همه ی ماژول ها
            //    lstResult = _db.Table<Module>().ToListAsync().Result;

            // تمام رابطه ها را بر میگرداند
            if (string.IsNullOrEmpty(Module_Code_Small))
                return lstResult;
            // تمام کالاهای زیرساخت یک ماژول مشخص را بر میگرداند
            else return lstResult.Where(d => d.Module_Code_Small.ToLower()
                .Equals(Module_Code_Small.ToLower())).ToList();
        }

        public Module GetModuleAsync(long id)
        {
            return _db.Table<Module>().FirstAsync(d => d.Id == id).Result;
        }

        // EnableType = 0 : فعال یا غیر فعال
        // EnableType = 1 : فقط فعال
        // EnableType = 2 : فقط غیر فعال
        public Module GetModuleAsync(string Module_SmallCode, string Item_SmallCode, int EnableType = 1)
        {
            if (EnableType == 0)
            {
                return _db.Table<Module>().Where(d => d.Module_Code_Small.ToLower().Equals(Module_SmallCode.ToLower()))
                    .FirstOrDefaultAsync(d => d.Item_Code_Small.ToLower().Equals(Item_SmallCode.ToLower())).Result;
            }
            else
            {
                bool enable = EnableType == 1;
                return _db.Table<Module>().Where(d => d.Module_Code_Small.ToLower().Equals(Module_SmallCode.ToLower()))
                    .Where(j => j.Enable == enable).FirstOrDefaultAsync(d => d.Item_Code_Small.ToLower().Equals(Item_SmallCode.ToLower())).Result;
            }
        }

        public Module GetModuleAsync(string Module_SmallCode, long index)
        {
            return _db.Table<Module>().Where(d => d.Module_Code_Small.ToLower().Equals(Module_SmallCode.ToLower()))
                .FirstOrDefaultAsync(d => d.Item_Index == index).Result;
        }

        public long AddModuleAsync(Module module)
        {
            module.Index = GetNewIndex_Module();
            _db.InsertAsync(module);
            return module.Id;
        }

        public int DeleteModuleAsync(Module module)
        {
            return _db.DeleteAsync(module).Result;
        }

        public void DeleteModule(Module module)
        {
            _db.DeleteAsync(module).Wait();
        }

        public int DeleteAllModulesAsync()
        {
            return _db.DeleteAllAsync<Module>().Result;
        }

        public int UpdateModuleAsync(Module module)
        {
            return _db.UpdateAsync(module).Result;
        }

        public long GetNewIndex_Module()
        {
            if (_db.Table<Module>().ToListAsync().Result.Any())
                return _db.Table<Module>().ToListAsync().Result.Max(d => d.Index) + 1;
            else return 1;
        }
        #endregion Module

        // ********** Action *************
        #region Action
        public List<Action> GetAllActionsAsync(long company_index)
        {
            //if(string.IsNullOrEmpty(MainItem_Code_Small))
            return _db.Table<Action>().Where(b => b.Company_Index == company_index).ToListAsync().Result;
            //else
            //    return _db.Table<Action>().Where(d=>d.Item_Code_Small.Equals(MainItem_Code_Small)).ToListAsync().Result;

        }

        public Action GetActionAsync(long index)
        {
            return _db.Table<Action>().FirstAsync(d => d.Index == index).Result;
        }

        public Action GetActionAsync(string name)
        {
            return _db.Table<Action>().FirstOrDefaultAsync(d => d.Name.ToLower().Equals(name.ToLower())).Result;
        }

        public long GetNewIndex_Action()
        {
            if (_db.Table<Action>().ToListAsync().Result.Any())
                return _db.Table<Action>().ToListAsync().Result.Last().Index + 1;
            else return 1;
        }

        public long AddActionAsync(Action action)
        {
            _db.InsertAsync(action).Wait();
            return action.Id;
        }

        public int DeleteActionAsync(Action action)
        {
            return _db.DeleteAsync(action).Result;
        }

        public int DeleteAllActionsAsync()
        {
            return _db.DeleteAllAsync<Action>().Result;
        }

        public int UpdateActionAsync(Action action)
        {
            return _db.UpdateAsync(action).Result;
        }
        #endregion Action

        // ********** OPC *************
        #region OPC
        public List<OPC> GetAllOPCsAsync(long company_index, string MainItem_Code_Small = null)
        {
            //if (string.IsNullOrEmpty(MainItem_Code_Small))
            return _db.Table<OPC>().Where(b => b.Company_Index == company_index).ToListAsync().Result;
            //else return _db.Table<OPC>().Where(d => d.Item_Code_Small.Equals(MainItem_Code_Small)).ToListAsync().Result;

        }

        public OPC GetOPCAsync(long index)
        {
            return _db.Table<OPC>().FirstAsync(d => d.Index == index).Result;
        }

        //public OPC GetOPCAsync(string SmallCode)
        //{
        //    return _db.Table<OPC>().FirstOrDefaultAsync(d => d.Code_Small.ToLower().Equals(SmallCode.ToLower())).Result;
        //}

        public long AddOPCAsync(OPC opc)
        {
            _db.InsertAsync(opc);
            return opc.Id;
        }

        public int DeleteOPCAsync(OPC opc)
        {
            return _db.DeleteAsync(opc).Result;
        }

        public int DeleteAllOPCsAsync()
        {
            return _db.DeleteAllAsync<OPC>().Result;
        }

        public int UpdateOPCAsync(OPC opc)
        {
            return _db.UpdateAsync(opc).Result;
        }

        public long GetNewIndex_OPC()
        {
            if (_db.Table<OPC>().ToListAsync().Result.Any())
                return _db.Table<OPC>().ToListAsync().Result.Max(d => d.Index) + 1;
            else return 1;
        }
        #endregion OPC


        // ********** OPC_Acions *************
        #region OPC_Acions
        public List<OPC_Acions> GetAllOPC_AcionsAsync(long company_index, long opc_Index = 0)
        {
            if (opc_Index == 0)
                return _db.Table<OPC_Acions>().Where(b => b.Company_Index == company_index).ToListAsync().Result;
            else return _db.Table<OPC_Acions>().Where(b => b.Company_Index == company_index).Where(d => d.OPC_Index == opc_Index).ToListAsync().Result;

        }

        public OPC_Acions GetOPC_AcionAsync(long id)
        {
            return _db.Table<OPC_Acions>().FirstAsync(d => d.Id == id).Result;
        }

        //public OPC_Acion GetOPC_AcionAsync(string SmallCode)
        //{
        //    return _db.Table<OPC_Acion>().FirstOrDefaultAsync(d => d.Code_Small.ToLower().Equals(SmallCode.ToLower())).Result;
        //}

        public long AddOPC_AcionAsync(OPC_Acions opc_Acion)
        {
            opc_Acion.Index = GetNewIndex_OPC_Acions();
            _db.InsertAsync(opc_Acion);
            return opc_Acion.Id;
        }

        public int DeleteOPC_AcionAsync(OPC_Acions opc_Acion)
        {
            return _db.DeleteAsync(opc_Acion).Result;
        }

        public int DeleteAllOPC_AcionsAsync()
        {
            return _db.DeleteAllAsync<OPC_Acions>().Result;
        }

        public int UpdateOPC_AcionAsync(OPC_Acions opc_Acion)
        {
            return _db.UpdateAsync(opc_Acion).Result;
        }

        public long GetNewIndex_OPC_Acions()
        {
            if (_db.Table<OPC_Acions>().ToListAsync().Result.Any())
                return _db.Table<OPC_Acions>().ToListAsync().Result.Max(d => d.Index) + 1;
            else return 1;
        }
        #endregion OPC_Acion


        // ********** Unit *************
        #region Unit
        public List<Unit> GetAllUnitsAsync(long company_index)
        {
            //if (string.IsNullOrEmpty(MainItem_Code_Small))
            return _db.Table<Unit>().Where(b => b.Company_Index == company_index).ToListAsync().Result;
            //else return _db.Table<Unit>().Where(d => d.Item_Code_Small.Equals(MainItem_Code_Small)).ToListAsync().Result;

        }

        public Unit GetUnitAsync(long index)
        {
            return _db.Table<Unit>().FirstAsync(d => d.Index == index).Result;
        }

        //public Unit GetUnitAsync(string SmallCode)
        //{
        //    return _db.Table<Unit>().FirstOrDefaultAsync(d => d.Code_Small.ToLower().Equals(SmallCode.ToLower())).Result;
        //}

        public long AddUnitAsync(Unit unit)
        {
            _db.InsertAsync(unit);
            return unit.Id;
        }

        public int DeleteUnitAsync(Unit unit)
        {
            return _db.DeleteAsync(unit).Result;
        }

        public int DeleteAllUnitsAsync()
        {
            return _db.DeleteAllAsync<Unit>().Result;
        }

        public int UpdateUnitAsync(Unit unit)
        {
            return _db.UpdateAsync(unit).Result;
        }

        public long GetNewIndex_Unit()
        {
            if (_db.Table<Unit>().ToListAsync().Result.Any())
                return _db.Table<Unit>().ToListAsync().Result.Max(d => d.Index) + 1;
            else return 1;
        }
        #endregion Unit


        // ********** Unit_Acion *************
        #region Unit_Acion
        public List<Unit_Acion> GetAllUnit_AcionsAsync(long company_index, long Unit_Index = 0)
        {
            if (Unit_Index == 0)
                return _db.Table<Unit_Acion>().Where(b => b.Company_Index == company_index).ToListAsync().Result;
            else return _db.Table<Unit_Acion>().Where(b => b.Company_Index == company_index)
                    .Where(d => d.Unit_Index == Unit_Index).ToListAsync().Result;

        }

        public Unit_Acion GetUnit_AcionAsync(long id)
        {
            return _db.Table<Unit_Acion>().FirstAsync(d => d.Id == id).Result;
        }

        //public Unit_Acion GetUnit_AcionAsync(string SmallCode)
        //{
        //    return _db.Table<Unit_Acion>().FirstOrDefaultAsync(d => d.Code_Small.ToLower().Equals(SmallCode.ToLower())).Result;
        //}

        public long AddUnit_AcionAsync(Unit_Acion unit_Acion)
        {
            unit_Acion.Index = GetNewIndex_Unit_Acion();
            _db.InsertAsync(unit_Acion);
            return unit_Acion.Id;
        }

        public int DeleteUnit_AcionAsync(Unit_Acion unit_Acion)
        {
            return _db.DeleteAsync(unit_Acion).Result;
        }

        public int DeleteAllUnit_AcionsAsync()
        {
            return _db.DeleteAllAsync<Unit_Acion>().Result;
        }

        public int UpdateUnit_AcionAsync(Unit_Acion unit_Acion)
        {
            return _db.UpdateAsync(unit_Acion).Result;
        }

        public long GetNewIndex_Unit_Acion()
        {
            if (_db.Table<Unit_Acion>().ToListAsync().Result.Any())
                return _db.Table<Unit_Acion>().ToListAsync().Result.Max(d => d.Index) + 1;
            else return 1;
        }
        #endregion Unit_Acion


        // ********** Warehouse *************
        #region Warehouse
        public List<Warehouse> GetAllWarehousesAsync(long company_index, bool JustActive = true)
        {
            if (JustActive)
                return _db.Table<Warehouse>().Where(b => b.Company_Index == company_index).Where(d => d.Active).ToListAsync().Result;
            else
                return _db.Table<Warehouse>().Where(b => b.Company_Index == company_index).ToListAsync().Result;

        }

        public Warehouse GetWarehouseAsync(long index)
        {
            return _db.Table<Warehouse>().FirstOrDefaultAsync(d => d.Index == index).Result;
        }

        public Warehouse GetWarehouseAsync(long company_index, string name)
        {
            return _db.Table<Warehouse>().Where(b => b.Company_Index == company_index)
                .FirstOrDefaultAsync(d => d.Name.ToLower().Equals(name.ToLower())).Result;
        }

        public long AddWarehouseAsync(Warehouse warehouse)
        {
            warehouse.Index = GetNewIndex_Warehouse();
            _db.InsertAsync(warehouse);
            return warehouse.Index;
        }

        public long AddWarehouse(Warehouse warehouse)
        {
            warehouse.Index = GetNewIndex_Warehouse();
            _db.InsertAsync(warehouse).Wait();
            return warehouse.Index;
        }

        public int DeleteWarehouseAsync(Warehouse warehouse)
        {
            return _db.DeleteAsync(warehouse).Result;
        }

        public int DeleteAllWarehousesAsync()
        {
            return _db.DeleteAllAsync<Warehouse>().Result;
        }

        public int UpdateWarehouseAsync(Warehouse warehouse)
        {
            return _db.UpdateAsync(warehouse).Result;
        }

        public long GetNewIndex_Warehouse()
        {
            if (_db.Table<Warehouse>().ToListAsync().Result.Any())
                return _db.Table<Warehouse>().ToListAsync().Result.Max(d => d.Index) + 1;
            else return 1;
        }
        #endregion Warehouse

        // ********** Warehouse_Request *************
        #region Warehouse_Request
        public List<Warehouse_Request> GetAllWarehouse_RequestsAsync(long company_index)
        {
            //if (Warehouse_Index == 0)
                return _db.Table<Warehouse_Request>().Where(b => b.Company_Index == company_index).ToListAsync().Result;
            //else return _db.Table<Warehouse_Request>().Where(b => b.Company_Index == company_index).Where(d => d.Warehouse_Index == Warehouse_Index).ToListAsync().Result;
        }

        public Warehouse_Request GetWarehouse_RequestAsync(long index)
        {
            return _db.Table<Warehouse_Request>().FirstAsync(d => d.Index == index).Result;
        }

        public long AddWarehouse_RequestAsync(Warehouse_Request warehouse_Request)
        {
            warehouse_Request.Index = GetNewIndex_Warehouse_Request();
            warehouse_Request.Index_in_Company = GetNewIndex_Warehouse_Request(company_index);
            _db.InsertAsync(warehouse_Request);
            return warehouse_Request.Index;
        }

        public int DeleteWarehouse_RequestAsync(Warehouse_Request warehouse_Request)
        {
            return _db.DeleteAsync(warehouse_Request).Result;
        }

        public int DeleteAllWarehouse_RequestsAsync()
        {
            return _db.DeleteAllAsync<Warehouse_Request>().Result;
        }

        public int UpdateWarehouse_RequestAsync(Warehouse_Request warehouse_Request)
        {
            return _db.UpdateAsync(warehouse_Request).Result;
        }

        public long GetNewIndex_Warehouse_Request(long company_index=0)
        {
            if (company_index <= 0)
            {
                if (_db.Table<Warehouse_Request>().ToListAsync().Result.Any())
                    return _db.Table<Warehouse_Request>().ToListAsync().Result.Max(d => d.Index) + 1;
            }
            else
            {
                if (_db.Table<Warehouse_Request>().Where(d=>d.Company_Index == company_index).ToListAsync().Result.Any())
                    return _db.Table<Warehouse_Request>().Where(d => d.Company_Index == company_index)
                        .ToListAsync().Result.Max(d => d.Index_in_Company) + 1;
            }

            return 1;
        }

        #endregion Warehouse_Request

        // ********** Warehouse_Request_History *************
        #region Warehouse_Request_History
        public List<Warehouse_Request_History> GetAllWarehouse_Request_HistorysAsync(long company_index,long Warehouse_Request_Index=0)
        {
            if(Warehouse_Request_Index <= 0)
                return _db.Table<Warehouse_Request_History>().Where(b => b.Company_Index == company_index).ToListAsync().Result;
            else return _db.Table<Warehouse_Request_History>().Where(b => b.Company_Index == company_index)
                    .Where(b => b.Warehouse_Request_Index == Warehouse_Request_Index).ToListAsync().Result;
        }

        public Warehouse_Request_History GetWarehouse_Request_HistoryAsync(long index)
        {
            return _db.Table<Warehouse_Request_History>().FirstAsync(d => d.Index == index).Result;
        }

        //public Warehouse_Request_History GetWarehouse_Request_HistoryAsync(string SmallCode)
        //{
        //    return _db.Table<Warehouse_Request_History>().FirstOrDefaultAsync(d => d.Code_Small.ToLower().Equals(SmallCode.ToLower())).Result;
        //}

        public long AddWarehouse_Request_HistoryAsync(Warehouse_Request_History warehouse_Request_History)
        {
            warehouse_Request_History.Index = GetNewIndex_Warehouse_Request_History();
            _db.InsertAsync(warehouse_Request_History);
            return warehouse_Request_History.Id;
        }

        public int DeleteWarehouse_Request_HistoryAsync(Warehouse_Request_History warehouse_Request_History)
        {
            return _db.DeleteAsync(warehouse_Request_History).Result;
        }

        public int DeleteAllWarehouse_Request_HistorysAsync()
        {
            return _db.DeleteAllAsync<Warehouse_Request_History>().Result;
        }

        public int UpdateWarehouse_Request_HistoryAsync(Warehouse_Request_History warehouse_Request_History)
        {
            return _db.UpdateAsync(warehouse_Request_History).Result;
        }

        public long GetNewIndex_Warehouse_Request_History()
        {
            if (_db.Table<Warehouse_Request_History>().ToListAsync().Result.Any())
                return _db.Table<Warehouse_Request_History>().ToListAsync().Result.Max(d => d.Index) + 1;
            else return 1;
        }

        #endregion Warehouse_Request_History

        // ********** Warehouse_Request_Row *************
        #region Warehouse_Request_Row
        public List<Warehouse_Request_Row> GetAllWarehouse_Request_RowsAsync(long company_index, long Warehouse_Request_Index = 0)
        {
            if (Warehouse_Request_Index == 0)
                return _db.Table<Warehouse_Request_Row>().Where(b => b.Company_Index == company_index).ToListAsync().Result;
            else return _db.Table<Warehouse_Request_Row>().Where(b => b.Company_Index == company_index)
                    .Where(d => d.Warehouse_Request_Index == Warehouse_Request_Index).ToListAsync().Result;

        }

        public Warehouse_Request_Row GetWarehouse_Request_RowAsync(long index)
        {
            return _db.Table<Warehouse_Request_Row>().FirstOrDefaultAsync(d => d.Index == index).Result;
        }

        public Warehouse_Request_Row GetWarehouse_Request_RowAsync(string SmallCode, long company_index)
        {
            return _db.Table<Warehouse_Request_Row>()
                .Where(b => b.Company_Index == company_index).FirstOrDefaultAsync
                (d => d.Item_SmallCode.ToLower().Equals(SmallCode.ToLower())).Result;
        }

        public long AddWarehouse_Request_RowAsync(Warehouse_Request_Row warehouse_Request_Row)
        {
            warehouse_Request_Row.Index = GetNewIndex_Warehouse_Request_Row();
            _db.InsertAsync(warehouse_Request_Row);
            return warehouse_Request_Row.Id;
        }

        public int DeleteWarehouse_Request_RowAsync(Warehouse_Request_Row warehouse_Request_Row)
        {
            return _db.DeleteAsync(warehouse_Request_Row).Result;
        }

        public int DeleteAllWarehouse_Request_RowsAsync()
        {
            return _db.DeleteAllAsync<Warehouse_Request_Row>().Result;
        }

        public int UpdateWarehouse_Request_RowAsync(Warehouse_Request_Row warehouse_Request_Row)
        {
            return _db.UpdateAsync(warehouse_Request_Row).Result;
        }

        public long GetNewIndex_Warehouse_Request_Row()
        {
            if (_db.Table<Warehouse_Request_Row>().ToListAsync().Result.Any())
                return _db.Table<Warehouse_Request_Row>().ToListAsync().Result.Max(d => d.Index) + 1;
            else return 1;
        }
        #endregion Warehouse_Request_Row

        // ********** Warehouse_Remittance *************
        #region Warehouse_Remittance
        public List<Warehouse_Remittance> GetAllWarehouse_RemittancesAsync(long company_index, int Warehouse_Index = 0)
        {
            if (Warehouse_Index == 0)
                return _db.Table<Warehouse_Remittance>().Where(b => b.Company_Index == company_index).ToListAsync().Result;
            else return _db.Table<Warehouse_Remittance>().Where(b => b.Company_Index == company_index).Where(d => d.Warehouse_Index == Warehouse_Index).ToListAsync().Result;
        }

        public Warehouse_Remittance GetWarehouse_RemittanceAsync(long id)
        {
            return _db.Table<Warehouse_Remittance>().FirstAsync(d => d.Id == id).Result;
        }

        public long AddWarehouse_RemittanceAsync(long company_index, Warehouse_Remittance warehouse_Remittance)
        {
            warehouse_Remittance.Index = GetNewIndex_Warehouse_Remittance();
            warehouse_Remittance.Index_in_Company = GetNewIndex_Warehouse_Remittance(company_index);
            _db.InsertAsync(warehouse_Remittance);
            return warehouse_Remittance.Id;
        }

        public int DeleteWarehouse_RemittanceAsync(Warehouse_Remittance warehouse_Remittance)
        {
            return _db.DeleteAsync(warehouse_Remittance).Result;
        }

        public int DeleteAllWarehouse_RemittancesAsync()
        {
            return _db.DeleteAllAsync<Warehouse_Remittance>().Result;
        }

        public int UpdateWarehouse_RemittanceAsync(Warehouse_Remittance warehouse_Remittance)
        {
            return _db.UpdateAsync(warehouse_Remittance).Result;
        }

        public long GetNewIndex_Warehouse_Remittance(long company_index = 0)
        {
            if (company_index <= 0)
            {
                if (_db.Table<Warehouse_Remittance>().ToListAsync().Result.Any())
                    return _db.Table<Warehouse_Remittance>().ToListAsync().Result.Max(d => d.Index) + 1;
            }
            else
            {
                if (_db.Table<Warehouse_Remittance>().Where(d => d.Company_Index == company_index).ToListAsync().Result.Any())
                    return _db.Table<Warehouse_Remittance>().Where(d => d.Company_Index == company_index)
                        .ToListAsync().Result.Max(d => d.Index_in_Company) + 1;
            }

            return 1;
        }
        #endregion Warehouse_Remittance

        // ********** Warehouse_Remittance_History *************
        #region Warehouse_Remittance_History
        public List<Warehouse_Remittance_History> GetAllWarehouse_Remittance_HistorysAsync(long company_index)
        {
            return _db.Table<Warehouse_Remittance_History>().Where(b => b.Company_Index == company_index).ToListAsync().Result;
        }

        public Warehouse_Remittance_History GetWarehouse_Remittance_HistoryAsync(long id)
        {
            return _db.Table<Warehouse_Remittance_History>().FirstAsync(d => d.Id == id).Result;
        }

        //public Warehouse_Remittance_History GetWarehouse_Remittance_HistoryAsync(string SmallCode)
        //{
        //    return _db.Table<Warehouse_Remittance_History>().FirstOrDefaultAsync(d => d.Code_Small.ToLower().Equals(SmallCode.ToLower())).Result;
        //}

        public long AddWarehouse_Remittance_HistoryAsync(Warehouse_Remittance_History warehouse_Remittance_History)
        {
            warehouse_Remittance_History.Index = GetNewIndex_Warehouse_Remittance_History();
            _db.InsertAsync(warehouse_Remittance_History);
            return warehouse_Remittance_History.Id;
        }

        public int DeleteWarehouse_Remittance_HistoryAsync(Warehouse_Remittance_History warehouse_Remittance_History)
        {
            return _db.DeleteAsync(warehouse_Remittance_History).Result;
        }

        public int DeleteAllWarehouse_Remittance_HistorysAsync()
        {
            return _db.DeleteAllAsync<Warehouse_Remittance_History>().Result;
        }

        public int UpdateWarehouse_Remittance_HistoryAsync(Warehouse_Remittance_History warehouse_Remittance_History)
        {
            return _db.UpdateAsync(warehouse_Remittance_History).Result;
        }

        public long GetNewIndex_Warehouse_Remittance_History()
        {
            if (_db.Table<Warehouse_Remittance_History>().ToListAsync().Result.Any())
                return _db.Table<Warehouse_Remittance_History>().ToListAsync().Result.Max(d => d.Index) + 1;
            else return 1;
        }

        #endregion Warehouse_Remittance_History


        // ********** Warehouse_Remittance_Row *************
        #region Warehouse_Remittance_Row
        public List<Warehouse_Remittance_Row> GetAllWarehouse_Remittance_RowsAsync(long company_index, long Warehouse_Remittance_Index = 0)
        {
            if (Warehouse_Remittance_Index == 0)
                return _db.Table<Warehouse_Remittance_Row>().Where(b => b.Company_Index == company_index).ToListAsync().Result;
            else return _db.Table<Warehouse_Remittance_Row>().Where(b => b.Company_Index == company_index)
                    .Where(d => d.Warehouse_Remittance_Index == Warehouse_Remittance_Index).ToListAsync().Result;

        }

        public Warehouse_Remittance_Row GetWarehouse_Remittance_RowAsync(long index)
        {
            return _db.Table<Warehouse_Remittance_Row>().FirstOrDefaultAsync(d => d.Index == index).Result;
        }

        public Warehouse_Remittance_Row GetWarehouse_Remittance_RowAsync( string SmallCode, long company_index)
        {
            return _db.Table<Warehouse_Remittance_Row>()
                .Where(b=>b.Company_Index == company_index).FirstOrDefaultAsync
                (d => d.Item_SmallCode.ToLower().Equals(SmallCode.ToLower())).Result;
        }

        public long AddWarehouse_Remittance_RowAsync(Warehouse_Remittance_Row warehouse_Remittance_Row)
        {
            warehouse_Remittance_Row.Index = GetNewIndex_Warehouse_Remittance_Row();
            _db.InsertAsync(warehouse_Remittance_Row);
            return warehouse_Remittance_Row.Id;
        }

        public int DeleteWarehouse_Remittance_RowAsync(Warehouse_Remittance_Row warehouse_Remittance_Row)
        {
            return _db.DeleteAsync(warehouse_Remittance_Row).Result;
        }

        public int DeleteAllWarehouse_Remittance_RowsAsync()
        {
            return _db.DeleteAllAsync<Warehouse_Remittance_Row>().Result;
        }

        public int UpdateWarehouse_Remittance_RowAsync(Warehouse_Remittance_Row warehouse_Remittance_Row)
        {
            return _db.UpdateAsync(warehouse_Remittance_Row).Result;
        }

        public long GetNewIndex_Warehouse_Remittance_Row()
        {
            if (_db.Table<Warehouse_Remittance_Row>().ToListAsync().Result.Any())
                return _db.Table<Warehouse_Remittance_Row>().ToListAsync().Result.Max(d => d.Index) + 1;
            else return 1;
        }
        #endregion Warehouse_Remittance_Row

        // ********** CostCenter *************
        #region CostCenter
        public List<CostCenter> GetAllCostCentersAsync(long company_index,int type = -1)
        {
            if(type<0)
                return _db.Table<CostCenter>().Where(b => b.Company_Index == company_index).ToListAsync().Result;
            else
                return _db.Table<CostCenter>().Where(b => b.Company_Index == company_index)
                    .Where(j=>j.Type == type).ToListAsync().Result;
        }

        public CostCenter GetCostCenterAsync(long index)
        {
            return _db.Table<CostCenter>().FirstOrDefaultAsync(d => d.Index == index).Result;
        }

        public long AddCostCenterAsync(long company_index,CostCenter costCenter)
        {
            costCenter.Index = GetNewIndex_CostCenter();
            costCenter.Index_in_Company = GetNewIndex_CostCenter(company_index);
            _db.InsertAsync(costCenter);
            return costCenter.Index;
        }

        public long AddCostCenter(long company_index,CostCenter costCenter)
        {
            costCenter.Index = GetNewIndex_CostCenter();
            costCenter.Index_in_Company = GetNewIndex_CostCenter(company_index);
            _db.InsertAsync(costCenter).Wait();
            return costCenter.Index;
        }

        public int DeleteCostCenterAsync(CostCenter costCenter)
        {
            return _db.DeleteAsync(costCenter).Result;
        }

        public int DeleteAllCostCentersAsync()
        {
            return _db.DeleteAllAsync<CostCenter>().Result;
        }

        public int UpdateCostCenterAsync(CostCenter costCenter)
        {
            return _db.UpdateAsync(costCenter).Result;
        }

        public long GetNewIndex_CostCenter(long company_index = 0)
        {
            if (company_index <= 0)
            {
                if (_db.Table<CostCenter>().ToListAsync().Result.Any())
                    return _db.Table<CostCenter>().ToListAsync().Result.Max(d => d.Index) + 1;
            }
            else
            {
                if (_db.Table<CostCenter>().Where(d => d.Company_Index == company_index).ToListAsync().Result.Any())
                    return _db.Table<CostCenter>().Where(d => d.Company_Index == company_index)
                        .ToListAsync().Result.Max(d => d.Index_in_Company) + 1;
            }

            return 1;
        }
        #endregion CostCenter

        // ********** PurchaseRequest *************
        #region PurchaseRequest
        public List<PurchaseRequest> GetAllPurchaseRequestsAsync(long company_index)
        {
            return _db.Table<PurchaseRequest>().Where(b => b.Company_Index == company_index).ToListAsync().Result;
        }

        public PurchaseRequest GetPurchaseRequestAsync(long index)
        {
            return _db.Table<PurchaseRequest>().FirstOrDefaultAsync(d => d.Index == index).Result;
        }

        public long AddPurchaseRequestAsync(PurchaseRequest purchaseRequest)
        {
            purchaseRequest.Index = GetNewIndex_PurchaseRequest();
            _db.InsertAsync(purchaseRequest);
            return purchaseRequest.Id;
        }

        public int DeletePurchaseRequestAsync(PurchaseRequest purchaseRequest)
        {
            return _db.DeleteAsync(purchaseRequest).Result;
        }

        public int DeleteAllPurchaseRequestsAsync()
        {
            return _db.DeleteAllAsync<PurchaseRequest>().Result;
        }

        public int UpdatePurchaseRequestAsync(PurchaseRequest purchaseRequest)
        {
            return _db.UpdateAsync(purchaseRequest).Result;
        }

        public long GetNewIndex_PurchaseRequest()
        {
            if (_db.Table<PurchaseRequest>().ToListAsync().Result.Any())
                return _db.Table<PurchaseRequest>().ToListAsync().Result.Max(d => d.Index) + 1;
            else return 1;
        }
        #endregion PurchaseRequest





    }
}