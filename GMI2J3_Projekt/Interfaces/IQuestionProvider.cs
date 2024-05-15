using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMI2J3_Projekt;
public interface IQuestionProvider
{
    QuestionSet<TestQuestion> GetCoreQuestionSet(int num, string unit = "");
    QuestionSet<PracticeQuestion> GetFullQuestionSet(int num, string unit = "");
    PracticeQuestion? GetQuestionById(Guid id);
}
