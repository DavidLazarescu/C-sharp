using QuizzGame.ViewModel.Helper;
using System;
using System.Collections.Generic;
using System.Text;

namespace QuizzGame.Model
{
    public class UserData : HasUserIds
    {
        public string UniqueId { get; set; }
        public string ParentUserId { get; set; }
        public int TotalGamesPlayed { get; set; }
    }
}
