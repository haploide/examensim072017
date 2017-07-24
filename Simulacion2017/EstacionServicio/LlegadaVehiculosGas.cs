using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EstacionServicio
{
    class LlegadaVehiculosGas : Evento
    {
        private double rndTiempoGas;
        private double tiempoEntreLlegadas;
        private double proximaLlegada;
        private string nombreEvento = "llegada_gas";
        private Random randomGas;
        private double mu;
        
        public LlegadaVehiculosGas(double mu, Random random)
        {
            this.mu = mu;
            randomGas = random;
        }
        public override string getNombreEvento()
        {
            return nombreEvento;
        }
        public double getRandom()
        {
            return rndTiempoGas;
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
            rndTiempoGas = randomGas.NextDouble();
            tiempoEntreLlegadas = Distribuciones.Exponencial(mu, rndTiempoGas);
            proximaLlegada = tiempoEntreLlegadas + reloj;
        }
    }
}
