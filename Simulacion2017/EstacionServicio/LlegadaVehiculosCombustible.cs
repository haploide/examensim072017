using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EstacionServicio
{
    class LlegadaVehiculosCombustible : Evento
    {
        private double rndTiempoCombustible;
        private double tiempoEntreLlegadas;
        private double proximaLlegada;
        private string nombreEvento = "llegada_combustible";
        private Random randomCombustible;
        private double mu;


        public LlegadaVehiculosCombustible(double mu, Random random)
        {
            this.mu = mu;
            randomCombustible = random;
        }
        public override string getNombreEvento()
        {
            return nombreEvento;
        }

        public double getRandom()
        {
            return rndTiempoCombustible;
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
            rndTiempoCombustible = randomCombustible.NextDouble();
            tiempoEntreLlegadas = Distribuciones.Exponencial(mu, rndTiempoCombustible);
            proximaLlegada = tiempoEntreLlegadas + reloj;
        }
    }
}
