using System.Collections.Generic;
using System.Dynamic;

namespace Simple.Web.Windsor.Owin.Services
{
    /// <summary>
    /// do my database 
    /// </summary>
    public class DataService
    {
        public IList<string> _messages = new List<string>();
        dynamic Db;

        public DataService(dynamic Db)
        {
            this.Db = Db;
        }


        public void AddMessage(string message)
        {
            _messages.Add(message);
        }


        //[AdminOnlyFilter]
        public Customer Create(string firstname, string lastname) 
        {
            //if(account.Cannot("ElvateGroup:Super") {
            //    throw 
            //}
            
            dynamic person = new ExpandoObject();
            person.Name = "steve";
            person.Age = 50;
            this.Db.Users.Insert(person);

            return new Customer
            {
                FirstName = person.FirstName,
                LastName = person.LastName
            };
        }

    }

    public class Customer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }


    }

}