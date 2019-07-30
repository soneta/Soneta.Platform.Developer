# Soneta.Platform.Developer
[![NuGet](https://img.shields.io/nuget/v/Soneta.Platform.Developer.svg)](https://www.nuget.org/packages/Soneta.Platform.Developer/)
[![NuGet](https://img.shields.io/nuget/dt/Soneta.Platform.Developer.svg)](https://www.nuget.org/packages/Soneta.Platform.Developer/)
[![Build Status](https://dev.azure.com/soneta/GitHub/_apis/build/status/Soneta.Platform.Developer%20CI?branchName=master)](https://dev.azure.com/soneta/GitHub/_build/latest?definitionId=3&branchName=master)

# Wstęp

Soneta Platform Devlper jest to dodatek zawierający zestaw szablonów implementujący [Soneta.sdk](https://github.com/soneta/Soneta.MsBuild.SDK). Soneta Platform Devlper zachęca programistę do wprowadzenia modułowości w swoich dodatkach, poprzez rozdzielenie logiki biznesowaje, interfejsu użytkownika oraz testów od siebie. Jest to realizowane dzięki automatycznemu utworzeniu 3 typów projeków, które odpowiadają wyżej wspomnianemu podziałowi. W celu zachowania pełnej elastyczności i swobody w projektowaniu programista ma możliwość utworzenia każdego typu projektu osobno. <br>
Podczas tworzenia nowego projektu użytkownik może wybrać jeden z poniższych szablonów:
<ul>
    <li>Soneta Addon Project – to właściwy projekt logiki dodatku</li>
    <li>Soneta Addon Config – w jego skład wchodzą pliki konfiguracyjne wymagane do prawidłowego działania projektów</li>
    <li>Soneta Addon Project Tests– projekt zawierający testy</li>
    <li>Soneta Addon Project UI – projekt zawierający elementy interfejsu użytkownika dla dodatku</li>
    <li>Soneta Addon- jest to kompletny projekt dodatku zawierający wszystkie powyższe szablony.</li>
</ul>
<img src="Soneta.Platform.Developer\documentation\pictures\vs_projects_list.jpg">

# Instalacja
Dodatek Soneta Platform Developer może zostać zainstalowany za pomocą konsoli używając polecenia:
```
„dotnet new -i Soneta.Platform.Developer”.
```
Listę zainstalowanych dodatków możemy podejrzeć za pomocą polecenia:
```
dotnet new -l
```
Dodatek może być także zainstalowany z poziomu **Visual Studio**. Robimy to poprzez menu **Extensions --> Manage Extensions**, a następnie **wyszukujemy Soneta Platform Developer**.  Pobieramy i instalujemy nasz dodatek, w tym celu należy ponownie uruchomić Visual Studio. Po poprawnym zainstalowaniu naszego dodatku będziemy mieli możliwość stworzenia nowego projektu według zainstalowanych szablonów.
# Tworzenie nowego dodatku przyuzyciu Visual Studio
Tworzenie nowego dodatku w Visual Studio jest bardzo proste i sprowadza się jedynie do wybrania odpowiedniego szablonu. W celu utworzenie pełnego projektu dodatku zaleca się użycie szablonu **Soneta Addon**.
# Tworzenie nowego dodatku przy użyciu Visual Studio Code
W celu wykorzystania Visual Studio Code na początku należy zainstalować wtyczkę „**C# for Visual Studio Code**” oraz dodatek Soneta Platform Developer. Następnie za pomocą konsoli utworzyć nowy projekt dotatku wybierając szablon **soneta-addon** oraz utworzyć nową solucję. W kolejnym kroku **do solucji** należy dodać wszystkie **utworzone projekty** oraz stworzyć domyślne **taski** do **kompilacji i debugowania**. Cały proces został opisany szczegółowo poniżej.
<br>

Na początku otwieramy **Visual Studio Code** i przechodzimy do zakładki **Extensions**( Ctrl+ Shift +x) i instalujemy wtyczkę „C# for Visual Studio Code”.<br>
<img src="Soneta.Platform.Developer\documentation\pictures\vsc_extension.jpg">

Następnie otworzymy **terminal** w Visual Studio Code (View--> Terminal)  i **instalujemy dodatek Soneta Platform Developer**.  Jest do dodatek , który zawiera nowe szablony. W celu instalacji dodatku wywołujemy w konsoli komendę: **„dotnet new -i Soneta.Platform.Developer”**.
Za pomocą plecenia „**dotnet new -l**” możemy podejrzeć listę wszystkich zainstalowanych szablonów.
<br>

Następnie należy dodać do naszego obszaru roboczego folder w którym umieszczony będzie nasz dodatek (File-> Add Folder To Workspace…). Mój folder będzie nazywał się „MyExtension”.
W kolejnym kroku za pomocą terminala należy **utworzyć projekt** dodatku za pomocą szablonu soneta-addon. Zrobimy to za pomocą polecenia „**dotnet new soneta-addon**”. W celu automatycznego **utworzenia katalogu**, w którym umieszczone będa nasze projekty należy skorzytać z komendy „**dotnet new soneta-addon -n MyExtension**”.  Po poprawnym wykonaniu polecenia powinniśmy zobaczyć 3 nowo powstałe projekty:
<ul>
    <li>MyExtension</li>
    <li>MyExtenison.Tests</li>
    <li>MyExtenison.UI</li>
</ul>
Oraz pliki :
<ul>
    <li>Directory.Build.propos</li>
    <li>global.json</li>
</ul>

 Podczas tworzenia szablonów dostępny jest także parametr „-o” umożliwiający podanie ściażki w której zostaną utworzone projekty.<br>

Następnie za pomocą polecenia „**dotnet new sln**” utworzymy nową solucję i dodamy do niej wszystkie utworzone wcześniej projekty za pomocą pleceń:
```
dotnet sln add .\MyExtension\MyExtension.csproj
dotnet sln add .\MyExtension.Tests\MyExtension.Tests.csproj
dotnet sln add .\MyExtension.UI\MyExtension.UI.csproj
```
<img src="Soneta.Platform.Developer\documentation\pictures\vsc_files.jpg"><br>
Następnie wciskamy F1 i wybieramy:
```
 „Tasks: Configure Default Build Task” --> „Create tasks.json file fromtemplate” --> .NET Core
 ``` 
W wyniku naszych działań powstanie plik „tasks.json”.
<br><br>
<img src="Soneta.Platform.Developer\documentation\pictures\vsc_task.jpg" width=700><br>
Teraz możemy zbudować nasze rozwiazanie uruchamiając task „**tasks.json**” . Robimy to w podobny sposób jak wtowrzyliśmy task, a mianowicie wciskamy **F1--> Tasks:Run BuildTask--> Build**. Po wykonaniu tej operacji do naszego rozwiązania zostanie dodany folder „**bin**” .
<br>
<br>
Następnie do folderu **.vscode** dodamy plik **lunch.json** i wklejamy do niego:
```
{
  "version": "0.2.0",
  "configurations": [
    {
         "name": "Debug",
         "type":"clr",
         "request": "launch",
         "preLaunchTask": "build",
         "program": "C:/Program Files (x86)/Soneta/enova365 15.4.7010.15330/SonetaExplorer.exe",
         "args":[],
         "windows": {
          "args": ["/extpath=${workspaceFolder}/bin/Debug/net46"]},        
         "console": "internalConsole",
         "stopAtEntry": false,
         "internalConsoleOptions": "openOnSessionStart"
     }
  ]
}
```
W pliku **lunch.json** należy **ustawić ścieżkę enovy**, którą chcemy wykorzystywać do debugowania. Aby uruchomić **debugowanie** naciskamy **F5**.






