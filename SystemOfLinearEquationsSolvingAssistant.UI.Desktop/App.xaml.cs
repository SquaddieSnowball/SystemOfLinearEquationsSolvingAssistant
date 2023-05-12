﻿using System.Windows;

namespace SystemOfLinearEquationsSolvingAssistant.UI.Desktop;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e) => DependenciesConfiguration.Configure();
}