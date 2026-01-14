using POS_Shop.Helpers;
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
    public partial class AddEditUserForm : Form
    {
        private AuthUser _existingUser;
        private bool _isEditMode;
        private UserRepository _userRepository;

        public AddEditUserForm()
        {
            InitializeComponent();
            _userRepository = new UserRepository();
            InitializeForm();
        }

        public AddEditUserForm(AuthUser user) : this()
        {
            _existingUser = user;
            _isEditMode = true;
            LoadUserData();
        }

        private void AddEditUserForm_Load(object sender, EventArgs e)
        {
            // Set focus to first control
            if (_isEditMode)
            {
                txtUsername.Select();
            }
            else
            {
                txtUsername.Focus();
            }
        }

        private void InitializeForm()
        {
            // Populate role dropdown
            LoadRoles();

            // Set default values
            chkIsActive.Checked = true;

            if (_isEditMode)
            {
                lblTitle.Text = "Edit User";
                this.Text = "Edit User";

                // For edit mode, password fields are optional
                lblPassword.Text = "New Password (optional):";
                lblConfirmPassword.Text = "Confirm New Password (optional):";
            }
            else
            {
                lblTitle.Text = "Add New User";
                this.Text = "Add New User";
            }

            // Configure tooltips
            var toolTip = new ToolTip();
            toolTip.SetToolTip(txtUsername, "Enter username (2-50 characters)");
            toolTip.SetToolTip(txtEmail, "Enter valid email address");
            toolTip.SetToolTip(txtFullName, "Enter full name (optional)");
            toolTip.SetToolTip(txtPassword, "Enter password (min 6 characters)");
            toolTip.SetToolTip(txtConfirmPassword, "Re-enter password for confirmation");
            toolTip.SetToolTip(cmbRole, "Select user role");
            toolTip.SetToolTip(chkIsActive, "Check to make user active");
            toolTip.SetToolTip(btnSave, "Save user information");
            toolTip.SetToolTip(btnCancel, "Cancel and close dialog");
        }

        private void LoadRoles()
        {
            try
            {
                // Get all roles from enum
                var roles = Enum.GetValues(typeof(AuthUserRole))
                    .Cast<AuthUserRole>()
                    .ToList();

                // Exclude SuperAdmin from dropdown for new users
                if (!_isEditMode)
                {
                    roles = roles.Where(r => r != AuthUserRole.SuperAdmin).ToList();
                }

                cmbRole.DataSource = roles;
                cmbRole.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading roles: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadUserData()
        {
            if (_existingUser == null) return;

            try
            {
                txtUsername.Text = _existingUser.Username;
                txtEmail.Text = _existingUser.Email;

                // Set role in combobox
                cmbRole.SelectedItem = _existingUser.Role;

                // Disable role change for SuperAdmin
                if (_existingUser.Role == AuthUserRole.SuperAdmin)
                {
                    cmbRole.Enabled = false;
                    toolTip1.SetToolTip(cmbRole, "SuperAdmin role cannot be changed");
                }

                chkIsActive.Checked = _existingUser.IsActive;

                // Disable active status for SuperAdmin
                if (_existingUser.Role == AuthUserRole.SuperAdmin)
                {
                    chkIsActive.Enabled = false;
                    toolTip1.SetToolTip(chkIsActive, "SuperAdmin must always be active");
                }

                // For existing users, clear password fields
                txtPassword.Clear();
                txtConfirmPassword.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading user data: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            try
            {
                if (_isEditMode)
                {
                    UpdateUser();
                }
                else
                {
                    CreateUser();
                }
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message, "Operation Not Allowed",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private bool ValidateInput()
        {
            // Username validation
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("Username is required.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return false;
            }

            if (txtUsername.Text.Length < 2)
            {
                MessageBox.Show("Username must be at least 2 characters long.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return false;
            }

            if (txtUsername.Text.Length > 50)
            {
                MessageBox.Show("Username cannot exceed 50 characters.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return false;
            }

            // Email validation
            if (string.IsNullOrWhiteSpace(txtEmail.Text))
            {
                MessageBox.Show("Email is required.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }

            if (!IsValidEmail(txtEmail.Text))
            {
                MessageBox.Show("Please enter a valid email address.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }

            if (txtEmail.Text.Length > 100)
            {
                MessageBox.Show("Email cannot exceed 100 characters.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }

            // Full name validation
            if (txtFullName.Text.Length > 100)
            {
                MessageBox.Show("Full name cannot exceed 100 characters.", "Validation Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtFullName.Focus();
                return false;
            }

            // Password validation (for new users)
            if (!_isEditMode)
            {
                if (string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    MessageBox.Show("Password is required for new users.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPassword.Focus();
                    return false;
                }

                if (txtPassword.Text.Length < 6)
                {
                    MessageBox.Show("Password must be at least 6 characters long.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPassword.Focus();
                    return false;
                }

                if (txtPassword.Text.Length > 100)
                {
                    MessageBox.Show("Password cannot exceed 100 characters.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPassword.Focus();
                    return false;
                }

                if (txtPassword.Text != txtConfirmPassword.Text)
                {
                    MessageBox.Show("Passwords do not match.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPassword.Focus();
                    txtPassword.SelectAll();
                    return false;
                }
            }
            else if (!string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                // For edit mode, if password is provided, validate it
                if (txtPassword.Text.Length < 6)
                {
                    MessageBox.Show("Password must be at least 6 characters long.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPassword.Focus();
                    return false;
                }

                if (txtPassword.Text.Length > 100)
                {
                    MessageBox.Show("Password cannot exceed 100 characters.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPassword.Focus();
                    return false;
                }

                if (txtPassword.Text != txtConfirmPassword.Text)
                {
                    MessageBox.Show("Passwords do not match.", "Validation Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPassword.Focus();
                    txtPassword.SelectAll();
                    return false;
                }
            }

            return true;
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private void CreateUser()
        {
            Cursor = Cursors.WaitCursor;
            btnSave.Enabled = false;

            try
            {
                var user = new AuthUser
                {
                    Username = txtUsername.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    PasswordHash = PasswordHasher.HashPassword(txtPassword.Text),
                    Role = (AuthUserRole)cmbRole.SelectedItem,
                    IsActive = chkIsActive.Checked,
                    CreatedAt = DateTime.Now
                };

                if (_userRepository.CreateUser(user))
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show("Failed to create user. Username or email may already exist.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtUsername.Focus();
                    txtUsername.SelectAll();
                }
            }
            finally
            {
                Cursor = Cursors.Default;
                btnSave.Enabled = true;
            }
        }

        private void UpdateUser()
        {
            Cursor = Cursors.WaitCursor;
            btnSave.Enabled = false;

            try
            {
                var user = new AuthUser
                {
                    Id = _existingUser.Id,
                    Username = txtUsername.Text.Trim(),
                    Email = txtEmail.Text.Trim(),
                    Role = (AuthUserRole)cmbRole.SelectedItem,
                    IsActive = chkIsActive.Checked,
                    CreatedAt = _existingUser.CreatedAt,
                    LastLogin = _existingUser.LastLogin
                };

                // Only update password if provided
                if (!string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    user.PasswordHash = PasswordHasher.HashPassword(txtPassword.Text);
                }
                else
                {
                    user.PasswordHash = _existingUser.PasswordHash;
                }

                if (_userRepository.UpdateUser(user))
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show("Failed to update user. Username or email may already exist.", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtUsername.Focus();
                    txtUsername.SelectAll();
                }
            }
            finally
            {
                Cursor = Cursors.Default;
                btnSave.Enabled = true;
            }
        }

        // Keyboard shortcuts
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Escape:
                    btnCancel_Click(null, null);
                    return true;

                case Keys.Control | Keys.S:
                    if (btnSave.Enabled)
                        btnSave_Click(null, null);
                    return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        // Control validation events
        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            // You can add real-time validation here if needed
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            // You can add real-time validation here if needed
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            // Show password strength indicator if needed
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            _userRepository?.Dispose();
            base.OnFormClosing(e);
        }

        // Tooltip component
        private ToolTip toolTip1 = new ToolTip();
    }
}
