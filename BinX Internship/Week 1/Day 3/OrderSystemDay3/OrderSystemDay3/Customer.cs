using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace OrderSystemDay3
{
    internal class Customer : INotifiable
    {
        private string name;
        private string email;

        public string Name
        {
            get { return name; }
        }

        public string Email
        {
            get { return email; }
        }

        public Customer(string name, string email)
        {

            if (string.IsNullOrEmpty(name))
            {
                this.name = "Unknown Customer";
            }
            else
            {
                this.name = name;
            }

            if (string.IsNullOrEmpty(email))
            {
                this.email = "No Email";
            }
            else
            {
                this.email = email;
            }
        }

        public void SendNotification()
        {
            Console.WriteLine($"Notification sent to customer {name} at {email}.");
        }

        public override string ToString()
        {
            return $"Customer: {name} - Email: {email}";
        }
    }
}
