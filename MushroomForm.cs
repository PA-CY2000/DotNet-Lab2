using System.Drawing;

namespace WinFormsApp1
{
    public class MushroomForm : Form
    {
        private readonly LoginForm loginForm;
        private readonly Label questionLabel;
        private readonly RadioButton yesRadioButton;
        private readonly RadioButton noRadioButton;
        private readonly Button nextButton;
        private readonly Button identifyButton;
        private bool? convexCup;
        private bool? forest;
        private bool? ring;
        private bool? gills;
        private string currentQuestion = string.Empty;
        private string? identifiedMushroom;
        private bool isNavigating;

        public MushroomForm(LoginForm loginForm)
        {
            this.loginForm = loginForm;

            Text = "Mushroom Identification";
            StartPosition = FormStartPosition.CenterScreen;
            ClientSize = new Size(700, 500);
            BackColor = Color.White;
            FormClosed += MushroomForm_FormClosed;

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
                Text = "Mushroom Identification",
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 25, FontStyle.Bold),
                ForeColor = blue,
                Location = new Point(120, 65),
                Size = new Size(460, 55)
            };

            questionLabel = new Label
            {
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Font = new Font("Segoe UI", 12),
                ForeColor = blue,
                Location = new Point(120, 150),
                Size = new Size(460, 45)
            };

            yesRadioButton = new RadioButton
            {
                Name = "rdoYes",
                Text = "Yes",
                AutoSize = true,
                Location = new Point(45, 28)
            };

            noRadioButton = new RadioButton
            {
                Name = "rdoNo",
                Text = "No",
                AutoSize = true,
                Location = new Point(145, 28)
            };

            GroupBox answerGroupBox = new GroupBox
            {
                Text = "Answer",
                ForeColor = blue,
                Location = new Point(225, 205),
                Size = new Size(250, 70)
            };
            answerGroupBox.Controls.Add(yesRadioButton);
            answerGroupBox.Controls.Add(noRadioButton);

            nextButton = new Button
            {
                Name = "btnNext",
                Text = "Next",
                Location = new Point(235, 295),
                Size = new Size(90, 40),
                BackColor = blue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            nextButton.Click += NextButton_Click;

            identifyButton = new Button
            {
                Name = "btnIdentifyMushroom",
                Text = "Identify Mushroom",
                Location = new Point(335, 295),
                Size = new Size(140, 40),
                BackColor = blue,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Enabled = false
            };
            identifyButton.Click += IdentifyButton_Click;

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
                card, titleLabel, questionLabel, answerGroupBox, nextButton,
                identifyButton, backButton, logoutButton
            });
            card.SendToBack();

            AskQuestion("Does your mushroom have a convex cup?", "convex cup");
        }

        private void AskQuestion(string question, string questionName)
        {
            questionLabel.Text = question;
            currentQuestion = questionName;
            yesRadioButton.Checked = false;
            noRadioButton.Checked = false;
        }

        private void NextButton_Click(object? sender, EventArgs e)
        {
            if (!yesRadioButton.Checked && !noRadioButton.Checked)
            {
                MessageBox.Show("Please answer the question before continuing.", "Answer Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool answer = yesRadioButton.Checked;

            // The first two answers narrow the choices, then one final answer identifies the mushroom.
            switch (currentQuestion)
            {
                case "convex cup":
                    convexCup = answer;
                    AskQuestion("Does your mushroom grow in a forest?", "forest");
                    break;

                case "forest":
                    forest = answer;
                    if (convexCup == true)
                    {
                        if (forest == false)
                        {
                            CompleteIdentification("Agaric Jaunissant");
                        }
                        else
                        {
                            AskQuestion("Does your mushroom have a ring?", "ring");
                        }
                    }
                    else
                    {
                        if (forest == false)
                        {
                            CompleteIdentification("Coprin chevelu");
                        }
                        else
                        {
                            AskQuestion("Does your mushroom have gills?", "gills");
                        }
                    }
                    break;

                case "ring":
                    ring = answer;
                    CompleteIdentification(ring == true ? "Amanite tue-mouche" : "Pied bleu");
                    break;

                case "gills":
                    gills = answer;
                    CompleteIdentification(gills == true ? "Girolle" : "Cepe de bordeau");
                    break;
            }
        }

        private void CompleteIdentification(string mushroomName)
        {
            identifiedMushroom = mushroomName;
            nextButton.Enabled = false;
            identifyButton.Enabled = true;
            ShowIdentificationResult();
        }

        private void ShowIdentificationResult()
        {
            if (identifiedMushroom == null)
            {
                return;
            }

            DialogResult answer = MessageBox.Show(
                $"Your mushroom is: {identifiedMushroom}.\n\nWould you like to start another identification?",
                "Identification Result",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Information);

            if (answer == DialogResult.Yes)
            {
                StartNewIdentification();
            }
        }

        private void IdentifyButton_Click(object? sender, EventArgs e)
        {
            ShowIdentificationResult();
        }

        private void StartNewIdentification()
        {
            convexCup = null;
            forest = null;
            ring = null;
            gills = null;
            identifiedMushroom = null;
            nextButton.Enabled = true;
            identifyButton.Enabled = false;
            AskQuestion("Does your mushroom have a convex cup?", "convex cup");
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

        private void MushroomForm_FormClosed(object? sender, FormClosedEventArgs e)
        {
            if (!isNavigating && !loginForm.IsDisposed)
            {
                loginForm.Show();
            }
        }
    }
}
