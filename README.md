# FileOnQ.Prism.Popups.XCT
A community plugin for Prism Library built by and maintained by FileOnQ.

FileOnQ.Prism.Popups.XCT is a Prism Library plugin that adds Dialog API support to the Xamarin Community Toolkit Popup Control. 

[![CI Build](https://github.com/FileOnQ/Prism.Popups.XCT/actions/workflows/dotnet.yml/badge.svg)](https://github.com/FileOnQ/Prism.Popups.XCT/actions/workflows/dotnet.yml)

## Status
This code is used by FileOnQ for our core mobile applications and we are committed to supporting this plugin. We have been using this plugin since 2019 with the original implementation of XCT Popups that was made by [Andrew Hoefling](https://github.com/ahoefling).

The project is currently in the initial phase of transitioning the internal library to an open source project for the community. The table below shows the features we want to ship for the first stable release of this plugin.

**1st Release Checklist**

| Phase             | Status |
|-------------------|--------|
| Initial Code Drop | ✅ Done|
| ContentView       | ✅ Done|
| DialogParameters Helpers | ✅ Done|
| Attached Properties | Not Started |
| Prism 8.1         | ✅ Done|
| Prism 8.0         | Not Started |
| Prism 7.2         | Not Started |
| Prism 7.1         | Not Started |
| NuGet Nightly     | Not Started |
| NuGet Releases    | Not Started |

## Setup
Add the NuGet to all shared code and platform code.

TODO - Add NuGet and Nightly NuGet

Add the following code to your RegisterTypes method in `App.xaml.cs`
```c#
protected override void RegisterTypes(IContainerRegistry containerRegistry)
{
    containerRegistry.UseXctPopups();
}
```

Then when you want to show a popup resolve the `IDialogService`. See detailed usage below.

## Supported Platforms
The library is dependent on the Xamarin Community Toolkit's popup support. Please check the official documentation at their repository for target platform and versions.

| Platform  | Status |
|-----------|--------|
| iOS       | ✅     |
| Android   | ✅     |
| UWP       | ✅     |      

## Supported Libraries

| Library                   | Version   | Status |
|---------------------------|-----------|--------|
| Xamarin Community Toolkit | 1.2.0+    | 🔃     |
| Prism Library             | 8.1       | ✅     |
| Prism Library             | 8.0       | Not Started |
| Prism Library             | 7.2       | Not Started |
| Prism Library             | 7.1       | Not Started |


# Usage
To display a Xamarin Community Toolkit popup you need to complete 2 actions
1. Run setup method `UseXctPopups()`
2. Register your dialog
3. Using the `IDialogService` display your popup

## Run Setup Method
In your `App.xaml.cs` you will need to add the following method to the `RegisterTypes` method.

```c#
protected override void RegisterTypes(IContainerRegistry containerRegistry)
{
    containerRegistry.UseXctPopups();
}
```

## Register Dialog
Create a standard Xamarin Community Toolkit using the `Popup` class. 

**SamplePopup.xaml**
```xml
<?xml version="1.0" encoding="utf-8" ?>
<xct:Popup xmlns="http://xamarin.com/schemas/2014/forms"
           xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
           xmlns:xct="http://xamarin.com/schemas/2020/toolkit"
           Size="250, 250"
           x:Class="PrismPopupsSample.Views.SamplePopup">

    <xct:Popup.Content>
        <StackLayout>
            <Label Text="Hello from XCT Popup!"  />
        </StackLayout>
    </xct:Popup.Content>
    
</xct:Popup>
```

In your `App.xaml.cs` you need to register your dialog with Prism.
```c#
containerRegistry.RegisterDialog<SamplePopup>("sample");
```

## Show Popup
To display the popup you will need to resolve the `IDialogService` and display it.

```c#
public class MainViewModel
{
    IDialogService dialogService;
    public MainViewModel(IDialogService dialogService) =>
        this.dialogService = dialogService;

    void ShowPopup() =>
        this.dialogService.ShowDialog("sample");
}
```


# Documentation
For complete documentation visit the official documentation.

*Work in progess*


# Created By FileOnQ
This plugin was created by [Andrew Hoefling](https://github.com/ahoefling) and donated to the open source community by FileOnQ, Inc.
