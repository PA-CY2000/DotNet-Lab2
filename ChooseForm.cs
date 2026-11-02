using System.Drawing;

namespace WinFormsApp1
{
    public class ChooseForm : Form
    {
        private readonly LoginForm loginForm;
        private bool isNavigating;

        public ChooseForm(LoginForm loginForm)
        {
            this.loginForm = loginForm;

            Text = "Choose System";
            StartPosition = FormStartPosition.CenterScreen;
            ClientSize = new Size(700, 450);
            BackColor = Color.White;
            FormClosed += ChooseForm_FormClosed;

            Color blue = Color.FromArgb(35, 75, 180);
            Panel card = new Panel
            {
                Location = new Point(100, 55),
                Size = new Size(500, 320),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White
            };

            Label welcomeLabel = new Label
            {
                Text = "Choose",
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 25, FontStyle.Bold),
                ForeColor = blue,
                Location = new Point(120, 85),
                Size = new Size(460, 55)
            };

            Label instructionLabel = new Label
            {
                Text = "Welcome! Select a system to continue.",
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 11),
                ForeColor = blue,
                Location = new Point(120, 145),
                Size = new Size(460, 30)
            };

            Button bicycleButton = new Button
            {
                Name = "btnBicycleRenting",
                Text = "Bicycle Renting",
                Location = new Point(165, 230),
                Size = new Size(155, 55),
                BackColor = blue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            bicycleButton.Click += BicycleButton_Click;

            Button mushroomButton = new Button
            {
                Name = "btnMushroomIdentification",
                Text = "Mushroom Identification",
                Location = new Point(380, 230),
                Size = new Size(155, 55),
                BackColor = blue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            mushroomButton.Click += MushroomButton_Click;

            Controls.AddRange(new Control[] { card, welcomeLabel, instructionLabel, bicycleButton, mushroomButton });
            card.SendToBack();
        }

        private void BicycleButton_Click(object? sender, EventArgs e)
        {
            isNavigating = true;
            BicycleForm bicycleForm = new BicycleForm(loginForm);
            bicycleForm.Show();
            Close();
        }

        private void MushroomButton_Click(object? sender, EventArgs e)
        {
            isNavigating = true;
            MushroomForm mushroomForm = new MushroomForm(loginForm);
            mushroomForm.Show();
            Close();
        }

        private void ChooseForm_FormClosed(object? sender, FormClosedEventArgs e)
        {
            if (!isNavigating && !loginForm.IsDisposed)
            {
                loginForm.Show();
            }
        }
    }
}
