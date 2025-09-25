using System;
using System.Linq;
using Soneta.Business;
using Soneta.Business.UI;
using %NAMESPACE%;

[assembly: FolderView("%NAMESPACE%/%VIEWINFOCLASS%",
    Priority = %VIEWINFOPRIORITY%,
    Description = "%VIEWINFODESCRYPTION%",
    TableName = "%VIEWINFOTABLENAME%",
    ViewType = typeof(%VIEWINFOCLASS%)
)]

namespace %NAMESPACE%
{
	public class %VIEWINFOCLASS% : ViewInfo
	{
        public %VIEWINFOCLASS%()
        {
            // View wiążemy z odpowiednią definicją viewform.xml poprzez property ResourceName
            ResourceName = "%VIEWINFONAME%";

            // Inicjowanie contextu
            InitContext += %VIEWINFOCLASS%_InitContext;

            // Tworzenie view zawierającego konkretne dane
            CreateView += %VIEWINFOCLASS%_CreateView;
        }

        void %VIEWINFOCLASS%_InitContext(object sender, ContextEventArgs args) {
        }

        void %VIEWINFOCLASS%_CreateView(object sender, CreateViewEventArgs args) {
            %VIEWINFOCLASS%.WParams parameters;
            if (!args.Context.Get(out parameters)) 
                return;
            args.View = ViewCreate(parameters);
        }

        public class WParams : ContextBase {
            public WParams(Context context) : base(context)
            {
            }
	    }

        protected View ViewCreate(WParams pars)
	    {
            View view = null;
            return view;
        }

	}
}
