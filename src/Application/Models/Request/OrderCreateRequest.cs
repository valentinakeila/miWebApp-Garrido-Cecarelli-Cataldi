using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Domain.Entities;
using Domain.Enums;

namespace Application.Models.Request
{
    public class OrderCreateRequest
    {

        public int UserId { get; set; }

        public int ProductId { get; set; }
        public int UnitsAmount { get; set; }


    }
}

