﻿using SystemOfLinearEquationsSolvingAssistant.ViewModels.Commands.Base;
using SystemOfLinearEquationsSolvingAssistant.BL.Entities;
using SystemOfLinearEquationsSolvingAssistant.BL.Services.Abstractions;
using SystemOfLinearEquationsSolvingAssistant.BL.Services.Entities.SoleParser;
using SystemOfLinearEquationsSolvingAssistant.ViewModels.Services.Entities.EventBus.IntegrationEvents;
using SystemOfLinearEquationsSolvingAssistant.ViewModels.Services.Abstractions;
using SystemOfLinearEquationsSolvingAssistant.ViewModels.ViewModels.Base;

namespace SystemOfLinearEquationsSolvingAssistant.ViewModels.ViewModels;

public sealed class LoadingSoleFromFilesViewModel : ViewModel
{
    private readonly ISoleParser _soleParser;
    private readonly IEventBus _eventBus;
    private readonly IViewManager _viewManager;
    private readonly IUserDialogService _userDialogService;

    private string _title;
    private string _filePathMatrixA;
    private string _filePathVectorB;
    private string _decimalSeparator;
    private string _variableSeparator;
    private bool _isParsingProcessEnded;

    #region Properties

    public string Title
    {
        get => _title;
        set => Set(ref _title, value);
    }

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

    #endregion

    #region Commands

    public RelayCommand OpenFileMatrixACommand { get; }

    public RelayCommand OpenFileVectorBCommand { get; }

    public RelayCommand ConfirmLoadOptionsCommand { get; }

    #endregion

    public LoadingSoleFromFilesViewModel(ISoleParser soleParser, IEventBus eventBus,
        IViewManager viewManager, IUserDialogService userDialogService)
    {
        if (soleParser is null)
            throw new ArgumentNullException(nameof(soleParser), "Sole parser must not be null.");

        if (eventBus is null)
            throw new ArgumentNullException(nameof(eventBus), "Event bus must not be null.");

        if (viewManager is null)
            throw new ArgumentNullException(nameof(viewManager), "View manager must not be null.");

        if (userDialogService is null)
            throw new ArgumentNullException(nameof(userDialogService), "User dialog service must not be null.");

        _soleParser = soleParser;
        _eventBus = eventBus;
        _viewManager = viewManager;
        _userDialogService = userDialogService;

        _title = "Loading a system of linear equations from files";
        _filePathMatrixA = string.Empty;
        _filePathVectorB = string.Empty;
        _decimalSeparator = string.Empty;
        _variableSeparator = string.Empty;
        _isParsingProcessEnded = true;

        OpenFileMatrixACommand = new RelayCommand
            (OnOpenFileMatrixACommandExecute, CanOpenFileMatrixACommandExecute, this);
        OpenFileVectorBCommand = new RelayCommand
            (OnOpenFileVectorBCommandExecute, CanOpenFileVectorBCommandExecute, this);
        ConfirmLoadOptionsCommand = new RelayCommand
            (OnConfirmLoadOptionsCommandExecute, CanConfirmLoadOptionsCommandExecute, this);
    }

    #region Command components

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

        _eventBus.Publish(new SoleLoadedIntegrationEvent(sole));
        _viewManager.CloseView("LoadingSoleFromFiles");
    }

    private bool CanConfirmLoadOptionsCommandExecute() =>
        !((string.IsNullOrEmpty(FilePathMatrixA) is true) || (string.IsNullOrEmpty(FilePathVectorB) is true) ||
        (string.IsNullOrEmpty(DecimalSeparator) is true) || (string.IsNullOrEmpty(VariableSeparator) is true));

    #endregion
}