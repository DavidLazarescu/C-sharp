using QuizzGame.Model;
using QuizzGame.ViewModel.Commands.QuizzGame;
using QuizzGame.ViewModel.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace QuizzGame.ViewModel
{
    public class QuizzVM : INotifyPropertyChanged
    {
        public ChooseAnswerCommand ChooseAnswerCommand { get; set; }
        public PlayGameCommand PlayGameCommand { get; set; }
        public NextQuestionCommand NextQuestionCommand { get; set; }
        public ToMenuWindowCommand ToMenuWindowCommand { get; set; }
        public ToInfoWindowCommand ToInfoWindowCommand { get; set; }


        private UserData userData;
        public UserData UserData
        {
            get { return userData; }
            set 
            { 
                userData = value;
                OnPropertyChanged("UserData");
            }
        }



        /*Keeps track of the current Question while ingame, gets increased everytime when "CurrentQuestion" gets set
          And gets resettet when going back to the Menu Window with the button*/
        public int CurrentQuestionIndex { get; set; }
        public Question CurrentQuestion
        {
            /*Getter -> Returns the Current Question
              Setter -> Increases the Current Question to the next question and displays the new Question Options*/
            get 
            {
                /*if the currentQuestion index is bigger than the count of all total questions, the game is over, so go to game over screen*/
                if (CurrentQuestionIndex > Questions.Count && GameOver == false)
                {
                    showGameOverWindow();
                    return null;
                }
                /*if there are Questions in the Questions List, and the current Question is in this list, return the current Question*/
                else if (Questions.Count > 0 && CurrentQuestionIndex <= Questions.Count)
                    return Questions[CurrentQuestionIndex - 1];
                /*Something didnt went as planed, return null and pause the app if in Debug mode*/
                else
                {
                    #if DEBUG
                    Console.ReadLine();
                    #endif
                    return null;
                }
            }
            set 
            {
                /*Increases the CurrentQuestionIndex variable and sets the questions to the GUI*/
                CurrentQuestionIndex++;
                OnPropertyChanged("CurrentQuestion");

                setCurrentQuestionAnswers();
                
            }
        }

        private List<string> currentQuestionAnswers;
        public List<string> CurrentQuestionAnswers
        {
            /*A List which stores the answer options for the current Question*/
            get { return currentQuestionAnswers; }
            set 
            { 
                currentQuestionAnswers = value;
                OnPropertyChanged("CurrentQuestionAnswers");
            }
        }


        private ObservableCollection<Question> questions;
        public ObservableCollection<Question> Questions
        {
            /*The Observable Collection of Questions*/
            get { return questions; }
            set 
            { 
                questions = value;

                /*Creates a Current Question*/
                if (Questions.Count > 0)
                    CurrentQuestion = new Question();

                OnPropertyChanged("Questions");
            }
        }

        private int questionsLeftCount;
        public int QuestionsLeftCount
        {
            /*A count of how many questions are still left*/
            get { return questionsLeftCount; }
            set 
            { 
                questionsLeftCount = value;
                OnPropertyChanged("QuestionsLeftCount");
            }
        }

        private int correctAnswers = 0;
        public int CorrectAnswers
        {
            /*A count of right answers*/
            get { return correctAnswers; }
            set 
            { 
                correctAnswers = value;
                OnPropertyChanged("CorrectAnswers");
            }
        }

        private int wrongAnswers = 0;
        public int WrongAnswers
        {
            /*A Count of wrong Answers*/
            get { return wrongAnswers; }
            set
            {
                wrongAnswers = value;
                OnPropertyChanged("WrongAnswers");
            }
        }

        private double percentageOfRightAnswers;
        public double PercentageOfRightAnswers
        {
            /*Calculated percentage of right answered questions (they get calculated after every game)*/
            get { return percentageOfRightAnswers; }
            set 
            { 
                percentageOfRightAnswers = value;
                OnPropertyChanged("PercentageOfRightAnswers");
            }
        }


        private string answer;
        public string Answer
        {
            /*The Answer if the choosed option was right or wrong*/
            get { return answer; }
            set 
            { 
                answer = value;
                OnPropertyChanged("Answer");
            }
        }

        #region Visibilitys
        /*Visibilitys of the different Window contents (e.g. Game, Menu, EndScreen)*/
        private Visibility gameVis;
        public Visibility GameVis
        {
            get { return gameVis; }
            set 
            { 
                gameVis = value;
                OnPropertyChanged("GameVis");
            }
        }

        private Visibility menuVis;
        public Visibility MenuVis
        {
            get { return menuVis; }
            set 
            { 
                menuVis = value;
                OnPropertyChanged("MenuVis");
            }
        }

        private Visibility gameOvervis;
        public Visibility GameOverVis
        {
            get { return gameOvervis; }
            set
            {
                gameOvervis = value;
                OnPropertyChanged("GameOverVis");
            }
        }
        #endregion

        #region Colors
        /*Properties which get bound to the question box colors*/
        private System.Windows.Media.Brush bg1;
        public System.Windows.Media.Brush BG1
        {
            get { return bg1; }
            set 
            { 
                bg1 = value;
                OnPropertyChanged("BG1");
            }
        }

        private System.Windows.Media.Brush bg2;
        public System.Windows.Media.Brush BG2
        {
            get { return bg2; }
            set
            {
                bg2 = value;
                OnPropertyChanged("BG2");
            }
        }

        private System.Windows.Media.Brush bg3;
        public System.Windows.Media.Brush BG3
        {
            get { return bg3; }
            set
            {
                bg3 = value;
                OnPropertyChanged("BG3");
            }
        }

        private System.Windows.Media.Brush bg4;
        public System.Windows.Media.Brush BG4
        {
            get { return bg4; }
            set
            {
                bg4 = value;
                OnPropertyChanged("BG4");
            }
        }
        #endregion

        public bool GameOver { get; set; }

        private bool roundOver;
        public bool RoundOver
        {
            /*Stores if the round is over*/
            get { return roundOver; }
            set 
            { 
                roundOver = value;
                OnPropertyChanged("RoundOver");
            }
        }

        public static bool isActive = false;

        #region ComboBox
        private List<string> kindOfQuestionsComboBox;
        public List<string> KindOfQuestionsComboBox
        {
            /*The List of option which gets binded as the ItemSource to the ComboBoxes*/
            get { return kindOfQuestionsComboBox; }
            set 
            {
                kindOfQuestionsComboBox = value;
                OnPropertyChanged("KindOfQuestionComboBox");
            }
        }

        private List<string> amountOfQuestionsComboBox;
        public List<string> AmountOfQuestionsComboBox
        {
            /*The List of option which gets binded as the ItemSource to the ComboBoxes*/
            get { return amountOfQuestionsComboBox; }
            set
            {
                amountOfQuestionsComboBox = value;
                OnPropertyChanged("AmountOfQuestionsComboBox");
            }
        }

        /*The Result of the ComboBox for the amount of Questions*/
        public string AmountOfQuestionsComboBoxAnswer { get; set; }

        private string kindOfQuestionsComboBoxAnswer;
        public string KindOfQuestionsComboBoxAnswer
        {
            /*The Result of the ComboBox for the type of Questions*/
            get { return kindOfQuestionsComboBoxAnswer; }
            set 
            { 
                kindOfQuestionsComboBoxAnswer = value;
                OnPropertyChanged("KindOfQuestionsComboBoxAnswer");
            }
        }
        #endregion


        #region events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion


        public QuizzVM()
        {
            UserData = new UserData();
            initUserData();

            /*Setup commands*/
            ChooseAnswerCommand = new ChooseAnswerCommand(this);
            PlayGameCommand = new PlayGameCommand(this);
            NextQuestionCommand = new NextQuestionCommand(this);
            ToMenuWindowCommand = new ToMenuWindowCommand(this);
            ToInfoWindowCommand = new ToInfoWindowCommand(this);

            /*Setup Properties*/
            Questions = new ObservableCollection<Question>();

            /*Setup the values for the ComboBoxes*/
            KindOfQuestionsComboBox = new List<string> { "Any Category", "General Knowledge", "Books", "Films", "Music", "Musicals & Theatre",
                "Television", "Video Games", "Board Games", "Sience & Nature", "Computers", "Mathematics", "Mythology", "Sports", "Geography",
                "History", "Politics", "Art", "Celebrities", "Animals"};

            AmountOfQuestionsComboBox = new List<string> { "5", "10", "12", "15", "20", "25" };

            /*Setup the default colors of the question boxes*/
            BG1 = new SolidColorBrush(Colors.LightGray);
            BG2 = new SolidColorBrush(Colors.LightGray);
            BG3 = new SolidColorBrush(Colors.LightGray);
            BG4 = new SolidColorBrush(Colors.LightGray);

            /*Start with Menu window*/
            showMenuWindow();

            GameOver = false;
        }


        #region Answers
        public void rightAnswer()
        {
            /*Triggers events which should happen, after the uses choosed a right answer*/


            /*Lets the right Question get Green and the rest Red*/
            if (currentQuestionAnswers[0] == CurrentQuestion.correct_answer)
            {
                BG1 = new SolidColorBrush(Colors.Green);
                BG2 = new SolidColorBrush(Colors.Red);
                BG3 = new SolidColorBrush(Colors.Red);
                BG4 = new SolidColorBrush(Colors.Red);
            }
            else if(currentQuestionAnswers[1] == CurrentQuestion.correct_answer)
            {
                BG1 = new SolidColorBrush(Colors.Red);
                BG2 = new SolidColorBrush(Colors.Green);
                BG3 = new SolidColorBrush(Colors.Red);
                BG4 = new SolidColorBrush(Colors.Red);
            }
            else if (currentQuestionAnswers[2] == CurrentQuestion.correct_answer)
            {
                BG1 = new SolidColorBrush(Colors.Red);
                BG2 = new SolidColorBrush(Colors.Red);
                BG3 = new SolidColorBrush(Colors.Green);
                BG4 = new SolidColorBrush(Colors.Red);
            }
            else if (currentQuestionAnswers[3] == CurrentQuestion.correct_answer)
            {
                BG1 = new SolidColorBrush(Colors.Red);
                BG2 = new SolidColorBrush(Colors.Red);
                BG3 = new SolidColorBrush(Colors.Red);
                BG4 = new SolidColorBrush(Colors.Green);
            }


            Answer = "true";
            RoundOver = true;
            CorrectAnswers++;
        }
        
        public void wrongAnswer()
        {
            /*Triggers events which should happen, after the uses choosed a wrong answer*/


            /*Lets the right Question get Green and the rest Red*/
            if (currentQuestionAnswers[0] == CurrentQuestion.correct_answer)
            {
                BG1 = new SolidColorBrush(Colors.Green);
                BG2 = new SolidColorBrush(Colors.Red);
                BG3 = new SolidColorBrush(Colors.Red);
                BG4 = new SolidColorBrush(Colors.Red);
            }
            else if (currentQuestionAnswers[1] == CurrentQuestion.correct_answer)
            {
                BG1 = new SolidColorBrush(Colors.Red);
                BG2 = new SolidColorBrush(Colors.Green);
                BG3 = new SolidColorBrush(Colors.Red);
                BG4 = new SolidColorBrush(Colors.Red);
            }
            else if (currentQuestionAnswers[2] == CurrentQuestion.correct_answer)
            {
                BG1 = new SolidColorBrush(Colors.Red);
                BG2 = new SolidColorBrush(Colors.Red);
                BG3 = new SolidColorBrush(Colors.Green);
                BG4 = new SolidColorBrush(Colors.Red);
            }
            else if (currentQuestionAnswers[3] == CurrentQuestion.correct_answer)
            {
                BG1 = new SolidColorBrush(Colors.Red);
                BG2 = new SolidColorBrush(Colors.Red);
                BG3 = new SolidColorBrush(Colors.Red);
                BG4 = new SolidColorBrush(Colors.Green);
            }


            Answer = "false";
            RoundOver = true;
            WrongAnswers++;
        }

        public void resetAnswerColors()
        {
            /*Resets all Question Colors to their default color (e.g. if no questions is choosed yet)*/
            BG1 = new SolidColorBrush(Colors.LightGray);
            BG2 = new SolidColorBrush(Colors.LightGray);
            BG3 = new SolidColorBrush(Colors.LightGray);
            BG4 = new SolidColorBrush(Colors.LightGray);
        }

        private void setCurrentQuestionAnswers()
        {
            /*Shuffles The options for the questions from the api and sets them to a property where they get binded*/
            List<string> tempList = new List<string>();

            if (CurrentQuestion != null)
            {
                /*Fill up temp array with all the Options to a question*/
                for (int i = 0; i < CurrentQuestion.incorrect_answers.Count; i++)
                {
                    CurrentQuestion.incorrect_answers[i] = Utility.UnescapeXml(CurrentQuestion.incorrect_answers[i]);
                    CurrentQuestion.incorrect_answers[i] = Utility.UnescapeHtml(CurrentQuestion.incorrect_answers[i]);
                    tempList.Add(CurrentQuestion.incorrect_answers[i]);
                }

                CurrentQuestion.correct_answer = Utility.UnescapeXml(CurrentQuestion.correct_answer);
                CurrentQuestion.correct_answer = Utility.UnescapeHtml(CurrentQuestion.correct_answer);
                tempList.Add(CurrentQuestion.correct_answer);


                /*Shuffle the temp array*/
                Utility.Shuffle(tempList);

                /*Set the real array to be the temp array*/
                CurrentQuestionAnswers = tempList;
            }
        }
        #endregion

        #region showWindows
        public void showMenuWindow()
        {
            /*Switch to the Menu window*/
            GameVis = Visibility.Collapsed;
            MenuVis = Visibility.Visible;
            GameOverVis = Visibility.Collapsed;
        }

        public void showGameWindow()
        {
            /*switch to the Game window*/
            GameVis = Visibility.Visible;
            MenuVis = Visibility.Collapsed;
            GameOverVis = Visibility.Collapsed;
        }

        public async void showGameOverWindow()
        {
            /*Switch back to the GameOver window and does some calculations*/

            GameOver = true;

            double a = correctAnswers; //One at least, needs to be a double, so the result gets a double too
            int b = Questions.Count;
            
            double temp = (a / b) * 100;
            PercentageOfRightAnswers = temp;

            /*Adding up the TotalGamesPlayed score, but need to create new object so the NotifyTrigger gets triggered*/
            UserData tempUserData = new UserData
            {
                ParentUserId = UserData.ParentUserId,
                UniqueId = UserData.UniqueId,
                TotalGamesPlayed = UserData.TotalGamesPlayed
            };

            tempUserData.TotalGamesPlayed += 1;

            UserData = tempUserData;

            await DatabaseHelper.Update(UserData);

            GameVis = Visibility.Collapsed;
            MenuVis = Visibility.Collapsed;
            GameOverVis = Visibility.Visible;
        }
        #endregion


        public async void initUserData()
        {
            UserData = await DatabaseHelper.ReadUserData<UserData>(UserData);

            /*If user logs in for the first time, a new User gets created in the database*/
            if(UserData == null)
            {
                UserData = new UserData
                {
                    ParentUserId = App.UserId
                };

                await DatabaseHelper.Insert(UserData);
                UserData = await DatabaseHelper.ReadUserData<UserData>(UserData);
            }
        }


        public void resetAfterRound()
        {
            /*Resets everything which gets changed during a new round, back to its default*/
            CurrentQuestionIndex = 0;
            CorrectAnswers = 0;
            WrongAnswers = 0;
        }

        private void OnPropertyChanged(string propertyName)
        {
            /*Triggers the OnNotifyPropertyChanged event for the given Event*/
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
