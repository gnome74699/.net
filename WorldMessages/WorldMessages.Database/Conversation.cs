//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WorldMessages.Database
{
    using System;
    using System.Collections.Generic;
    
    public partial class Conversation
    {
        public Conversation()
        {
            this.User = new HashSet<User>();
            this.Message = new HashSet<Message>();
        }
    
        public int Id { get; set; }
        public string Date { get; set; }
    
        public virtual ICollection<User> User { get; set; }
        public virtual ICollection<Message> Message { get; set; }
    }
}
