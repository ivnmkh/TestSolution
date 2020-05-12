using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TestSolution.Data
{
    public class Contacts
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }

      /*  public static List<Contacts> GetContacts()
        {
            var result = SqliteDataAccess.ReadSQlite();
          
            return result;
        }*/
    }
}
