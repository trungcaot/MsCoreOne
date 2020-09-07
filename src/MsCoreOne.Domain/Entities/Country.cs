using System;
using System.Collections.Generic;
using System.Text;

namespace MsCoreOne.Domain.Entities
{
    public class Country
    {
        public int Id { get; set; }

        public string SortName { get; set; }

        public string Name { get; set; }

        public string PhoneCode { get; set; }
    }
}
