<div align="center">
  
  [<img src="https://github.com/schdck/SimpleDialogs/blob/master/Docs/logo.png?raw=true">](https://github.com/schdck/SimpleDialogs)

  # SimpleDialogs
  
  [![license](https://img.shields.io/github/license/schdck/SimpleDialogs.svg)](https://github.com/schdck/SimpleDialogs/blob/master/LICENSE)
  [![NuGet](https://img.shields.io/nuget/v/SimpleDialogs.svg)](https://www.nuget.org/packages/SimpleDialogs/)
  [![NuGet](https://img.shields.io/nuget/dt/SimpleDialogs.svg)](https://www.nuget.org/packages/SimpleDialogs/)
  [![GitHub issues](https://img.shields.io/github/issues/schdck/SimpleDialogs.svg)](https://github.com/schdck/SimpleDialogs/issues)
  
 
  
  :speech_balloon: SimpleDialogs is a simple framework to help displaying dialogs on a WPF app
</div>

<hr>
  
## :bulb: About the project
I made this project to make my life easier when displaying messages to the user, since it was something that I saw myself implementing over and over again. Also, I used it to practice a few new concepts that I was studying. It is far from perfect, but its something I'm proud of.

Also: any help, tips, suggestions or critics are really appreciated.

This project was heavily inspired/based on [Mahapps.Metro](https://github.com/MahApps/MahApps.Metro) (and its dialogs) and on [Mahapps.Metro.SimpleChildWindow](https://github.com/punker76/MahApps.Metro.SimpleChildWindow), so a special thanks to [@punker76](https://github.com/punker76) and all the other maintainers/contributors.
  
## :rocket: Getting started
  
You can install SimpleDialogs through [NuGet](https://www.nuget.org/packages/SimpleDialogs/) by running the following command in the NuGet Package Manager Console  
  
```bash
  PM> Install-Package SimpleDialogs
```
  
... or through NuGet Package Manager directly on your project/solution.

Then just add the following `Resource Dictionary` 
  
```XML
<ResourceDictionary Source="pack://application:,,,/SimpleDialogs;component/Controls/Design/SimpleDialogs.xaml" />
```
  
... to your application resources and you are good to go!
  
**Also take a look at our :dart: [Quick start](https://github.com/schdck/SimpleDialogs/wiki/Quick-start) and at the :books: [Wiki](https://github.com/schdck/SimpleDialogs/wiki)**

## :camera: Screenshots
  
#### Progress dialogs
![](https://github.com/schdck/SimpleDialogs/blob/master/Docs/Screenshots/ProgressDialog.gif?raw=true)
  
#### Information dialog
![](https://github.com/schdck/SimpleDialogs/blob/master/Docs/Screenshots/InformationDialog.png?raw=true)
 
#### Success dialog
![](https://github.com/schdck/SimpleDialogs/blob/master/Docs/Screenshots/SuccessDialog.png?raw=true)
  
#### Warning dialog
![](https://github.com/schdck/SimpleDialogs/blob/master/Docs/Screenshots/WarningDialog.png?raw=true)
  
#### Error dialog
![](https://github.com/schdck/SimpleDialogs/blob/master/Docs/Screenshots/ErrorDialog.png?raw=true)
  
#### Critical error dialog
![](https://github.com/schdck/SimpleDialogs/blob/master/Docs/Screenshots/CriticalDialog.png?raw=true)
