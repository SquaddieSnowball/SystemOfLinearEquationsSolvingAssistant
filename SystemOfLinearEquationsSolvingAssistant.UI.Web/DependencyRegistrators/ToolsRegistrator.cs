using SystemOfLinearEquationsSolvingAssistant.Dependencies;
using SystemOfLinearEquationsSolvingAssistant.Dependencies.Entities;
using SystemOfLinearEquationsSolvingAssistant.UI.Web.Extensions.Tools.Linking;
using SystemOfLinearEquationsSolvingAssistant.UI.Web.Extensions.Tools.Linking.Implementations;
using SystemOfLinearEquationsSolvingAssistant.UI.Web.Extensions.Tools.Parsing;
using SystemOfLinearEquationsSolvingAssistant.UI.Web.Extensions.Tools.Parsing.Implementations;

namespace SystemOfLinearEquationsSolvingAssistant.UI.Web.DependencyRegistrators;

internal static class ToolsRegistrator
{
    public static void Register()
    {
        DependenciesContainer.Register<IHtmlViewVisualsParser, HtmlViewVisualsParser>(DependencyObjectLifetime.Transient);
        DependenciesContainer.Register<IHtmlViewLinker, HtmlViewLinker>(DependencyObjectLifetime.Transient);
    }
}