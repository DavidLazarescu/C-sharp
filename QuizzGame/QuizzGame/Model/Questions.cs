using System;
using System.Collections.Generic;
using System.Text;

namespace QuizzGame.Model
{
    public class QuestionCollection
    {
        public int response_code { get; set; }
        public List<Question> results { get; set; }
    }

    public class Question
    {
        public string category { get; set; }
        public string type { get; set; }
        public string difficulty { get; set; }
        public string question { get; set; }
        public string correct_answer { get; set; }
        public List<string> incorrect_answers { get; set; }
    }
}
