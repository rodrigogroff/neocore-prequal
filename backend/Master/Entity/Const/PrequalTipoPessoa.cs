using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Master.Entity.Const
{
    [ExcludeFromCodeCoverage]
    public static class PrequalTipoPessoa
    {
        public const string PF = "PF";
        public const string PJ = "PJ";

        public static readonly List<string> Lista = [ PF, PJ ];

        public static readonly List<EnumItem> Vector =
        [
            new EnumItem { Id = 1, Descricao = PF },
            new EnumItem { Id = 2, Descricao = PJ },
        ];
    }    
}
