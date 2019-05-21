using System;
using Soneta.Business;
using Soneta.Business.App;
using Soneta.Tools;
using $rootnamespace$;

// Sposób w jaki należy zarejestrować extender, który później zostanie użyty w interfejsie.
[assembly: Worker(typeof($dashboard_class$))]
namespace $rootnamespace$
{
	public class $dashboard_class$
	{
        [Context]
        public Login Login { get; set; }

        public bool IsVisible {
            get {
                var version = typeof (CoreTools).Assembly.GetName().Version;
                return BusApplication.Instance.Is365 && version.Major >= 10 && version.Minor >= 5;
            }
        }
	}
}
