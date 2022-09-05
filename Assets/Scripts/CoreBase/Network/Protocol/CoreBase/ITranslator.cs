

namespace GameProtocol.Protocol
{
    /// <summary>
    /// translator abstract
    /// </summary>
    public abstract class ITranslator
    {

        /// <summary>
        /// kip the singlaton of translator
        /// </summary>
        public static ITranslator Translator;

        public abstract string Loc(string source, string language);
    }
}
