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


        public Surtidor(string id, _TipoServicio tipo, double horaInicioOcio)
        {
            estado = _EstadoSurtidor.Libre;
            tipoServicio = tipo;
            cola = new Queue<Vehiculo>();
            idSurtidor = id;
            this.horaInicioOcio = horaInicioOcio;
            acumTiempoOcio=0.00;

        }
        public _EstadoSurtidor Estado
        {
            get
            {
                return estado;
            }

            set
            {
                estado = value;
            }
        }

        public _TipoServicio TipoServicio
        {
            get
            {
                return tipoServicio;
            }

            set
            {
                tipoServicio = value;
            }
        }

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
