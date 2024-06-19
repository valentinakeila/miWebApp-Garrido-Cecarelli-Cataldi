using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    internal class Order // add Client heritance
    {
        public int Id { get; set; }

        public Order(int id) // add Client heritance
        {
            Id = id;
        }
    }
}