using Autofac;
using GMI2J3_Projekt;

namespace GMI2J3_Projekt;

internal class Program
{
    private static IContainer? Container { get; set; }
    static void Main(string[] args)
    {
        var builder = new ContainerBuilder();

        builder.RegisterType<QuestionProvider>().As<IQuestionProvider>().SingleInstance();
        builder.RegisterType<QuestionReaderFromFile>().As<IQuestionReader>();
        builder.RegisterType<QuizService>().As<IQuizService>();
        builder.RegisterType<TestEvaluator>().As<ITestEvaluator>();
        builder.RegisterType<FileReader>().As<IFileReader>();


        Container = builder.Build();


        using var scope = Container.BeginLifetimeScope();

        var q = scope.Resolve<IQuizService>();
        RunProgram(q);
    }

    static void RunProgram(IQuizService service)
    {
        Console.WriteLine("Welcome to quiz program\nSelect type: \n1: Practice\n2: Test");
        string? quizType = Console.ReadLine();
        Console.WriteLine("Select number of questions: ");
        string? num = Console.ReadLine();
        Console.WriteLine("Select unit: ");
        string? unit = Console.ReadLine();

        if(quizType == null || num == null)
        {
            Console.WriteLine("Invalid input");
            return;
        }
        Console.Clear();
        switch (quizType)
        {
            // If user selects practice quiz, then get questions and start quiz
            case "1":
                QuestionSet<PracticeQuestion> practiceSet = service.StartPracticeQuiz(int.Parse(num), unit);
                RunPracticeQuiz(practiceSet);
                break;
            // If user selects test quiz, then get questions and start quiz, then submit quiz to receive score
            case "2":
                QuestionSet<TestQuestion> testSet = service.StartTestQuiz(int.Parse(num), unit);
                Console.WriteLine(service.SubmitQuiz(RunTestQuiz(testSet)).ToString());
                break;
            default:
                Console.WriteLine("Invalid input entered. ");
                break;
        }
        Console.ReadLine();
    }

    private static Dictionary<Guid, List<Guid>> RunTestQuiz(QuestionSet<TestQuestion> testSet)
    {
        // Prints out all the questions and options
        for (int i = 1; i <= testSet.Questions.Count; i++)
        {
            TestQuestion question = testSet.Questions.ElementAt(i-1);
            Console.WriteLine($"{i}: {question.Unit} {question.QuestionText}\n");
            for (int j = 1; j <= question.Options.Count; j++)
            {
                Console.WriteLine($"{j}: {question.Options.ElementAt(j-1).OptionText}");
            }
            if(question.IsMultipleChoice)
            {
                Console.WriteLine("\nMultiple choice question\n");
            }
            Console.WriteLine("----------------------------------");
        }

        // Collects and saves the answers from the user
        Dictionary<Guid, List<Guid>> answers = new Dictionary<Guid, List<Guid>>();
        for (int i = 1; i <= testSet.Questions.Count; i++)
        {
            Console.WriteLine($"Enter the number of the correct answer for question {i}");
            string? answer = Console.ReadLine() ?? throw new Exception("Invalid input");

            List<int> choices = answer.Split(",").Select(int.Parse).ToList();
            answers.Add(testSet.Questions.ElementAt(i-1).QuestionId, choices.Select(x => testSet.Questions.ElementAt(i-1).Options.ElementAt(x-1).OptionId).ToList());
        }
        return answers;
    }

    private static void RunPracticeQuiz(QuestionSet<PracticeQuestion> practiceSet)
    {
        // Prints one question at a time
        for (int i = 1; i <= practiceSet.Questions.Count; i++)
        {
            PracticeQuestion question = practiceSet.Questions.ElementAt(i-1);
            Console.WriteLine($"{i}: {question.Unit} {question.QuestionText}\n");
            for (int j = 1; j <= question.Options.Count; j++)
            {
                Console.WriteLine($"{j}: {question.Options.ElementAt(j-1).OptionText}");
            }
            if (question.IsMultipleChoice)
            {
                Console.WriteLine("\nMultiple choice question\n\n");
            }

            // Collects and checks the answer
            Console.Write("Enter answer: ");
            string? answer = Console.ReadLine();
            if (answer == null)
            {
                Console.WriteLine("Invalid input");
                return;
            }
            // User can input multiple answers separated by commas
            List<int> choices = answer.Split(",").Select(int.Parse).ToList();
            // Awful looking way to get the correct answers, but it works. No more time will be spent here.
            List<Guid> correctAnswers = question.Options.Where(x => x.IsCorrect).Select(x => x.OptionId).ToList();
            if (choices.Select(x => question.Options.ElementAt(x-1).OptionId).SequenceEqual(correctAnswers))
            {
                Console.WriteLine("Correct answer");
            }
            else
            {
                Console.WriteLine("Incorrect answer");
                Console.WriteLine("The correct answer was:");
                foreach (var correctAnswer in correctAnswers)
                {
                    Console.WriteLine(question.Options.ToList().Find(x => x.OptionId == correctAnswer).OptionText);
                }
            }
            Console.WriteLine("----------------------------------");
        }
    }
}
