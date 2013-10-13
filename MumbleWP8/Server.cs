using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MumbleWP8
{
    public class Server
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public int Ping { get; set; }
        public int MaxUsers { get; set; }
        public int CurrentUsers { get; set; }
        public string Users
        {
            get { return string.Concat(CurrentUsers, "/", MaxUsers); }
        }
    }
    public class PublicServer : Server
    {
        public string ContinentCode;
        public string CountryCode;
        public string Country;
        public string Region;
    }
}
