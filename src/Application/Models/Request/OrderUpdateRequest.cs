using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Request
{
    public class OrderUpdateRequest
    {

        public int UnitsAmount { get; set; }

        public OrderState State { get; set; }  

    }
}
