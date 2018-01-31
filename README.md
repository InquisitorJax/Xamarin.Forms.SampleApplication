# Xamarin.Forms.SampleApplication
Example of typical Xamarin Forms application. Project to illustrate basic mobile design concepts for Xamarin Forms Project

## Core Concepts

#### Mvvm Design Pattern
Provider ViewModelBase which exposes INotifyPropertyChanged necessary for Xaml Binding 

#### Resource Navigation API
This allows the viewmodels to be more loosely coupled. 
Navigation is done via logical string values and parameters

#### Xamarin Forms Navigator
Navigator class which take the responsibility of wiring up a logical location to a View / ViewModel implementation.

#### Dependency Injection / Inversion of Control
Core for testability. Classes implement interfaces. Dependencies are injected via a IoC container

#### Event Aggregator
Handle inter-ViewModel communication via message events (event aggregator implementing weak events)

#### Validation
Simple model validation implementation

#### Common controls and converters
ListView that maps an item tap to a ViewModel command
Common converters: To be replaced by Wibci.Xamarin.Forms.Converters nuget

#### Testing
sample test implementations using NUnit and Moq
