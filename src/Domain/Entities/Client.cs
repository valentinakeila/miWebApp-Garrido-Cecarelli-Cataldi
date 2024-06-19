
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    internal class Client : User
    {
        public string PhoneNumber { get; set; }
        public string Address { get; set; }

        public Client(string phoneNumber, string address)
        {
            PhoneNumber = phoneNumber;
            Address = address;
        }
    }
}