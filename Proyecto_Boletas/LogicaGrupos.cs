using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Boletas
{
    public static class LogicaGrupos
    {
        /// <summary>
        /// Determina el nombre completo de la materia de Ciencias (Conocimiento del Medio o Ciencias Naturales).
        /// </summary>
        public static string ObtenerNombreMateriaCiencias(string nombreGrupo)
        {
            string nombreNormalizado = nombreGrupo.ToLower().Trim();

            if (nombreNormalizado.Contains("Primero") || nombreNormalizado.Contains("Segundo"))
            {
                return "Conocimiento del Medio";
            }
            else if (nombreNormalizado.Contains("Tercero") ||
                      nombreNormalizado.Contains("cuarto") ||
                      nombreNormalizado.Contains("quinto") ||
                      nombreNormalizado.Contains("sexto"))
            {
                return "Ciencias Naturales";
            }
            return "Ciencias Naturales"; // Valor por defecto
        }
    }
}
