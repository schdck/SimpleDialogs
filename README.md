<div align="center">
  
  [<img src="https://github.com/schdck/SimpleDialogs/blob/master/Docs/logo.png?raw=true">](https://github.com/schdck/SimpleDialogs)

  # SimpleDialogs
  
  [![License](https://img.shields.io/github/license/schdck/SimpleDialogs.svg)](https://github.com/schdck/SimpleDialogs/blob/master/LICENSE)
  [![Build status](https://ci.appveyor.com/api/projects/status/ngnm9ni9rckwg4v4?svg=true)](https://ci.appveyor.com/project/schdck/simpledialogs)
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

Then just add one of the following `Resource Dictionary` 
  
```XML
<!-- LIGHT THEME: -->
<ResourceDictionary Source="pack://application:,,,/SimpleDialogs;component/Themes/Light.xaml" />
<!-- DARK THEME: -->
<ResourceDictionary Source="pack://application:,,,/SimpleDialogs;component/Themes/Dark.xaml" />
```
  
... to your application resources and you are good to go!
  
**Also take a look at our :dart: [Quick start](https://github.com/schdck/SimpleDialogs/wiki/Quick-start) and at the :books: [Wiki](https://github.com/schdck/SimpleDialogs/wiki)**

## :camera: Screenshots
  
#### Progress dialogs
<img src="https://github.com/schdck/SimpleDialogs/blob/master/Docs/Screenshots/ProgressDialogs.gif?raw=true"
     width="703"
     height="378" />

#### Information dialog
<img src="https://github.com/schdck/SimpleDialogs/blob/master/Docs/Screenshots/InformationDialog.png?raw=true"
     width="703"
     height="378" />
 
#### Success dialog
<img src="https://github.com/schdck/SimpleDialogs/blob/master/Docs/Screenshots/SuccessDialog.png?raw=true"
     width="703"
     height="378" />
  
#### Warning dialog
<img src="https://github.com/schdck/SimpleDialogs/blob/master/Docs/Screenshots/WarningDialog.png?raw=true"
     width="703"
     height="378" />
  
#### Error dialog
<img src="https://github.com/schdck/SimpleDialogs/blob/master/Docs/Screenshots/ErrorDialog.png?raw=true"
     width="703"
     height="378" />
  
#### Critical error dialog
<img src="https://github.com/schdck/SimpleDialogs/blob/master/Docs/Screenshots/CriticalDialog.png?raw=true"
     width="703"
     height="378" />

#### Input dialog
<img src="https://github.com/schdck/SimpleDialogs/blob/master/Docs/Screenshots/InputDialog.png?raw=true"
     width="703"
     height="378" />

#### Demo application
<img src="https://github.com/schdck/SimpleDialogs/blob/master/Docs/Screenshots/Demo.gif?raw=true"
     width="703"
     height="505" />
