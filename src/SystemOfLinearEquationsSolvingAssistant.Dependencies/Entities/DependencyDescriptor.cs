﻿namespace SystemOfLinearEquationsSolvingAssistant.Dependencies.Entities;

internal sealed class DependencyDescriptor
{
    public Type AbstractType { get; }

    public Type RealType { get; }

    public DependencyObjectLifetime Lifetime { get; }

    public object? DependencyObject { get; set; }

    public DependencyDescriptor(Type abstractType, Type realType, DependencyObjectLifetime lifetime) =>
        (AbstractType, RealType, Lifetime) =
        (abstractType, realType, lifetime);
}