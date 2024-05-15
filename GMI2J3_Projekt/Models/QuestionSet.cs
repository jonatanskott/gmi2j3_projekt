using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMI2J3_Projekt;
public class QuestionSet<T> where T : IQuestion
{
    public ICollection<T> Questions { get; private set; }
    public Guid QuestionContainerId { get; private set; }
    public QuizType Type { get; private set; }

    public QuestionSet(List<T> questions, Guid questionContainerId)
    {
        Questions = questions;
        QuestionContainerId = questionContainerId;
        Type = QuizType.Practice;
    }
}
