using Org.BouncyCastle.Asn1.Cmp;
using POS_Shop.Models.AuthModel;
using POS_Shop.Repositories.AuthRepos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POS_Shop.Views.Account.Auth
{
    public partial class UserManagementForm : Form
    {
        private UserRepository _userRepository;

        public UserManagementForm()
        {
            InitializeComponent();
            _userRepository = new UserRepository();
        }

        private void UserManagementForm_Load(object sender, EventArgs e)
        {
            InitializeDataGridView();
            LoadUsers();
            ConfigureUI();
        }

        private void ConfigureUI()
        {
            // Set form properties
            this.Icon = SystemIcons.Shield;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            // Configure tooltips
            var toolTip = new ToolTip();
            toolTip.SetToolTip(txtSearch, "Search by username, email, or full name");
            toolTip.SetToolTip(btnAddUser, "Add new user");
            toolTip.SetToolTip(btnEditUser, "Edit selected user");
            toolTip.SetToolTip(btnDeleteUser, "Delete selected user (soft delete)");
            toolTip.SetToolTip(btnRefresh, "Refresh user list");
            toolTip.SetToolTip(chkShowInactive, "Show inactive/deleted users");

            // Select first item in role filter
            cmbFilterRole.SelectedIndex = 0;
        }

        private void InitializeDataGridView()
        {
            // Clear existing columns
            dgvUsers.Columns.Clear();

            // Configure DataGridView
            dgvUsers.AutoGenerateColumns = false;
            dgvUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsers.MultiSelect = false;
            dgvUsers.ReadOnly = true;
            dgvUsers.AllowUserToAddRows = false;
            dgvUsers.AllowUserToDeleteRows = false;
            dgvUsers.RowHeadersVisible = false;
            dgvUsers.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Create columns
            var idColumn = new DataGridViewTextBoxColumn
            {
                Name = "Id",
                DataPropertyName = "Id",
                HeaderText = "ID",
                Width = 50,
                ReadOnly = true
            };

            var usernameColumn = new DataGridViewTextBoxColumn
            {
                Name = "Username",
                DataPropertyName = "Username",
                HeaderText = "Username",
                Width = 120
            };

            var emailColumn = new DataGridViewTextBoxColumn
            {
                Name = "Email",
                DataPropertyName = "Email",
                HeaderText = "Email",
                Width = 180
            };

            var fullNameColumn = new DataGridViewTextBoxColumn
            {
                Name = "FullName",
                DataPropertyName = "FullName",
                HeaderText = "Full Name",
                Width = 150
            };

            var roleColumn = new DataGridViewTextBoxColumn
            {
                Name = "Role",
                DataPropertyName = "Role",
                HeaderText = "Role",
                Width = 100
            };

            var isActiveColumn = new DataGridViewCheckBoxColumn
            {
                Name = "IsActive",
                DataPropertyName = "IsActive",
                HeaderText = "Active",
                Width = 60,
                ReadOnly = true
            };

            var createdDateColumn = new DataGridViewTextBoxColumn
            {
                Name = "CreatedDate",
                DataPropertyName = "CreatedDate",
                HeaderText = "Created Date",
                Width = 120,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "dd/MM/yyyy"
                }
            };

            var lastLoginColumn = new DataGridViewTextBoxColumn
            {
                Name = "LastLogin",
                DataPropertyName = "LastLogin",
                HeaderText = "Last Login",
                Width = 150,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    Format = "dd/MM/yyyy HH:mm",
                    NullValue = "Never"
                }
            };

            // Add columns to DataGridView
            dgvUsers.Columns.AddRange(new DataGridViewColumn[]
            {
            idColumn,
            usernameColumn,
            emailColumn,
            fullNameColumn,
            roleColumn,
            isActiveColumn,
            createdDateColumn,
            lastLoginColumn
            });

            // Configure alternating row colors
            dgvUsers.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);

            // Configure selection style
            dgvUsers.DefaultCellStyle.SelectionBackColor = Color.FromArgb(52, 152, 219);
            dgvUsers.DefaultCellStyle.SelectionForeColor = Color.White;
        }

        private void LoadUsers()
        {
            try
            {
                // Clear existing data
                dgvUsers.DataSource = null;

                // Get filter values
                AuthUserRole? roleFilter = null;
                if (cmbFilterRole.SelectedIndex > 0)
                {
                    var roleText = cmbFilterRole.SelectedItem.ToString();
                    if (Enum.TryParse<AuthUserRole>(roleText, out var selectedRole))
                    {
                        roleFilter = selectedRole;
                    }
                }

                var searchTerm = txtSearch.Text.Trim();
                var includeInactive = chkShowInactive.Checked;

                // Get users from repository
                var users = _userRepository.GetAllUsers(includeInactive, roleFilter, searchTerm);

                // Bind to DataGridView
                dgvUsers.DataSource = users;

                // Format active status column
                foreach (DataGridViewRow row in dgvUsers.Rows)
                {
                    var user = row.DataBoundItem as AuthUser;
                    if (user != null)
                    {
                        // Color code active status
                        if (!user.IsActive)
                        {
                            row.DefaultCellStyle.ForeColor = Color.Gray;
                            row.DefaultCellStyle.Font = new Font(dgvUsers.Font, FontStyle.Italic);
                        }

                        // Highlight SuperAdmin
                        if (user.Role == AuthUserRole.SuperAdmin)
                        {
                            row.DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 200);
                        }
                    }
                }

                // Update status
                var activeCount = users.Count(u => u.IsActive);
                var totalCount = users.Count;

                lblStatus.Text = $"Total: {totalCount} user(s) | Active: {activeCount} | Inactive: {totalCount - activeCount}";

                // Update window title
                this.Text = $"User Management - {totalCount} user(s)";

                // Enable/disable buttons based on selection
                UpdateButtonStates();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading users: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                lblStatus.Text = "Error loading users";
            }
        }

        private void UpdateButtonStates()
        {
            bool hasSelection = dgvUsers.SelectedRows.Count > 0;
            btnEditUser.Enabled = hasSelection;
            btnDeleteUser.Enabled = hasSelection;

            if (hasSelection)
            {
                var selectedUser = dgvUsers.SelectedRows[0].DataBoundItem as AuthUser;
                if (selectedUser != null)
                {
                    // Prevent deleting SuperAdmin
                    if (selectedUser.Role == AuthUserRole.SuperAdmin)
                    {
                        btnDeleteUser.Enabled = false;
                        btnDeleteUser.BackColor = Color.Gray;
                    }
                    else
                    {
                        btnDeleteUser.Enabled = true;
                        btnDeleteUser.BackColor = Color.FromArgb(231, 76, 60);
                    }
                }
            }
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            using (var addUserForm = new AddEditUserForm())
            {
                if (addUserForm.ShowDialog() == DialogResult.OK)
                {
                    LoadUsers();
                    MessageBox.Show("User added successfully!",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
        }

        private void btnEditUser_Click(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a user to edit.",
                    "No Selection",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            var selectedUser = dgvUsers.SelectedRows[0].DataBoundItem as AuthUser;
            if (selectedUser == null) return;

            using (var editUserForm = new AddEditUserForm(selectedUser))
            {
                if (editUserForm.ShowDialog() == DialogResult.OK)
                {
                    LoadUsers();
                    MessageBox.Show("User updated successfully!",
                        "Success",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
        }

        private void btnDeleteUser_Click(object sender, EventArgs e)
        {
            if (dgvUsers.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a user to delete.",
                    "No Selection",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            var selectedUser = dgvUsers.SelectedRows[0].DataBoundItem as AuthUser;
            if (selectedUser == null) return;

            // Confirm deletion
            var result = MessageBox.Show(
                $"Are you sure you want to {(selectedUser.IsActive ? "deactivate" : "permanently delete")} user '{selectedUser.Username}'?\n\n" +
                $"Role: {selectedUser.Role}\n" +
                $"Email: {selectedUser.Email}",
                selectedUser.IsActive ? "Confirm Deactivation" : "Confirm Permanent Deletion",
                MessageBoxButtons.YesNo,
                selectedUser.IsActive ? MessageBoxIcon.Warning : MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                try
                {
                    if (selectedUser.IsActive)
                    {
                        // Soft delete (deactivate)
                        if (_userRepository.ToggleUserStatus(selectedUser.Id))
                        {
                            LoadUsers();
                            MessageBox.Show($"User '{selectedUser.Username}' has been deactivated.",
                                "Success",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Failed to deactivate user.",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        // Hard delete (permanent)
                        if (_userRepository.DeleteUser(selectedUser.Id))
                        {
                            LoadUsers();
                            MessageBox.Show($"User '{selectedUser.Username}' has been permanently deleted.",
                                "Success",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Failed to delete user.",
                                "Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
                        }
                    }
                }
                catch (InvalidOperationException ex)
                {
                    MessageBox.Show(ex.Message,
                        "Operation Not Allowed",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadUsers();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadUsers();
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                LoadUsers();
                e.Handled = true;
            }
        }

        private void cmbFilterRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadUsers();
        }

        private void chkShowInactive_CheckedChanged(object sender, EventArgs e)
        {
            LoadUsers();
        }

        private void dgvUsers_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                btnEditUser_Click(sender, e);
            }
        }

        private void dgvUsers_SelectionChanged(object sender, EventArgs e)
        {
            UpdateButtonStates();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _userRepository?.Dispose();
            base.OnFormClosing(e);
        }

        // Keyboard shortcuts
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.F5:
                    btnRefresh_Click(null, null);
                    return true;

                case Keys.Control | Keys.F:
                    txtSearch.Focus();
                    txtSearch.SelectAll();
                    return true;

                case Keys.Control | Keys.N:
                    btnAddUser_Click(null, null);
                    return true;

                case Keys.Delete:
                    if (btnDeleteUser.Enabled)
                        btnDeleteUser_Click(null, null);
                    return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
