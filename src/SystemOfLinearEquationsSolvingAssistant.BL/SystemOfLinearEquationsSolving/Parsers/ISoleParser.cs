using SystemOfLinearEquationsSolvingAssistant.BL.SystemOfLinearEquationsSolving.Entities;
using SystemOfLinearEquationsSolvingAssistant.BL.SystemOfLinearEquationsSolving.Parsers.Entities;

namespace SystemOfLinearEquationsSolvingAssistant.BL.SystemOfLinearEquationsSolving.Parsers;

public interface ISoleParser
{
    void Initialize(string initializationString);

    Sole Parse(string pathA, string pathB, SoleParsingTemplate parsingTemplate);

    Task<Sole> ParseAsync(string pathA, string pathB, SoleParsingTemplate parsingTemplate);
}