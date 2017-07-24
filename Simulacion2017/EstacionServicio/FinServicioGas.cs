using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EstacionServicio
{
    class FinServicioGas : Evento
    {
        private double rndTiempo;
        private double demora;
        private double proximoFin;
        private string nombreEvento = "fin_carga_gas";
        private Random random;
        private double desde;
        private double hasta;
        private int idSurtidor;
        private double cteDemora;


        public FinServicioGas(double a, double b, int id, double cte, Random random)
        {
            desde = a;
            hasta = b;
            idSurtidor = id;
            cteDemora = cte;
            this.random = random;
        }

        public override string getNombreEvento()
        {
            return nombreEvento;
        }

        public int getIdSurtidor()
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
        public void setHoraFin(double horaFin)
        {
            proximoFin = horaFin;
        }
        public override double getProximaLlegada()
        {
            return proximoFin;
        }

        public override void simular(double reloj)
        {
            rndTiempo = random.NextDouble();
            demora = (Distribuciones.Uniforme(desde, hasta, rndTiempo)+cteDemora);
            proximoFin = demora + reloj;
        }
    }
}
