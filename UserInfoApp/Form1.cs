using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UserInfoApp
{
    public partial class Form1 : Form
    {
        int index = -1;
        BLL.Controller bllController = new BLL.Controller();
        public Form1()
        {
            InitializeComponent();
        }

        private void Create_Click(object sender, EventArgs e)
        {
            index = -1;
            Model.User user = new Model.User();
            user.Id = Guid.NewGuid().ToString();
            user.Name = textBoxUser.Text;
            user.Surname = textBoxSurname.Text;
            user.Cash = textBoxCash.Text;

            Model.Location location = new Model.Location();
            location.Name = textBoxLocation.Text;
            location.Zip = textBoxZip.Text;
            location.UserId = user.Id;

            MessageBox.Show(bllController.Create(user,location));
        }
        private void Read_Click(object sender, EventArgs e)
        {
            if (index == -1)
            {
                bllController.Read();
                if (bllController.Count() > 0)
                {
                    index = 0;
                }
            }
            else
            {
                index = (index + 1) % bllController.Count();
            }

            if (index >= 0)
            {
                Model.UserCard userCard = bllController.ReadForIndex(index);
                textBoxUser.Text = userCard.UserInfo.Name;
                textBoxSurname.Text = userCard.UserInfo.Surname;
                textBoxCash.Text = userCard.UserInfo.Cash;
                textBoxLocation.Text = userCard.LocationInfo.Name;
                textBoxZip.Text = userCard.LocationInfo.Zip;
            }
        }
        
        private void Update_Click(object sender, EventArgs e)
        {
            index = -1;
            bllController.currentUserCard.UserInfo.Name = textBoxUser.Text;
            bllController.currentUserCard.UserInfo.Surname = textBoxSurname.Text;
            bllController.currentUserCard.UserInfo.Cash = textBoxCash.Text;
            bllController.currentUserCard.LocationInfo.Name = textBoxLocation.Text;
            bllController.currentUserCard.LocationInfo.Zip = textBoxZip.Text;
            MessageBox.Show(bllController.Update());
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            index = -1;
            MessageBox.Show(bllController.Delete());
        }
    }
}
