using System.Drawing;

namespace WinFormsApp1
{
    public class LoginForm : Form
    {
        private readonly TextBox txtUsername;
        private readonly TextBox txtPassword;
        private readonly Button btnLogin;

        public LoginForm()
        {
            Text = "Login";
            StartPosition = FormStartPosition.CenterScreen;
            ClientSize = new Size(700, 500);
            BackColor = Color.White;
            FormClosed += LoginForm_FormClosed;

            Color blue = Color.FromArgb(35, 75, 180);
            Panel card = new Panel
            {
                Location = new Point(100, 55),
                Size = new Size(500, 370),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White
            };

            Label titleLabel = new Label
            {
                Text = "Login",
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 25, FontStyle.Bold),
                ForeColor = blue,
                Location = new Point(120, 75),
                Size = new Size(460, 55)
            };

            Label subtitleLabel = new Label
            {
                Text = "Bicycle Renting and Mushroom Identification System",
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 10),
                ForeColor = blue,
                Location = new Point(120, 130),
                Size = new Size(460, 30)
            };

            Label usernameLabel = new Label
            {
                Text = "Username:",
                AutoSize = true,
                ForeColor = blue,
                Location = new Point(180, 190)
            };

            txtUsername = new TextBox
            {
                Name = "txtUsername",
                Location = new Point(300, 185),
                Size = new Size(220, 27)
            };

            Label passwordLabel = new Label
            {
                Text = "Password:",
                AutoSize = true,
                ForeColor = blue,
                Location = new Point(180, 240)
            };

            txtPassword = new TextBox
            {
                Name = "txtPassword",
                Location = new Point(300, 235),
                Size = new Size(220, 27),
                UseSystemPasswordChar = true
            };

            btnLogin = new Button
            {
                Name = "btnLogin",
                Text = "Login",
                Location = new Point(290, 300),
                Size = new Size(140, 42),
                BackColor = blue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnLogin.Click += BtnLogin_Click;

            Controls.AddRange(new Control[]
            {
                card, titleLabel, subtitleLabel, usernameLabel, txtUsername,
                passwordLabel, txtPassword, btnLogin
            });
            card.SendToBack();
            AcceptButton = btnLogin;
        }

        private void BtnLogin_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("Please enter a username.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUsername.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                MessageBox.Show("Please enter a password.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Focus();
                return;
            }

            // Any non-empty username and password are accepted.
            ChooseForm chooseForm = new ChooseForm(this);
            chooseForm.Show();
            Hide();
        }

        private void LoginForm_FormClosed(object? sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
