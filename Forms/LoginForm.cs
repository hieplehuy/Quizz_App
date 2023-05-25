using Google.Cloud.Firestore;
using Login_Signup.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Login_Signup
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void buttonSignup_Click(object sender, EventArgs e)
        {
            Hide();
            RegisterForm form = new RegisterForm();
            form.ShowDialog();
            Close();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            string email = textBoxEmail.Text.Trim();
            string password = textBoxPassword.Text;

            var db = FirestoreHelper.Database;
            DocumentReference docRef = db.Collection("UserData").Document(email);

            UserData data = docRef.GetSnapshotAsync().Result.ConvertTo<UserData>();//class trong Classes

            if (data != null)
            {
                if (password == Security.Decrypt(data.Password))
                {
                    MessageBox.Show("Login Success");
                }
                else
                    MessageBox.Show("Login Failed");
            }
            else
                MessageBox.Show("Login Failed");
        }
    }
}
