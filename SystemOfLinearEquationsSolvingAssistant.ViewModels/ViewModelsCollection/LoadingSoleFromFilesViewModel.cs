using SystemOfLinearEquationsSolvingAssistant.BL.SystemOfLinearEquationsSolving.Entities;
using SystemOfLinearEquationsSolvingAssistant.BL.SystemOfLinearEquationsSolving.Parsers.Entities;
using SystemOfLinearEquationsSolvingAssistant.BL.SystemOfLinearEquationsSolving.Parsers;
using SystemOfLinearEquationsSolvingAssistant.ViewModels.Commands.Base;
using SystemOfLinearEquationsSolvingAssistant.ViewModels.Services.Interfaces;
using SystemOfLinearEquationsSolvingAssistant.ViewModels.ViewModelsCollection.Base;
using SystemOfLinearEquationsSolvingAssistant.ViewModels.Entities.IntegrationEvents;

namespace SystemOfLinearEquationsSolvingAssistant.ViewModels.ViewModelsCollection;

public sealed class LoadingSoleFromFilesViewModel : ViewModel
{
    private readonly ISoleParser _soleParser;
    private readonly IEventBusService _eventBusService;
    private readonly IViewManagerService _viewManagerService;
    private readonly IUserDialogService _userDialogService;

    private string _filePathMatrixA;
    private string _filePathVectorB;
    private string _decimalSeparator;
    private string _variableSeparator;
    private bool _isParsingProcessEnded;

    public string FilePathMatrixA
    {
        get => _filePathMatrixA;
        set => Set(ref _filePathMatrixA, value);
    }

    public string FilePathVectorB
    {
        get => _filePathVectorB;
        set => Set(ref _filePathVectorB, value);
    }

    public string DecimalSeparator
    {
        get => _decimalSeparator;
        set => Set(ref _decimalSeparator, value);
    }

    public string VariableSeparator
    {
        get => _variableSeparator;
        set => Set(ref _variableSeparator, value);
    }

    public bool IsParsingProcessEnded
    {
        get => _isParsingProcessEnded;
        set => Set(ref _isParsingProcessEnded, value);
    }

    public RelayCommand<object?> OpenFileMatrixACommand { get; }

    public RelayCommand<object?> OpenFileVectorBCommand { get; }

    public RelayCommand<object?> ConfirmLoadOptionsCommand { get; }

    public LoadingSoleFromFilesViewModel(ISoleParser soleParser, IEventBusService eventBusService,
        IViewManagerService viewManagerService, IUserDialogService userDialogService)
    {
        if (soleParser is null)
            throw new ArgumentNullException(nameof(soleParser),
                "Sole parser must not be null.");

        if (eventBusService is null)
            throw new ArgumentNullException(nameof(eventBusService),
                "Event bus service must not be null.");

        if (viewManagerService is null)
            throw new ArgumentNullException(nameof(viewManagerService),
                "View manager service must not be null.");

        if (userDialogService is null)
            throw new ArgumentNullException(nameof(userDialogService),
                "User dialog service must not be null.");

        _soleParser = soleParser;
        _eventBusService = eventBusService;
        _viewManagerService = viewManagerService;
        _userDialogService = userDialogService;

        _filePathMatrixA = string.Empty;
        _filePathVectorB = string.Empty;
        _decimalSeparator = string.Empty;
        _variableSeparator = string.Empty;
        _isParsingProcessEnded = true;

        OpenFileMatrixACommand = new RelayCommand<object?>
            (OnOpenFileMatrixACommandExecute, CanOpenFileMatrixACommandExecute, this);
        OpenFileVectorBCommand = new RelayCommand<object?>
            (OnOpenFileVectorBCommandExecute, CanOpenFileVectorBCommandExecute, this);
        ConfirmLoadOptionsCommand = new RelayCommand<object?>
            (OnConfirmLoadOptionsCommandExecute, CanConfirmLoadOptionsCommandExecute, this);
    }

    private void OnOpenFileMatrixACommandExecute()
    {
        string filePathMatrixA = _userDialogService.ShowOpenFileDialog("Text files(*.txt)|*.txt", "Path to matrix A");

        if (string.IsNullOrEmpty(filePathMatrixA) is false)
            FilePathMatrixA = filePathMatrixA;
    }

    private bool CanOpenFileMatrixACommandExecute() => true;

    private void OnOpenFileVectorBCommandExecute()
    {
        string filePathVectorB = _userDialogService.ShowOpenFileDialog("Text files(*.txt)|*.txt", "Path to vector B");

        if (string.IsNullOrEmpty(filePathVectorB) is false)
            FilePathVectorB = filePathVectorB;
    }

    private bool CanOpenFileVectorBCommandExecute() => true;

    private async void OnConfirmLoadOptionsCommandExecute()
    {
        IsParsingProcessEnded = false;

        Sole sole;

        try
        {
            sole = await _soleParser.ParseAsync(FilePathMatrixA, FilePathVectorB,
                new SoleParsingTemplate(DecimalSeparator, VariableSeparator));
        }
        catch (Exception ex)
        {
            _userDialogService.ShowErrorMessage(ex.Message);

            return;
        }
        finally
        {
            IsParsingProcessEnded = true;
        }

        _eventBusService.Publish(new SoleLoadedIntegrationEvent(sole));
        _viewManagerService.CloseView("LoadingSoleFromFilesWindow");
    }

    private bool CanConfirmLoadOptionsCommandExecute() =>
        !(string.IsNullOrEmpty(FilePathMatrixA) || string.IsNullOrEmpty(FilePathVectorB) ||
        string.IsNullOrEmpty(DecimalSeparator) || string.IsNullOrEmpty(VariableSeparator));
}