using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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
        private double timpoAPartirDeDondeMostrar;
        private int cantidadDeEventosAMostrar;


        //Variables necesarias
        private double relojSimulacion;
        private int identificadorVehiculo = 0;
        bool resul = true;
        TextBox invalido = null;




        public frm_principal()
        {
            InitializeComponent();
        }

        private void btnSimular_Click(object sender, EventArgs e)
        {
            if (validarDatos())
            {




            }
            else
            {
                indicarErrorYPonerFocus();
                resul = true;
            }

        }
        private void darFormato(TextBox txt, bool poner)
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
        private bool validarDatos()
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
                    if (!Validaciones.estaVacio((TextBox)con) || !Validaciones.validarNumero((TextBox)con))
                    {
                        invalido = (TextBox)con;
                        resul = false;
                    }
                }
            }

        }
        private void indicarErrorYPonerFocus()
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

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro que desea Reiniciar?\n\nSe Perderán los cambios realizados", "Atención", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1) == System.Windows.Forms.DialogResult.Yes)
            {
                reset();
            }
        }
        private void reset()
        {
            //dgv_Simulacion.Rows.Clear();
            //txtLlegadaPersonas.Text = "0,5";
            //txtDemoraGratuitos.Text = "1";
            //txtDemoraPagos.Text = "1";
            //txtProbGratuitos.Text = "0,7";
            //txtProbPagos.Text = "0,3";
            //txtColaMax.Text = "4";
            //txtDuracionAyuda.Text = "2";
            //txtReduccionTiempo.Text = "0,5";
            //txtTiempoOciosoPagos.Text = "0,00";
            //txtTiempoPromEnCola.Text = "0,00";
            //txtCantAyudasAux.Text = "0,00";
            //txtCantExperimentos.Text = "4";
            //txtNUltimos.Text = "20";
            //txtAPartir.Text = "0";
            //darFormato(txtTiempoOciosoPagos, false);
            //darFormato(txtCantAyudasAux, false);
            //darFormato(txtTiempoPromEnCola, false);
            //resul = true;
            //invalido = null;
            //identificadorPersona = 0;
            //btnIntegracion.Enabled = false;
            //bandera = true;
            //banderaBloqueo = true;

            //int columnasExtras = dgv_Simulacion.Columns.Count - 23;
            //for (int i = 1; i <= (columnasExtras); i++)
            //{
            //    try
            //    {
            //        dgv_Simulacion.Columns.Remove("estadoP" + i);
            //        dgv_Simulacion.Columns.Remove("horaP" + i);
            //    }
            //    catch (Exception)
            //    {


            //    }
            //}
            //btn_simular.Enabled = true;

        }

    }
}
