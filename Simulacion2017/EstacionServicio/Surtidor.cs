using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static EstacionServicio.Estados;

namespace EstacionServicio
{
    public abstract class Surtidor
    {
        private _EstadoSurtidor estado;
        private _TipoServicio tipoServicio;
        private string idSurtidor;
        private Queue<Vehiculo> cola;
        private double horaInicioOcio;
        private double acumTiempoOcio;
        private string nombreEstado;


        public Surtidor(string id, _TipoServicio tipo, double horaInicioOcio, string nombre)
        {
            estado = _EstadoSurtidor.Libre;
            tipoServicio = tipo;
            cola = new Queue<Vehiculo>();
            idSurtidor = id;
            this.horaInicioOcio = horaInicioOcio;
            acumTiempoOcio = 0.00;
            nombreEstado = nombre;

        }
        public _EstadoSurtidor Estado { get; set; }

        public _TipoServicio TipoServicio { get; set; }
        
        public string getNombreEstado()
        {
            return nombreEstado;
        }

        public void ponerEnCola(Vehiculo vehiculo)
        {
            cola.Enqueue(vehiculo);
        }

        public Vehiculo sacarDeCola()
        {
            return cola.Dequeue();
        }

        public void setNombreEstado(string nombre)
        {
            nombreEstado = nombre;
        }

        public int tamañoCola()
        {
            return cola.Count;
        }

        public void setIdSurtidor(string id)
        {
            this.idSurtidor = id;
        }

        public string getIdSurtidor()
        {
            return idSurtidor;
        }

        public void setHoraInicioOcio(double horaInicio)
        {
            this.horaInicioOcio = horaInicio;
        }

        public double getHoraInicioOcio()
        {
            return horaInicioOcio;
        }

        public void acumularTiempoOcio(double tiempoOcio)
        {
            acumTiempoOcio += tiempoOcio;
        }
        public Queue<Vehiculo> getCola()
        {
            return cola;
        }

    }
}
