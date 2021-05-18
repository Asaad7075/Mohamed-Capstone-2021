using DataAccessInterfaces;
using DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessFakes
{
    /// <summary>
    /// Chantal Shirley
    /// Created: 2021/04/16
    /// 
    /// Fake client test data.
    /// </summary>
    public class ClientFake : IClientAccessor
    {
        private List<Client> _clients = new List<Client>();
        private Dictionary<string, Client> _passwordHashes = new Dictionary<string, Client>();

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/04/16
        /// 
        /// Zero-argument constructor.
        /// </summary>
        public ClientFake()
        {
            _clients.Add(new Client
            {
                ClientID = 10004,
                FirstName = "Chantal",
                LastName = "Shirley",
                Email = "Chantal@back-on-track.com"
            });
            _clients.Add(new Client
            {
                ClientID = 10001,
                FirstName = "Jakub",
                LastName = "Kawski",
                Email = "Jakub@back-on-track.com"
            });
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/04/25
        /// 
        /// Tests inserting a new client.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="passwordHash"></param>
        /// <returns></returns>
        public bool InsertNewClientAccount(Client client, string passwordHash)
        {
            if (passwordHash.Equals("") || passwordHash.Equals(null) ||
                client == null)
            {
                return false;
            }
            else
            {
                _clients.Add(client);
                _passwordHashes.Add(passwordHash, client);
                return true;
            }
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/04/25
        /// 
        /// Tests Selecting a client by 
        /// their email and passwordhash
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool SelectClientByEmailAndPassword(string email, string passwordHash)
        {
            Client client = _passwordHashes[passwordHash];
            if (client != null && client.Email.Equals(email))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/04/16
        /// 
        /// Returns an array of a
        /// person's first and last name.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public string[] SelectClientFirstLastNameByEmail(string email)
        {
            string[] name = new string[2];
            foreach (Client client in _clients)
            {
                if (client.Email.ToLower() == email.ToLower())
                {
                    name[0] = client.FirstName;
                    name[1] = client.LastName;
                    return name;
                }
                else
                {
                    return null;
                }
            }

            return null;
        }

        /// <summary>
        /// Chantal Shirley
        /// Created: 2021/04/16
        /// 
        /// Returns a client ID
        /// based on their email.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public int SelectClientIdByEmail(string email)
        {
            foreach (Client client in _clients)
            {
                if (client.Email.Equals(email))
                {
                    return (client.ClientID);
                }
            }
            return -1;
        }
    }
}
