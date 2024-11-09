using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagementApp
{
    internal class Supplier
    {
        public Supplier()
        {
        }
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public Supplier(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
