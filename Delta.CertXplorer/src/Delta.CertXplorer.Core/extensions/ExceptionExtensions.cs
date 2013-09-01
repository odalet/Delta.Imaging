using System;
using System.Text;

namespace Delta.CertXplorer
{
    /// <summary>
    /// Provides the <see cref="System.Exception"/> class with a pair of extension methods
    /// allowing to retrieve (and format) detailed information about the exception.
    /// </summary>
    public static class ExceptionExtensions
    {
         //<summary>
         //Crée et retourne une chaîne représentant l'exception passée en parametre.
         //</summary>
         //<example>
         //La chaine suivant est la représentation d'une exception obtenue lors de l'exécution de
         //Sides.
         //<code>
         // + [System.Exception] Erreur dans l'exécution de la requête : SELECT nom_utilisateur AS NOM, pwd_utilisateur AS PWD, nom_profil_utilisateur AS PROFIL, statut_utilisateur AS STATUT, prenom_utilisateur AS PRENOM, login_utilisateur AS LOGIN FROM utilisateur 
         //       | at Delta.CertXplorer.Data.ADOHelper.ExecuteQueryDataSet(String command, TxHelper tx, Boolean fillSchema) in C:\CRIHOME\Delta.CertXplorer\Delta.CertXplorer\data\adohelper.cs:line 958
         //       | at Delta.CertXplorer.Data.ADOHelper.ExecuteQueryDataSet(String command, TxHelper tx) in C:\CRIHOME\Delta.CertXplorer\Delta.CertXplorer\data\adohelper.cs:line 908
         //       | at Delta.CertXplorer.Mapping.Dal.DAObjectR.List(DAFilter filter, DAOrder order, TxHelper tx) in C:\CRIHOME\Delta.CertXplorer\Delta.CertXplorer\Mapping\Dal\DAObjectR.cs:line 249
         //       + [System.Exception] Impossible de contacter la base de données
         //       | at Delta.CertXplorer.Data.ADOHelper.GetConnection() in C:\CRIHOME\Delta.CertXplorer\Delta.CertXplorer\data\adohelper.cs:line 194
         //       | at Delta.CertXplorer.Data.ADOHelper.CreateCommand(TxHelper tx) in C:\CRIHOME\Delta.CertXplorer\Delta.CertXplorer\data\adohelper.cs:line 231
         //       | at Delta.CertXplorer.Data.ADOHelper.ExecuteQueryDataSet(String command, TxHelper tx, Boolean fillSchema) in C:\CRIHOME\Delta.CertXplorer\Delta.CertXplorer\data\adohelper.cs:line 942
         //       + [Delta.CertXplorer.Data.ADOHelperConfigException] Impossible de créer ou d'ouvrir l'objet connection (Assembly : Npgsql ; DbConnectionClass : Npgsql.NpgsqlConnection)
         //          | at Delta.CertXplorer.Data.ADOHelper.CreateConnection() in C:\CRIHOME\Delta.CertXplorer\Delta.CertXplorer\data\adohelper.cs:line 141
         //          | at Delta.CertXplorer.Data.ADOHelper.GetConnection() in C:\CRIHOME\Delta.CertXplorer\Delta.CertXplorer\data\adohelper.cs:line 154
         //          + [System.NullReferenceException] Object reference not set to an instance of an object.
         //             | at Delta.CertXplorer.Data.ADOHelper.CreateConnection() in C:\CRIHOME\Delta.CertXplorer\Delta.CertXplorer\data\adohelper.cs:line 131
         //</code>
         //<p>L'exception obtenue est de type <see cref="SystemException"/>. Elle a été interceptée
         //par la méthode Delta.CertXplorer.Mapping.Dal.DAObjectR.List(DAFilter filter, DAOrder order, TxHelper tx)
         //à la ligne 908. Elle avait initialement été lancée par la méthode
         //<c>Delta.CertXplorer.Data.ADOHelper.ExecuteQueryDataSet(String command, TxHelper tx, Boolean fillSchema)</c>
         //à la ligne 958 et avait traversé la méthode
         //<c>Delta.CertXplorer.Data.ADOHelper.ExecuteQueryDataSet(String command, TxHelper tx)</c>.</p>
         //<p>Cette exception été provoquée (<see cref="Exception.InnerException"/> par une exception
         //de type <see cref="SystemException"/> interceptée par la méthode
         //<c>Delta.CertXplorer.Data.ADOHelper.ExecuteQueryDataSet(String command, TxHelper tx, Boolean fillSchema)</c>
         //et initialement lancée par la méthode <c>Delta.CertXplorer.Data.ADOHelper.GetConnection()</c>.</p>
         //<p>Et ainsi de suite...</p>
         //</example>
         //<param name="exception">Exception dont on veut une représentation</param>
         //<returns>Chaine représentant l'exception</returns>


        /// <summary>
        /// Returns Exception information into a formatted string.
        /// </summary>
        /// <param name="exception">The exception to describe.</param>
        /// <returns>Formated (and indented) string giving information about <paramref name="exception"/></returns>
        public static string ToFormattedString(this Exception exception)
        {
            if (exception == null) return string.Empty;

            const string tab = "   ";
            const string leafEx = " + ";
            const string leafTr = " | ";
            string indent = string.Empty;

            var builder = new StringBuilder();
            for (var currentException = exception; currentException != null; currentException = currentException.InnerException)
            {
                builder.Append(indent);
                builder.Append(leafEx);
                builder.Append("[");
                builder.Append(currentException.GetType().ToString());
                builder.Append("] ");
                builder.Append(currentException.Message);
                builder.Append(Environment.NewLine);

                indent += tab;

                if (currentException.StackTrace == null)
                    continue;

                var stackTrace = currentException.StackTrace
                    .Replace(Environment.NewLine, "\n").Split('\n');


                for (int i = 0; i < stackTrace.Length; i++)
                {
                    var current = stackTrace[i];
                    if (string.IsNullOrEmpty(current)) continue;

                    builder.Append(indent);
                    builder.Append(leafTr);
                    builder.Append(current.Trim());
                    builder.Append(Environment.NewLine);
                }
            }

            return builder.ToString();
		}

        public static string GetFullDiagnosticsInformation(this object exceptionObject)
        {
            if (exceptionObject == null)
                exceptionObject = "NO EXCEPTION DATA";
            try
            {
                if (exceptionObject is Exception)
                {
                    var exception = (Exception)exceptionObject;
                    return GetFullDiagnosticsText(exception.ToFormattedString());
                }
                else
                {
                    var exceptionMessage = exceptionObject == null ? "?" : exceptionObject.ToString();
                    return GetFullDiagnosticsText(exceptionMessage);
                }
            }
            catch (Exception ex)
            {
                var debugException = ex;
                return string.Format("Could not retrieve diagnostics data: {0}\r\n{1}", ex.Message, ex.ToFormattedString());
            }
        }

        private static string GetFullDiagnosticsText(string initialString)
        {
            var builder = new StringBuilder();
            builder.AppendLine(initialString);
            builder.AppendLine();
            builder.AppendLine(new string('-', 80));
            builder.AppendLine("Current AppDomain's loaded assemblies:");

            try
            {
                AppDomain.CurrentDomain.AppendModuleDescriptions(builder);
            }
            catch (Exception ex)
            {
                builder.AppendLine(string.Format("Error: Unable to retrieve the loaded assemblies list:{0}\r\n{1}",
                    ex.Message, ex.ToFormattedString()));
            }

            return builder.ToString();
        }
    }
}
