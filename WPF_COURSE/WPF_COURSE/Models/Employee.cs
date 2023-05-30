using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_COURSE.Models
{
    public class Employee
    {
        private int id;
        public int Id
        {
            get { return id; }
            set { id = value;  }
        }

        private string? name;
        public string? Name
        {
            get { return name; }
            set { name = value; }
        }

        private int age;
        public int Age
        {
            get { return age; }
            set { age = value;  }
        }

        private string? email;

        public string? Email
        {
            get { return email; }
            set { email = value; }
        }

        private string? phone;

        public string? Phone
        {
            get { return phone; }
            set { phone = value;  }
        }

        private string? job;

        public string? Job
        {
            get { return job; }
            set { job = value;  }
        }

    }
}
