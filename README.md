# Soneta.Platform.Developer
[![NuGet](https://img.shields.io/nuget/v/Soneta.Platform.Developer.svg)](https://www.nuget.org/packages/Soneta.Platform.Developer/)
[![NuGet](https://img.shields.io/nuget/dt/Soneta.Platform.Developer.svg)](https://www.nuget.org/packages/Soneta.Platform.Developer/)
[![Build Status](https://dev.azure.com/soneta/GitHub/_apis/build/status/Soneta.Platform.Developer%20CI?branchName=master)](https://dev.azure.com/soneta/GitHub/_build/latest?definitionId=3&branchName=master)

# Link dla instalatora dodatku dla VS 2019
[SonetaPlatformDeveloper 3.0.3](https://marketplace.visualstudio.com/_apis/public/gallery/publishers/enovaMSDN/vsextensions/SonetaPlatformDeveloper/3.0.3/vspackage)

# Wstęp

Soneta Platform Developer jest to rozszerzenie Visual Studio 2022 zawierające zestaw szablonów Visual Studio, które implementują elementy [Soneta.MsBuild.SDK](https://github.com/soneta/Soneta.MsBuild.SDK). Soneta Platform Developer promuje rozdzielanie logiczne tworzonych solucji rozszerzeń zgodnie z architekturą enova365, poprzez podział rozwiązania na osobne projekty warstwy logiki biznesowej, warstwy interfejsu użytkownika oraz warstwy testującej. Realizacja takiego uporządkowania polega na automatycznym utworzeniu przez SDK trzech projektów według typów, które odpowiadają wyżej wspomnianemu podziałowi. W celu zachowania pełnej elastyczności i swobody w projektowaniu twórca rozszerzenia enova365 ma możliwość utworzenia każdego typu projektu oddzielnie. <br>
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
Dodatek może być także zainstalowany z poziomu **Visual Studio**. Robimy to poprzez menu **Extensions --> Manage Extensions**, a następnie **wyszukujemy Soneta Platform Developer**.  Pobieramy i instalujemy nasz dodatek, w tym celu należy ponownie uruchomić Visual Studio. Po poprawnym zainstalowaniu naszego dodatku będziemy mieli możliwość stworzenia nowego projektu przy użyciu zainstalowanych szablonów.<br>
**Uwaga!**<br>
Wraz z pojawieniem się wersji Visual Studio 2022, szblony intalowane za pomocą komendy 'dotnet new' są widoczne także w Visual Studio. W celu poprawnego utworzenia zestawu projektów  w oknie konfiguracji nowego projektu należy **zaznaczyć** opcję **'Place solution and project in the same directory'.**
<img src="Soneta.Platform.Developer\documentation\pictures\vs_configure_project
.jpg">

# Tworzenie nowego dodatku przy użyciu Visual Studio
Tworzenie nowego dodatku w Visual Studio jest bardzo proste i sprowadza się jedynie do wybrania odpowiedniego szablonu. W celu utworzenie pełnego projektu dodatku zaleca się użycie szablonu **Soneta Addon**.
Uwaga ! Starsze wersje Visual Studio nie obsługują elementów zastosowanych w Soneta SDK, które korzysta np. z nowych formatów projektów (.csproj) dostępnych dopiero od wersji VS 2019.<br>
Jeżeli w Visual Studio jest zainstalowane poprzednie rozwiązanie jako rozszerzenia Soneta Studio Ext należy je odinstalować - nie będzie ono już potrzebne.
# Tworzenie nowego dodatku przy użyciu Visual Studio Code
W celu wykorzystania Visual Studio Code na początku należy zainstalować wtyczkę „**C# for Visual Studio Code**” oraz dodatek Soneta Platform Developer. Następnie za pomocą konsoli utworzyć nowy projekt dodatku wybierając szablon **soneta-addon** oraz utworzyć nową solucję. W kolejnym kroku **do solucji** należy dodać wszystkie **utworzone projekty** oraz stworzyć domyślne **taski** do **kompilacji i debugowania**. Cały proces został opisany szczegółowo poniżej.
<br>

Na początku otworzymy **Visual Studio Code** i przejdziemy do zakładki **Extensions**( Ctrl+ Shift +x) i instalujemy wtyczkę „C# for Visual Studio Code”.<br>
<img src="Soneta.Platform.Developer\documentation\pictures\vsc_extension.jpg">

Następnie otwieramy **terminal** w Visual Studio Code (View--> Terminal)  i **instalujemy dodatek Soneta Platform Developer**.  Jest do dodatek , który zawiera nowe szablony. W celu instalacji dodatku wywołujemy w konsoli komendę: **„dotnet new -i Soneta.Platform.Developer”**.
Za pomocą plecenia „**dotnet new -l**” możemy podejrzeć listę wszystkich zainstalowanych szablonów.
<br>

Następnie dodajemy do naszego obszaru roboczego folder w którym umieszczony będzie nasz dodatek (File-> Add Folder To Workspace…). Mój folder będzie nazywał się „MyExtension”.
W kolejnym kroku za pomocą terminala należy **utworzyć projekt** dodatku za pomocą szablonu soneta-addon. Zrobimy to za pomocą polecenia „**dotnet new soneta-addon**”. W celu automatycznego **utworzenia katalogu**, w którym umieszczone będą nasze projekty, należy skorzystać z komendy „**dotnet new soneta-addon -n MyExtension**”.  Po poprawnym wykonaniu polecenia powinniśmy zobaczyć 3 nowo powstałe projekty:
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

 Podczas tworzenia szablonów dostępny jest także parametr „-o” umożliwiający podanie ścieżki w której zostaną utworzone projekty.<br>

Następnie za pomocą polecenia „**dotnet new sln**” utworzymy nową solucję i dodamy do niej wszystkie utworzone wcześniej projekty za pomocą pleceń:
```
dotnet sln add .\MyExtension\MyExtension.csproj
dotnet sln add .\MyExtension.Tests\MyExtension.Tests.csproj
dotnet sln add .\MyExtension.UI\MyExtension.UI.csproj
```
<img src="Soneta.Platform.Developer\documentation\pictures\vsc_files.jpg"><br>
Możemy również użyć komendy Power Shella, która doda do solucji wszystkie projekty zawarte w folderze. Utworzenie nowej solucji nie jest konieczne dla Visual Studio Code.
```
Get-ChildItem *.csproj -Recurse | ForEach-Object { dotnet sln add $_.FullName }
```
Następnie wciskamy F1 i wybieramy:
```
 „Tasks: Configure Default Build Task” --> „Create tasks.json file fromtemplate” --> .NET Core
 ``` 
W wyniku naszych działań powstanie plik „tasks.json”.
<br><br>
<img src="Soneta.Platform.Developer\documentation\pictures\vsc_task.jpg" width=700><br>
Teraz możemy zbudować nasze rozwiązanie uruchamiając task „**tasks.json**” . Robimy to w podobny sposób jak utowrzyliśmy task, a mianowicie wciskamy **F1--> Tasks:Run BuildTask--> Build**. Po wykonaniu tej operacji do naszego rozwiązania zostanie dodany folder „**bin**” .
<br>
<br>
Następnie do folderu **.vscode** dodamy plik **launch.json** i wklejamy do niego:
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
W pliku **launch.json** należy **ustawić ścieżkę enova365**, którą chcemy wykorzystywać do debugowania. Do debugowanie można użyć nie tylko **Soneta Explorer**, ale również **SonetaServer** lub inne produkty, które się pojawią.  Aby uruchomić **debugowanie** naciskamy **F5**.

## Intellisense dla plików config, business i form.xml
Visual Studio nie wymaga dodatkowej konfiguracji, natomiast Visual Studio Code nie obsługuje domyślnie podpowiedzi w plikach xml w oparciu o xsd. Dlatego należy doinstalować rozszerzenie **Xml Complete**. Działa on w oparciu o schemy udostępnione na naszej stronie internetowej. Nowo dodane pliki xml posiadają odpowiednie atrybuty, istniejące już w dodatku pliki należy uzupełnić o atrybuty xmlns:xsi, xmlns:xsd praz xsi:schemaLocation tak jak na poniższym przykładzie. 

```
          xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
          xmlns:xsd="http://www.w3.org/2001/XMLSchema"
          xmlns="http://www.enova.pl/schema/form.xsd"
          xsi:schemaLocation="http://www.enova.pl/schema/ http://www.enova.pl/schema/form.xsd"
```


# Współpraca
W celu zaproponowania zmian należy stworzyć Pull Request do gałęzi develop. Po podjęciu decyzji o wydaniu nowej wersji branch develop zostanie zmergowany do mastera i dodatek zostanie automatycznie wydany.

# Proces wydawania nowych wersji
Dokument [instrukcja wydania] szczegółowo opisuje potrzebne czynności zmierzające do wydania nowej wersji.

[instrukcja wydania]: RELEASING.md
