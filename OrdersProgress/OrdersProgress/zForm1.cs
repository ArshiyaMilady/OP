using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OrdersProgress
{
    public partial class zForm1 : X210_ExampleForm_Normal
    {
        public zForm1()
        {
            InitializeComponent();
        }

        private void ZForm1_Shown(object sender, EventArgs e)
        {
            //MessageBox.Show(Stack.UserLevel_Type.ToString());
            //dgvData.DataSource = Program.dbOperations.GetAllCompaniesAsync();// Stack.Company_Index);
            //dgvData.DataSource = Program.dbOperations.GetAllUL_FeaturesAsync(Stack.Company_Index);
            //dgvData.DataSource = Program.dbOperations.GetAllUser_Level_UL_FeaturesAsync(Stack.Company_Index);
            //dgvData.DataSource = Program.dbOperations.GetAllOrder_Item_PropertiesAsync();
            //dgvData.DataSource = Program.dbOperations.GetAllOrdersAsync(Stack.Company_Index);
            //dgvData.DataSource = Program.dbOperations.GetAllUsersAsync(Stack.Company_Index);
            //dgvData.DataSource = Program.dbOperations.GetAllLoginHistorysAsync(Stack.Company_Index)
            //    .OrderByDescending(d=>d.DateTime_mi).ToList();

            //List<long> lstEnabled_OL = Program.dbOperations.GetAllOrder_LevelsAsync(Stack.Company_Index).Select(d => d.Index).ToList();
            //dgvData.DataSource = Program.dbOperations
            //    .GetAllOrder_OLsAsync(Stack.Company_Index)
            //    .Where(b => lstEnabled_OL.Contains(b.OrderLevel_Index)).ToList();

            //dgvData.DataSource = Program.dbOperations.GetAllOrder_OLsAsync(Stack.Company_Index);
            dgvData.DataSource = Program.dbOperations
                   .GetAllUser_Level_UL_FeaturesAsync(Stack.Company_Index, Stack.UserLevel_Index).ToList();
            //dgvData.DataSource = new ThisProject().AllSubRelations("KK_002");
        }


        List<Models.Item> items = new List<Models.Item>();
        // تمام قطعات یک ماژول را برمیگرداند، حتی اگر ماژول از ماژولهای دیگری ساخته شده باشد
        private void AllModule_Items(string sModule_SmallCode)
        {
            if (Program.dbOperations.GetAllModulesAsync(Stack.Company_Index,1, sModule_SmallCode).Any())
            {
                foreach (Models.Module md in Program.dbOperations.GetAllModulesAsync(Stack.Company_Index,1, sModule_SmallCode))
                {
                    Models.Item item = Program.dbOperations.GetItemAsync(md.Item_Code_Small);
                    if (!item.Module) items.Add(item);
                    else AllModule_Items(item.Code_Small);
                }
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            foreach(Models.User  user in Program.dbOperations.GetAllUsersAsync(0,0))//Stack.Company_Index))
            {
                //Program.dbOperations.DeleteUser(user);
                //user.IsDefault = false;
                //Program.dbOperations.UpdateUserAsync(user);
            }

            //Program.dbOperations.DeleteAllOrder_LevelsAsync();

            #region تعریف شرکت برای تمام داده های ذخیره شده در برنامه
            ////Program.dbOperations.DeleteAllCompaniesAsync();
            //foreach (var x in Program.dbOperations.GetAllUsersAsync(0))
            //{
            //    x.Company_Index = 1;
            //    Program.dbOperations.UpdateUserAsync(x);
            //}

            //foreach (var x in Program.dbOperations.GetAllLoginHistorysAsync(0))
            //{
            //    x.Company_Index = 1;
            //    Program.dbOperations.UpdateLoginHistoryAsync(x);
            //}

            //foreach (var x in Program.dbOperations.GetAllUser_LevelsAsync(Stack.Company_Index))
            //{
            //    x.Company_Index = 1;
            //    Program.dbOperations.UpdateUser_LevelAsync(x);
            //}

            //foreach (var x in Program.dbOperations.GetAllUL_See_ULsAsync(0))
            //{
            //    x.Company_Index = 1;
            //    Program.dbOperations.UpdateUL_See_ULAsync(x);
            //}

            //foreach (var x in Program.dbOperations.GetAllUser_ULsAsync(0))
            //{
            //    x.Company_Index = 1;
            //    Program.dbOperations.UpdateUser_ULAsync(x);
            //}

            //foreach (var x in Program.dbOperations.GetAllUL_FeaturesAsync(0))
            //{
            //    x.Company_Index = 1;
            //    Program.dbOperations.UpdateUL_FeatureAsync(x);
            //}

            //foreach (var x in Program.dbOperations.GetAllUser_Level_UL_FeaturesAsync(0))
            //{
            //    x.Company_Index = 1;
            //    Program.dbOperations.UpdateUser_Level_UL_FeatureAsync(x);
            //}

            //foreach (var x in Program.dbOperations.GetAllUser_FilesAsync(0))
            //{
            //    x.Company_Index = 1;
            //    Program.dbOperations.UpdateUser_FileAsync(x);
            //}

            //foreach (var x in Program.dbOperations.GetAllOrdersAsync(0))
            //{
            //    x.Company_Index = 1;
            //    Program.dbOperations.UpdateOrderAsync(x);
            //}

            //foreach (var x in Program.dbOperations.GetAllCollectionsAsync(0))
            //{
            //    x.Company_Index = 1;
            //    Program.dbOperations.UpdateCollectionAsync(x);
            //}

            //foreach (var x in Program.dbOperations.GetAllOrdersCollectionsAsync(0))
            //{
            //    x.Company_Index = 1;
            //    Program.dbOperations.UpdateOrdersCollectionAsync(x);
            //}

            //foreach (var x in Program.dbOperations.GetAllCollection_ItemsAsync(0))
            //{
            //    x.Company_Index = 1;
            //    Program.dbOperations.UpdateCollection_ItemAsync(x);
            //}

            //foreach (var x in Program.dbOperations.GetAllCollection_ActionsAsync(0))
            //{
            //    x.Company_Index = 1;
            //    Program.dbOperations.UpdateCollection_ActionAsync(x);
            //}

            //foreach (var x in Program.dbOperations.GetAllCollection_Action_HistorysAsync(0))
            //{
            //    x.Company_Index = 1;
            //    Program.dbOperations.UpdateCollection_Action_HistoryAsync(x);
            //}

            //foreach (var x in Program.dbOperations.GetAllOrder_AttachmentsAsync(0))
            //{
            //    x.Company_Index = 1;
            //    Program.dbOperations.UpdateOrder_AttachmentAsync(x);
            //}

            //foreach (var x in Program.dbOperations.GetAllFilesAsync(0))
            //{
            //    x.Company_Index = 1;
            //    Program.dbOperations.UpdateFileAsync(x);
            //}

            //foreach (var x in Program.dbOperations.GetAllCustomersAsync(0))
            //{
            //    x.Company_Index = 1;
            //    Program.dbOperations.UpdateCustomerAsync(x);
            //}

            //foreach (var x in Program.dbOperations.GetAllOrder_CustomersAsync(0))
            //{
            //    x.Company_Index = 1;
            //    Program.dbOperations.UpdateOrder_CustomerAsync(x);
            //}

            //foreach (var x in Program.dbOperations.GetAllOrder_ItemsAsync(0))
            //{
            //    x.Company_Index = 1;
            //    Program.dbOperations.UpdateOrder_ItemAsync(x);
            //}

            //foreach (var x in Program.dbOperations.GetAllOrder_StockItemsAsync(0))
            //{
            //    x.Company_Index = 1;
            //    Program.dbOperations.UpdateOrder_StockItemAsync(x);
            //}

            //foreach (var x in Program.dbOperations.GetAllOrder_Item_PropertiesAsync(0))
            //{
            //    x.Company_Index = 1;
            //    Program.dbOperations.UpdateOrder_Item_PropertyAsync(x);
            //}

            //foreach (var x in Program.dbOperations.GetAllOrder_LevelsAsync(0))
            //{
            //    x.Company_Index = 1;
            //    Program.dbOperations.UpdateOrder_LevelAsync(x);
            //}

            //foreach (var x in Program.dbOperations.GetAllOL_PrerequisitesAsync(0))
            //{
            //    x.Company_Index = 1;
            //    Program.dbOperations.UpdateOL_PrerequisiteAsync(x);
            //}

            //foreach (var x in Program.dbOperations.GetAllOL_ULsAsync(0))
            //{
            //    x.Company_Index = 1;
            //    Program.dbOperations.UpdateOL_ULAsync(x);
            //}

            //foreach (var x in Program.dbOperations.GetAllOrder_HistorysAsync(0))
            //{
            //    x.Company_Index = 1;
            //    Program.dbOperations.UpdateOrder_HistoryAsync(x);
            //}

            //foreach (var x in Program.dbOperations.GetAllProformasAsync(0))
            //{
            //    x.Company_Index = 1;
            //    Program.dbOperations.UpdateProformaAsync(x);
            //}

            //foreach (var x in Program.dbOperations.GetAllProforma_RowsAsync(0))
            //{
            //    x.Company_Index = 1;
            //    Program.dbOperations.UpdateProforma_RowAsync(x);
            //}

            //foreach (var x in Program.dbOperations.GetAllOrderPrioritysAsync(0))
            //{
            //    x.Company_Index = 1;
            //    Program.dbOperations.UpdateOrderPriorityAsync(x);
            //}

            //foreach (var x in Program.dbOperations.GetAllPropertiesAsync(0))
            //{
            //    x.Company_Index = 1;
            //    Program.dbOperations.UpdatePropertyAsync(x);
            //}

            //foreach (var x in Program.dbOperations.GetAllItemsAsync(0))
            //{
            //    x.Company_Index = 1;
            //    Program.dbOperations.UpdateItemAsync(x);
            //}

            //foreach (var x in Program.dbOperations.GetAllItem_FilesAsync(0))
            //{
            //    x.Company_Index = 1;
            //    Program.dbOperations.UpdateItem_FileAsync(x);
            //}

            //foreach (var x in Program.dbOperations.GetAllItem_PropertiesAsync(0, null))
            //{
            //    x.Company_Index = 1;
            //    Program.dbOperations.UpdateItem_PropertyAsync(x);
            //}

            //foreach (var x in Program.dbOperations.GetAllItem_OPCsAsync(0))
            //{
            //    x.Company_Index = 1;
            //    Program.dbOperations.UpdateItem_OPCAsync(x);
            //}
            
            //foreach (var x in Program.dbOperations.GetAllModulesAsync(0,0))
            //{
            //    x.Company_Index = 1;
            //    Program.dbOperations.UpdateModuleAsync(x);
            //}
            
            //foreach (var x in Program.dbOperations.GetAllActionsAsync(0))
            //{
            //    x.Company_Index = 1;
            //    Program.dbOperations.UpdateActionAsync(x);
            //}
            
            //foreach (var x in Program.dbOperations.GetAllOPCsAsync(0))
            //{
            //    x.Company_Index = 1;
            //    Program.dbOperations.UpdateOPCAsync(x);
            //}
            
            //foreach (var x in Program.dbOperations.GetAllOPC_AcionsAsync(0))
            //{
            //    x.Company_Index = 1;
            //    Program.dbOperations.UpdateOPC_AcionAsync(x);
            //}

            //foreach (var x in Program.dbOperations.GetAllContractorsAsync(0))
            //{
            //    x.Company_Index = 1;
            //    Program.dbOperations.UpdateContractorAsync(x);
            //}

            //foreach (var x in Program.dbOperations.GetAllContractor_AcionsAsync(0))
            //{
            //    x.Company_Index = 1;
            //    Program.dbOperations.UpdateContractor_AcionAsync(x);
            //}

            //foreach (var x in Program.dbOperations.GetAllWarehousesAsync(0))
            //{
            //    x.Company_Index = 1;
            //    Program.dbOperations.UpdateWarehouseAsync(x);
            //}

            //foreach (var x in Program.dbOperations.GetAllWarehouse_InventorysAsync(0))
            //{
            //    x.Company_Index = 1;
            //    Program.dbOperations.UpdateWarehouse_InventoryAsync(x);
            //}

            //foreach (var x in Program.dbOperations.GetAllWarehouse_RemittancesAsync(0))
            //{
            //    x.Company_Index = 1;
            //    Program.dbOperations.UpdateWarehouse_RemittanceAsync(x);
            //}

            //foreach (var x in Program.dbOperations.GetAllWarehouse_Remittance_ItemsAsync(0))
            //{
            //    x.Company_Index = 1;
            //    Program.dbOperations.UpdateWarehouse_Remittance_ItemAsync(x);
            //}
            #endregion

        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
