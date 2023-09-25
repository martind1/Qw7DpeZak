using System.Collections;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Reflection.Metadata;

namespace QwTest7.Portal.Services.Kmp.Helper
{
    internal static class DbUtils
    {
        #region Feldnamen korrigieren (Groß/Klein)

        // Feldnamen korrigieren (Groß/Klein) anhand Entity Property Names und Relflection

        public static string AdjustFieldname(string Fieldname, IList<string> FieldList)
        {
            foreach (var field in FieldList)
            {
                string fieldname = field.ToString();
                if (fieldname.ToUpper() == Fieldname.ToUpper())
                    return fieldname;
            }
            return Fieldname;
        }

        //Feldnamen korrigieren: IDicionary Keys korrigieren
        //zB für FormatList, FLD1=Asw,Status -> fld1=Asw,Status
        public static void AdjustListKeys(IDictionary Fieldnames, IList<string> FieldList)
        {
            foreach (string oldname in Fieldnames.Keys)
            {
                string newname = AdjustFieldname(oldname, FieldList);
                if (newname != oldname)
                {
                    Fieldnames[newname] = Fieldnames[oldname];
                    Fieldnames.Remove(oldname);
                }
            }
        }

        //Fieldliste einer Entity. Mit FieldInfos. Mit Formatstrings von Formatlist (wenn vorhanden)
        public static IDictionary<string, FieldInfo> GetFieldlist(Type TEntity)
        {
            PropertyInfo[] propertyInfos;
            propertyInfos = TEntity.GetProperties();  // BindingFlags.Public);

            // Formatlist laden:
            // In Entity:
            // [NotMapped]
            // public IDictionary<string, string> Formatlist { get; private set; } = new Dictionary<string, string>() ...
            string constFormatlist = "Formatlist";
            bool hasFormatlist = propertyInfos.Where(x => x.Name == constFormatlist).FirstOrDefault() != null;
            var formatlist = new Dictionary<string, string>();
            if (hasFormatlist)
            {
                var entity = Activator.CreateInstance(TEntity, true);
                var s1 = entity.GetType().GetProperty(constFormatlist).GetValue(entity, null);
                var helplist = (Dictionary<string, string>)s1;
                //Anlegen case insensitiv:
                foreach (var item in helplist)
                    formatlist.Add(item.Key.ToUpper(), item.Value);
            }

            // write fieldlist
            // nur Felder mit Getter und Setter (auch [NotMapped])
            var fieldlist = new Dictionary<string, FieldInfo>();
            foreach (PropertyInfo propertyInfo in propertyInfos.
                Where(p => p.GetGetMethod() != null && p.GetSetMethod() != null))
            {
                string formatstring = "";
                //Vergleichen case insensitiv:
                if (hasFormatlist && formatlist != null)
                    formatlist.TryGetValue(propertyInfo.Name.ToUpper(), out formatstring);

                fieldlist.Add(propertyInfo.Name, new FieldInfo(propertyInfo.PropertyType, formatstring));
            }

            // Fieldinfo.Formatstring befüllen:

            return fieldlist;
        }

        public static bool HasFilterChar(string kmpstr)
        {
            return kmpstr.Contains('%') || kmpstr.Contains('_');
        }

        #endregion

    }




}