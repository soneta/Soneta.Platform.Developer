# Instrukcja wydania ze szczególnym uwzględnieniem wersjonowania.

### Czynności ręczne przed wydaniem i zmierzające do wydania.

1. Zweryfikować stan synchronizacji gałęzi `develop` i `master`. W razie potrzeby wykonać odpowiednią synchronizację.
2. Funkcjonalność pożądana w nowej wersji powinna być zakończona. W tym celu należy zweryfikować stan _Pull requests_.
3. Zgłoszenia dotyczące zrealizowanej funkcjonalności powinny być zamknięte. Zweryfikować stan _Issues_.
4. Ostatnia wydana wersja [Soneta.SDK] powinna być opublikowana. Zweryfikować [Soneta.SDK na nuget.org].
5. Na gałęzi `develop` zmodyfikować następujące pliki:
   * [Directory.Build.props] w elemencie `SonetaPackageVersion` ustawić ostatnie oficjalne wydanie enova
   * [global.json] we właściwości `Soneta.Sdk` ustawić ostatnie oficjalne wydanie `Soneta.MsBuild.SDK`
   * [version.json] we właściwości `version` **usunąć część _prerelease_** pozostawiając ją w formacie `<x.y.z>`
   * [vsixmanifest] w atrybucie `Version` elementu `Identity` podnieść numer wersji w formacie `<x.y.z+1>`
6. Utworzyć i opublikować commit z komunikatem stwierdzającym o zmianie wersji.
   ```
   git commit -m "Wersja x.y.z; SDK <wersja SDK>; enova <wersja enova>; vsix <wersja vsix>"
   git push origin develop
   ```
7. Zintegrować `develop` z `master` i opublikować wydanie z `master`
   ```
   git checkout master
   git merge develop
   git push origin master
   ```

### Część zautomatyzowana

Proces publikacji paczki _NuGet_ uruchomiony został automatycznie. Jest realizowany przez [pipeline wydania], którego stan należy zweryfikować. Publikacja może oczekiwać na akceptację osób wskazanych w organizacji. W trakcie oczekiwania na akceptację można podjąć kolejne czynności opisane w następnej sekcji.

### Czynności ręczne po wydaniu

1. Na gałęzi `develop` **przywrócić część _prerelease_** i podnieść numer wersji `<x.y.z+1>-beta.{height}` w pliku [version.json]
2. Utworzyć i opublikować commit z komunikatem stwierdzającym o zmianie wersji.
   ```
   git commit -m "Wersja następna: x.y.z+1-prerelease"
   git push origin develop
   ```
3. Artefakt w postaci paczki `vsix` należy ręcznie opublikować w [VS marketplace].

##### `Koniec pracy. Gratulacje!`

[version.json]: Soneta.Platform.Developer/Soneta.Platform.Developer/version.json
[repozytorium]: https://github.com/soneta/Soneta.Platform.Developer
[Soneta.SDK]: https://github.com/soneta/Soneta.MsBuild.SDK
[Soneta.SDK na nuget.org]: https://www.nuget.org/packages/Soneta.Sdk
[pipeline wydania]: https://dev.azure.com/soneta/GitHub/_release?_a=releases&view=mine&definitionId=3
[Directory.Build.props]: Soneta.Platform.Developer/SonetaAddon/SonetaAddonProject/Directory.Build.props
[global.json]: Soneta.Platform.Developer/SonetaAddon/SonetaAddonProject/global.json
[vsixmanifest]: Soneta.Platform.Developer/Soneta.Platform.Developer/source.extension.vsixmanifest
[VS marketplace]: https://marketplace.visualstudio.com
