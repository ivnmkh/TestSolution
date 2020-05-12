using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace TestSolution.Data
{
    public class SqliteDataAccess : DependencyObject
    {

        public static List<Contacts> LoadContact()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                var output = cnn.Query<Contacts>("select * from Contacts", new DynamicParameters());
                return output.ToList();
            }
        }
        public static void addContacts(Contacts _contact)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("insert into Contacts (Name, SurName,PhoneNumber,Email) values (@Name, @SurName,@PhoneNumber,@Email)", _contact);

            }
        }
        public static void EditContacts(Contacts _contact)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("UPDATE Contacts SET Name = @Name, Surname = @SurName, PhoneNumber=@PhoneNumber, Email =@Email Where id = @Id", _contact);
            }
        }

        public static void DeleteContacts(Contacts _contact)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Execute("delete from Contacts where id =@id and name =@Name and surname = @SurName and email=@Email", _contact);
            }
        }
        public static List<Contacts> SearchContacts(string Sstr)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                Sstr = "'%" + Sstr + "%'";
                var output = cnn.Query<Contacts>("SELECT * FROM Contacts WHERE name LIKE "+Sstr+ " OR Surname LIKE " + Sstr + " OR email LIKE " + Sstr + " OR PhoneNumber LIKE " + Sstr, new DynamicParameters());
                return output.ToList();
            }
        }

        private static string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }

        public static List<Contacts> contact = new List<Contacts>();
        public static List<Contacts> ReadSQlite()
        {
            return contact = SqliteDataAccess.LoadContact();
        }

    }
}
