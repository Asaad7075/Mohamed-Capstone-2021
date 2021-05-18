using DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessInterfaces
{
    public interface IClientAccessor
    {
        int SelectClientIdByEmail(string email);
        string[] SelectClientFirstLastNameByEmail(string email);
        bool SelectClientByEmailAndPassword(string email, string password);
        bool InsertNewClientAccount(Client client, string passwordHash);
    }
}
