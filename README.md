# Xamarin.Forms.SampleApplication
Example of typical Xamarin Forms application. Project to illustrate basic mobile design concepts for Xamarin Forms Project

##Core Concepts

####Mvvm Design Pattern
Provider ViewModelBase which exposes INotifyPropertyChanged necessary for Xaml Binding 

####Resource Navigation API
This allows the viewmodels to be more loosely coupled. 
Navigation is done via logical string values and parameters

####Dependency Injection / Inversion of Control
Core for testability. Classes implement interfaces. Dependencies are injected via a IoC container

####Event Aggregator
Handle inter-ViewModel communication via message events (event aggregator implementing weak events)

####Testing
sample test implementations using NUnit and Moq
