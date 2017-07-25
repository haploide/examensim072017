using System;
using System.Threading;



namespace EstacionServicio
{
    class LlegadaVehiculosGas : Evento
    {
        private double rndTiempoGas;
        private double tiempoEntreLlegadas;
        private double proximaLlegada;
        private string nombreEvento = "llegada_gas";
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

        public override void simular(double reloj, double random)
        {
            
            rndTiempoGas = random;
            tiempoEntreLlegadas = Distribuciones.Exponencial(mu, rndTiempoGas);
            proximaLlegada = tiempoEntreLlegadas + reloj;
        }
    }
}
