using System.Data;
using System.Text;
using SystemOfLinearEquationsSolvingAssistant.BL.SystemOfLinearEquationsSolving.Entities;
using SystemOfLinearEquationsSolvingAssistant.BL.SystemOfLinearEquationsSolving.Solvers;
using SystemOfLinearEquationsSolvingAssistant.ViewModels.Commands.Base;
using SystemOfLinearEquationsSolvingAssistant.ViewModels.Models.EventBusService.IntegrationEvents;
using SystemOfLinearEquationsSolvingAssistant.ViewModels.Services.Interfaces;
using SystemOfLinearEquationsSolvingAssistant.ViewModels.ViewModelsCollection.Base;

namespace SystemOfLinearEquationsSolvingAssistant.ViewModels.ViewModelsCollection;

public sealed class MainViewModel : ViewModel
{
    private const int MinDataDimension = 1;
    private const int MaxDataDimension = 999;
    private const int MinThreadsNumParallel = 2;
    private const int MaxThreadsNumParallel = 99;

    private readonly ISoleSolver _soleSolver;
    private readonly IViewManagerService _viewManagerService;
    private readonly IEventBusService _eventBusService;
    private readonly IUserDialogService _userDialogService;
    private readonly ISoleSolvingAlgorithmNameService _soleSolvingAlgorithmNameService;

    private int _currentDataDimension;
    private DataTable _dataTableMatrixA;
    private DataTable _dataTableVectorB;
    private string _solutionSet;
    private string[] _algorithmNamesSerial;
    private string[] _algorithmNamesParallel;
    private int _algorithmNamesSerialSelectedIndex;
    private int _algorithmNamesParallelSelectedIndex;
    private int _threadsNumParallel;
    private TimeSpan _elapsedTimeSerial;
    private TimeSpan _elapsedTimeParallel;
    private bool _isSolvingProcessEnded;

    public DataTable DataTableMatrixA
    {
        get => _dataTableMatrixA;
        set => Set(ref _dataTableMatrixA, value);
    }

    public DataTable DataTableVectorB
    {
        get => _dataTableVectorB;
        set => Set(ref _dataTableVectorB, value);
    }

    public string SolutionSet
    {
        get => _solutionSet;
        set => Set(ref _solutionSet, value);
    }

    public string[] AlgorithmNamesSerial
    {
        get => _algorithmNamesSerial;
        set => Set(ref _algorithmNamesSerial, value);
    }

    public string[] AlgorithmNamesParallel
    {
        get => _algorithmNamesParallel;
        set => Set(ref _algorithmNamesParallel, value);
    }

    public int AlgorithmNamesSerialSelectedIndex
    {
        get => _algorithmNamesSerialSelectedIndex;
        set => Set(ref _algorithmNamesSerialSelectedIndex, value);
    }

    public int AlgorithmNamesParallelSelectedIndex
    {
        get => _algorithmNamesParallelSelectedIndex;
        set => Set(ref _algorithmNamesParallelSelectedIndex, value);
    }

    public int ThreadsNumParallel
    {
        get => _threadsNumParallel;
        set => Set(ref _threadsNumParallel, value);
    }

    public TimeSpan ElapsedTimeSerial
    {
        get => _elapsedTimeSerial;
        set => Set(ref _elapsedTimeSerial, value);
    }

    public TimeSpan ElapsedTimeParallel
    {
        get => _elapsedTimeParallel;
        set => Set(ref _elapsedTimeParallel, value);
    }

    public bool IsSolvingProcessEnded
    {
        get => _isSolvingProcessEnded;
        set => Set(ref _isSolvingProcessEnded, value);
    }

    public RelayCommand<object?> AddDataDimensionCommand { get; }

    public RelayCommand<object?> RemoveDataDimensionCommand { get; }

    public RelayCommand<object?> ResetDataCommand { get; }

    public RelayCommand<object?> LoadFromFilesCommand { get; }

    public RelayCommand<object?> AddThreadParallelCommand { get; }

    public RelayCommand<object?> RemoveThreadParallelCommand { get; }

    public RelayCommand<object?> SolveSerialCommand { get; }

    public RelayCommand<object?> SolveParallelCommand { get; }

    public MainViewModel(ISoleSolver soleSolver, IViewManagerService viewManagerService, IEventBusService eventBusService,
        IUserDialogService userDialogService, ISoleSolvingAlgorithmNameService soleSolvingAlgorithmNameService)
    {
        if (soleSolver is null)
            throw new ArgumentNullException(nameof(soleSolver),
                "Sole solver must not be null.");

        if (viewManagerService is null)
            throw new ArgumentNullException(nameof(viewManagerService),
                "View manager service must not be null.");

        if (eventBusService is null)
            throw new ArgumentNullException(nameof(eventBusService),
                "Event bus service must not be null.");

        if (userDialogService is null)
            throw new ArgumentNullException(nameof(userDialogService),
                "User dialog service must not be null.");

        if (soleSolvingAlgorithmNameService is null)
            throw new ArgumentNullException(nameof(soleSolvingAlgorithmNameService),
                "Sole solving algorithm name service must not be null.");

        _soleSolver = soleSolver;
        _viewManagerService = viewManagerService;
        _eventBusService = eventBusService;
        _userDialogService = userDialogService;
        _soleSolvingAlgorithmNameService = soleSolvingAlgorithmNameService;

        _dataTableMatrixA = new DataTable();
        _dataTableVectorB = new DataTable();
        _solutionSet = "Click \"Solve\" to display the solution set.";
        _algorithmNamesSerial = _soleSolvingAlgorithmNameService.AlgorithmNamesSerial.ToArray();
        _algorithmNamesParallel = _soleSolvingAlgorithmNameService.AlgorithmNamesParallel.ToArray();
        _threadsNumParallel = MinThreadsNumParallel;
        _isSolvingProcessEnded = true;

        AddDataDimensionCommand = new RelayCommand<object?>
            (OnAddDataDimensionCommandExecute, CanAddDataDimensionCommandExecute, this);
        RemoveDataDimensionCommand = new RelayCommand<object?>
            (OnRemoveDataDimensionCommandExecute, CanRemoveDataDimensionCommandExecute, this);
        ResetDataCommand = new RelayCommand<object?>
            (OnResetDataCommandExecute, CanResetDataCommandExecute, this);
        LoadFromFilesCommand = new RelayCommand<object?>
            (OnLoadFromFilesCommandExecute, CanLoadFromFilesCommandExecute, this);
        AddThreadParallelCommand = new RelayCommand<object?>
            (OnAddThreadParallelCommandExecute, CanAddThreadParallelCommandExecute, this);
        RemoveThreadParallelCommand = new RelayCommand<object?>
            (OnRemoveThreadParallelCommandExecute, CanRemoveThreadParallelCommandExecute, this);
        SolveSerialCommand = new RelayCommand<object?>
            (OnSolveSerialCommandExecute, CanSolveSerialCommandExecute, this);
        SolveParallelCommand = new RelayCommand<object?>
            (OnSolveParallelCommandExecute, CanSolveParallelCommandExecute, this);

        _eventBusService.Subscribe<SoleLoadedIntegrationEvent>(OnSoleLoadedIntegrationEvent);

        ResetData();
    }

    private void OnAddDataDimensionCommandExecute() => ChangeDataDimension(1, true);

    private bool CanAddDataDimensionCommandExecute() => _currentDataDimension is not MaxDataDimension;

    private void OnRemoveDataDimensionCommandExecute() => ChangeDataDimension(1, false);

    private bool CanRemoveDataDimensionCommandExecute() => _currentDataDimension is not MinDataDimension;

    private void OnResetDataCommandExecute() => ResetData();

    private bool CanResetDataCommandExecute() => true;

    private void OnLoadFromFilesCommandExecute() =>
        _viewManagerService.ShowView("LoadingSoleFromFiles", "Main", true);

    private bool CanLoadFromFilesCommandExecute() => true;

    private void OnAddThreadParallelCommandExecute() => ThreadsNumParallel++;

    private bool CanAddThreadParallelCommandExecute() => ThreadsNumParallel is not MaxThreadsNumParallel;

    private void OnRemoveThreadParallelCommandExecute() => ThreadsNumParallel--;

    private bool CanRemoveThreadParallelCommandExecute() => ThreadsNumParallel is not MinThreadsNumParallel;

    private async void OnSolveSerialCommandExecute()
    {
        IsSolvingProcessEnded = false;

        Sole sole = GetSoleFromData();
        SoleSolvingAlgorithmSerial solvingAlgorithm = _soleSolvingAlgorithmNameService
            .GetAlgorithmByNameSerial(AlgorithmNamesSerial[AlgorithmNamesSerialSelectedIndex]);

        SoleSolvingResults solvingResults = await _soleSolver.SolveSerialAsync(sole, solvingAlgorithm);

        ElapsedTimeSerial = solvingResults.ElapsedTime;
        SetSolutionSet(solvingResults);

        IsSolvingProcessEnded = true;
    }

    private bool CanSolveSerialCommandExecute() =>
        (AlgorithmNamesSerialSelectedIndex >= 0) && (AlgorithmNamesSerialSelectedIndex < AlgorithmNamesSerial.Length);

    private async void OnSolveParallelCommandExecute()
    {
        IsSolvingProcessEnded = false;

        Sole sole = GetSoleFromData();
        SoleSolvingAlgorithmParallel solvingAlgorithm = _soleSolvingAlgorithmNameService
            .GetAlgorithmByNameParallel(AlgorithmNamesParallel[AlgorithmNamesParallelSelectedIndex]);

        SoleSolvingResults solvingResults = await _soleSolver.SolveParallelAsync(sole, solvingAlgorithm, ThreadsNumParallel);

        ElapsedTimeParallel = solvingResults.ElapsedTime;
        SetSolutionSet(solvingResults);

        IsSolvingProcessEnded = true;
    }

    private bool CanSolveParallelCommandExecute() =>
        (AlgorithmNamesParallelSelectedIndex >= 0) && (AlgorithmNamesParallelSelectedIndex < AlgorithmNamesParallel.Length);

    private void OnSoleLoadedIntegrationEvent(SoleLoadedIntegrationEvent soleLoadedIntegrationEvent)
    {
        Sole sole = soleLoadedIntegrationEvent.Sole;

        if (sole is null)
            return;

        if (sole.Dimension < MinDataDimension)
        {
            _userDialogService.ShowErrorMessage("The system of linear equations cannot be loaded because it is too small. " +
                $"Minimum table capacity: \"{MinDataDimension}\"");

            return;
        }

        if (sole.Dimension > MaxDataDimension)
        {
            _userDialogService.ShowErrorMessage("The system of linear equations cannot be loaded because it is too large. " +
                $"Maximum table capacity: \"{MaxDataDimension}\"");

            return;
        }

        DataTable dataTableMatrixA = new();
        DataTable dataTableVectorB = new();

        for (var i = 0; i < sole.Dimension; i++)
            dataTableMatrixA.Columns.Add((i + 1).ToString(), typeof(double)).DefaultValue = default(double);

        dataTableVectorB.Columns.Add("1", typeof(double)).DefaultValue = default(double);

        for (var i = 0; i < sole.Dimension; i++)
        {
            _ = dataTableMatrixA.Rows.Add();
            _ = dataTableVectorB.Rows.Add();
        }

        for (var i = 0; i < sole.Dimension; i++)
        {
            for (var j = 0; j < sole.Dimension; j++)
                dataTableMatrixA.Rows[i][j] = sole.A[i, j];

            dataTableVectorB.Rows[i][0] = sole.B[i];
        }

        _currentDataDimension = sole.Dimension;

        DataTableMatrixA = dataTableMatrixA;
        DataTableVectorB = dataTableVectorB;
    }

    private void ClearData()
    {
        DataTable dataTableMatrixA = new();
        DataTable dataTableVectorB = new();

        dataTableMatrixA.Columns.Add("1", typeof(double)).DefaultValue = default(double);
        dataTableVectorB.Columns.Add("1", typeof(double)).DefaultValue = default(double);

        _ = dataTableMatrixA.Rows.Add();
        _ = dataTableVectorB.Rows.Add();

        _currentDataDimension = 1;

        DataTableMatrixA = dataTableMatrixA;
        DataTableVectorB = dataTableVectorB;
    }

    private void ChangeDataDimension(int dimensionCount, bool isExpanding)
    {
        if (((isExpanding is true) && ((_currentDataDimension + dimensionCount) > MaxDataDimension)) ||
            ((isExpanding is false) && ((_currentDataDimension - dimensionCount) < MinDataDimension)))
            return;

        DataTable dataTableMatrixA = DataTableMatrixA.Copy();
        DataTable dataTableVectorB = DataTableVectorB.Copy();

        if (isExpanding is true)
        {
            for (var i = 0; i < dimensionCount; i++)
            {
                _currentDataDimension++;

                dataTableMatrixA.Columns
                    .Add(new DataColumn(_currentDataDimension.ToString(), typeof(double)) { DefaultValue = default(double) });

                _ = dataTableMatrixA.Rows.Add();
                _ = dataTableVectorB.Rows.Add();
            }
        }
        else
        {
            for (var i = 0; i < dimensionCount; i++)
            {
                _currentDataDimension--;

                dataTableMatrixA.Columns.RemoveAt(_currentDataDimension);

                dataTableMatrixA.Rows.RemoveAt(_currentDataDimension);
                dataTableVectorB.Rows.RemoveAt(_currentDataDimension);
            }
        }

        DataTableMatrixA = dataTableMatrixA;
        DataTableVectorB = dataTableVectorB;
    }

    private void ResetData()
    {
        ClearData();
        ChangeDataDimension(4, true);
    }

    private Sole GetSoleFromData()
    {
        double[,] a = new double[_currentDataDimension, _currentDataDimension];
        double[] b = new double[_currentDataDimension];

        for (var i = 0; i < _currentDataDimension; i++)
            for (var j = 0; j < _currentDataDimension; j++)
                a[i, j] = (double)DataTableMatrixA.Rows[i][j];

        for (var i = 0; i < _currentDataDimension; i++)
            b[i] = (double)DataTableVectorB.Rows[i][0];

        return new Sole(a, b);
    }

    private void SetSolutionSet(SoleSolvingResults solvingResults)
    {
        if (solvingResults.SolutionSet.Any() is true)
        {
            StringBuilder solutionSetStringBuilder = new();

            for (var i = 0; i < _currentDataDimension; i++)
                _ = solutionSetStringBuilder.Append($"x{i + 1}={solvingResults.SolutionSet[i]};   ");

            SolutionSet = solutionSetStringBuilder.ToString();
        }
        else
            SolutionSet = "This system of linear equations has no unique solutions.";
    }
}