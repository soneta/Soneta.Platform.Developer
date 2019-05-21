using System;
using Soneta.Business;
using Soneta.Business.UI;
using $rootnamespace$;

[assembly: Worker(typeof($worker_class$), typeof($worker_datattype$))]
namespace $rootnamespace$
{
    public class $worker_class$
    {
$if$ ($worker_registerparams$ == 1)
        [Context]
        public $worker_paramsclass$ @params {
            get;
            set;
        }
$endif$

        // TODO -> Należy podmienić podany opis akcji na bardziej czytelny dla uzytkownika
        [Action("$worker_class$/ToDo", Mode = ActionMode.SingleSession | ActionMode.ConfirmSave | ActionMode.Progress)]
        public MessageBoxInformation ToDo() {
$if$ ($worker_registerparams$ == 0)
            return new MessageBoxInformation("Czy wykonać operację ?") {
                Text = "Opis operacji",
                YesHandler = () => "Operacja została zakończona",
                NoHandler = () => "Operacja przerwana"
            };
$endif$
$if$ ($worker_registerparams$ == 1)
            return new MessageBoxInformation("Potwierdzasz wykonanie operacji ?") {
                Text = "Opis operacji",
                YesHandler = () => {
                    using (var t = @params.Session.Logout(true)) {
                        t.Commit();
                    }
                    return "Operacja została zakończona";
                },
                NoHandler = () => "Operacja przerwana"
            };
$endif$
        }
    }

$if$ ($worker_registerparams$ == 1)
    public class $worker_paramsclass$ : ContextBase
    {
        public $worker_paramsclass$(Context context) : base(context)
        {
        }

        // TODO -> Poniższy parametr dodany dla celów poglądowych. Należy usunąć.
        public string Parametr1 { get; set; }
    }
$endif$
}
