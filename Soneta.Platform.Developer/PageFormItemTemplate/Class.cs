using Soneta.Business;
using Soneta.Business.App;
$if$ ($pageform_registerfolder$ == 1)
using Soneta.Business.UI;$endif$
using $rootnamespace$;

$if$ ($pageform_registerfolder$ == 0)
// Sposób w jaki należy zarejestrować extender, który później zostanie użyty w interfejsie.
[assembly: Worker(typeof($safeitemrootname$))]$endif$

$if$ ($pageform_registerfolder$ == 1)
// Sposób w jaki należy zarejestrować page który będzie wyswietlany jako folderw interfejsie.
[assembly: FolderView("$defaultnamespace$/$safeitemrootname$",
    Priority = $pageform_priority$,
    Description = "$pageform_caption$",
    ObjectType = typeof($safeitemrootname$),
    ObjectPage = "$pageform_xml$",
    ReadOnlySession = false,
    ConfigSession = false
)]$endif$

namespace $rootnamespace$
{
	public class $safeitemrootname$
	{
        [Context]
        public Session Session { get; set; }

        [Context]
        public Login Login { get; set; }
    }
}
