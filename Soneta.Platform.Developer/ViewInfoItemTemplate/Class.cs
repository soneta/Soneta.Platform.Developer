using System;
$if$ ($targetframeworkversion$ >= 3.5)using System.Linq;
$endif$
using Soneta.Business;
using Soneta.Business.UI;
using $rootnamespace$;

[assembly: FolderView("$defaultnamespace$/$safeitemrootname$ViewInfo",
    Priority = $viewinfo_priority$,
    Description = "$viewinfo_description$",
    TableName = "$viewinfo_tablename$",
    ViewType = typeof($safeitemrootname$ViewInfo)
)]

namespace $rootnamespace$
{
	public class $safeitemrootname$ViewInfo : ViewInfo
	{
        public $safeitemrootname$ViewInfo()
        {
            // View wiążemy z odpowiednią definicją viewform.xml poprzez property ResourceName
            ResourceName = "$safeitemrootname$";

            // Inicjowanie contextu
            InitContext += $safeitemrootname$ViewInfo_InitContext;

            // Tworzenie view zawierającego konkretne dane
            CreateView += $safeitemrootname$ViewInfo_CreateView;
        }

        void $safeitemrootname$ViewInfo_InitContext(object sender, ContextEventArgs args) {
        }

        void $safeitemrootname$ViewInfo_CreateView(object sender, CreateViewEventArgs args) {
            $safeitemrootname$ViewInfo.WParams parameters;
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
