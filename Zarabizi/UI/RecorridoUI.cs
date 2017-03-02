using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Zarabizi.Models.Metadata;

namespace Zarabizi.UI
{
    public class RecorridoUI
    {
        /// Retorna una tabla html con los recorridos
        /// Recibe una lista            
        internal static Table ShowRecorridos(IList<Recorrido> list)
        {
            Table table = new Table();
            //Minetras puede leer filas

            foreach (Recorrido recorrido in list)
            {
                //Creo la fila
                TableRow row = new TableRow();

                //Por cada columna de recorrido creo una celda y le asigno texto
                TableCell idRecorridoCell = new TableCell();
                idRecorridoCell.Text = recorrido.IdRecorrido.ToString();
                
                //Por cada columna de recorrido creo una celda y le asigno texto
                TableCell distanciaRecorridoCell = new TableCell();
                distanciaRecorridoCell.Text = recorrido.DistanciaRecorrido.ToString();

                //Por cada columna de recorrido creo una celda y le asigno texto
                TableCell fechaSalidaRecorridoCell = new TableCell();
                fechaSalidaRecorridoCell.Text = recorrido.FechaSalidaRecorrido.ToString();

                //Por cada columna de recorrido creo una celda y le asigno texto
                TableCell fechaLlegadaRecorridoCell = new TableCell();
                fechaLlegadaRecorridoCell.Text = recorrido.FechaLlegadaRecorrido.ToString();

                //Por cada columna de recorrido creo una celda y le asigno texto
                TableCell idEstacionInicioCell = new TableCell();
                idEstacionInicioCell.Text = recorrido.IdEstacionInicio.ToString();

                //Por cada columna de recorrido creo una celda y le asigno texto
                TableCell idEstacionFinalCell = new TableCell();
                idEstacionFinalCell.Text = recorrido.IdEstacionFinal.ToString();

                //Por cada columna de recorrido creo una celda y le asigno texto
                TableCell idBicicletaCell = new TableCell();
                idBicicletaCell.Text = recorrido.IdBicicleta.ToString();

                //Por cada columna de recorrido creo una celda y le asigno texto
                TableCell idSocioCell = new TableCell();
                idSocioCell.Text = recorrido.IdSocio.ToString();

                //Le añado la celda a la coleccion de celdas de la fila
                row.Cells.Add(idRecorridoCell);
                row.Cells.Add(distanciaRecorridoCell);
                row.Cells.Add(fechaSalidaRecorridoCell);
                row.Cells.Add(fechaLlegadaRecorridoCell);
                row.Cells.Add(idEstacionInicioCell);
                row.Cells.Add(idEstacionFinalCell);
                row.Cells.Add(idBicicletaCell);
                row.Cells.Add(idSocioCell);

                //Le añado la fila a la tabla
                table.Rows.Add(row);
            }
            return (table);
        }
    }
}