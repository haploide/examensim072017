using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EstacionServicio
{
    public abstract class Evento : IComparable<Evento>
    {
        public abstract double getProximaLlegada();
        public abstract string getNombreEvento();
        public abstract void simular(double reloj, double random);

        public int CompareTo(Evento otroEvento)
        {
            return Math.Sign(this.getProximaLlegada() - otroEvento.getProximaLlegada());
        }
        public Evento getSiguienteEvento(Evento otroEvento)
        {
            if (getProximaLlegada() == 0.00)
            {
                return otroEvento;
            }
            else if (otroEvento == null || otroEvento.getProximaLlegada() == 0.00)
            {
                return this;
            }
            else if (this.getProximaLlegada() < otroEvento.getProximaLlegada())
            {
                return this;
            }
            else
            {
                return otroEvento;
            }

        }
    }
}
