using Autofac;
using GMI2J3_Projekt.Interfaces;

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


        Container = builder.Build();


        using var scope = Container.BeginLifetimeScope();

        var q = scope.Resolve<IQuizService>();
        //QuestionSet<PracticeQuestion> h = q.StartPracticeQuiz(9, "1.2");
        QuestionSet<TestQuestion> h = q.StartTestQuiz(9, "1.2");


        foreach (var question in h.Questions)
        {
            Console.WriteLine(question.Unit + question.QuestionText);
            foreach(var option in question.Options)
            {
                Console.WriteLine(option.OptionText);
            }
            Console.WriteLine();
        }


        Console.ReadLine();
    }

}
