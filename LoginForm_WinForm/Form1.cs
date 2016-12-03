using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace LoginForm_WinForm
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private bool isSuchLogin(string login)
        {
            string[] allUsers;
            using (StreamReader fs = new StreamReader("users.txt"))
            {
                allUsers = fs.ReadToEnd().Split();
            }
            foreach (string word in allUsers)
            {
                if (word == login)
                {
                    return true;
                }
            }
            return false;     
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(LoginBox.Text))
            {
                MessageBox.Show("Enter login");
            }
            if(String.IsNullOrEmpty(PassBox.Text))
            {
                MessageBox.Show("Enter password");
            }
            else
            {
                if (PassBox.Text.Length<5)
                {
                    MessageBox.Show("Password is too short");
                    PassBox.Text = "";
                    return;
                }

                if (isSuchLogin(LoginBox.Text)== true)
                {
                    MessageBox.Show("Such user already exists");
                    clearBoxes();
                    return;
                }
                if (isSuchLogin(LoginBox.Text) == false)
                {
                    using (StreamWriter sw = new StreamWriter("users.txt", true))
                    {
                        sw.WriteLine();
                        sw.Write(LoginBox.Text);
                        sw.Write(" ");
                        sw.Write(PassBox.Text);
                    }
                    MessageBox.Show("Registration completed successfully");
                    clearBoxes();
                }
            }
        }
        private void clearBoxes()
        {
            LoginBox.Text = "";
            PassBox.Text = "";
        }
        private void PassBox_TextChanged(object sender, EventArgs e)
        {
            PassProgress.Value = PassBox.Text.Length;
        }

        private void SignInButton_Click(object sender, EventArgs e)
        {
            if (isSuchLogin(LoginBox.Text)==false)
            {
                MessageBox.Show("Login or password is incorrect");
                return;
            }
            else
            {
                using (StreamReader reader = new StreamReader("users.txt"))
                {
                    string line;
                    while ((line  = reader.ReadLine())!= null)
                    {
                        string[] logpass = line.Split(' ');
                        if (logpass[0] == LoginBox.Text && logpass[1] == PassBox.Text)
                        {
                            MessageBox.Show("Login success");
                            clearBoxes();
                            break;
                        }
                    }
                }
            }
        }
    }
}
