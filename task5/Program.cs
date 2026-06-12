namespace task5
{

    enum Level
    {
        Easy,
        Medium,
        Hard
    }

    abstract class Question
    {
        public string Header { get; set; }
        public int Marks { get; set; }
        public Level Level { get; set; }

        public Question(string header, int marks, Level level)
        {
            Header = header;
            Marks = marks;
            Level = level;
        }

        public abstract void Display();

        public abstract bool CheckAnswer(string answer);
    }

    class TrueFalseQuestion : Question
    {
        public bool CorrectAnswer { get; set; }

        public TrueFalseQuestion(string header, int marks,
            Level level, bool correctAnswer)
            : base(header, marks, level)
        {
            CorrectAnswer = correctAnswer;
        }

        public override void Display()
        {
            Console.WriteLine(Header);
            Console.WriteLine("1- True");
            Console.WriteLine("2- False");
        }

        public override bool CheckAnswer(string answer)
        {
            bool userAnswer = answer == "1";
            return userAnswer == CorrectAnswer;
        }
    }

    class ChooseOneQuestion : Question
    {
        public string[] Choices { get; set; }

        public int CorrectChoice { get; set; }

        public ChooseOneQuestion(string header,
            int marks,
            Level level,
            string[] choices,
            int correctChoice)
            : base(header, marks, level)
        {
            Choices = choices;
            CorrectChoice = correctChoice;
        }

        public override void Display()
        {
            Console.WriteLine(Header);

            for (int i = 0; i < Choices.Length; i++)
            {
                Console.WriteLine($"{i + 1}- {Choices[i]}");
            }
        }

        public override bool CheckAnswer(string answer)
        {
            return int.Parse(answer) == CorrectChoice;
        }
    }
    class MultipleChoiceQuestion : Question
    {
        public string[] Choices { get; set; }
        public List<int> CorrectAnswers { get; set; }

        public MultipleChoiceQuestion(string header,
            int marks,
            Level level,
            string[] choices,
            List<int> correctAnswers)
            : base(header, marks, level)
        {
            Choices = choices;
            CorrectAnswers = correctAnswers;
        }

        public override void Display()
        {
            Console.WriteLine(Header);

            for (int i = 0; i < Choices.Length; i++)
            {
                Console.WriteLine($"{i + 1}- {Choices[i]}");
            }
        }

        public override bool CheckAnswer(string answer)
        {
            string[] arr = answer.Split(',');

            if (arr.Length != CorrectAnswers.Count)
            {
                return false;
            }

            for (int i = 0; i < arr.Length; i++)
            {
                int userAnswer = int.Parse(arr[i]);

                if (userAnswer != CorrectAnswers[i])
                {
                    return false;
                }
            }

            return true;
        }
    }

    internal class Program
    {
        static List<Question> QuestionBank = new List<Question>();

        static void DoctorMode()
        {
            Console.Write("Enter number of questions: ");
            int count = int.Parse(Console.ReadLine());

            for (int i = 0; i < count; i++)
            {
                Console.WriteLine("1- True/False");
                Console.WriteLine("2- Choose One");
                Console.WriteLine("3- Multiple Choice");

                int type = Convert.ToInt32(Console.ReadLine());

                Console.Write("Question: ");
                string header = Console.ReadLine();

                Console.Write("Marks: ");
                int marks = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("0-Easy");
                Console.WriteLine("1-Medium");
                Console.WriteLine("2-Hard");

                Level level = (Level)int.Parse(Console.ReadLine());

                switch (type)
                {
                    case 1:

                        Console.Write("Correct Answer (true or false): ");
                        bool answer = bool.Parse(Console.ReadLine());

                        QuestionBank.Add(
                            new TrueFalseQuestion(header, marks, level, answer));

                        break;

                    case 2:

                        string[] choices = new string[4];

                        for (int j = 0; j < 4; j++)
                        {
                            Console.Write($"Choice {j + 1}: ");
                            choices[j] = Console.ReadLine();
                        }

                        Console.Write("Correct Choice Number: ");
                        int correctChoice = Convert.ToInt32(Console.ReadLine());

                        QuestionBank.Add(
                            new ChooseOneQuestion(header, marks, level,
                            choices, correctChoice));

                        break;

                    case 3:

                        string[] mcChoices = new string[4];

                        for (int j = 0; j < 4; j++)
                        {
                            Console.Write($"Choice {j + 1}: ");
                            mcChoices[j] = Console.ReadLine();
                        }

                        Console.Write("How many correct answers? ");
                        int num = Convert.ToInt32(Console.ReadLine());

                        List<int> answers = new List<int>();

                        for (int j = 0; j < num; j++)
                        {
                            Console.Write($"Correct Answer {j + 1}: ");
                            answers.Add(int.Parse(Console.ReadLine()));
                        }

                        QuestionBank.Add(
                            new MultipleChoiceQuestion(header,
                            marks,
                            level,
                            mcChoices,
                            answers));

                        break;
                }
            }
        }
        static void StudentMode()
        {
            Console.WriteLine("1- Practical");
            Console.WriteLine("2- Final");

          
            int examType = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("0-Easy");
            Console.WriteLine("1-Medium");
            Console.WriteLine("2-Hard");

            Level level = (Level)int.Parse(Console.ReadLine());

            List<Question> examQuestions = new List<Question>();

            for (int i = 0; i < QuestionBank.Count; i++)
            {
                if (QuestionBank[i].Level == level)
                {
                    examQuestions.Add(QuestionBank[i]);
                }
            }

            int numberOfQuestions;

            if (examType == 1) 
            {
                numberOfQuestions = examQuestions.Count / 2;
            }
            else 
            {
                numberOfQuestions = examQuestions.Count;
            }

            int totalMarks = 0;
            int studentMarks = 0;

            for (int i = 0; i < numberOfQuestions; i++)
            {
                examQuestions[i].Display();

                Console.Write("Answer: ");
                string answer = Console.ReadLine();

                totalMarks += examQuestions[i].Marks;

                if (examQuestions[i].CheckAnswer(answer))
                {
                    studentMarks += examQuestions[i].Marks;
                }
            }

            Console.WriteLine("Your Result: "
                + studentMarks + " / " + totalMarks);
        }
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("1- Doctor Mode");
                Console.WriteLine("2- Student Mode");
                Console.WriteLine("3- Exit");

                int choice =Convert.ToInt32(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        DoctorMode();
                        break;

                    case 2:
                        StudentMode();
                        break;

                    case 3:
                        return;
                }
            }
        }
    }
}
