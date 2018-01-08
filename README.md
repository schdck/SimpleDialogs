<div align="center">
  
  [<img src="https://github.com/schdck/SimpleDialogs/blob/master/logo.png?raw=true">](https://github.com/schdck/SimpleDialogs)

  # SimpleDialogs
  
  :speech_balloon: SimpleDialogs is a simple framework to help displaying dialogs on a WPF app
</div>

<hr>
  
## :bulb: About the project
I made this project to make my life easier when displaying messages to the user and for study purposes. Any help, tips, suggestions or critics are really appreciated.

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
![](https://i.imgur.com/R9BLTfo.gif)
  
#### Information dialog
![](https://i.imgur.com/plOvEVp.png")
 
#### Success dialog
![](https://i.imgur.com/uFWmZNi.png")
  
#### Warning dialog
![](https://i.imgur.com/8G4zNoR.png")
  
#### Error dialog
![](https://i.imgur.com/IC6jEvr.png")
  
#### Critical error dialog
![](https://i.imgur.com/fqnbnu9.png")
