using System.Data;
using System.Text;
using SystemOfLinearEquationsSolvingAssistant.BL.Entities;
using SystemOfLinearEquationsSolvingAssistant.BL.Services.Abstractions;
using SystemOfLinearEquationsSolvingAssistant.BL.Services.Entities.SoleSolver;
using SystemOfLinearEquationsSolvingAssistant.ViewModels.Commands.Base;
using SystemOfLinearEquationsSolvingAssistant.ViewModels.Services.Abstractions;
using SystemOfLinearEquationsSolvingAssistant.ViewModels.Services.Entities.EventBus.IntegrationEvents;
using SystemOfLinearEquationsSolvingAssistant.ViewModels.ViewModels.Base;

namespace SystemOfLinearEquationsSolvingAssistant.ViewModels.ViewModels;

/// <summary>
/// Represents the model for the "Main" view.
/// </summary>
public sealed class MainViewModel : ViewModel
{
    private const int MinDataDimension = 1;
    private const int MaxDataDimension = 999;
    private const int MinThreadsNumParallel = 2;
    private const int MaxThreadsNumParallel = 99;

    private readonly ISoleSolver _soleSolver;
    private readonly IEventBus _eventBus;
    private readonly ISoleSolvingAlgorithmNameService _soleSolvingAlgorithmNameService;
    private readonly IViewManager _viewManager;
    private readonly IUserDialogService _userDialogService;

    private int _currentDataDimension;
    private string _title;
    private DataTable _dataTableMatrixA;
    private DataTable _dataTableVectorB;
    private string _solutionSet;
    private string[] _algorithmNamesSerial;
    private string[] _algorithmNamesParallel;
    private string _algorithmNamesSerialSelectedItem;
    private string _algorithmNamesParallelSelectedItem;
    private int _threadsNumParallel;
    private TimeSpan _elapsedTimeSerial;
    private TimeSpan _elapsedTimeParallel;
    private bool _isSolvingProcessEnded;

    #region Properties

    /// <summary>
    /// Gets or sets the title of the view.
    /// </summary>
    public string Title
    {
        get => _title;
        set => Set(ref _title, value);
    }

    /// <summary>
    /// Gets or sets the data table with matrix A.
    /// </summary>
    public DataTable DataTableMatrixA
    {
        get => _dataTableMatrixA;
        set => Set(ref _dataTableMatrixA, value);
    }

    /// <summary>
    /// Gets or sets the data table with vector B.
    /// </summary>
    public DataTable DataTableVectorB
    {
        get => _dataTableVectorB;
        set => Set(ref _dataTableVectorB, value);
    }

    /// <summary>
    /// Gets or sets the solution set.
    /// </summary>
    public string SolutionSet
    {
        get => _solutionSet;
        set => Set(ref _solutionSet, value);
    }

    /// <summary>
    /// Gets or sets the serial algorithm names.
    /// </summary>
    public string[] AlgorithmNamesSerial
    {
        get => _algorithmNamesSerial;
        set => Set(ref _algorithmNamesSerial, value);
    }

    /// <summary>
    /// Gets or sets the parallel algorithm names.
    /// </summary>
    public string[] AlgorithmNamesParallel
    {
        get => _algorithmNamesParallel;
        set => Set(ref _algorithmNamesParallel, value);
    }

    /// <summary>
    /// Gets or sets the selected serial algorithm name.
    /// </summary>
    public string AlgorithmNamesSerialSelectedItem
    {
        get => _algorithmNamesSerialSelectedItem;
        set => Set(ref _algorithmNamesSerialSelectedItem, value);
    }

    /// <summary>
    /// Gets or sets the selected parallel algorithm name.
    /// </summary>
    public string AlgorithmNamesParallelSelectedItem
    {
        get => _algorithmNamesParallelSelectedItem;
        set => Set(ref _algorithmNamesParallelSelectedItem, value);
    }

    /// <summary>
    /// Gets or sets the number of threads.
    /// </summary>
    public int ThreadsNumParallel
    {
        get => _threadsNumParallel;
        set => Set(ref _threadsNumParallel, value);
    }

    /// <summary>
    /// Gets or sets the elapsed time during a serial solution.
    /// </summary>
    public TimeSpan ElapsedTimeSerial
    {
        get => _elapsedTimeSerial;
        set => Set(ref _elapsedTimeSerial, value);
    }

    /// <summary>
    /// Gets or sets the elapsed time during a parallel solution.
    /// </summary>
    public TimeSpan ElapsedTimeParallel
    {
        get => _elapsedTimeParallel;
        set => Set(ref _elapsedTimeParallel, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether the solving process has ended.
    /// </summary>
    public bool IsSolvingProcessEnded
    {
        get => _isSolvingProcessEnded;
        set => Set(ref _isSolvingProcessEnded, value);
    }

    #endregion

    #region Commands

    /// <summary>
    /// Adds a data dimension.
    /// </summary>
    public RelayCommandGeneric<int> AddDataDimensionsCommand { get; }

    /// <summary>
    /// Removes a data dimension.
    /// </summary>
    public RelayCommandGeneric<int> RemoveDataDimensionsCommand { get; }

    /// <summary>
    /// Resets data.
    /// </summary>
    public RelayCommandGeneric<int> ResetDataCommand { get; }

    /// <summary>
    /// Opens a view for loading system of linear equations from files.
    /// </summary>
    public RelayCommand LoadFromFilesCommand { get; }

    /// <summary>
    /// Adds a thread.
    /// </summary>
    public RelayCommandGeneric<int> AddThreadsParallelCommand { get; }

    /// <summary>
    /// Removes a thread.
    /// </summary>
    public RelayCommandGeneric<int> RemoveThreadsParallelCommand { get; }

    /// <summary>
    /// Solves a system of linear equations serially.
    /// </summary>
    public RelayCommandGeneric<string> SolveSerialCommand { get; }

    /// <summary>
    /// Solves a system of linear equations parallelly.
    /// </summary>
    public RelayCommandGeneric<string> SolveParallelCommand { get; }

    #endregion

    /// <summary>
    /// Initializes a new instance of <see cref="MainViewModel"/> with the specified services.
    /// </summary>
    /// <param name="soleSolver"><see cref="ISoleSolver"/> instance.</param>
    /// <param name="eventBus"><see cref="IEventBus"/> instance.</param>
    /// <param name="soleSolvingAlgorithmNameService"><see cref="ISoleSolvingAlgorithmNameService"/> instance.</param>
    /// <param name="viewManager"><see cref="IViewManager"/> instance.</param>
    /// <param name="userDialogService"><see cref="IUserDialogService"/> instance.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public MainViewModel(ISoleSolver soleSolver, IEventBus eventBus,
        ISoleSolvingAlgorithmNameService soleSolvingAlgorithmNameService,
        IViewManager viewManager, IUserDialogService userDialogService)
    {
        if (soleSolver is null)
            throw new ArgumentNullException(nameof(soleSolver),
                "Sole solver must not be null.");

        if (eventBus is null)
            throw new ArgumentNullException(nameof(eventBus),
                "Event bus must not be null.");

        if (soleSolvingAlgorithmNameService is null)
            throw new ArgumentNullException(nameof(soleSolvingAlgorithmNameService),
                "Sole solving algorithm name service must not be null.");

        if (viewManager is null)
            throw new ArgumentNullException(nameof(viewManager),
                "View manager must not be null.");

        if (userDialogService is null)
            throw new ArgumentNullException(nameof(userDialogService),
                "User dialog service must not be null.");

        _soleSolver = soleSolver;
        _eventBus = eventBus;
        _soleSolvingAlgorithmNameService = soleSolvingAlgorithmNameService;
        _viewManager = viewManager;
        _userDialogService = userDialogService;

        _title = "System of linear equations solving assistant";
        _dataTableMatrixA = new DataTable();
        _dataTableVectorB = new DataTable();
        _solutionSet = "Click \"Solve\" to display the solution set.";
        _algorithmNamesSerial = _soleSolvingAlgorithmNameService.AlgorithmNamesSerial.ToArray();
        _algorithmNamesParallel = _soleSolvingAlgorithmNameService.AlgorithmNamesParallel.ToArray();
        _algorithmNamesSerialSelectedItem = _algorithmNamesSerial.First();
        _algorithmNamesParallelSelectedItem = _algorithmNamesParallel.First();
        _threadsNumParallel = MinThreadsNumParallel;
        _isSolvingProcessEnded = true;

        AddDataDimensionsCommand = new RelayCommandGeneric<int>
            (OnAddDataDimensionsCommandExecute, CanAddDataDimensionsCommandExecute, this);
        RemoveDataDimensionsCommand = new RelayCommandGeneric<int>
            (OnRemoveDataDimensionsCommandExecute, CanRemoveDataDimensionsCommandExecute, this);
        ResetDataCommand = new RelayCommandGeneric<int>
            (OnResetDataCommandExecute, CanResetDataCommandExecute, this);
        LoadFromFilesCommand = new RelayCommand
            (OnLoadFromFilesCommandExecute, CanLoadFromFilesCommandExecute, this);
        AddThreadsParallelCommand = new RelayCommandGeneric<int>
            (OnAddThreadsParallelCommandExecute, CanAddThreadsParallelCommandExecute, this);
        RemoveThreadsParallelCommand = new RelayCommandGeneric<int>
            (OnRemoveThreadsParallelCommandExecute, CanRemoveThreadsParallelCommandExecute, this);
        SolveSerialCommand = new RelayCommandGeneric<string>
            (OnSolveSerialCommandExecute, CanSolveSerialCommandExecute, this);
        SolveParallelCommand = new RelayCommandGeneric<string>
            (OnSolveParallelCommandExecute, CanSolveParallelCommandExecute, this);

        _eventBus.Subscribe<SoleLoadedIntegrationEvent>(OnSoleLoadedIntegrationEvent);

        ResetData(5);
    }

    #region Command components

    private void OnAddDataDimensionsCommandExecute(int dimensionsCount)
    {
        if (CanAddDataDimensionsCommandExecute(dimensionsCount) is false)
            return;

        ChangeDataDimension(dimensionsCount, true);
    }

    private bool CanAddDataDimensionsCommandExecute(int dimensionsCount) =>
        !((dimensionsCount < 1) || (_currentDataDimension + dimensionsCount > MaxDataDimension));

    private void OnRemoveDataDimensionsCommandExecute(int dimensionsCount)
    {
        if (CanRemoveDataDimensionsCommandExecute(dimensionsCount) is false)
            return;

        ChangeDataDimension(dimensionsCount, false);
    }

    private bool CanRemoveDataDimensionsCommandExecute(int dimensionsCount) =>
        !((dimensionsCount < 1) || (_currentDataDimension - dimensionsCount < MinDataDimension));

    private void OnResetDataCommandExecute(int dimensionsCount)
    {
        if (CanResetDataCommandExecute(dimensionsCount) is false)
            return;

        ResetData(dimensionsCount);
    }

    private bool CanResetDataCommandExecute(int dimensionsCount) =>
        dimensionsCount is >= MinDataDimension and <= MaxDataDimension;

    private void OnLoadFromFilesCommandExecute() => _viewManager.ShowView("LoadingSoleFromFiles", "Main", true);

    private bool CanLoadFromFilesCommandExecute() => true;

    private void OnAddThreadsParallelCommandExecute(int threadsNum)
    {
        if (CanAddThreadsParallelCommandExecute(threadsNum) is false)
            return;

        ThreadsNumParallel += threadsNum;
    }

    private bool CanAddThreadsParallelCommandExecute(int threadsNum) =>
        !((threadsNum < 1) || (ThreadsNumParallel + threadsNum > MaxThreadsNumParallel));

    private void OnRemoveThreadsParallelCommandExecute(int threadsNum)
    {
        if (CanRemoveThreadsParallelCommandExecute(threadsNum) is false)
            return;

        ThreadsNumParallel -= threadsNum;
    }

    private bool CanRemoveThreadsParallelCommandExecute(int threadsNum) =>
        !((threadsNum < 1) || (ThreadsNumParallel - threadsNum < MinThreadsNumParallel));

    private async void OnSolveSerialCommandExecute(string? algorithmName)
    {
        if (CanSolveSerialCommandExecute(algorithmName) is false)
            return;

        IsSolvingProcessEnded = false;
        Sole sole = GetSoleFromData();
        SoleSolvingAlgorithmSerial solvingAlgorithm =
            _soleSolvingAlgorithmNameService.GetAlgorithmByNameSerial(algorithmName!);

        SoleSolvingResults solvingResults = await _soleSolver.SolveSerialAsync(sole, solvingAlgorithm);

        ElapsedTimeSerial = solvingResults.ElapsedTime;
        SetSolutionSet(solvingResults);
        IsSolvingProcessEnded = true;
    }

    private bool CanSolveSerialCommandExecute(string? algorithmName) => AlgorithmNamesSerial.Contains(algorithmName);

    private async void OnSolveParallelCommandExecute(string? algorithmName)
    {
        if (CanSolveParallelCommandExecute(algorithmName) is false)
            return;

        IsSolvingProcessEnded = false;
        Sole sole = GetSoleFromData();
        SoleSolvingAlgorithmParallel solvingAlgorithm =
            _soleSolvingAlgorithmNameService.GetAlgorithmByNameParallel(algorithmName!);

        SoleSolvingResults solvingResults = await _soleSolver.SolveParallelAsync(sole, solvingAlgorithm, ThreadsNumParallel);

        ElapsedTimeParallel = solvingResults.ElapsedTime;
        SetSolutionSet(solvingResults);
        IsSolvingProcessEnded = true;
    }

    private bool CanSolveParallelCommandExecute(string? algorithmName) => AlgorithmNamesParallel.Contains(algorithmName);

    #endregion

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

    private void ChangeDataDimension(int dimensionsCount, bool isExpanding)
    {
        if (((isExpanding is true) && (_currentDataDimension + dimensionsCount > MaxDataDimension)) ||
            ((isExpanding is false) && (_currentDataDimension - dimensionsCount < MinDataDimension)))
            return;

        DataTable dataTableMatrixA = DataTableMatrixA.Copy();
        DataTable dataTableVectorB = DataTableVectorB.Copy();

        if (isExpanding is true)
        {
            for (var i = 0; i < dimensionsCount; i++)
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
            for (var i = 0; i < dimensionsCount; i++)
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

    private void ResetData(int dimensionsCount)
    {
        ClearData();
        ChangeDataDimension(dimensionsCount - 1, true);
    }

    private Sole GetSoleFromData()
    {
        double[,] a = new double[_currentDataDimension, _currentDataDimension];
        double[] b = new double[_currentDataDimension];

        for (var i = 0; i < _currentDataDimension; i++)
        {
            for (var j = 0; j < _currentDataDimension; j++)
                a[i, j] = (double)DataTableMatrixA.Rows[i][j];

            b[i] = (double)DataTableVectorB.Rows[i][0];
        }

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