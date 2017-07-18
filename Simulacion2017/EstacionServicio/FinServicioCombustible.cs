using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EstacionServicio
{
    class FinServicioCombustible : Evento
    {
        private double rndTiempo;
        private double demora;
        private double proximoFin;
        private string nombreEvento = "fin_carga_combustible";
        private Random random = new Random();
        private double desde;
        private double hasta;
        private string idSurtidor;


        public FinServicioCombustible(double a, double b, string id)
        {
            desde = a;
            hasta = b;
            idSurtidor = id;
        }

        public override string getNombreEvento()
        {
            return nombreEvento;
        }

        public string getIdSurtidor()
        {
            return idSurtidor;
        }
        public double getRandom()
        {
            return rndTiempo;
        }
        public double getTiempoEntreLlegada()
        {
            return demora;
        }

        public override double getProximaLlegada()
        {
            return proximoFin;
        }

        public override void simular(double reloj)
        {
            rndTiempo = random.NextDouble();
            demora = Distribuciones.Uniforme(desde,hasta,rndTiempo);
            proximoFin = demora + reloj;
        }
    }
}
