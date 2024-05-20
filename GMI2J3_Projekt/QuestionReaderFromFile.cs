global using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;

namespace GMI2J3_Projekt;

public class QuestionReaderFromFile : IQuestionReader
{
    IFileReader _fileReader;
    ICollection<PracticeQuestion> _questions;
    public QuestionReaderFromFile(IFileReader fileReader)
    {
        _fileReader = fileReader;
        _questions = LoadQuestionsFromFile();
    }

    public ICollection<PracticeQuestion> GetQuestions()
    {
        return _questions;
    }

    public ICollection<PracticeQuestion> LoadQuestionsFromFile()
    {
        string json = _fileReader.ReadAllText("test_questions.json");

        Dictionary<string, List<PracticeQuestion>>? deserializedObject = JsonConvert.DeserializeObject<Dictionary<string, List<PracticeQuestion>>>(json);
        if (deserializedObject == null)
        {
            throw new Exception("Could not deserialize questions");
        }

        return deserializedObject["questions"];
    }


}

