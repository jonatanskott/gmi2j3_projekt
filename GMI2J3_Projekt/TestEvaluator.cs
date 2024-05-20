using GMI2J3_Projekt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMI2J3_Projekt;
public class TestEvaluator : ITestEvaluator
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

            if (ListsAreEqual(kvp.Value, correctAnswer))
            {
                correctAnswers++;
            }
        }

        return new TestResult(correctAnswers, answers.Count);
    }

    // Comparing 2 lists if they contain same guids, must sort for sequenceequal to work
    private bool ListsAreEqual(List<Guid> l1, List<Guid> l2)
    {
        if(l1.Count != l2.Count)
        {
            return false;
        }

        l1.Sort();
        l2.Sort();

        return l1.SequenceEqual(l2);
    }
}
