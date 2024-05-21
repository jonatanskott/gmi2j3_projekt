
using GMI2J3_Projekt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GMI2J3_Projekt;
public class QuestionProvider : IQuestionProvider
{
    private IQuestionReader _questionReader;
    
    public QuestionProvider(IQuestionReader questionReader)
    {
        _questionReader = questionReader;
    }

    // Gets a set number of questions with full information. Used for practice quiz
    public QuestionSet<PracticeQuestion> GetFullQuestionSet(int num, string unit = "")
    {
        if(num > 20)
        {
            throw new Exception("Too many questions requested");
        } else if(num < 1)
        {
            throw new Exception("Must request at least one question");
        }

        ICollection<PracticeQuestion> questions = _questionReader.GetQuestions();

        List<PracticeQuestion> questionList = new List<PracticeQuestion>();

        foreach (PracticeQuestion question in questions)
        {
            List<PracticeOption> options = new List<PracticeOption>();

            foreach (PracticeOption option in question.Options)
            {
                options.Add(new PracticeOption(option.OptionId, option.OptionText, option.IsCorrect));
            }

            questionList.Add(new PracticeQuestion(question.QuestionId, question.QuestionText, options, question.Unit));
        }

        return new QuestionSet<PracticeQuestion>(questionList.FindAll(x => x.Unit.StartsWith(unit)).OrderBy(x => Random.Shared.Next()).Take(num).ToList(), Guid.NewGuid());
        
    }

    // Gets a set number of questions with only the core information, no correct answers provided. Used for test quiz
    public QuestionSet<TestQuestion> GetCoreQuestionSet(int num, string unit = "")
    {
        if (num > 20)
        {
            throw new Exception("Too many questions requested");
        }
        else if (num < 1)
        {
            throw new Exception("Must request at least one question");
        }

        ICollection<PracticeQuestion> questions = _questionReader.GetQuestions();

        List<TestQuestion> questionList = new List<TestQuestion>();

        foreach (PracticeQuestion question in questions)
        {
            List<TestOption> options = new List<TestOption>();

            foreach (PracticeOption option in question.Options)
            {
                options.Add(new TestOption(option.OptionId, option.OptionText));
            }

            // Sets the number of correct choices for the question from information in jsonquestion, since the information cant be found in coreoption
            questionList.Add(new TestQuestion(question));
        }

        return new QuestionSet<TestQuestion>(questionList.FindAll(x => x.Unit.StartsWith(unit)).OrderBy(x => Random.Shared.Next()).Take(num).ToList(), Guid.NewGuid());
    }

    internal ICollection<PracticeQuestion> GetAllQuestions()
    {
        return _questionReader.GetQuestions();
    }

    public PracticeQuestion? GetQuestionById(Guid id)
    {
        return _questionReader.GetQuestions().FirstOrDefault(x => x.QuestionId == id);
    }

}