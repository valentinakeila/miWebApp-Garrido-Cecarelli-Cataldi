using System;

namespace Domain.Entities
{
    public class Admin : User
    {
        public string JobType { get; set; }

        public Admin(string job)
        {
            JobType = job;
        }
    }
}