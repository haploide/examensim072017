using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EstacionServicio
{
    class LlegadaVehiculosGas : Evento
    {
        private double rndTiempo;
        private double tiempoEntreLlegadas;
        private double proximaLlegada;
        private string nombreEvento = "llegada_gas";
        private Random random = new Random();
        private double mu;
        
        public LlegadaVehiculosGas(double mu)
        {
            this.mu = mu;
        }
        public override string getNombreEvento()
        {
            return nombreEvento;
        }
        public double getRandom()
        {
            return rndTiempo;
        }
        public double getTiempoEntreLlegada()
        {
            return tiempoEntreLlegadas;
        }
        public override double getProximaLlegada()
        {
            return proximaLlegada;
        }

        public override void simular(double reloj)
        {
            rndTiempo = random.NextDouble();
            tiempoEntreLlegadas = Distribuciones.Exponencial(mu, rndTiempo);
            proximaLlegada = tiempoEntreLlegadas + reloj;
        }
    }
}
