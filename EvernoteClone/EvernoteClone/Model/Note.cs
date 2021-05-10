using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvernoteClone.Model
{

    public interface HasId
    {
        public string Id { get; set; }
    }

    public class Note : HasId
    {
        public string Id { get; set; }
        public string ParentNotebookId { get; set; }
        public string Title { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdatedTime { get; set; }
        public string FileLocation { get; set; }
    }
}
