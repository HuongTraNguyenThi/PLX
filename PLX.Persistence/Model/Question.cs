using System;
using System.Collections.Generic;

namespace PLX.Persistence.Model
{
    public class Question : BaseEntity
    {
        public Question()
        {
            this.CustomerQuestions = new HashSet<CustomerQuestion>();
        }
        public string Content { get; set; }
        public ICollection<CustomerQuestion> CustomerQuestions { get; set; }
    }
}