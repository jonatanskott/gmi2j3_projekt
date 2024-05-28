using GMI2J3_Projekt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMI2J3_Projekt;
public class QuizService : IQuizService
{
    private IQuestionProvider _questionProvider;
    private ITestEvaluator _testEvaluator;
    public QuizService(IQuestionProvider questionProvider, ITestEvaluator testEvaluator)
    {
        _questionProvider = questionProvider;
        _testEvaluator = testEvaluator;
    }

    public QuestionSet<TestQuestion> StartTestQuiz(int numberOfQuestions, string? unit="")
    {
        if (unit == null)
        {
            return _questionProvider.GetCoreQuestionSet(numberOfQuestions);
        }
        return _questionProvider.GetCoreQuestionSet(numberOfQuestions, unit);
    }
    public QuestionSet<PracticeQuestion> StartPracticeQuiz(int numberOfQuestions, string unit="")
    {
        return _questionProvider.GetFullQuestionSet(numberOfQuestions, unit);
    }

    // Submits a quiz, returns the score
    public TestResult SubmitQuiz(Dictionary<Guid, List<Guid>> answers)
    {
        return _testEvaluator.EvaluateTest(answers);
    }
}
