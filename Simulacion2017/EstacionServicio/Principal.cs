using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static EstacionServicio.Estados;

namespace EstacionServicio
{
    public partial class frm_principal : Form
    {
        //Servidores y Eventos del Sistema
        private LlegadaVehiculosCombustible llegadaVehiculoCombustible;
        private LlegadaVehiculosGas llegadaVehiculoGas;
        private FinServicioCombustible finServicioCombustibleSurt1;
        private FinServicioCombustible finServicioCombustibleSurt2;
        private FinServicioCombustible finServicioCombustibleSurt3;
        private FinServicioCombustible finServicioCombustibleSurt4;
        private FinServicioGas finServicioGasSurt1;
        private FinServicioGas finServicioGasSurt2;
        private FinServicioGas finServicioGasSurt3;
        private Surtidor SurtidorCombustible1;
        private Surtidor SurtidorCombustible2;
        private Surtidor SurtidorCombustible3;
        private Surtidor SurtidorCombustible4;
        private Surtidor SurtidorGas1;
        private Surtidor SurtidorGas2;
        private Surtidor SurtidorGas3;

        //Variables de Estadisticas
        private double ContVehiculosCombustibleIngresanAlSitema;
        private double ContVehiculosGasIngresanAlSitema;
        private double ContVehiculosCombustibleRechazados;
        private double ContVehiculosGasRechazados;
        private double ContVehiculosCombustibleAtendidos;
        private double ContVehiculosGasAtendidos;
        private double AcumTiempoEsperaVehiculosCombustible;
        private double AcumTiempoEsperaVehiculosGas;
        private double ContVehiConTiempoEsperaVehiculosCombustible;
        private double ContVehiConTiempoEsperaVehiculosGas;
        private double AcumTiempoOcioServidoresCombustible;
        private double AcumTiempoOcioServidoresGas;


        //Parametros de Llega
        private double mediaLlegadaCombustible;
        private double mediaLlegadaGas;
        private int colaMaxima;

        //Parametros de Finalizacion
        private double valorAUniformeFinCombustible;
        private double valorBUniformeFinCombustible;
        private double valorAUniformeFinGas;
        private double valorBUniformeFinGas;
        private double cteSumadaAUniformeGas;

        //Parametros de la corrida
        private double tiempoFinCorrida;
        private double tiempoAPartirDeDondeMostrar;
        private int cantidadDeEventosAMostrar;


        //Variables necesarias
        private double relojSimulacion;
        private int identificadorVehiculo;
        private int eventosYaSimuladosYMostrados;
        private Random random;
        bool resul = true;
        TextBox invalido = null;




        public frm_principal()
        {
            InitializeComponent();
        }

        private void btnSimular_Click(object sender, EventArgs e)
        {
            //Inicio Bloque Simulacion
            if (validarDatos())
            {
                //Inicializacion de Variables
                relojSimulacion = 0.00;
                identificadorVehiculo = 0;
                eventosYaSimuladosYMostrados = 0;
                random = new Random();

                //Inicializacion de Estadisticas
                ContVehiculosCombustibleIngresanAlSitema = 0;
                ContVehiculosGasIngresanAlSitema = 0;
                ContVehiculosCombustibleRechazados = 0;
                ContVehiculosGasRechazados = 0;
                ContVehiculosCombustibleAtendidos = 0;
                ContVehiculosGasAtendidos = 0;
                AcumTiempoEsperaVehiculosCombustible = 0.00;
                AcumTiempoEsperaVehiculosGas = 0.00;
                ContVehiConTiempoEsperaVehiculosCombustible = 0.00;
                ContVehiConTiempoEsperaVehiculosGas = 0.00;
                AcumTiempoOcioServidoresCombustible = 0.00;
                AcumTiempoOcioServidoresGas = 0.00;

                //Capturar Parametros de la Corrida
                tiempoFinCorrida = Convert.ToDouble(txtTiempoSim.Text);
                tiempoAPartirDeDondeMostrar = Convert.ToDouble(txtMinDesde.Text);
                cantidadDeEventosAMostrar = Convert.ToInt32(txtNEventosMostrar.Text);

                //Capturar Parametros de Llegadas
                mediaLlegadaCombustible = Convert.ToDouble(txtLlegadaConbustible.Text);
                mediaLlegadaGas = Convert.ToDouble(txtLlegadaGas.Text);
                colaMaxima = Convert.ToInt32(txtColaMax.Text);

                //Capturar Parametros de Finalizacion
                valorAUniformeFinCombustible = Convert.ToDouble(txtFinCombustibleDesde.Text);
                valorBUniformeFinCombustible = Convert.ToDouble(txtFinCombustibleHasta.Text);
                valorAUniformeFinGas = Convert.ToDouble(txtFinGasDesde.Text);
                valorBUniformeFinGas = Convert.ToDouble(txtFinGasHasta.Text);
                cteSumadaAUniformeGas = Convert.ToDouble(txtFinGasCte.Text);

                //Instanciar Objetos
                llegadaVehiculoCombustible = new LlegadaVehiculosCombustible(mediaLlegadaCombustible, random);
                llegadaVehiculoGas = new LlegadaVehiculosGas(mediaLlegadaGas, random);
                finServicioCombustibleSurt1 = new FinServicioCombustible(valorAUniformeFinCombustible, valorBUniformeFinCombustible, 1, random);
                finServicioCombustibleSurt2 = new FinServicioCombustible(valorAUniformeFinCombustible, valorBUniformeFinCombustible, 2, random);
                finServicioCombustibleSurt3 = new FinServicioCombustible(valorAUniformeFinCombustible, valorBUniformeFinCombustible, 3, random);
                finServicioCombustibleSurt4 = new FinServicioCombustible(valorAUniformeFinCombustible, valorBUniformeFinCombustible, 4, random);
                finServicioGasSurt1 = new FinServicioGas(valorAUniformeFinGas, valorBUniformeFinGas, 1, cteSumadaAUniformeGas, random);
                finServicioGasSurt2 = new FinServicioGas(valorAUniformeFinGas, valorBUniformeFinGas, 2, cteSumadaAUniformeGas, random);
                finServicioGasSurt3 = new FinServicioGas(valorAUniformeFinGas, valorBUniformeFinGas, 3, cteSumadaAUniformeGas, random);
                SurtidorCombustible1 = new Surtidor(1, _TipoServicio.Combustible, 0.00, "Libre");
                SurtidorCombustible2 = new Surtidor(2, _TipoServicio.Combustible, 0.00, "Libre");
                SurtidorCombustible3 = new Surtidor(3, _TipoServicio.Combustible, -1, "Libre");
                SurtidorCombustible4 = new Surtidor(4, _TipoServicio.Combustible, 0.00, "Libre");
                SurtidorGas1 = new Surtidor(1, _TipoServicio.Gas, 0.00, "Libre");
                SurtidorGas2 = new Surtidor(2, _TipoServicio.Gas, -1, "Libre");
                SurtidorGas3 = new Surtidor(3, _TipoServicio.Gas, 0.00, "Libre");


                //Creacion de Variables Auxiliares
                Vehiculo vehiculoActualSurtComb1 = null;
                Vehiculo vehiculoActualSurtComb2 = null;
                Vehiculo vehiculoActualSurtComb3 = null;
                Vehiculo vehiculoActualSurtComb4 = null;
                Vehiculo vehiculoActualSurtGas1 = null;
                Vehiculo vehiculoActualSurtGas2 = null;
                Vehiculo vehiculoActualSurtGas3 = null;

                //bloqueo el boton simular para obligar a resetear antes de una nueva simulacion
                btnSimular.Enabled = false;

                //Limpiar Grilla 
                dgvResultados.Rows.Clear();

                //Inicio de La Simulacion
                if (relojSimulacion == 0.00)//Calculos los primeros eventos
                {
                    llegadaVehiculoCombustible.simular(relojSimulacion);
                    llegadaVehiculoGas.simular(relojSimulacion);

                    if (tiempoAPartirDeDondeMostrar == 0.00 && eventosYaSimuladosYMostrados < cantidadDeEventosAMostrar)//Veo si tengo que graficar
                    {

                        int i = dgvResultados.Rows.Add();
                        agregarEventoAGrilla(i, true, llegadaVehiculoCombustible, 0);
                        agregarEventoAGrilla(i, true, llegadaVehiculoGas, 0);
                        agregarDatosAGrila(i, "Inicio Simulacion");
                        agregarSurtidoresAGrilla(i);
                        agregarEventoAGrilla(i, true, finServicioCombustibleSurt1, finServicioCombustibleSurt1.getIdSurtidor());
                        agregarEventoAGrilla(i, true, finServicioCombustibleSurt2, finServicioCombustibleSurt2.getIdSurtidor());
                        agregarEventoAGrilla(i, true, finServicioCombustibleSurt3, finServicioCombustibleSurt3.getIdSurtidor());
                        agregarEventoAGrilla(i, true, finServicioCombustibleSurt4, finServicioCombustibleSurt4.getIdSurtidor());
                        agregarEventoAGrilla(i, true, finServicioGasSurt1, finServicioGasSurt1.getIdSurtidor());
                        agregarEventoAGrilla(i, true, finServicioGasSurt2, finServicioGasSurt2.getIdSurtidor());
                        agregarEventoAGrilla(i, true, finServicioGasSurt3, finServicioGasSurt3.getIdSurtidor());

                        eventosYaSimuladosYMostrados++;
                    }


                }

                //Bucle principal de Simulacion
                while (relojSimulacion < tiempoFinCorrida)
                {

                    Evento siguienteEvento = llegadaVehiculoCombustible.getSiguienteEvento(llegadaVehiculoGas.getSiguienteEvento(finServicioCombustibleSurt1.getSiguienteEvento(finServicioCombustibleSurt2.getSiguienteEvento(finServicioCombustibleSurt3.getSiguienteEvento(finServicioCombustibleSurt4.getSiguienteEvento(finServicioGasSurt1.getSiguienteEvento(finServicioGasSurt2.getSiguienteEvento(finServicioGasSurt3))))))));

                    relojSimulacion = siguienteEvento.getProximaLlegada();//Actualizo el reloj

                    //Dependiendo del tipo de evento que sea sera lo que se simule

                    if (siguienteEvento is LlegadaVehiculosCombustible)//Si el evento es una llegada de un vehiculo a cargar combustible
                    {
                        int i = 0;
                        identificadorVehiculo++;

                        //Veo cual surtidor de combustible tiene una cola menor al parametro cola max
                        if (SurtidorCombustible1.tamañoCola() < colaMaxima)
                        {
                            //Hay Lugar en el surtidor 1
                            if (SurtidorCombustible1.Estado == _EstadoSurtidor.Libre)//Veo Si esta atendiendo a un vehiculos
                            {
                                SurtidorCombustible1.Estado = _EstadoSurtidor.Ocupado;//Actualizo el estado del Surtidor
                                vehiculoActualSurtComb1 = new Vehiculo("EstadoVehi" + identificadorVehiculo, _EstadoVehiculo.Siendo_Atendido_Conbustible, _TipoServicio.Combustible, 0.00);
                                finServicioCombustibleSurt1.simular(relojSimulacion);

                                AcumTiempoOcioServidoresCombustible += (relojSimulacion - SurtidorCombustible1.getHoraInicioOcio());
                                SurtidorCombustible1.setHoraInicioOcio(-1.00);
                                ContVehiConTiempoEsperaVehiculosCombustible++;

                                //Pregunto si tengo que mostrar en grilla
                                if (tiempoAPartirDeDondeMostrar<relojSimulacion&&eventosYaSimuladosYMostrados < cantidadDeEventosAMostrar)
                                {

                                    i = dgvResultados.Rows.Add();
                                    agregarEventoAGrilla(i, true, llegadaVehiculoCombustible, 0);
                                    agregarEventoAGrilla(i, true, llegadaVehiculoGas, 0);
                                    agregarDatosAGrila(i, ((LlegadaVehiculosCombustible)siguienteEvento).getNombreEvento());
                                    agregarSurtidoresAGrilla(i);
                                    agregarEventoAGrilla(i, true, finServicioCombustibleSurt1, finServicioCombustibleSurt1.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioCombustibleSurt2, finServicioCombustibleSurt2.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioCombustibleSurt3, finServicioCombustibleSurt3.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioCombustibleSurt4, finServicioCombustibleSurt4.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioGasSurt1, finServicioGasSurt1.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioGasSurt2, finServicioGasSurt2.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioGasSurt3, finServicioGasSurt3.getIdSurtidor());

                                    eventosYaSimuladosYMostrados++;
                                }
                            }
                            else//Vehiculo a la cola
                            {
                                SurtidorCombustible1.ponerEnCola(new Vehiculo("EstadoVehi" + identificadorVehiculo, _EstadoVehiculo.Esperando_Atencion_Conbustible, _TipoServicio.Combustible, relojSimulacion));

                                //Pregunto si tengo que mostrar en grilla
                                if (tiempoAPartirDeDondeMostrar < relojSimulacion && eventosYaSimuladosYMostrados < cantidadDeEventosAMostrar)
                                {

                                    i = dgvResultados.Rows.Add();
                                    agregarEventoAGrilla(i, true, llegadaVehiculoCombustible, 0);
                                    agregarEventoAGrilla(i, true, llegadaVehiculoGas, 0);
                                    agregarDatosAGrila(i, ((LlegadaVehiculosCombustible)siguienteEvento).getNombreEvento());
                                    agregarSurtidoresAGrilla(i);
                                    agregarEventoAGrilla(i, true, finServicioCombustibleSurt1, finServicioCombustibleSurt1.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioCombustibleSurt2, finServicioCombustibleSurt2.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioCombustibleSurt3, finServicioCombustibleSurt3.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioCombustibleSurt4, finServicioCombustibleSurt4.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioGasSurt1, finServicioGasSurt1.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioGasSurt2, finServicioGasSurt2.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioGasSurt3, finServicioGasSurt3.getIdSurtidor());

                                    eventosYaSimuladosYMostrados++;
                                }
                            }

                            ContVehiculosCombustibleIngresanAlSitema++;
                        }
                        else if (SurtidorCombustible2.tamañoCola() < colaMaxima)
                        {
                            //Hay Lugar en el surtidor 2
                            if (SurtidorCombustible2.Estado == _EstadoSurtidor.Libre)//Veo Si esta atendiendo a un vehiculos
                            {
                                SurtidorCombustible2.Estado = _EstadoSurtidor.Ocupado;//Actualizo el estado del Surtidor
                                vehiculoActualSurtComb2 = new Vehiculo("EstadoVehi" + identificadorVehiculo, _EstadoVehiculo.Siendo_Atendido_Conbustible, _TipoServicio.Combustible, 0.00);
                                finServicioCombustibleSurt2.simular(relojSimulacion);

                                AcumTiempoOcioServidoresCombustible += (relojSimulacion - SurtidorCombustible2.getHoraInicioOcio());
                                SurtidorCombustible2.setHoraInicioOcio(-1.00);
                                ContVehiConTiempoEsperaVehiculosCombustible++;

                                //Pregunto si tengo que mostrar en grilla
                                if (tiempoAPartirDeDondeMostrar < relojSimulacion && eventosYaSimuladosYMostrados < cantidadDeEventosAMostrar)
                                {

                                    i = dgvResultados.Rows.Add();
                                    agregarEventoAGrilla(i, true, llegadaVehiculoCombustible, 0);
                                    agregarEventoAGrilla(i, true, llegadaVehiculoGas, 0);
                                    agregarDatosAGrila(i, ((LlegadaVehiculosCombustible)siguienteEvento).getNombreEvento());
                                    agregarSurtidoresAGrilla(i);
                                    agregarEventoAGrilla(i, true, finServicioCombustibleSurt1, finServicioCombustibleSurt1.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioCombustibleSurt2, finServicioCombustibleSurt2.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioCombustibleSurt3, finServicioCombustibleSurt3.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioCombustibleSurt4, finServicioCombustibleSurt4.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioGasSurt1, finServicioGasSurt1.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioGasSurt2, finServicioGasSurt2.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioGasSurt3, finServicioGasSurt3.getIdSurtidor());

                                    eventosYaSimuladosYMostrados++;
                                }

                            }
                            else//Vehiculo a la cola
                            {
                                SurtidorCombustible2.ponerEnCola(new Vehiculo("EstadoVehi" + identificadorVehiculo, _EstadoVehiculo.Esperando_Atencion_Conbustible, _TipoServicio.Combustible, relojSimulacion));

                                //Pregunto si tengo que mostrar en grilla
                                if (tiempoAPartirDeDondeMostrar < relojSimulacion && eventosYaSimuladosYMostrados < cantidadDeEventosAMostrar)
                                {

                                    i = dgvResultados.Rows.Add();
                                    agregarEventoAGrilla(i, true, llegadaVehiculoCombustible, 0);
                                    agregarEventoAGrilla(i, true, llegadaVehiculoGas, 0);
                                    agregarDatosAGrila(i, ((LlegadaVehiculosCombustible)siguienteEvento).getNombreEvento());
                                    agregarSurtidoresAGrilla(i);
                                    agregarEventoAGrilla(i, true, finServicioCombustibleSurt1, finServicioCombustibleSurt1.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioCombustibleSurt2, finServicioCombustibleSurt2.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioCombustibleSurt3, finServicioCombustibleSurt3.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioCombustibleSurt4, finServicioCombustibleSurt4.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioGasSurt1, finServicioGasSurt1.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioGasSurt2, finServicioGasSurt2.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioGasSurt3, finServicioGasSurt3.getIdSurtidor());

                                    eventosYaSimuladosYMostrados++;
                                }

                            }

                            ContVehiculosCombustibleIngresanAlSitema++;

                        }
                        else if (SurtidorCombustible3.tamañoCola() < colaMaxima)
                        {
                            //Hay Lugar en el surtidor 3
                            if (SurtidorCombustible3.Estado == _EstadoSurtidor.Libre)//Veo Si esta atendiendo a un vehiculos
                            {
                                SurtidorCombustible3.Estado = _EstadoSurtidor.Ocupado;//Actualizo el estado del Surtidor
                                vehiculoActualSurtComb3 = new Vehiculo("EstadoVehi" + identificadorVehiculo, _EstadoVehiculo.Siendo_Atendido_Conbustible, _TipoServicio.Combustible, 0.00);
                                finServicioCombustibleSurt3.simular(relojSimulacion);

                                AcumTiempoOcioServidoresCombustible += (relojSimulacion - SurtidorCombustible3.getHoraInicioOcio());
                                SurtidorCombustible3.setHoraInicioOcio(-1.00);
                                ContVehiConTiempoEsperaVehiculosCombustible++;

                                //Pregunto si tengo que mostrar en grilla
                                if (tiempoAPartirDeDondeMostrar < relojSimulacion && eventosYaSimuladosYMostrados < cantidadDeEventosAMostrar)
                                {

                                    i = dgvResultados.Rows.Add();
                                    agregarEventoAGrilla(i, true, llegadaVehiculoCombustible, 0);
                                    agregarEventoAGrilla(i, true, llegadaVehiculoGas, 0);
                                    agregarDatosAGrila(i, ((LlegadaVehiculosCombustible)siguienteEvento).getNombreEvento());
                                    agregarSurtidoresAGrilla(i);
                                    agregarEventoAGrilla(i, true, finServicioCombustibleSurt1, finServicioCombustibleSurt1.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioCombustibleSurt2, finServicioCombustibleSurt2.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioCombustibleSurt3, finServicioCombustibleSurt3.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioCombustibleSurt4, finServicioCombustibleSurt4.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioGasSurt1, finServicioGasSurt1.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioGasSurt2, finServicioGasSurt2.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioGasSurt3, finServicioGasSurt3.getIdSurtidor());

                                    eventosYaSimuladosYMostrados++;
                                }

                            }
                            else//Vehiculo a la cola
                            {
                                SurtidorCombustible3.ponerEnCola(new Vehiculo("EstadoVehi" + identificadorVehiculo, _EstadoVehiculo.Esperando_Atencion_Conbustible, _TipoServicio.Combustible, relojSimulacion));

                                //Pregunto si tengo que mostrar en grilla
                                if (tiempoAPartirDeDondeMostrar < relojSimulacion && eventosYaSimuladosYMostrados < cantidadDeEventosAMostrar)
                                {

                                    i = dgvResultados.Rows.Add();
                                    agregarEventoAGrilla(i, true, llegadaVehiculoCombustible, 0);
                                    agregarEventoAGrilla(i, true, llegadaVehiculoGas, 0);
                                    agregarDatosAGrila(i, ((LlegadaVehiculosCombustible)siguienteEvento).getNombreEvento());
                                    agregarSurtidoresAGrilla(i);
                                    agregarEventoAGrilla(i, true, finServicioCombustibleSurt1, finServicioCombustibleSurt1.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioCombustibleSurt2, finServicioCombustibleSurt2.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioCombustibleSurt3, finServicioCombustibleSurt3.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioCombustibleSurt4, finServicioCombustibleSurt4.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioGasSurt1, finServicioGasSurt1.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioGasSurt2, finServicioGasSurt2.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioGasSurt3, finServicioGasSurt3.getIdSurtidor());

                                    eventosYaSimuladosYMostrados++;
                                }
                            }

                            ContVehiculosCombustibleIngresanAlSitema++;
                        }
                        else if (SurtidorCombustible4.tamañoCola() < colaMaxima)
                        {
                            //Hay Lugar en el surtidor 4
                            if (SurtidorCombustible4.Estado == _EstadoSurtidor.Libre)//Veo Si esta atendiendo a un vehiculos
                            {
                                SurtidorCombustible4.Estado = _EstadoSurtidor.Ocupado;//Actualizo el estado del Surtidor
                                vehiculoActualSurtComb4 = new Vehiculo("EstadoVehi" + identificadorVehiculo, _EstadoVehiculo.Siendo_Atendido_Conbustible, _TipoServicio.Combustible, 0.00);
                                finServicioCombustibleSurt4.simular(relojSimulacion);

                                AcumTiempoOcioServidoresCombustible += (relojSimulacion - SurtidorCombustible4.getHoraInicioOcio());
                                SurtidorCombustible4.setHoraInicioOcio(-1.00);
                                ContVehiConTiempoEsperaVehiculosCombustible++;

                                //Pregunto si tengo que mostrar en grilla
                                if (tiempoAPartirDeDondeMostrar < relojSimulacion && eventosYaSimuladosYMostrados < cantidadDeEventosAMostrar)
                                {

                                    i = dgvResultados.Rows.Add();
                                    agregarEventoAGrilla(i, true, llegadaVehiculoCombustible, 0);
                                    agregarEventoAGrilla(i, true, llegadaVehiculoGas, 0);
                                    agregarDatosAGrila(i, ((LlegadaVehiculosCombustible)siguienteEvento).getNombreEvento());
                                    agregarSurtidoresAGrilla(i);
                                    agregarEventoAGrilla(i, true, finServicioCombustibleSurt1, finServicioCombustibleSurt1.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioCombustibleSurt2, finServicioCombustibleSurt2.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioCombustibleSurt3, finServicioCombustibleSurt3.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioCombustibleSurt4, finServicioCombustibleSurt4.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioGasSurt1, finServicioGasSurt1.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioGasSurt2, finServicioGasSurt2.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioGasSurt3, finServicioGasSurt3.getIdSurtidor());

                                    eventosYaSimuladosYMostrados++;
                                }
                            }
                            else//Vehiculo a la cola
                            {
                                SurtidorCombustible4.ponerEnCola(new Vehiculo("EstadoVehi" + identificadorVehiculo, _EstadoVehiculo.Esperando_Atencion_Conbustible, _TipoServicio.Combustible, relojSimulacion));

                                //Pregunto si tengo que mostrar en grilla
                                if (tiempoAPartirDeDondeMostrar < relojSimulacion && eventosYaSimuladosYMostrados < cantidadDeEventosAMostrar)
                                {

                                    i = dgvResultados.Rows.Add();
                                    agregarEventoAGrilla(i, true, llegadaVehiculoCombustible, 0);
                                    agregarEventoAGrilla(i, true, llegadaVehiculoGas, 0);
                                    agregarDatosAGrila(i, ((LlegadaVehiculosCombustible)siguienteEvento).getNombreEvento());
                                    agregarSurtidoresAGrilla(i);
                                    agregarEventoAGrilla(i, true, finServicioCombustibleSurt1, finServicioCombustibleSurt1.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioCombustibleSurt2, finServicioCombustibleSurt2.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioCombustibleSurt3, finServicioCombustibleSurt3.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioCombustibleSurt4, finServicioCombustibleSurt4.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioGasSurt1, finServicioGasSurt1.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioGasSurt2, finServicioGasSurt2.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioGasSurt3, finServicioGasSurt3.getIdSurtidor());

                                    eventosYaSimuladosYMostrados++;
                                }
                            }

                            ContVehiculosCombustibleIngresanAlSitema++;
                        }
                        else
                        {
                            //No hay lugar en ningun surtidor de combustible
                            ContVehiculosCombustibleRechazados++;
                            identificadorVehiculo--;

                            //Pregunto si tengo que mostrar en grilla
                            if (tiempoAPartirDeDondeMostrar < relojSimulacion && eventosYaSimuladosYMostrados < cantidadDeEventosAMostrar)
                            {

                                i = dgvResultados.Rows.Add();
                                agregarEventoAGrilla(i, true, llegadaVehiculoCombustible, 0);
                                agregarEventoAGrilla(i, true, llegadaVehiculoGas, 0);
                                agregarDatosAGrila(i, ((LlegadaVehiculosCombustible)siguienteEvento).getNombreEvento());
                                agregarSurtidoresAGrilla(i);
                                agregarEventoAGrilla(i, true, finServicioCombustibleSurt1, finServicioCombustibleSurt1.getIdSurtidor());
                                agregarEventoAGrilla(i, true, finServicioCombustibleSurt2, finServicioCombustibleSurt2.getIdSurtidor());
                                agregarEventoAGrilla(i, true, finServicioCombustibleSurt3, finServicioCombustibleSurt3.getIdSurtidor());
                                agregarEventoAGrilla(i, true, finServicioCombustibleSurt4, finServicioCombustibleSurt4.getIdSurtidor());
                                agregarEventoAGrilla(i, true, finServicioGasSurt1, finServicioGasSurt1.getIdSurtidor());
                                agregarEventoAGrilla(i, true, finServicioGasSurt2, finServicioGasSurt2.getIdSurtidor());
                                agregarEventoAGrilla(i, true, finServicioGasSurt3, finServicioGasSurt3.getIdSurtidor());

                                eventosYaSimuladosYMostrados++;
                            }

                        }
                        llegadaVehiculoCombustible.simular(relojSimulacion);

                    }
                    else if (siguienteEvento is LlegadaVehiculosGas)//Si el evento es una llegada de un vehiculo a cargar gas
                    {
                        int i = 0;
                        identificadorVehiculo++;


                        //Veo cual surtidor de Gas tiene una cola menor al parametro cola max
                        if (SurtidorGas1.tamañoCola() < colaMaxima)
                        {
                            //Hay Lugar en el surtidor 1
                            if (SurtidorGas1.Estado == _EstadoSurtidor.Libre)//Veo Si esta atendiendo a un vehiculos
                            {
                                SurtidorGas1.Estado = _EstadoSurtidor.Ocupado;
                                vehiculoActualSurtGas1 = new Vehiculo("EstadoVehi" + identificadorVehiculo, _EstadoVehiculo.Siendo_Atendido_Gas, _TipoServicio.Gas, 0.00);
                                finServicioGasSurt1.simular(relojSimulacion);

                                AcumTiempoOcioServidoresGas += (relojSimulacion - SurtidorGas1.getHoraInicioOcio());
                                SurtidorGas1.setHoraInicioOcio(-1.00);
                                ContVehiConTiempoEsperaVehiculosGas++;

                                //Pregunto si tengo que mostrar en grilla
                                if (tiempoAPartirDeDondeMostrar < relojSimulacion && eventosYaSimuladosYMostrados < cantidadDeEventosAMostrar)
                                {

                                    i = dgvResultados.Rows.Add();
                                    agregarEventoAGrilla(i, true, llegadaVehiculoCombustible, 0);
                                    agregarEventoAGrilla(i, true, llegadaVehiculoGas, 0);
                                    agregarDatosAGrila(i, ((LlegadaVehiculosGas)siguienteEvento).getNombreEvento());
                                    agregarSurtidoresAGrilla(i);
                                    agregarEventoAGrilla(i, true, finServicioCombustibleSurt1, finServicioCombustibleSurt1.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioCombustibleSurt2, finServicioCombustibleSurt2.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioCombustibleSurt3, finServicioCombustibleSurt3.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioCombustibleSurt4, finServicioCombustibleSurt4.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioGasSurt1, finServicioGasSurt1.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioGasSurt2, finServicioGasSurt2.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioGasSurt3, finServicioGasSurt3.getIdSurtidor());

                                    eventosYaSimuladosYMostrados++;
                                }
                            }
                            else//Vehiculo a la cola
                            {
                                SurtidorGas1.ponerEnCola(new Vehiculo("EstadoVehi" + identificadorVehiculo, _EstadoVehiculo.Siendo_Atendido_Gas, _TipoServicio.Gas, relojSimulacion));

                                //Pregunto si tengo que mostrar en grilla
                                if (tiempoAPartirDeDondeMostrar < relojSimulacion && eventosYaSimuladosYMostrados < cantidadDeEventosAMostrar)
                                {

                                    i = dgvResultados.Rows.Add();
                                    agregarEventoAGrilla(i, true, llegadaVehiculoCombustible, 0);
                                    agregarEventoAGrilla(i, true, llegadaVehiculoGas, 0);
                                    agregarDatosAGrila(i, ((LlegadaVehiculosGas)siguienteEvento).getNombreEvento());
                                    agregarSurtidoresAGrilla(i);
                                    agregarEventoAGrilla(i, true, finServicioCombustibleSurt1, finServicioCombustibleSurt1.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioCombustibleSurt2, finServicioCombustibleSurt2.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioCombustibleSurt3, finServicioCombustibleSurt3.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioCombustibleSurt4, finServicioCombustibleSurt4.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioGasSurt1, finServicioGasSurt1.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioGasSurt2, finServicioGasSurt2.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioGasSurt3, finServicioGasSurt3.getIdSurtidor());

                                    eventosYaSimuladosYMostrados++;
                                }
                            }
                            ContVehiculosGasIngresanAlSitema++;

                        }
                        else if (SurtidorGas2.tamañoCola() < colaMaxima)
                        {
                            //Hay Lugar en el surtidor 2
                            if (SurtidorGas2.Estado == _EstadoSurtidor.Libre)//Veo Si esta atendiendo a un vehiculos
                            {
                                SurtidorGas2.Estado = _EstadoSurtidor.Ocupado;
                                vehiculoActualSurtGas2 = new Vehiculo("EstadoVehi" + identificadorVehiculo, _EstadoVehiculo.Siendo_Atendido_Gas, _TipoServicio.Gas, 0.00);
                                finServicioGasSurt2.simular(relojSimulacion);

                                AcumTiempoOcioServidoresGas += (relojSimulacion - SurtidorGas2.getHoraInicioOcio());
                                SurtidorGas2.setHoraInicioOcio(-1.00);
                                ContVehiConTiempoEsperaVehiculosGas++;

                                //Pregunto si tengo que mostrar en grilla
                                if (tiempoAPartirDeDondeMostrar < relojSimulacion && eventosYaSimuladosYMostrados < cantidadDeEventosAMostrar)
                                {

                                    i = dgvResultados.Rows.Add();
                                    agregarEventoAGrilla(i, true, llegadaVehiculoCombustible, 0);
                                    agregarEventoAGrilla(i, true, llegadaVehiculoGas, 0);
                                    agregarDatosAGrila(i, ((LlegadaVehiculosGas)siguienteEvento).getNombreEvento());
                                    agregarSurtidoresAGrilla(i);
                                    agregarEventoAGrilla(i, true, finServicioCombustibleSurt1, finServicioCombustibleSurt1.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioCombustibleSurt2, finServicioCombustibleSurt2.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioCombustibleSurt3, finServicioCombustibleSurt3.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioCombustibleSurt4, finServicioCombustibleSurt4.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioGasSurt1, finServicioGasSurt1.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioGasSurt2, finServicioGasSurt2.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioGasSurt3, finServicioGasSurt3.getIdSurtidor());

                                    eventosYaSimuladosYMostrados++;
                                }
                            }
                            else//Vehiculo a la cola
                            {
                                SurtidorGas2.ponerEnCola(new Vehiculo("EstadoVehi" + identificadorVehiculo, _EstadoVehiculo.Siendo_Atendido_Gas, _TipoServicio.Gas, relojSimulacion));

                                //Pregunto si tengo que mostrar en grilla
                                if (tiempoAPartirDeDondeMostrar < relojSimulacion && eventosYaSimuladosYMostrados < cantidadDeEventosAMostrar)
                                {

                                    i = dgvResultados.Rows.Add();
                                    agregarEventoAGrilla(i, true, llegadaVehiculoCombustible, 0);
                                    agregarEventoAGrilla(i, true, llegadaVehiculoGas, 0);
                                    agregarDatosAGrila(i, ((LlegadaVehiculosGas)siguienteEvento).getNombreEvento());
                                    agregarSurtidoresAGrilla(i);
                                    agregarEventoAGrilla(i, true, finServicioCombustibleSurt1, finServicioCombustibleSurt1.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioCombustibleSurt2, finServicioCombustibleSurt2.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioCombustibleSurt3, finServicioCombustibleSurt3.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioCombustibleSurt4, finServicioCombustibleSurt4.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioGasSurt1, finServicioGasSurt1.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioGasSurt2, finServicioGasSurt2.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioGasSurt3, finServicioGasSurt3.getIdSurtidor());

                                    eventosYaSimuladosYMostrados++;
                                }
                            }
                            ContVehiculosGasIngresanAlSitema++;

                        }
                        else if (SurtidorGas3.tamañoCola() < colaMaxima)
                        {
                            //Hay Lugar en el surtidor 3
                            if (SurtidorGas3.Estado == _EstadoSurtidor.Libre)//Veo Si esta atendiendo a un vehiculos
                            {
                                SurtidorGas3.Estado = _EstadoSurtidor.Ocupado;
                                vehiculoActualSurtGas3 = new Vehiculo("EstadoVehi" + identificadorVehiculo, _EstadoVehiculo.Siendo_Atendido_Gas, _TipoServicio.Gas, 0.00);
                                finServicioGasSurt3.simular(relojSimulacion);

                                AcumTiempoOcioServidoresGas += (relojSimulacion - SurtidorGas3.getHoraInicioOcio());
                                SurtidorGas3.setHoraInicioOcio(-1.00);
                                ContVehiConTiempoEsperaVehiculosGas++;

                                //Pregunto si tengo que mostrar en grilla
                                if (tiempoAPartirDeDondeMostrar < relojSimulacion && eventosYaSimuladosYMostrados < cantidadDeEventosAMostrar)
                                {

                                    i = dgvResultados.Rows.Add();
                                    agregarEventoAGrilla(i, true, llegadaVehiculoCombustible, 0);
                                    agregarEventoAGrilla(i, true, llegadaVehiculoGas, 0);
                                    agregarDatosAGrila(i, ((LlegadaVehiculosGas)siguienteEvento).getNombreEvento());
                                    agregarSurtidoresAGrilla(i);
                                    agregarEventoAGrilla(i, true, finServicioCombustibleSurt1, finServicioCombustibleSurt1.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioCombustibleSurt2, finServicioCombustibleSurt2.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioCombustibleSurt3, finServicioCombustibleSurt3.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioCombustibleSurt4, finServicioCombustibleSurt4.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioGasSurt1, finServicioGasSurt1.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioGasSurt2, finServicioGasSurt2.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioGasSurt3, finServicioGasSurt3.getIdSurtidor());

                                    eventosYaSimuladosYMostrados++;
                                }
                            }
                            else//Vehiculo a la cola
                            {
                                SurtidorGas3.ponerEnCola(new Vehiculo("EstadoVehi" + identificadorVehiculo, _EstadoVehiculo.Siendo_Atendido_Gas, _TipoServicio.Gas, relojSimulacion));

                                //Pregunto si tengo que mostrar en grilla
                                if (tiempoAPartirDeDondeMostrar < relojSimulacion && eventosYaSimuladosYMostrados < cantidadDeEventosAMostrar)
                                {

                                    i = dgvResultados.Rows.Add();
                                    agregarEventoAGrilla(i, true, llegadaVehiculoCombustible, 0);
                                    agregarEventoAGrilla(i, true, llegadaVehiculoGas, 0);
                                    agregarDatosAGrila(i, ((LlegadaVehiculosGas)siguienteEvento).getNombreEvento());
                                    agregarSurtidoresAGrilla(i);
                                    agregarEventoAGrilla(i, true, finServicioCombustibleSurt1, finServicioCombustibleSurt1.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioCombustibleSurt2, finServicioCombustibleSurt2.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioCombustibleSurt3, finServicioCombustibleSurt3.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioCombustibleSurt4, finServicioCombustibleSurt4.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioGasSurt1, finServicioGasSurt1.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioGasSurt2, finServicioGasSurt2.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioGasSurt3, finServicioGasSurt3.getIdSurtidor());

                                    eventosYaSimuladosYMostrados++;
                                }
                            }
                            ContVehiculosGasIngresanAlSitema++;
                        }
                        else
                        {
                            //No hay lugar en ningun surtidor de combustible
                            ContVehiculosGasRechazados++;
                            identificadorVehiculo--;

                        }
                        llegadaVehiculoGas.simular(relojSimulacion);

                    }
                    else if (siguienteEvento is FinServicioGas)//Si el evento es un fin de atencion gas
                    {
                        int i = 0;

                        switch (((FinServicioGas)siguienteEvento).getIdSurtidor())//Veo cual surtidor termino de cargar
                        {
                            case 1:
                                vehiculoActualSurtGas1 = null;
                                ContVehiculosGasAtendidos++;
                                finServicioGasSurt1.setHoraFin(0.00);

                                if (SurtidorGas1.tamañoCola() > 0)//Veo si hay vehiculos esperando en cola
                                {
                                    vehiculoActualSurtGas1 = SurtidorGas1.sacarDeCola(); //Saco uno de la cola
                                    vehiculoActualSurtGas1.Estado = _EstadoVehiculo.Siendo_Atendido_Gas;
                                    AcumTiempoEsperaVehiculosGas += (relojSimulacion - vehiculoActualSurtGas1.getInicioEspera());
                                    ContVehiConTiempoEsperaVehiculosGas++;
                                    finServicioGasSurt1.simular(relojSimulacion);


                                }
                                else
                                {
                                    SurtidorGas1.Estado = _EstadoSurtidor.Libre;
                                    SurtidorGas1.setHoraInicioOcio(relojSimulacion);
                                }

                                //Pregunto si tengo que mostrar en grilla
                                if (tiempoAPartirDeDondeMostrar < relojSimulacion && eventosYaSimuladosYMostrados < cantidadDeEventosAMostrar)
                                {

                                    i = dgvResultados.Rows.Add();
                                    agregarEventoAGrilla(i, true, llegadaVehiculoCombustible, 0);
                                    agregarEventoAGrilla(i, true, llegadaVehiculoGas, 0);
                                    agregarDatosAGrila(i, ((FinServicioGas)siguienteEvento).getNombreEvento());
                                    agregarSurtidoresAGrilla(i);
                                    agregarEventoAGrilla(i, true, finServicioCombustibleSurt1, finServicioCombustibleSurt1.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioCombustibleSurt2, finServicioCombustibleSurt2.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioCombustibleSurt3, finServicioCombustibleSurt3.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioCombustibleSurt4, finServicioCombustibleSurt4.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioGasSurt1, finServicioGasSurt1.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioGasSurt2, finServicioGasSurt2.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioGasSurt3, finServicioGasSurt3.getIdSurtidor());

                                    eventosYaSimuladosYMostrados++;
                                }

                                break;
                            case 2:
                                vehiculoActualSurtGas2 = null;
                                ContVehiculosGasAtendidos++;
                                finServicioGasSurt2.setHoraFin(0.00);

                                if (SurtidorGas2.tamañoCola() > 0)//Veo si hay vehiculos esperando en cola
                                {
                                    vehiculoActualSurtGas2 = SurtidorGas2.sacarDeCola(); //Saco uno de la cola
                                    vehiculoActualSurtGas2.Estado = _EstadoVehiculo.Siendo_Atendido_Gas;
                                    AcumTiempoEsperaVehiculosGas += (relojSimulacion - vehiculoActualSurtGas2.getInicioEspera());
                                    ContVehiConTiempoEsperaVehiculosGas++;
                                    finServicioGasSurt2.simular(relojSimulacion);


                                }
                                else
                                {
                                    SurtidorGas2.Estado = _EstadoSurtidor.Libre;
                                    SurtidorGas2.setHoraInicioOcio(relojSimulacion);
                                }

                                //Pregunto si tengo que mostrar en grilla
                                if (tiempoAPartirDeDondeMostrar < relojSimulacion && eventosYaSimuladosYMostrados < cantidadDeEventosAMostrar)
                                {

                                    i = dgvResultados.Rows.Add();
                                    agregarEventoAGrilla(i, true, llegadaVehiculoCombustible, 0);
                                    agregarEventoAGrilla(i, true, llegadaVehiculoGas, 0);
                                    agregarDatosAGrila(i, ((FinServicioGas)siguienteEvento).getNombreEvento());
                                    agregarSurtidoresAGrilla(i);
                                    agregarEventoAGrilla(i, true, finServicioCombustibleSurt1, finServicioCombustibleSurt1.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioCombustibleSurt2, finServicioCombustibleSurt2.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioCombustibleSurt3, finServicioCombustibleSurt3.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioCombustibleSurt4, finServicioCombustibleSurt4.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioGasSurt1, finServicioGasSurt1.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioGasSurt2, finServicioGasSurt2.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioGasSurt3, finServicioGasSurt3.getIdSurtidor());

                                    eventosYaSimuladosYMostrados++;
                                }

                                break;
                            case 3:
                                vehiculoActualSurtGas3 = null;
                                ContVehiculosGasAtendidos++;
                                finServicioGasSurt3.setHoraFin(0.00);

                                if (SurtidorGas3.tamañoCola() > 0)//Veo si hay vehiculos esperando en cola
                                {
                                    vehiculoActualSurtGas3 = SurtidorGas3.sacarDeCola(); //Saco uno de la cola
                                    vehiculoActualSurtGas3.Estado = _EstadoVehiculo.Siendo_Atendido_Gas;
                                    AcumTiempoEsperaVehiculosGas += (relojSimulacion - vehiculoActualSurtGas3.getInicioEspera());
                                    ContVehiConTiempoEsperaVehiculosGas++;
                                    finServicioGasSurt3.simular(relojSimulacion);


                                }
                                else
                                {
                                    SurtidorGas3.Estado = _EstadoSurtidor.Libre;
                                    SurtidorGas3.setHoraInicioOcio(relojSimulacion);
                                }

                                //Pregunto si tengo que mostrar en grilla
                                if (tiempoAPartirDeDondeMostrar < relojSimulacion && eventosYaSimuladosYMostrados < cantidadDeEventosAMostrar)
                                {

                                    i = dgvResultados.Rows.Add();
                                    agregarEventoAGrilla(i, true, llegadaVehiculoCombustible, 0);
                                    agregarEventoAGrilla(i, true, llegadaVehiculoGas, 0);
                                    agregarDatosAGrila(i, ((FinServicioGas)siguienteEvento).getNombreEvento());
                                    agregarSurtidoresAGrilla(i);
                                    agregarEventoAGrilla(i, true, finServicioCombustibleSurt1, finServicioCombustibleSurt1.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioCombustibleSurt2, finServicioCombustibleSurt2.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioCombustibleSurt3, finServicioCombustibleSurt3.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioCombustibleSurt4, finServicioCombustibleSurt4.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioGasSurt1, finServicioGasSurt1.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioGasSurt2, finServicioGasSurt2.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioGasSurt3, finServicioGasSurt3.getIdSurtidor());

                                    eventosYaSimuladosYMostrados++;
                                }

                                break;


                        }

                    }
                    else if (siguienteEvento is FinServicioCombustible)//Si el evento es un fin de atencion combustible
                    {
                        int i = 0;

                        switch (((FinServicioCombustible)siguienteEvento).getIdSurtidor())//Veo si hay vehiculos esperando en cola
                        {
                            case 1:
                                vehiculoActualSurtComb1 = null;
                                ContVehiculosCombustibleAtendidos++;
                                finServicioCombustibleSurt1.setHoraFin(0.00);

                                if (SurtidorCombustible1.tamañoCola() > 0)//Veo si hay vehiculos esperando en cola
                                {
                                    vehiculoActualSurtComb1 = SurtidorCombustible1.sacarDeCola(); //Saco uno de la cola
                                    vehiculoActualSurtComb1.Estado = _EstadoVehiculo.Esperando_Atencion_Conbustible;
                                    AcumTiempoEsperaVehiculosCombustible += (relojSimulacion - vehiculoActualSurtComb1.getInicioEspera());
                                    ContVehiConTiempoEsperaVehiculosCombustible++;
                                    finServicioCombustibleSurt1.simular(relojSimulacion);

                                }
                                else
                                {
                                    SurtidorCombustible1.Estado = _EstadoSurtidor.Libre;
                                    SurtidorCombustible1.setHoraInicioOcio(relojSimulacion);

                                }

                                //Pregunto si tengo que mostrar en grilla
                                if (tiempoAPartirDeDondeMostrar < relojSimulacion && eventosYaSimuladosYMostrados < cantidadDeEventosAMostrar)
                                {

                                    i = dgvResultados.Rows.Add();
                                    agregarEventoAGrilla(i, true, llegadaVehiculoCombustible, 0);
                                    agregarEventoAGrilla(i, true, llegadaVehiculoGas, 0);
                                    agregarDatosAGrila(i, ((FinServicioCombustible)siguienteEvento).getNombreEvento());
                                    agregarSurtidoresAGrilla(i);
                                    agregarEventoAGrilla(i, true, finServicioCombustibleSurt1, finServicioCombustibleSurt1.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioCombustibleSurt2, finServicioCombustibleSurt2.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioCombustibleSurt3, finServicioCombustibleSurt3.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioCombustibleSurt4, finServicioCombustibleSurt4.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioGasSurt1, finServicioGasSurt1.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioGasSurt2, finServicioGasSurt2.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioGasSurt3, finServicioGasSurt3.getIdSurtidor());

                                    eventosYaSimuladosYMostrados++;
                                }

                                break;
                            case 2:
                                vehiculoActualSurtComb2 = null;
                                ContVehiculosCombustibleAtendidos++;
                                finServicioCombustibleSurt2.setHoraFin(0.00);

                                if (SurtidorCombustible2.tamañoCola() > 0)//Veo si hay vehiculos esperando en cola
                                {
                                    vehiculoActualSurtComb2 = SurtidorCombustible2.sacarDeCola(); //Saco uno de la cola
                                    vehiculoActualSurtComb2.Estado = _EstadoVehiculo.Esperando_Atencion_Conbustible;
                                    AcumTiempoEsperaVehiculosCombustible += (relojSimulacion - vehiculoActualSurtComb2.getInicioEspera());
                                    ContVehiConTiempoEsperaVehiculosCombustible++;
                                    finServicioCombustibleSurt2.simular(relojSimulacion);

                                }
                                else
                                {
                                    SurtidorCombustible2.Estado = _EstadoSurtidor.Libre;
                                    SurtidorCombustible2.setHoraInicioOcio(relojSimulacion);

                                }

                                //Pregunto si tengo que mostrar en grilla
                                if (tiempoAPartirDeDondeMostrar < relojSimulacion && eventosYaSimuladosYMostrados < cantidadDeEventosAMostrar)
                                {

                                    i = dgvResultados.Rows.Add();
                                    agregarEventoAGrilla(i, true, llegadaVehiculoCombustible, 0);
                                    agregarEventoAGrilla(i, true, llegadaVehiculoGas, 0);
                                    agregarDatosAGrila(i, ((FinServicioCombustible)siguienteEvento).getNombreEvento());
                                    agregarSurtidoresAGrilla(i);
                                    agregarEventoAGrilla(i, true, finServicioCombustibleSurt1, finServicioCombustibleSurt1.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioCombustibleSurt2, finServicioCombustibleSurt2.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioCombustibleSurt3, finServicioCombustibleSurt3.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioCombustibleSurt4, finServicioCombustibleSurt4.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioGasSurt1, finServicioGasSurt1.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioGasSurt2, finServicioGasSurt2.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioGasSurt3, finServicioGasSurt3.getIdSurtidor());

                                    eventosYaSimuladosYMostrados++;
                                }

                                break;
                            case 3:
                                vehiculoActualSurtComb3 = null;
                                ContVehiculosCombustibleAtendidos++;
                                finServicioCombustibleSurt3.setHoraFin(0.00);

                                if (SurtidorCombustible3.tamañoCola() > 0)//Veo si hay vehiculos esperando en cola
                                {
                                    vehiculoActualSurtComb3 = SurtidorCombustible3.sacarDeCola(); //Saco uno de la cola
                                    vehiculoActualSurtComb3.Estado = _EstadoVehiculo.Esperando_Atencion_Conbustible;
                                    AcumTiempoEsperaVehiculosCombustible += (relojSimulacion - vehiculoActualSurtComb3.getInicioEspera());
                                    ContVehiConTiempoEsperaVehiculosCombustible++;
                                    finServicioCombustibleSurt3.simular(relojSimulacion);

                                }
                                else
                                {
                                    SurtidorCombustible3.Estado = _EstadoSurtidor.Libre;
                                    SurtidorCombustible3.setHoraInicioOcio(relojSimulacion);

                                }

                                //Pregunto si tengo que mostrar en grilla
                                if (tiempoAPartirDeDondeMostrar < relojSimulacion && eventosYaSimuladosYMostrados < cantidadDeEventosAMostrar)
                                {

                                    i = dgvResultados.Rows.Add();
                                    agregarEventoAGrilla(i, true, llegadaVehiculoCombustible, 0);
                                    agregarEventoAGrilla(i, true, llegadaVehiculoGas, 0);
                                    agregarDatosAGrila(i, ((FinServicioCombustible)siguienteEvento).getNombreEvento());
                                    agregarSurtidoresAGrilla(i);
                                    agregarEventoAGrilla(i, true, finServicioCombustibleSurt1, finServicioCombustibleSurt1.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioCombustibleSurt2, finServicioCombustibleSurt2.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioCombustibleSurt3, finServicioCombustibleSurt3.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioCombustibleSurt4, finServicioCombustibleSurt4.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioGasSurt1, finServicioGasSurt1.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioGasSurt2, finServicioGasSurt2.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioGasSurt3, finServicioGasSurt3.getIdSurtidor());

                                    eventosYaSimuladosYMostrados++;
                                }

                                break;
                            case 4:
                                vehiculoActualSurtComb4 = null;
                                ContVehiculosCombustibleAtendidos++;
                                finServicioCombustibleSurt4.setHoraFin(0.00);

                                if (SurtidorCombustible4.tamañoCola() > 0)//Veo si hay vehiculos esperando en cola
                                {
                                    vehiculoActualSurtComb4 = SurtidorCombustible4.sacarDeCola(); //Saco uno de la cola
                                    vehiculoActualSurtComb4.Estado = _EstadoVehiculo.Esperando_Atencion_Conbustible;
                                    AcumTiempoEsperaVehiculosCombustible += (relojSimulacion - vehiculoActualSurtComb4.getInicioEspera());
                                    ContVehiConTiempoEsperaVehiculosCombustible++;
                                    finServicioCombustibleSurt4.simular(relojSimulacion);

                                }
                                else
                                {
                                    SurtidorCombustible4.Estado = _EstadoSurtidor.Libre;
                                    SurtidorCombustible4.setHoraInicioOcio(relojSimulacion);

                                }

                                //Pregunto si tengo que mostrar en grilla
                                if (tiempoAPartirDeDondeMostrar < relojSimulacion && eventosYaSimuladosYMostrados < cantidadDeEventosAMostrar)
                                {

                                    i = dgvResultados.Rows.Add();
                                    agregarEventoAGrilla(i, true, llegadaVehiculoCombustible, 0);
                                    agregarEventoAGrilla(i, true, llegadaVehiculoGas, 0);
                                    agregarDatosAGrila(i, ((FinServicioCombustible)siguienteEvento).getNombreEvento());
                                    agregarSurtidoresAGrilla(i);
                                    agregarEventoAGrilla(i, true, finServicioCombustibleSurt1, finServicioCombustibleSurt1.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioCombustibleSurt2, finServicioCombustibleSurt2.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioCombustibleSurt3, finServicioCombustibleSurt3.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioCombustibleSurt4, finServicioCombustibleSurt4.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioGasSurt1, finServicioGasSurt1.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioGasSurt2, finServicioGasSurt2.getIdSurtidor());
                                    agregarEventoAGrilla(i, true, finServicioGasSurt3, finServicioGasSurt3.getIdSurtidor());

                                    eventosYaSimuladosYMostrados++;
                                }

                                break;
                        }

                    }



                }
            }
            else
            {
                indicarErrorYPonerFocus();
                resul = true;
            }

            //Calcular y Mostrar Estadisticas
            // txtPorcenAutosAtendidos.Text = ((ContVehiculosCombustibleAtendidos / ContVehiculosCombustibleIngresanAlSitema) * 100).ToString();//proporcio de atendido respecto a los que ingresaron al sistema
            txtPorcenAutosAtendidos.Text = ((ContVehiculosCombustibleAtendidos / ContVehiculosCombustibleIngresanAlSitema) * 100).ToString();
            //txtPorcenAutosCombAtendidos.Text = ((ContVehiculosCombustibleAtendidos/ContVehiculosCombustibleIngresanAlSitema)*100).ToString();//proporcion de atendido respecto a los que ingresaron al sistema combustible
            txtPorcenAutosCombAtendidos.Text = ((ContVehiculosCombustibleAtendidos / (ContVehiculosCombustibleIngresanAlSitema+ContVehiculosCombustibleRechazados)) * 100).ToString();//proporcion de en relacion al total, es decir ingresaron + rechazados
            //txtPorcenAutosGasAtendidos.Text = ((ContVehiculosGasAtendidos/ContVehiculosGasIngresanAlSitema)*100).ToString();//proporcio de atendido respecto a los que ingresaron al sistema gas
            txtPorcenAutosGasAtendidos.Text = ((ContVehiculosGasAtendidos / (ContVehiculosGasIngresanAlSitema+ContVehiculosGasRechazados)) * 100).ToString();
            txtPromTiempoOcioSurt.Text = (((AcumTiempoOcioServidoresCombustible + AcumTiempoOcioServidoresGas) / 7) * 100).ToString();
            txtPromTiempoOcioSurtComb.Text = ((AcumTiempoOcioServidoresCombustible / 4) * 100).ToString();
            txtPromTiempoOcioSurtGas.Text = ((AcumTiempoOcioServidoresGas / 3) * 100).ToString();
            txtTiempoPromEsperaVehi.Text = ((((AcumTiempoEsperaVehiculosCombustible + AcumTiempoEsperaVehiculosGas) / (ContVehiConTiempoEsperaVehiculosCombustible + ContVehiConTiempoEsperaVehiculosGas))) * 100).ToString();
            txtTiempoPromEsperaVehiComb.Text = ((AcumTiempoEsperaVehiculosCombustible / ContVehiConTiempoEsperaVehiculosCombustible) * 100).ToString();
            txtTiempoPromEsperaVehiGas.Text = ((AcumTiempoEsperaVehiculosGas / ContVehiConTiempoEsperaVehiculosGas) * 100).ToString();
            txtTotalCombRechazados.Text = ContVehiculosCombustibleRechazados.ToString();
            txtTotalCombustibleIngresados.Text = ContVehiculosCombustibleIngresanAlSitema.ToString();
            txtTotalGasIngresados.Text = ContVehiculosGasIngresanAlSitema.ToString();
            txtTotalGasRechazados.Text = ContVehiculosGasRechazados.ToString();
            txtTotalIngresados.Text = (ContVehiculosGasIngresanAlSitema + ContVehiculosCombustibleIngresanAlSitema).ToString();
            txtTotalRechazados.Text = (ContVehiculosCombustibleRechazados + ContVehiculosGasRechazados).ToString();

            darFormatoATodos(true);

            //Fin Bloque Simulacion
        }
        private void agregarColumnasAGrilla(int identificadorVehiculo)
        {
            dgvResultados.Columns.Add("colEstadoVehiculo" + identificadorVehiculo, "Estado Vehiculos" + identificadorVehiculo);
            dgvResultados.Columns.Add("colHoraInicioEsperaVehiculo" + identificadorVehiculo, "Hora Inicio Espera Vehiculo" + identificadorVehiculo);
        }

        private void agregarEventoAGrilla(int i, bool conRandom, Evento evento, int idSurt)
        {
            if (evento is LlegadaVehiculosCombustible)
            {
                if (conRandom)
                {
                    dgvResultados.Rows[i].Cells["colRNDLlegadaComb"].Value = ((LlegadaVehiculosCombustible)evento).getRandom();
                    dgvResultados.Rows[i].Cells["colTiempoLlegadaComb"].Value = ((LlegadaVehiculosCombustible)evento).getTiempoEntreLlegada();
                }
                else
                {
                    dgvResultados.Rows[i].Cells["colRNDLlegadaComb"].Value = "-";
                    dgvResultados.Rows[i].Cells["colTiempoLlegadaComb"].Value = "-";

                }
                dgvResultados.Rows[i].Cells["colProxLlegadaComb"].Value = ((LlegadaVehiculosCombustible)evento).getProximaLlegada();

            }
            else if (evento is LlegadaVehiculosGas)
            {
                if (conRandom)
                {
                    dgvResultados.Rows[i].Cells["colRNDLlegadaGas"].Value = ((LlegadaVehiculosGas)evento).getRandom();
                    dgvResultados.Rows[i].Cells["colTiempoLlegadaGas"].Value = ((LlegadaVehiculosGas)evento).getTiempoEntreLlegada();
                }
                else
                {
                    dgvResultados.Rows[i].Cells["colRNDLlegadaGas"].Value = "-";
                    dgvResultados.Rows[i].Cells["colTiempoLlegadaGas"].Value = "-";

                }
                dgvResultados.Rows[i].Cells["colProxLlegadaGas"].Value = ((LlegadaVehiculosGas)evento).getProximaLlegada();

            }
            else if (evento is FinServicioGas)
            {
                //if (conRandom)
                //{
                //    dgvResultados.Rows[i].Cells["rndTiempo"].Value = ((FinServicioGas)evento).getRandom();
                //    dgvResultados.Rows[i].Cells["tiempoEntre"].Value = ((FinServicioGas)evento).getTiempoEntreLlegada();
                //}
                //else
                //{
                //    dgvResultados.Rows[i].Cells["rndTiempo"].Value = "-";
                //    dgvResultados.Rows[i].Cells["tiempoEntre"].Value = "-";

                //}
                switch (idSurt)
                {
                    case 1:
                        dgvResultados.Rows[i].Cells["colFinGasServ1"].Value = ((FinServicioGas)evento).getProximaLlegada();
                        break;
                    case 2:
                        dgvResultados.Rows[i].Cells["colFinGasServ2"].Value = ((FinServicioGas)evento).getProximaLlegada();
                        break;
                    case 3:
                        dgvResultados.Rows[i].Cells["colFinGasServ3"].Value = ((FinServicioGas)evento).getProximaLlegada();
                        break;

                }


            }
            else if (evento is FinServicioCombustible)
            {
                //if (conRandom)
                //{
                //    dgvResultados.Rows[i].Cells["rndTiempo"].Value = ((FinServicioCombustible)evento).getRandom();
                //    dgvResultados.Rows[i].Cells["tiempoEntre"].Value = ((FinServicioCombustible)evento).getTiempoEntreLlegada();
                //}
                //else
                //{
                //    dgvResultados.Rows[i].Cells["rndTiempo"].Value = "-";
                //    dgvResultados.Rows[i].Cells["tiempoEntre"].Value = "-";

                //}
                switch (idSurt)
                {
                    case 1:
                        dgvResultados.Rows[i].Cells["colFinCombustibleServ1"].Value = ((FinServicioCombustible)evento).getProximaLlegada();
                        break;
                    case 2:
                        dgvResultados.Rows[i].Cells["colFinCombustibleServ2"].Value = ((FinServicioCombustible)evento).getProximaLlegada();
                        break;
                    case 3:
                        dgvResultados.Rows[i].Cells["colFinCombustibleServ3"].Value = ((FinServicioCombustible)evento).getProximaLlegada();
                        break;
                    case 4:
                        dgvResultados.Rows[i].Cells["colFinCombustibleServ4"].Value = ((FinServicioCombustible)evento).getProximaLlegada();
                        break;
                }

            }

        }
        private void agregarSurtidoresAGrilla(int i)//Graficar Los Surtidores
        {
            //Surtidores Combustible
            dgvResultados.Rows[i].Cells["colEstadoCombSurt1"].Value = SurtidorCombustible1.Estado.ToString();
            dgvResultados.Rows[i].Cells["colEstadoCombSurt2"].Value = SurtidorCombustible2.Estado.ToString();
            dgvResultados.Rows[i].Cells["colEstadoCombSurt3"].Value = SurtidorCombustible3.Estado.ToString();
            dgvResultados.Rows[i].Cells["colEstadoCombSurt4"].Value = SurtidorCombustible4.Estado.ToString();
            dgvResultados.Rows[i].Cells["colColaCombSurt1"].Value = SurtidorCombustible1.tamañoCola();
            dgvResultados.Rows[i].Cells["colColaCombSurt2"].Value = SurtidorCombustible2.tamañoCola();
            dgvResultados.Rows[i].Cells["colColaCombSurt3"].Value = SurtidorCombustible3.tamañoCola();
            dgvResultados.Rows[i].Cells["colColaCombSurt4"].Value = SurtidorCombustible4.tamañoCola();

            //Surtidores Gas
            dgvResultados.Rows[i].Cells["colEstadoGasSurt1"].Value = SurtidorGas1.Estado.ToString();
            dgvResultados.Rows[i].Cells["colEstadoGasSurt2"].Value = SurtidorGas1.Estado.ToString();
            dgvResultados.Rows[i].Cells["colEstadoGasSurt3"].Value = SurtidorGas1.Estado.ToString();
            dgvResultados.Rows[i].Cells["colColaGasSurt1"].Value = SurtidorGas1.tamañoCola();
            dgvResultados.Rows[i].Cells["colColaGasSurt2"].Value = SurtidorGas2.tamañoCola();
            dgvResultados.Rows[i].Cells["colColaGasSurt3"].Value = SurtidorGas3.tamañoCola();

            //Los tiempos de ocio de Combustible

            dgvResultados.Rows[i].Cells["colInicioOcioCombSurt1"].Value = "-";
            if (SurtidorCombustible1.getHoraInicioOcio() != -1.00)
            {
                dgvResultados.Rows[i].Cells["colInicioOcioCombSurt1"].Value = SurtidorCombustible1.getHoraInicioOcio();
            }
            dgvResultados.Rows[i].Cells["colInicioOcioCombSurt2"].Value = "-";
            if (SurtidorCombustible2.getHoraInicioOcio() != -1.00)
            {
                dgvResultados.Rows[i].Cells["colInicioOcioCombSurt2"].Value = SurtidorCombustible2.getHoraInicioOcio();
            }
            dgvResultados.Rows[i].Cells["colInicioOcioCombSurt3"].Value = "-";
            if (SurtidorCombustible3.getHoraInicioOcio() != -1.00)
            {
                dgvResultados.Rows[i].Cells["colInicioOcioCombSurt3"].Value = SurtidorCombustible3.getHoraInicioOcio();
            }
            dgvResultados.Rows[i].Cells["colInicioOcioCombSurt4"].Value = "-";
            if (SurtidorCombustible4.getHoraInicioOcio() != -1.00)
            {
                dgvResultados.Rows[i].Cells["colInicioOcioCombSurt4"].Value = SurtidorCombustible4.getHoraInicioOcio();
            }

            //Los tiempos de ocio de Gas

            dgvResultados.Rows[i].Cells["colInicioOcioGasSurt1"].Value = "-";
            if (SurtidorGas1.getHoraInicioOcio() != -1.00)
            {
                dgvResultados.Rows[i].Cells["colInicioOcioGasSurt1"].Value = SurtidorGas1.getHoraInicioOcio();
            }
            dgvResultados.Rows[i].Cells["colInicioOcioGasSurt2"].Value = "-";
            if (SurtidorGas2.getHoraInicioOcio() != -1.00)
            {
                dgvResultados.Rows[i].Cells["colInicioOcioGasSurt2"].Value = SurtidorGas2.getHoraInicioOcio();
            }
            dgvResultados.Rows[i].Cells["colInicioOcioGasSurt3"].Value = "-";
            if (SurtidorGas3.getHoraInicioOcio() != -1.00)
            {
                dgvResultados.Rows[i].Cells["colInicioOcioGasSurt3"].Value = SurtidorGas3.getHoraInicioOcio();
            }

        }

        private void agregarDatosAGrila(int i, string evento)
        {
            //Mostrar datos de la simulacion y estadisticas
            dgvResultados.Rows[i].Cells["colEvento"].Value = evento;
            dgvResultados.Rows[i].Cells["colReloj"].Value = relojSimulacion;
            dgvResultados.Rows[i].Cells["colAcumOcioCombSurt"].Value = AcumTiempoOcioServidoresCombustible;
            dgvResultados.Rows[i].Cells["colAcumOcioGasSurt"].Value = AcumTiempoOcioServidoresGas;
            dgvResultados.Rows[i].Cells["colAcumCombIngresados"].Value = ContVehiculosCombustibleIngresanAlSitema;
            dgvResultados.Rows[i].Cells["colAcumGasIngresaron"].Value = ContVehiculosGasIngresanAlSitema;
            dgvResultados.Rows[i].Cells["colAcumCombRechazados"].Value = ContVehiculosCombustibleRechazados;
            dgvResultados.Rows[i].Cells["colAcumGasRechazados"].Value = ContVehiculosGasRechazados;
            dgvResultados.Rows[i].Cells["colAcumCombAtendidos"].Value = ContVehiculosCombustibleAtendidos;
            dgvResultados.Rows[i].Cells["colAcumGasAtendidos"].Value = ContVehiculosGasAtendidos;
            dgvResultados.Rows[i].Cells["colAcumTiempoEsperaComb"].Value = AcumTiempoEsperaVehiculosCombustible;
            dgvResultados.Rows[i].Cells["colAcumTiempoEsperaGas"].Value = AcumTiempoEsperaVehiculosGas;
            dgvResultados.Rows[i].Cells["colAcumOcioCombSurt"].Value = AcumTiempoOcioServidoresCombustible;
            dgvResultados.Rows[i].Cells["colAcumOcioGasSurt"].Value = AcumTiempoOcioServidoresGas;
            dgvResultados.Rows[i].Cells["colContVehiculosEsperaronComb"].Value = ContVehiConTiempoEsperaVehiculosCombustible;
            dgvResultados.Rows[i].Cells["colContVehiculosEsperaronGas"].Value = ContVehiConTiempoEsperaVehiculosGas;

        }
        private void agregarVehiculoAGrilla(Vehiculo vehiculo, int i)
        {

        }


        private void darFormato(TextBox txt, bool poner)//Da formato para resaltar los resultados estadisticos
        {
            if (poner)
            {
                txt.BackColor = Color.FromArgb(255, 199, 206);
                txt.ForeColor = Color.FromArgb(156, 0, 6);
            }
            else
            {
                txt.BackColor = SystemColors.Control;
                txt.ForeColor = SystemColors.WindowText;
            }
        }
        private bool validarDatos()//Punto de partida para validar la completitud y tipo de datos
        {
            foreach (Control con in this.Controls)
            {
                if (con is GroupBox)
                {
                    validar((GroupBox)con);

                }
            }
            return resul;
        }

        private void validar(GroupBox grupo)
        {
            foreach (Control con in grupo.Controls)
            {


                if (con is GroupBox)
                {
                    validar((GroupBox)con);
                }
                if (con is TextBox)
                {
                    if (!Validaciones.estaVacio((TextBox)con) || !Validaciones.validarNumero((TextBox)con))//Valida que los campos no esten vacios y sean de tipo numerico
                    {
                        invalido = (TextBox)con;
                        resul = false;
                    }
                }
            }

        }
        private void indicarErrorYPonerFocus()//Informa de datos invalidos y pone el foco en el ultimo control que fue invalido
        {
            MessageBox.Show("Parametro Vacío o Incorrecto", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            invalido.Focus();
        }

        private void txtLlegadaConbustible_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 110 || e.KeyValue == 190)
            {
                e.SuppressKeyPress = true;
            }
        }

        private void txtColaMax_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 110 || e.KeyValue == 190 || e.KeyValue == 188)
            {
                e.SuppressKeyPress = true;
            }
        }

        private void btnReset_Click(object sender, EventArgs e)//Advertencia de que se perderan los datos con el reset
        {
            //if (MessageBox.Show("¿Está seguro que desea Reiniciar?\n\nSe Perderán los cambios realizados", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
            //{
            reset();
            //}
        }
        private void reset()
        {
            //Limpiar la grilla
            dgvResultados.Rows.Clear();

            //Poner Valores Originales en texbox
            txtColaMax.Text = "2";
            txtFinCombustibleDesde.Text = "3";
            txtFinCombustibleHasta.Text = "7";
            txtFinGasCte.Text = "5";
            txtFinGasDesde.Text = "3";
            txtFinGasHasta.Text = "7";
            txtLlegadaConbustible.Text = "3";
            txtLlegadaGas.Text = "7";
            txtMinDesde.Text = "0";
            txtNEventosMostrar.Text = "10";
            txtPorcenAutosAtendidos.Text = "0.00";
            txtPorcenAutosCombAtendidos.Text = "0.00";
            txtPorcenAutosGasAtendidos.Text = "0.00";
            txtPromTiempoOcioSurt.Text = "0.00";
            txtPromTiempoOcioSurtComb.Text = "0.00";
            txtPromTiempoOcioSurtGas.Text = "0.00";
            txtTiempoPromEsperaVehi.Text = "0.00";
            txtTiempoPromEsperaVehiComb.Text = "0.00";
            txtTiempoPromEsperaVehiGas.Text = "0.00";
            txtTiempoSim.Text = "60";
            txtTotalCombRechazados.Text = "0";
            txtTotalCombustibleIngresados.Text = "0";
            txtTotalGasIngresados.Text = "0";
            txtTotalGasRechazados.Text = "0";
            txtTotalIngresados.Text = "0";
            txtTotalRechazados.Text = "0";


            //Sacar Color A resultados
            darFormatoATodos(false);

            //Reset a variables
            resul = true;
            invalido = null;
            identificadorVehiculo = 0;
            relojSimulacion = 0.00;


            //Remover Columnas de Objetos Temporales
            //int columnasExtras = dgvResultados.Columns.Count - 23;
            //for (int i = 1; i <= (columnasExtras); i++)
            //{
            //    try
            //    {
            //        dgvResultados.Columns.Remove("estadoP" + i);
            //        dgvResultados.Columns.Remove("horaP" + i);
            //    }
            //    catch (Exception)
            //    {


            //    }
            //}

            //Habilitar Boton Simular
            btnSimular.Enabled = true;

        }

        private void darFormatoATodos(bool formato)
        {
            darFormato(txtPorcenAutosAtendidos, formato);
            darFormato(txtPorcenAutosCombAtendidos, formato);
            darFormato(txtPorcenAutosGasAtendidos, formato);
            darFormato(txtPromTiempoOcioSurt, formato);
            darFormato(txtPromTiempoOcioSurtComb, formato);
            darFormato(txtPromTiempoOcioSurtGas, formato);
            darFormato(txtTiempoPromEsperaVehi, formato);
            darFormato(txtTiempoPromEsperaVehiComb, formato);
            darFormato(txtTiempoPromEsperaVehiGas, formato);
            darFormato(txtTotalCombRechazados, formato);
            darFormato(txtTotalCombustibleIngresados, formato);
            darFormato(txtTotalGasIngresados, formato);
            darFormato(txtTotalGasRechazados, formato);
            darFormato(txtTotalIngresados, formato);
            darFormato(txtTotalRechazados, formato);
        }
    }
}
