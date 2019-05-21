using Soneta.Types;

namespace SonetaAddon
{
    public static class EnsureSonetaTypesReferenceClass
    {
        /// <summary>
        /// Ważne ze względnu na wczytywanie bibliotek, które mają referencje do Soneta.Types
        /// Tylko biblioteki z referencją do Soneta.Types są wczytywane podczas analizowania form.xml
        /// Cały plik "EnsureSonetaTypesReferenceClass.cs" można usunąć, jeśli dodatek zawiera jawne odwołanie
        /// do biblioteki Soneta.Types, w innym wypadku plik należy pozostawić
        /// </summary>
#pragma warning disable 414
        private static readonly FromTo EnsureSonetaTypesReferenceVariable = new FromTo();
#pragma warning restore 414
    }
}