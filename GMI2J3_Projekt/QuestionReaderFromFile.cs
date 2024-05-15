global using Newtonsoft.Json;
//global using System.Text.Json;
//global using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;

namespace GMI2J3_Projekt;
//public class QuestionReaderFromFile : IQuestionReader
//{
//    public ICollection<Question> GetQuestions()
//    {
//        string json = File.ReadAllText(@"C:\Users\jonat\Documents\Kurser\Software Testing 1\Project\questions.json");


//        Dictionary<string, List<Question>>? deserializedObject = JsonSerializer.Deserialize<Dictionary<string, List<Question>>>(json);
//        if (deserializedObject == null)
//        {
//            throw new Exception("Could not deserialize questions");
//        }

//        return deserializedObject["questions"];
//    }
//}

//Using Newtonsoft.Json instead of System.Text.Json because of the ability to use constructor with parameters during deserialization

public class QuestionReaderFromFile : IQuestionReader
{
    ICollection<PracticeQuestion> _questions;
    public QuestionReaderFromFile()
    {
        _questions = LoadQuestionsFromFile();
        
    }

    public ICollection<PracticeQuestion> GetQuestions()
    {
        return _questions;
    }

    private ICollection<PracticeQuestion> LoadQuestionsFromFile()
    {
        string json = File.ReadAllText("questions.json");

        Dictionary<string, List<PracticeQuestion>>? deserializedObject = JsonConvert.DeserializeObject<Dictionary<string, List<PracticeQuestion>>>(json);
        if (deserializedObject == null)
        {
            throw new Exception("Could not deserialize questions");
        }

        return deserializedObject["questions"];
    }
}

