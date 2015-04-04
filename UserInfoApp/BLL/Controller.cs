using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserInfoApp.BLL
{
    public class Controller
    {
        public Model.UserCard currentUserCard = null;
        DAL.DataHub dataHub = new DAL.DataHub();

        public void SaveToMemory(Model.UserCard userCard)
        {
            currentUserCard = userCard;
        }
        public string Create(Model.User user, Model.Location location)
        {
            string msg = "Created!";

            Model.UserCard userCard = new Model.UserCard();
            userCard.LocationInfo = location;
            userCard.UserInfo = user;

            SaveToMemory(userCard);
            dataHub.Create(userCard);

            msg = string.Format("{0} Now we have {1} items", msg, dataHub.Count() + 1);

            return msg;
        }

        public int Count()
        {
            return dataHub.Count();
        }
        public void Read()
        {
           dataHub.Read();
        }

        public Model.UserCard ReadForIndex(int index)
        {
            currentUserCard = dataHub.Read(index);
            return currentUserCard;
        }
        public string Update()
        {
            string msg = "Updated!";

            if (currentUserCard != null)
            {
                dataHub.Update(currentUserCard);
            }
            else
            {
                msg = "Error!";
            }

            return msg;
        }
        public string Delete()
        {
            string msg = "Deleted!";

            if (currentUserCard != null)
            {
                dataHub.Delete(currentUserCard);
            }
            else
            {
                msg = "Error!";
            }

            return msg;
        }
    }
}
