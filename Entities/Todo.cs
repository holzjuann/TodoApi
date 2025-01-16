using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Entities.Enums;

namespace TodoApi.Entities
{
    public class Todo
    {
        public int Id {get;set;}
        public string Title {get;set;}
        public string? Description {get;set;} = null;
        public EnumStatusTodo Status {get;set;}
        public DateTime Created {get;set;} = DateTime.Now;
        public DateTime? Finished {get;set;} = null;
    }
}