using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMI2J3_Projekt;
public interface IQuizService
{
    QuestionSet<TestQuestion> StartTestQuiz(int num, string unit = "");
    QuestionSet<PracticeQuestion> StartPracticeQuiz(int num, string unit = "");
    TestResult SubmitQuiz(Dictionary<Guid, List<Guid>> answers);
}
