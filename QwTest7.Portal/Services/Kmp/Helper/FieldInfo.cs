using QwTest7.Portal.Services.Kmp.Enums;

namespace QwTest7.Portal.Services.Kmp.Helper
{
    public class FieldInfo
    {
        private string _formatstring;
        public FormatOptions Options;
        public string AswName;
        public string Formatstring
        {
            get => _formatstring;
            set
            {
                // bestimmt _formatstring, Options und AswName:
                Options = FormatOptions.alNone;
                AswName = "";
                _formatstring = "";
                if (!string.IsNullOrEmpty(value))
                {
                    var fl = value.Split(",");
                    for (int i = 0; i < fl.Length; i++)
                    {
                        switch (fl[i])
                        {
                            case "r":
                                Options |= FormatOptions.alRight;
                                break;
                            case "l":
                                Options |= FormatOptions.alLeft;
                                break;
                            case "c":
                                Options |= FormatOptions.alCenter;
                                break;
                            case "IGN":
                                Options |= FormatOptions.alIgnoreName;
                                break;
                            case "A":
                                Options |= FormatOptions.alAutoGenerate;
                                break;
                            case "N":
                                Options |= FormatOptions.alRequired;
                                break;
                            case "R":
                                Options |= FormatOptions.alReadOnly;
                                break;
                            case "C":
                                Options |= FormatOptions.alInternalCalc;
                                break;
                            case "INT":
                                Options |= FormatOptions.alInt;
                                break;
                            case "TL0":
                                Options |= FormatOptions.alTrimLeft0;
                                break;
                            case "ASW":
                                Options |= FormatOptions.alAsw;
                                AswName = fl[++i];
                                break;
                            default:
                                _formatstring = string.Join(",", fl[i..]);
                                i = fl.Length;  //Ende
                                break;
                        }
                    }
                    if (_formatstring != null)
                        if (!_formatstring.StartsWith("{"))  //0.00 -> {0:0.00} wg string.Format(FormatString, value,
                            _formatstring = "{0:" + _formatstring + "}";
                }

            }
        }

        public FieldType fieldType { get; set; }
        private Type _propertyType;
        public Type PropertyType
        {
            get => _propertyType;
            set
            {
                _propertyType = value;

                if (value == typeof(int) || value == typeof(short) || value == typeof(byte) ||
                    value == typeof(sbyte) || value == typeof(uint) || value == typeof(long) ||
                    value == typeof(bool) || value == typeof(int) || value == typeof(long) ||
                    value == typeof(int?) || value == typeof(short?) || value == typeof(byte?) ||
                    value == typeof(sbyte?) || value == typeof(uint?) || value == typeof(long?) ||
                    value == typeof(bool?) || value == typeof(int?) || value == typeof(long?)
                    )
                    fieldType = FieldType.ftInt;

                else if (value == typeof(double) || value == typeof(float) || value == typeof(decimal) ||
                         value == typeof(double?) || value == typeof(float?) || value == typeof(decimal?)
                    )
                    fieldType = FieldType.ftFloat;

                else if (value == typeof(string)   //kein nullable string??? || value == typeof(string?)
                    )
                    fieldType = FieldType.ftString;

                else if (value == typeof(DateTime) || value == typeof(DateTime?))
                    fieldType = FieldType.ftDateTime;

                else
                    throw new NotImplementedException($"propertyType={value}");

            }
        }

        public FieldInfo(Type propertyType, string formatstring)
        {
            PropertyType = propertyType;
            Formatstring = formatstring;
        }
    }




}