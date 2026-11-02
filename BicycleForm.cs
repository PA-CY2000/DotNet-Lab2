using System.Drawing;

namespace WinFormsApp1
{
    public class BicycleForm : Form
    {
        private readonly LoginForm loginForm;
        private readonly TextBox txtStartTime;
        private readonly TextBox txtEndTime;
        private readonly TextBox txtTotal;
        private bool isNavigating;

        public BicycleForm(LoginForm loginForm)
        {
            this.loginForm = loginForm;

            Text = "Bicycle Renting";
            StartPosition = FormStartPosition.CenterScreen;
            ClientSize = new Size(700, 500);
            BackColor = Color.White;
            FormClosed += BicycleForm_FormClosed;

            Color blue = Color.FromArgb(35, 75, 180);
            Panel card = new Panel
            {
                Location = new Point(100, 40),
                Size = new Size(500, 410),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White
            };

            Label titleLabel = new Label
            {
                Text = "Bicycle Renting",
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 25, FontStyle.Bold),
                ForeColor = blue,
                Location = new Point(120, 65),
                Size = new Size(460, 55)
            };

            Label startLabel = new Label
            {
                Text = "Starting Time:",
                AutoSize = true,
                ForeColor = blue,
                Location = new Point(175, 155)
            };
            txtStartTime = new TextBox
            {
                Name = "txtStartTime",
                Location = new Point(315, 150),
                Size = new Size(180, 27)
            };

            Label endLabel = new Label
            {
                Text = "Ending Time:",
                AutoSize = true,
                ForeColor = blue,
                Location = new Point(175, 205)
            };
            txtEndTime = new TextBox
            {
                Name = "txtEndTime",
                Location = new Point(315, 200),
                Size = new Size(180, 27)
            };

            Button calculateButton = new Button
            {
                Name = "btnCalculate",
                Text = "Calculate",
                Location = new Point(290, 250),
                Size = new Size(120, 40),
                BackColor = blue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            calculateButton.Click += CalculateButton_Click;

            Label totalLabel = new Label
            {
                Text = "Total Cost:",
                AutoSize = true,
                ForeColor = blue,
                Location = new Point(175, 325)
            };
            txtTotal = new TextBox
            {
                Name = "txtTotal",
                Location = new Point(315, 320),
                Size = new Size(180, 27),
                ReadOnly = true,
                BackColor = Color.White
            };

            Button backButton = new Button
            {
                Name = "btnBack",
                Text = "Back",
                Location = new Point(175, 390),
                Size = new Size(120, 40),
                ForeColor = blue,
                FlatStyle = FlatStyle.Flat
            };
            backButton.Click += BackButton_Click;

            Button logoutButton = new Button
            {
                Name = "btnLogout",
                Text = "Logout",
                Location = new Point(405, 390),
                Size = new Size(120, 40),
                ForeColor = blue,
                FlatStyle = FlatStyle.Flat
            };
            logoutButton.Click += LogoutButton_Click;

            Controls.AddRange(new Control[]
            {
                card, titleLabel, startLabel, txtStartTime, endLabel, txtEndTime,
                calculateButton, totalLabel, txtTotal, backButton, logoutButton
            });
            card.SendToBack();
        }

        private void CalculateButton_Click(object? sender, EventArgs e)
        {
            if (!int.TryParse(txtStartTime.Text, out int startTime))
            {
                MessageBox.Show("Starting time must be a whole number from 0 to 23.", "Invalid Starting Time", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtStartTime.Focus();
                return;
            }

            if (!int.TryParse(txtEndTime.Text, out int endTime))
            {
                MessageBox.Show("Ending time must be a whole number from 1 to 24.", "Invalid Ending Time", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEndTime.Focus();
                return;
            }

            if (startTime < 0 || startTime > 23)
            {
                MessageBox.Show("Starting time must be between 0 and 23.", "Invalid Starting Time", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtStartTime.Focus();
                return;
            }

            if (endTime < 1 || endTime > 24)
            {
                MessageBox.Show("Ending time must be between 1 and 24.", "Invalid Ending Time", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEndTime.Focus();
                return;
            }

            if (startTime >= endTime)
            {
                MessageBox.Show("Starting time must be less than ending time.", "Invalid Rental Period", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Add the rate for every hour so the rental can cross rate periods.
            int totalCost = 0;
            for (int hour = startTime; hour < endTime; hour++)
            {
                totalCost += GetHourlyRate(hour);
            }

            txtTotal.Text = $"{totalCost} RWF";
        }

        private static int GetHourlyRate(int hour)
        {
            if (hour < 7 || hour >= 21)
            {
                return 500;
            }

            if (hour < 14 || hour >= 19)
            {
                return 1000;
            }

            return 1500;
        }

        private void BackButton_Click(object? sender, EventArgs e)
        {
            isNavigating = true;
            ChooseForm chooseForm = new ChooseForm(loginForm);
            chooseForm.Show();
            Close();
        }

        private void LogoutButton_Click(object? sender, EventArgs e)
        {
            isNavigating = true;
            loginForm.Show();
            Close();
        }

        private void BicycleForm_FormClosed(object? sender, FormClosedEventArgs e)
        {
            if (!isNavigating && !loginForm.IsDisposed)
            {
                loginForm.Show();
            }
        }
    }
}
