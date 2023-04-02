using WebApplication1.Interface;
using System.ComponentModel.Composition;

namespace WebApplication1
{
    public class PorezEkstenzija : Interfacei
    {
        [Export(typeof(Interfacei))]
        public double VratiCijenuSaPDV(string drzava, double cijena)
        {
            double cijenaPDV = 0.0;
            if (drzava == "HR - 25%") { cijenaPDV = cijena * 1.25; }
            else if (drzava == "BiH - 17%") { cijenaPDV = cijena * 1.17; }
            return cijenaPDV;
        }
    }
}