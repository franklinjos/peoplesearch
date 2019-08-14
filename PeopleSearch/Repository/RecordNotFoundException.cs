using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PeopleSearch.Repository
{
    public class RecordNotFoundException : Exception

    {
        public RecordNotFoundException() { }
        public RecordNotFoundException(int Id) : base(string.Format("Record with Id {0}", Id)){}
    }
}