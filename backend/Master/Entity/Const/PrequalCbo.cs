using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Master.Entity.Const
{
    [ExcludeFromCodeCoverage]
    public static class PrequalCbo
    {
        public static EnumItem? Busca(string texto)
        {
            if (string.IsNullOrEmpty(texto))   
                return null;
            
            if (!int.TryParse(texto.Trim(), out var mId))
                return null;
                
            return texto.Length switch
            {
                1 => PrequalCbo1D.Vector.FirstOrDefault(y => y.Id == mId),
                2 => PrequalCbo2D.Vector.FirstOrDefault(y => y.Id == mId),
                3 => PrequalCbo3D.Vector.FirstOrDefault(y => y.Id == mId),
                4 => PrequalCbo4D.Vector.FirstOrDefault(y => y.Id == mId),
                6 => PrequalCbo6D.Vector.FirstOrDefault(y => y.Id == mId),
                _ => null,
            };
        }        
    }
}
