using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static EstacionServicio.Estados;

namespace EstacionServicio
{
    public class Vehiculo
    {
        private string id;
        private _EstadoVehiculo estado;
        private _TipoServicio tipoServicio;
        private double horaInicioEspera;

        public Vehiculo(string id, _EstadoVehiculo estado, _TipoServicio tipo, double horaInicioEspera)
        {
            this.id = id;
            this.estado = estado;
            this.tipoServicio = tipo;
            this.horaInicioEspera = horaInicioEspera;
        }

        public void setHoraInicioEspera(double horaInicioEspera)
        {
            this.horaInicioEspera = horaInicioEspera;
        }
        public double getInicioEspera()
        {
            return horaInicioEspera;
        }

        public _TipoServicio Tipo
        {
            get { return tipoServicio; }
            set { tipoServicio = value; }
        }

        public _EstadoVehiculo Estado
        {
            get { return estado; }
            set { estado = value; }
        }

        public string Id
        {
            get { return id; }
            set { id = value; }
        }
    }
}
