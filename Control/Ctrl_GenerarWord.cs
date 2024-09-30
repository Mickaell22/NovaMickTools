using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Wordprocessing;
using DocumentFormat.OpenXml.Packaging;
using System.Windows.Forms;
using NPOI.XWPF.UserModel;
using Path = System.IO.Path;

namespace Control
{
    public class Ctrl_GenerarWord
    {
        //Funcion para generar un word en solitario
        public void GenerarWord(string asignatura, string tema, string fecha, string paralelo, string docente, string baseFolderPath)
        {
            if (string.IsNullOrEmpty(baseFolderPath))
            {
                MessageBox.Show("Seleccione una carpeta base primero.","Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Ruta de la carpeta de la asignatura (nombre completo)
            string carpetaAsignatura = Path.Combine(baseFolderPath, asignatura);

            // Generar la abreviatura
            string abreviatura = GenerarAbreviatura(asignatura);

            // Buscar el próximo número disponible
            int count = 1;
            string archivoDocx;
            do
            {
                archivoDocx = Path.Combine(carpetaAsignatura, $"{abreviatura} ({count}).docx");
                count++;
            } while (File.Exists(archivoDocx));

            // Crear el documento Word basado en la plantilla
            try
            {
                string plantillaPath = "C:\\Users\\ASUS\\Documents\\BASE.docx";
                CrearArchivoDocx(plantillaPath, archivoDocx, asignatura, tema, fecha, paralelo, docente);
                MessageBox.Show("Archivo generado exitosamente.","Creado exitosamente", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el archivo: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CrearArchivoDocx(string plantillaPath, string archivoDocx, string asignatura, string tema, string fecha, string paralelo, string docente)
        {
            try
            {
                using (FileStream templateStream = new FileStream(plantillaPath, FileMode.Open, FileAccess.Read))
                {
                    XWPFDocument docx = new XWPFDocument(templateStream);

                    // Buscar y agregar textos en el documento
                    foreach (XWPFParagraph paragraph in docx.Paragraphs)
                    {
                        AddTextAfterPlaceholder(paragraph, "ASIGNATURA", asignatura);
                        AddTextAfterPlaceholder(paragraph, "FECHA", fecha);
                        AddTextAfterPlaceholder(paragraph, "TEMA", tema);
                        AddTextAfterPlaceholder(paragraph, "PARALELOS", paralelo);
                        AddTextAfterPlaceholder(paragraph, "DOCENTE", docente);
                        AddTextAfterPlaceholder(paragraph, "Tema", tema);
                    }

                    // Guardar el archivo en la ruta especificada
                    using (FileStream fs = new FileStream(archivoDocx, FileMode.Create, FileAccess.Write))
                    {
                        docx.Write(fs);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al crear el archivo: " + ex.Message);
            }
        }

        private void AddTextAfterPlaceholder(XWPFParagraph paragraph, string placeholder, string newValue)
        {
            string dosPuntos = ":";
            for (int i = 0; i < paragraph.Runs.Count; i++)
            {
                XWPFRun run = paragraph.Runs[i];
                string runText = run.Text;

                if (runText.Contains(placeholder))
                {
                    // Dividir el run en dos partes: antes y después del placeholder
                    int placeholderIndex = runText.IndexOf(placeholder) + placeholder.Length;
                    string beforePlaceholder = runText.Substring(0, placeholderIndex);
                    string afterPlaceholder = runText.Substring(placeholderIndex);

                    // Reemplazar el texto en el run actual
                    run.SetText(beforePlaceholder, 0);

                    // Crear un nuevo run para el nuevo valor con el mismo formato
                    XWPFRun newRun = paragraph.InsertNewRun(i + 1);
                    newRun.SetText(": " + newValue);

                    // Copiar el formato del run original
                    /*newRun.IsBold = run.IsBold*/;
                    newRun.IsItalic = run.IsItalic;
                    newRun.Underline = run.Underline;
                    newRun.FontSize = run.FontSize;
                    newRun.FontFamily = run.FontFamily;
                    newRun.SetColor(run.GetColor());

                    // Crear un tercer run para el texto después del placeholder
                    XWPFRun afterRun = paragraph.InsertNewRun(i + 2);
                    afterRun.SetText(afterPlaceholder);

                    // Copiar el formato del run original al tercer run
                    //afterRun.IsBold = run.IsBold;
                    afterRun.IsItalic = run.IsItalic;
                    afterRun.Underline = run.Underline;
                    afterRun.FontSize = run.FontSize;
                    afterRun.FontFamily = run.FontFamily;
                    afterRun.SetColor(run.GetColor());

                    break; // Salir del bucle una vez que se ha encontrado y procesado el placeholder
                }
            }
        }

        //Funcion para generar un word en conjunto
        public void GenerarWordGrupal(string asignatura, string tema, string fecha, string paralelo, string docente, string baseFolderPath, List<string> participantes)
        {
            if (string.IsNullOrEmpty(baseFolderPath))
            {
                MessageBox.Show("Seleccione una carpeta base primero.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Ruta de la carpeta de la asignatura (nombre completo)
            string carpetaAsignatura = Path.Combine(baseFolderPath, asignatura);

            // Generar la abreviatura
            string abreviatura = GenerarAbreviatura(asignatura);

            // Buscar el próximo número disponible
            int count = 1;
            string archivoDocx;
            do
            {
                archivoDocx = Path.Combine(carpetaAsignatura, $"{abreviatura} ({count}).docx");
                count++;
            } while (File.Exists(archivoDocx));

            // Crear el documento Word basado en la plantilla
            try
            {
                string plantillaPath = "C:\\Users\\ASUS\\Documents\\Base_Grupal.docx";
                CrearArchivoDocxGrupal(plantillaPath, archivoDocx, asignatura, tema, fecha, paralelo, docente, participantes);
                MessageBox.Show("Archivo generado exitosamente.", "Creado exitosamente", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar el archivo: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CrearArchivoDocxGrupal(string plantillaPath, string archivoDocx, string asignatura, string tema, string fecha, string paralelo, string docente, List<string> participantes)
        {
            try
            {
                using (FileStream templateStream = new FileStream(plantillaPath, FileMode.Open, FileAccess.Read))
                {
                    XWPFDocument docx = new XWPFDocument(templateStream);

                    // Buscar y agregar textos en el documento
                    foreach (XWPFParagraph paragraph in docx.Paragraphs)
                    {
                        // Reemplazar los placeholders con el formato correcto
                        ReplaceText(paragraph, "grado", paralelo);
                        ReplaceText(paragraph, "Temita", tema);
                        ReplaceText(paragraph, "mate", asignatura);
                        ReplaceText(paragraph, "profe", docente);

                        // Agregar participantes debajo del placeholder "AUTORES"
                        if (paragraph.ParagraphText.Contains("AUTORES"))
                        {
                            foreach (var participante in participantes)
                            {
                                XWPFRun run = paragraph.CreateRun();
                                run.AddCarriageReturn(); // Añadir nueva línea antes de cada participante
                                run.SetText(participante);
                            }
                        }
                    }

                    // Guardar el archivo en la ruta especificada
                    using (FileStream fs = new FileStream(archivoDocx, FileMode.Create, FileAccess.Write))
                    {
                        docx.Write(fs);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al crear el archivo: " + ex.Message);
            }
        }

        private void ReplaceText(XWPFParagraph paragraph, string placeholder, string newValue)
        {
            for (int i = 0; i < paragraph.Runs.Count; i++)
            {
                XWPFRun run = paragraph.Runs[i];
                string runText = run.Text;

                if (runText.Contains(placeholder))
                {
                    // Reemplazar el texto en el run actual
                    run.SetText(runText.Replace(placeholder, newValue), 0);
                }
            }
        }





        // Función para generar la abreviatura
        private string GenerarAbreviatura(string asignatura)
        {
            return string.Join("", asignatura.Split(' ')
                .Where(word => word.Length > 2)
                .Select(word => word[0])
                .Select(char.ToUpper));
        }
    


}
}
