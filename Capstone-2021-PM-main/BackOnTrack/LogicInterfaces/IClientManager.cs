using DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicInterfaces
{
    public interface IClientManager
    {
        int GetClientIDByEmail(string email);
        string[] RetrieveClientFirstLastNameByEmail(string email);
        bool AuthenticateUser(string email, string password);
        bool CreateNewUser(Client client, string passwordHash);
    }
}
