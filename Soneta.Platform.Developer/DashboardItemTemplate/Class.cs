using System;
using Soneta.Business;
using Soneta.Business.App;
using Soneta.Tools;
using %NAMESPACE%;

// Sposób w jaki należy zarejestrować extender, który później zostanie użyty w interfejsie.
[assembly: Worker(typeof(%DASHBOARDCLASS%))]
namespace %NAMESPACE%
{
	public class %DASHBOARDCLASS%
	{
        [Context]
        public Login Login { get; set; }

        public bool IsVisible => true;
       
	}
}
