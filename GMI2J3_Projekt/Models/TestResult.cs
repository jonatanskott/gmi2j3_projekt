using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GMI2J3_Projekt;
public class TestResult
{
    int _correctAnswers;
    int _numOfQuestions;
    public TestResult()
    {
        
    }

    public TestResult(int correctAnswers, int questions)
    {
        _correctAnswers = correctAnswers;
        _numOfQuestions = questions;
    }

    public float GetScore()
    {
        return (float)_correctAnswers / _numOfQuestions;
    }

    public override string ToString()
    {
        return $"{_correctAnswers} correct out of {_numOfQuestions}";
    }
}
