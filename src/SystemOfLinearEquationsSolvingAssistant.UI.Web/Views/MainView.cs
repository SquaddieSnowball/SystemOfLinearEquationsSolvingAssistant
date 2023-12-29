using SimpleHttpServer.Entities;
using SimpleHttpServer.Entities.Components;
using System.Text;
using System.Text.Json;
using System.Windows.Input;
using SystemOfLinearEquationsSolvingAssistant.BL.SystemOfLinearEquationsSolving.Entities;
using SystemOfLinearEquationsSolvingAssistant.UI.Web.Views.Base;
using SystemOfLinearEquationsSolvingAssistant.UI.Web.Views.Entities;
using SystemOfLinearEquationsSolvingAssistant.ViewModels.Entities.IntegrationEvents;
using SystemOfLinearEquationsSolvingAssistant.ViewModels.Services.Interfaces;

namespace SystemOfLinearEquationsSolvingAssistant.UI.Web.Views;

internal sealed class MainView : HtmlView
{
    private readonly IEventBusService _eventBusService;
    private readonly ISoleSolvingAlgorithmNameService _soleSolvingAlgorithmNameService;

    public override string PagePath => "index.html";

    public MainView(IEventBusService eventBusService, ISoleSolvingAlgorithmNameService soleSolvingAlgorithmNameService)
    {
        if (soleSolvingAlgorithmNameService is null)
        {
            throw new ArgumentNullException(nameof(soleSolvingAlgorithmNameService),
                "Sole solving algorithm name service must not be null.");
        }

        if (eventBusService is null)
        {
            throw new ArgumentNullException(nameof(eventBusService),
                "Event bus service must not be null.");
        }

        (_eventBusService, _soleSolvingAlgorithmNameService) = (eventBusService, soleSolvingAlgorithmNameService);

        GetHandler = req => HandleGet();
        PostHandler = req => HandlePost(req);
    }

    protected override void OnRelinkRequest(EventArgs e) => base.OnRelinkRequest(e);

    private void ValidateView()
    {
        if (Page is null)
        {
            throw new InvalidOperationException("This operation cannot be performed because " +
                "the page has not been initialized.");
        }

        if (ViewModel is null)
        {
            throw new InvalidOperationException("This operation cannot be performed because " +
                "the view model has not been initialized.");
        }

        if (HttpServerResponseGenerator is null)
        {
            throw new InvalidOperationException("This operation cannot be performed because " +
                "the HTTP server response generator has not been initialized.");
        }
    }

    private HttpResponse HandleGet()
    {
        ValidateView();

        return new(
            HttpResponseStatus.OK,
            new HttpHeader[]
            {
                new(HttpHeaderGroup.Response, "Content-Type", "text/html; charset=utf-8")
            },
            Encoding.UTF8.GetBytes(Page!));
    }

    private HttpResponse HandlePost(HttpRequest request)
    {
        ValidateView();

        try
        {
            HandleSoleSolving(request.Body!);
            OnRelinkRequest(EventArgs.Empty);
        }
        catch
        {
            return HttpServerResponseGenerator!.GenerateResponse(HttpResponseStatus.BadRequest);
        }

        return new HttpResponse(
            HttpResponseStatus.NoContent,
            Enumerable.Empty<HttpHeader>(),
            Encoding.UTF8.GetBytes(HttpResponseStatus.NoContent.ToString()));
    }

    private void HandleSoleSolving(IEnumerable<byte> solvingDataBytes)
    {
        SoleSolvingData solvingData = GetSoleSolvingData(solvingDataBytes);

        int dimension = solvingData.A.Length;
        double[,] a = new double[dimension, dimension];
        double[] b = new double[dimension];

        for (var i = 0; i < dimension; i++)
        {
            for (var j = 0; j < dimension; j++)
                a[i, j] = double.Parse(solvingData.A[i][j]);

            b[i] = double.Parse(solvingData.B[i]);
        }

        _eventBusService.Publish(new SoleLoadedIntegrationEvent(new Sole(a, b)));

        Type viewModelType = ViewModel!.GetType();

        if (solvingData.ThreadsNum is null)
        {
            viewModelType
                .GetProperty("AlgorithmNamesSerialSelectedItem")?
                .SetValue(ViewModel, solvingData.SolvingAlgorithm);

            ((ICommand?)viewModelType
                .GetProperty("SolveSerialCommand")?
                .GetValue(ViewModel))?
                .Execute(solvingData.SolvingAlgorithm);
        }
        else
        {
            viewModelType
                .GetProperty("AlgorithmNamesParallelSelectedItem")?
                .SetValue(ViewModel, solvingData.SolvingAlgorithm);

            viewModelType
                .GetProperty("ThreadsNumParallel")?
                .SetValue(ViewModel, solvingData.ThreadsNum);

            ((ICommand?)viewModelType
                .GetProperty("SolveParallelCommand")?
                .GetValue(ViewModel))?
                .Execute(solvingData.SolvingAlgorithm);
        }

        bool? isSolvingProcessEnded;

        do
        {
            isSolvingProcessEnded = (bool?)(
                viewModelType
                    .GetProperty("IsSolvingProcessEnded")?
                    .GetValue(ViewModel));
        }
        while (isSolvingProcessEnded is false);
    }

    private SoleSolvingData GetSoleSolvingData(IEnumerable<byte> solvingDataBytes)
    {
        if (solvingDataBytes is null)
            throw new ArgumentNullException(nameof(solvingDataBytes), "Solving data bytes must not be null.");

        SoleSolvingData? solvingData = JsonSerializer.Deserialize<SoleSolvingData>(
            solvingDataBytes.ToArray(),
            new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            });

        if ((solvingData is null) || (solvingData.A is null) ||
            (solvingData.B is null) || (solvingData.SolvingAlgorithm is null))
            throw new ArgumentException("The solving data bytes do not contain valid data.", nameof(solvingDataBytes));

        if (solvingData.ThreadsNum is null)
            _ = _soleSolvingAlgorithmNameService.GetAlgorithmByNameSerial(solvingData.SolvingAlgorithm);
        else
            _ = _soleSolvingAlgorithmNameService.GetAlgorithmByNameParallel(solvingData.SolvingAlgorithm);

        return solvingData;
    }
}