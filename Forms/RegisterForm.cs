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
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            Hide();
            LoginForm form = new LoginForm();
            form.ShowDialog();
            Close();
        }

        private UserData GetWriteData()
        {
            string email = textBoxEmail.Text.Trim();
            string password = Security.Encrypt(textBoxPassword.Text);
            string name = textBoxName.Text.Trim();

            return new UserData()
            {
                Email = email,
                Password = password,
                Name = name
            };
        }

        private void buttonSignup_Click(object sender, EventArgs e)
        {
            if (CheckIfUserAlreadyExist())
            {
                MessageBox.Show("User Already Exist");
                return;
            }

            var db = FirestoreHelper.Database;
            var data = GetWriteData();

            DocumentReference docRef = db.Collection("UserData").Document(data.Email);
            docRef.SetAsync(data);
            MessageBox.Show("Success");
        }

        private bool CheckIfUserAlreadyExist()
        {
            string email = textBoxEmail.Text.Trim();
            //string password = textBoxPassword.Text;

            var db = FirestoreHelper.Database;
            DocumentReference docRef = db.Collection("UserData").Document(email);

            UserData data = docRef.GetSnapshotAsync().Result.ConvertTo<UserData>();//class trong Classes

            if (data != null)
            {
                return true;
            }
            else
                return false;
        }
    }
}
