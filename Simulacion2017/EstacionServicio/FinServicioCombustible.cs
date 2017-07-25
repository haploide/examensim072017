using System;


namespace EstacionServicio
{
    class FinServicioCombustible : Evento
    {
        private double rndTiempo;
        private double demora;
        private double proximoFin;
        private string nombreEvento = "fin_carga_combustible";
        private double desde;
        private double hasta;
        private int idSurtidor;


        public FinServicioCombustible(double a, double b, int id)
        {
            desde = a;
            hasta = b;
            idSurtidor = id;
            
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

        public override void simular(double reloj, double random)
        {

            rndTiempo = random;
            demora = Distribuciones.Uniforme(desde,hasta,rndTiempo);
            proximoFin = demora + reloj;
        }
    }
}
