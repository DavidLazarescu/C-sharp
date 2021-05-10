using Newtonsoft.Json;
using QuizzGame.Model;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace QuizzGame.ViewModel.Helper
{
    public class QuestionsHelper
    {
        public static string API_ENDPOINT = "https://opentdb.com/api.php?amount={0}{1}&type=multiple";

        public static async Task<List<Question>> GetQuestions(QuizzVM vm)
        {
            using(HttpClient client = new HttpClient())
            {
                var res = await client.GetAsync(createUrl(vm));
                var jsonResult = await res.Content.ReadAsStringAsync();

                if (res.IsSuccessStatusCode)
                {
                    List<Question> questionList = new List<Question>();

                    var obj = JsonConvert.DeserializeObject<QuestionCollection>(jsonResult);

                    foreach (Question question in obj.results)
                    {

                        question.question = Utility.UnescapeXml(question.question);
                        question.question = Utility.UnescapeHtml(question.question);

                        questionList.Add(question);
                    }

                    return questionList;
                }
                else
                {
                    /*this should never occur, if it does, its an error!*/
                    #if DEBUG
                    Console.ReadLine();
                    #endif
                    return null;
                }
            }
        }


        private static string createUrl(QuizzVM vm)
        {
            /*Creates and returns URL dependent from the Selected values of the ComboBox*/
            string temp = string.Empty;


            switch (vm.KindOfQuestionsComboBoxAnswer)
            {
                case "Any Category":
                    temp = string.Format(API_ENDPOINT, vm.AmountOfQuestionsComboBoxAnswer, "");
                    break;
                case "General Knowledge":
                    temp = string.Format(API_ENDPOINT, vm.AmountOfQuestionsComboBoxAnswer, "&category=9");
                    break;
                case "Books":
                    temp = string.Format(API_ENDPOINT, vm.AmountOfQuestionsComboBoxAnswer, "&category=10");
                    break;
                case "Films":
                    temp = string.Format(API_ENDPOINT, vm.AmountOfQuestionsComboBoxAnswer, "&category=11");
                    break;
                case "Music":
                    temp = string.Format(API_ENDPOINT, vm.AmountOfQuestionsComboBoxAnswer, "&category=12");
                    break;
                case "Musicals & Theatre":
                    temp = string.Format(API_ENDPOINT, vm.AmountOfQuestionsComboBoxAnswer, "&category=13");
                    break;
                case "Television":
                    temp = string.Format(API_ENDPOINT, vm.AmountOfQuestionsComboBoxAnswer, "&category=14");
                    break;
                case "Video Games":
                    temp = string.Format(API_ENDPOINT, vm.AmountOfQuestionsComboBoxAnswer, "&category=15");
                    break;
                case "Board Games":
                    temp = string.Format(API_ENDPOINT, vm.AmountOfQuestionsComboBoxAnswer, "&category=16");
                    break;
                case "Sience & Nature":
                    temp = string.Format(API_ENDPOINT, vm.AmountOfQuestionsComboBoxAnswer, "&category=17");
                    break;
                case "Computers":
                    temp = string.Format(API_ENDPOINT, vm.AmountOfQuestionsComboBoxAnswer, "&category=18");
                    break;
                case "Mathematics":
                    temp = string.Format(API_ENDPOINT, vm.AmountOfQuestionsComboBoxAnswer, "&category=19");
                    break;
                case "Mythology":
                    temp = string.Format(API_ENDPOINT, vm.AmountOfQuestionsComboBoxAnswer, "&category=20");
                    break;
                case "Sports":
                    temp = string.Format(API_ENDPOINT, vm.AmountOfQuestionsComboBoxAnswer, "&category=21");
                    break;
                case "Geography":
                    temp = string.Format(API_ENDPOINT, vm.AmountOfQuestionsComboBoxAnswer, "&category=22");
                    break;
                case "History":
                    temp = string.Format(API_ENDPOINT, vm.AmountOfQuestionsComboBoxAnswer, "&category=23");
                    break;
                case "Politics":
                    temp = string.Format(API_ENDPOINT, vm.AmountOfQuestionsComboBoxAnswer, "&category=24");
                    break;
                case "Art":
                    temp = string.Format(API_ENDPOINT, vm.AmountOfQuestionsComboBoxAnswer, "&category=25");
                    break;
                case "Celebrities":
                    temp = string.Format(API_ENDPOINT, vm.AmountOfQuestionsComboBoxAnswer, "&category=26");
                    break;
                case "Animals":
                    temp = string.Format(API_ENDPOINT, vm.AmountOfQuestionsComboBoxAnswer, "&category=27");
                    break;
            }

            return temp;
        }
    }
}
