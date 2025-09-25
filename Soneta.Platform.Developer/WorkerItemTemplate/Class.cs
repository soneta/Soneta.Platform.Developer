using System;
using Soneta.Business;
using Soneta.Business.UI;
using %NAMESPACE%;

[assembly: Worker(typeof(%WORKERCLASSNAME%), typeof(%WORKERDATATYPE%))]
namespace %NAMESPACE%
{
    public class %WORKERCLASSNAME%
    {
$if$  (worker-params)
        [Context]
        public %WORKERCLASSNAME%Params @params {
            get;
            set;
        }
$endif$
        // Szczegółowy opis : https://dok.enova.pl/programowanie/string-messageboxinformation,4292
        [Action("%WORKERCLASSNAME%/ToDo", Mode = ActionMode.SingleSession | ActionMode.ConfirmSave | ActionMode.Progress)]
        public MessageBoxInformation ToDo() {
$if$ (!worker-params)
            return new MessageBoxInformation("Czy wykonać operację ?") {
                Text = "Opis operacji",
                YesHandler = () => "Operacja została zakończona",
                NoHandler = () => "Operacja przerwana"
            };
$endif$
$if$ (worker-params)
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

$if$ (worker-params)
    public class %WORKERCLASSNAME%Params : ContextBase
    {
        public %WORKERCLASSNAME%Params(Context context) : base(context)
        {
        }
        public string Parametr1 { get; set; }
    }
$endif$
}
