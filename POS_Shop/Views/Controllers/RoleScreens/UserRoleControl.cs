using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Spreadsheet;
using POS_Shop.Helpers;
using POS_Shop.Interfaces;
using POS_Shop.Models;
using POS_Shop.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_Shop.Views.Controllers.RoleScreens
{
    public partial class UserRoleControl : UserControl
    {

        private int PageSize = 30;
        private int PageIndex = 1;
        private int RecordCount = 0;
        private string SearchTerm = "";


        public UserRoleControl()
        {
            InitializeComponent();
            this.Load += UserRoleControl_Load;
        }

        private async void UserRoleControl_Load(object sender, EventArgs e)
        {
            await LoadRolesForDataGridView(); // Now you can await it safely
        }


        private async Task LoadRolesForDataGridView()
        {
            using (var context = new POSDbContext())
            {
                IRoleRepository roleRepository = new RoleRepository(context);
                //var cities = await cityRepository.GetCitiesListAsync();

                var result = await roleRepository.GetRolesPagingListAsync(PageIndex, PageSize, SearchTerm);
                RecordCount = result.totalCount;
                DataTable dt = new DataTable();
                dt.Columns.Add("ID", typeof(int));
                dt.Columns.Add("Name", typeof(string));

                foreach (var country in result.data)
                {
                    dt.Rows.Add(country.Id, country.Name);
                }

                //CountryDatagridView.AutoGenerateColumns = true;
                RoleDatagridView.ReadOnly = true;
                RoleDatagridView.AllowUserToAddRows = false;

                RoleDatagridView.DataSource = dt;

                UpdatePager();
            }
        }
        private void UpdatePager()
        {
            int totalPages = (int)Math.Ceiling((double)RecordCount / PageSize);
            //  lblStatus.Text = $"Page {PageIndex} of {totalPages} | Total Records: {RecordCount}";

            PrevBtn.Enabled = PageIndex > 1;
            NextBtn.Enabled = PageIndex < totalPages;
        }

        private async void SaveRoleBtn_Click(object sender, EventArgs e)
        {
            try
            {

                var model = new Role()
                {
                    RoleName = RoleNameTxt.Text
                };

                if (!model.IsValid(out var results))
                {
                    var errors = string.Join("\n", results.Select(r => r.ErrorMessage));
                    MessageBox.Show($"{errors}", "Validation Errors", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (string.IsNullOrEmpty(RoleNameTxt.Text))
                {

                    MessageBox.Show("Please Enter Role Name ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using (var context = new POSDbContext())
                {
                    if (context.Roles.Any(s => s.RoleName == RoleNameTxt.Text))
                    {

                        MessageBox.Show("Role name is already exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    LoadingManager.ShowLoading();

                    context.Roles.Add(model);

                    await context.SaveChangesAsync();
                }

                ClearFormFunction();
                await LoadRolesForDataGridView();

                LoadingManager.HideLoading();
                MessageBox.Show("Role saved successfully!");
            }
            catch (Exception ex)
            {

                LoadingManager.HideLoading();
                MessageBox.Show("An error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void ClearFormFunction()
        {
            // Clear all input fields
            RoleIdTxt.Clear();
            RoleNameTxt.Clear();
        }
        private void ResetFromBtn_Click(object sender, EventArgs e)
        {
            ClearFormFunction();
        }

        private async void UpdateRoleBtn_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(RoleIdTxt.Text) || !int.TryParse(RoleIdTxt.Text, out int roleId))
            {
                MessageBox.Show("Invalid Role ID for update.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var model = new Role()
            {
                Id = roleId,
                RoleName = RoleNameTxt.Text,
            };
            if (!model.IsValid(out var results))
            {
                var errors = string.Join("\n", results.Select(r => r.ErrorMessage));
                MessageBox.Show($"{errors}", "Validation Errors", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            using (var context = new POSDbContext())
            {
                var existingRole = context.Roles.FirstOrDefault(s => s.Id == model.Id);

                if (existingRole == null)
                {
                    MessageBox.Show("Role not found for update.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                existingRole.RoleName = model.RoleName;

                context.Roles.Attach(existingRole);
                context.Entry(existingRole).State = EntityState.Modified;
                await context.SaveChangesAsync();

                MessageBox.Show("Role updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                UpdateRoleBtn.Visible = false;
                await LoadRolesForDataGridView();
                ClearFormFunction();
            }
        }

        private async void PrevBtn_Click(object sender, EventArgs e)
        {
            if (PageIndex > 1)
            {
                PageIndex--;
                await LoadRolesForDataGridView();
            }
        }

        private async void NextBtn_Click(object sender, EventArgs e)
        {
            int totalPages = (int)Math.Ceiling((double)RecordCount / PageSize);
            if (PageIndex < totalPages)
            {
                PageIndex++;
                await LoadRolesForDataGridView();
            }
        }

        private async void SearchRoleTxt_TextChange(object sender, EventArgs e)
        {
            PageIndex = 1;
            SearchTerm = SearchRoleTxt.Text.Trim();
            await LoadRolesForDataGridView();
        }

        private void RoleDatagridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = RoleDatagridView.Rows[e.RowIndex];
                RoleNameTxt.Text = row.Cells["Name"].Value.ToString();
                RoleIdTxt.Text = row.Cells["ID"].Value.ToString();
              
                UpdateRoleBtn.Enabled = true;
                UpdateRoleBtn.Visible = true;
            }
        }

    }
}
