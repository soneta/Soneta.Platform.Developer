using Soneta.Business;
using Soneta.Business.App;
$if$ ($pageform_registerfolder$ == 1)
using Soneta.Business.UI;$endif$
using %NAMESPACE%;

$if$ (!pageform-register-folder)
// Sposób w jaki należy zarejestrować extender, który później zostanie użyty w interfejsie.
[assembly: Worker(typeof(%CLASSNAME%))]$endif$

$if$ (pageform-register-folder)
// Sposób w jaki należy zarejestrować page który będzie wyswietlany jako folderw interfejsie.
[assembly: FolderView("%NAMESPACE%/%CLASSNAME%",
    Priority = %PAGEFORMPRIORITY%,
    Description = "%PAGEFORMCAPTION%",
    ObjectType = typeof(%CLASSNAME%),
    ObjectPage = "%CLASSNAME%.%PAGEFORMPAGENAME%.pageform.xml",
    ReadOnlySession = false,
    ConfigSession = false
)]$endif$

namespace %NAMESPACE%
{
	public class %CLASSNAME%
	{
        [Context]
        public Session Session { get; set; }

        [Context]
        public Login Login { get; set; }
    }
}
