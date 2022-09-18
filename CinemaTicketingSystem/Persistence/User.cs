using System;

namespace Persistence
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Type { get; set; }
        public Cinema Cine { get; set; }

        public User()
        {

        }
        public User(string Username, string Password, string Type, Cinema Cine)
        {
            this.Username = Username;
            this.Password = Password;
            this.Type = Type;
            this.Cine = Cine;
        }
    }
}