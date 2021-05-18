using DataAccessInterfaces;
using DataAccessLayer;
using DomainModels;
using LogicInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicLayer
{
    /// <summary>
    /// Chantal Shirley
    /// Created: 2021/04/08
    /// 
    /// Logic for client data.
    /// </summary>
    public class ClientManager : IClientManager
    {
        private IClientAccessor _clientAccessor;

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/04/09
        /// 
        /// Zero-argument constructor.
        /// </summary>
        public ClientManager()
        {
            _clientAccessor = new ClientAccessor();
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/04/09
        /// 
        /// Allows for DI with the client manager.
        /// </summary>
        /// <param name="suppliedClientAccessor"></param>
        public ClientManager(IClientAccessor suppliedClientAccessor)
        {
            _clientAccessor = suppliedClientAccessor;
        }

        public string[] RetrieveClientFirstLastNameByEmail(string email)
        {
            string[] clientName = null;

            try
            {
                clientName = _clientAccessor.SelectClientFirstLastNameByEmail(email);
            }
            catch (Exception)
            {
                throw new ApplicationException("Client name could not be retrieved.");
            }

            return clientName;
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/04/09
        /// 
        /// Gets client email by client id.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public int GetClientIDByEmail(string email)
        {
            int result = 0;

            try
            {
                result = _clientAccessor.SelectClientIdByEmail(email);
            }
            catch (Exception)
            {
                throw new ApplicationException("Client id could not be retrieved.");
            }

            return result;
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/04/24
        /// 
        /// Returns true if user exists in
        /// database.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool AuthenticateUser(string email, string password)
        {
            bool result = false;

            password = password.hashSHA256().ToUpper();

            try
            {
                result = _clientAccessor.SelectClientByEmailAndPassword(email, password);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Bad Username or Password");
            }

            return result;
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/04/24
        /// 
        /// Returns true if user was
        /// created and saved in the db
        /// successfully.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="passwordHash"></param>
        /// <returns></returns>
        public bool CreateNewUser(Client client, string password)
        {
            bool result = false;

            password = password.hashSHA256().ToUpper();

            try
            {
                result = _clientAccessor.InsertNewClientAccount(client, password);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Unable to create account.");
            }

            return result;
        }
    }
}
