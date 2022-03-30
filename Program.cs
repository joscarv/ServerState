using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerStateEmail
{
    internal class Program
    {
        static void Main(string[] args)
        {            
            string open = "";
            string bodyMail = "";
            string openStoreMessage = "";
            int connectionError = 0;
            int openStoreCount = 0;
            SQLServer sql = new SQLServer();
            List<Tienda> tiendas = sql.getTiendas();

            if (tiendas == null) { Log.writeLog("No se pudo mandar "); return; }

            open += "<p><strong>";
            foreach (Tienda tienda in tiendas)
            {
                string status = sql.getServerState(tienda.Id, tienda.Ip);
                if(status.Equals("Error"))
                {
                    openStoreMessage += $"Sin conexion con la unidad {tienda.Id+1000} {tienda.Name} <br>";
                    connectionError++;
                } else if (!status.Equals(""))
                {
                    open += status + "<br>";
                    openStoreCount++;
                }
            }
            open += "</p></strong>";

            if(openStoreCount > 0)
            {
                bodyMail += $"<p>Unidades con estatus abierto: <strong>{openStoreCount} Unidaes</strong></p>";
                bodyMail += "<IMG id='camilo' border='0' alt='Gordas' src='cid:camilo' width='180' height='250'><br>";
                bodyMail += "<h3>GORDAS!! Revisen las siguientes tiendas que probablemente generaron informacion en CEROS</h3>";
                bodyMail += "<p><ol>";
                bodyMail += "<li>Revisar que no se hayan quedado terminales abiertas</li>";
                bodyMail += "<li>Si el estatus del Xpress Server esta abierto CORRER CIERRE DESDE BACKOFFICE</li>";
                bodyMail += "<li>Enviar nuevamente informacion notificando a Genesix y Stratus por correo para que reprocesen los nuevos archivos</li>";
                bodyMail += "</ol></p>";
                bodyMail += "<p><strong>" + open + "</p></strong>";
            }
            if(connectionError > 0)
            {
                bodyMail += $"<p>Unidades con error de conexion: <strong>{connectionError} Unidaes</strong><br>";                
                bodyMail += "<p><strong>" + openStoreMessage + "</p></strong>";
            }

            if(openStoreCount > 0)
            {
                Email email = new Email();
                email.sendEmail(bodyMail);
            } else
            {
                Log.writeLog("No es necesario enviar email.");
            }
            
        }
    }
}
