using SystemOfLinearEquationsSolvingAssistant.BL.Entities;
using SystemOfLinearEquationsSolvingAssistant.BL.Services.Entities.SoleParser;

namespace SystemOfLinearEquationsSolvingAssistant.BL.Services.Abstractions;

public interface ISoleParser
{
    void Initialize(string initializationString);

    Sole Parse(string pathA, string pathB, SoleParsingTemplate parsingTemplate);

    Task<Sole> ParseAsync(string pathA, string pathB, SoleParsingTemplate parsingTemplate);
}