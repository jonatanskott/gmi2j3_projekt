using GMI2J3_Projekt.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMI2J3_Projekt;
internal class TestEvaluator : ITestEvaluator
{
    IQuestionProvider _questionProvider;    
    public TestEvaluator(IQuestionProvider questionProvider)
    {

        _questionProvider = questionProvider;

    }

    public TestResult EvaluateTest(Dictionary<Guid, List<Guid>> answers)
    {
        int correctAnswers = 0;
        foreach (KeyValuePair<Guid, List<Guid>> kvp in answers)
        {
            PracticeQuestion correctQuestion = _questionProvider.GetQuestionById(kvp.Key) ?? throw new Exception("question not found");

            List<Guid> correctAnswer = correctQuestion.Options.Where(x => x.IsCorrect).Select(x => x.OptionId).ToList();

            if (kvp.Value == correctAnswer)
            {
                correctAnswers++;
            }
        }

        return new TestResult(correctAnswers, answers.Count);
    }
}
