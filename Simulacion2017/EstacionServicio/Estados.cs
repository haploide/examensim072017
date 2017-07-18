using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EstacionServicio
{
    public class Estados
    {
        public enum _EstadoVehiculo
        {
            Esperando_Atencion_Conbustible,
            Esperando_Atencion_Gas,
            Siendo_Atendido_Conbustible,
            Siendo_Atendido_Gas
        }
        public enum _EstadoSurtidor
        {
            Libre,
            Ocupado
        }

        public enum _TipoServicio
        {
            Combustible,
            Gas
        }
    }
}
